using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using static Gizmo.DAL.Extensions.DclExtensions.PostgreSql;

namespace Gizmo.DAL.Extensions.DdlExtensions;

internal static class PostgreSql
{
    public static async Task EnsureLoginExists(DatabaseFacade facade, string login, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login name cannot be null or empty.", nameof(login));

            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("postgres").ToConnectionString();

            await using var connection = new NpgsqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM pg_roles WHERE rolname = @loginName";
            command.Parameters.AddWithValue("@loginName", login);

            var exists = await command.ExecuteScalarAsync(ct) != null;

            if (!exists)
            {
                command.Parameters.Clear();
                command.CommandText = $"CREATE ROLE \"{login}\" LOGIN SUPERUSER";
                await command.ExecuteNonQueryAsync(ct);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create PostgreSQL login '{login}'.", ex);
        }
    }

    public static async Task<IEnumerable<string>> GetNonSystemDbNames(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("postgres").ToConnectionString();

            await using var connection = new NpgsqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText =
                """
                    SELECT datname 
                    FROM pg_database 
                    WHERE datistemplate = false 
                    AND datname NOT IN ('postgres')
                    ORDER BY datname
                """;

            var dbNames = new List<string>();

            await using (var reader = await command.ExecuteReaderAsync(ct))
            {
                while (await reader.ReadAsync(ct))
                {
                    dbNames.Add(reader.GetString(0));
                }
            }

            return dbNames;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve PostgreSQL database names.", ex);
        }
    }

    public static async Task<bool> Exists(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("postgres").ToConnectionString();

            await using var connection = new NpgsqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM pg_database WHERE datname = @databaseName";
            command.Parameters.AddWithValue("@databaseName", metadata.DatabaseName);

            var found = false;

            await using (var reader = await command.ExecuteReaderAsync(ct))
            {
                if (await reader.ReadAsync(ct))
                {
                    found = Convert.ToInt32(reader[0]) > 0;
                }
            }

            return found;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to check if PostgreSQL database exists.", ex);
        }
    }

    public static async Task Backup(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(backupFile))
                throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

            var metadata = ConnectionMetadata.FromConnectionString(facade.GetConnectionString());

            string commandFile = "pg_dump";

            var processInfo = new ProcessStartInfo
            {
                FileName = commandFile,
                Arguments = $"--dbname=\"{metadata.DatabaseName}\" --file=\"{backupFile}\" --no-owner",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            if (!string.IsNullOrEmpty(metadata.Password))
                processInfo.Environment["PGPASSWORD"] = metadata.Password;

            if (!string.IsNullOrEmpty(metadata.Username))
                processInfo.Environment["PGUSER"] = metadata.Username;

            if (!string.IsNullOrEmpty(metadata.Host))
                processInfo.Environment["PGHOST"] = metadata.Host;

            if (metadata.Port > 0)
                processInfo.Environment["PGPORT"] = metadata.Port.ToString();

            using var process = Process.Start(processInfo);

            if (process is null)
                throw new InvalidOperationException($"Failed to start pg_dump process. Command: {commandFile} {processInfo.Arguments}");

            _ = await process.StandardOutput.ReadToEndAsync(ct);
            var error = await process.StandardError.ReadToEndAsync(ct);

            await process.WaitForExitAsync(ct);

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"pg_dump failed: {error}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to backup PostgreSQL database to '{backupFile}'", ex);
        }
    }

    public static async Task Restore(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(backupFile))
                throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

            var metadata = facade.GetConnectionMetadata();
            var postgresConnectionString = metadata.ChangeDatabaseTo("postgres").ToConnectionString();

            await using var connection = new NpgsqlConnection(postgresConnectionString);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText =
                """
                    SELECT pg_terminate_backend(pg_stat_activity.pid)
                    FROM pg_stat_activity
                    WHERE pg_stat_activity.datname = @databaseName
                    AND pid <> pg_backend_pid()
                """;

            command.Parameters.AddWithValue("@databaseName", metadata.DatabaseName);
            await command.ExecuteNonQueryAsync(ct);

            command.Parameters.Clear();
            command.CommandText =
                $"""
                    DROP DATABASE IF EXISTS "{metadata.DatabaseName}";
                    CREATE DATABASE "{metadata.DatabaseName}" OWNER "{metadata.Username}";
                    CREATE ROLE postgres LOGIN SUPERUSER;
                """;

            await command.ExecuteNonQueryAsync(ct);
            await connection.CloseAsync();

            var docker = Environment.GetEnvironmentVariable("POSTGRES_DOCKER");
            var isDocker = !string.IsNullOrEmpty(docker);

            var fileExtension = Path.GetExtension(backupFile);

            var cmd = fileExtension switch
            {
                ".dump" => "psql",
                _ => throw new NotSupportedException($"Unsupported backup file format: {fileExtension}")
            };

            string fileName;
            var arguments = new List<string>
            {
                $"-d {metadata.DatabaseName}",
                $"-U {metadata.Username}", // Default user for PostgreSQL
                $"-f \"{backupFile}\"",
                $"-v ON_ERROR_STOP=1", //abort on the first error. Otherwise psql keeps going and you end up half-restored
                "--single-transaction", //wrap the whole run in one transaction. If anything fails, nothing persists. Works as long as the script doesn’t try to use its own explicit BEGIN/COMMIT
                "-X", //don’t read ~/.psqlrc. Keeps the session clean and reproducible
            };


            if (isDocker)
            {
                fileName = "docker";
                arguments.Insert(0, $"exec {docker} {cmd}");
            }
            else
            {
                fileName = cmd;
            }

            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = string.Join(" ", arguments),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            var stdoutTask = process.StandardOutput.ReadToEndAsync(ct);
            var stderrTask = process.StandardError.ReadToEndAsync(ct);

            await process.WaitForExitAsync(ct);

            var stdout = await stdoutTask;
            var stderr = await stderrTask;

            if (process.ExitCode != 0 || !string.IsNullOrEmpty(stderr))
            {
                throw new InvalidOperationException($"{cmd} failed with error code {process.ExitCode}: {stderr}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to restore PostgreSQL database from '{backupFile}'", ex);
        }
    }

    public static async Task Drop(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            var metadata = facade.GetConnectionMetadata();

            if (metadata.DatabaseName.Equals("postgres", StringComparison.OrdinalIgnoreCase)
                || metadata.DatabaseName.Equals("template0", StringComparison.OrdinalIgnoreCase)
                || metadata.DatabaseName.Equals("template1", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Cannot drop system database '{metadata.DatabaseName}'.");
            }

            var cs = metadata.ChangeDatabaseTo("postgres").ToConnectionString();

            await using var connection = new NpgsqlConnection(cs);
            await connection.OpenAsync(ct);

            // Disconnect all users
            await using var command = connection.CreateCommand();

            command.CommandText =
                """
                    SELECT pg_terminate_backend(pg_stat_activity.pid)
                    FROM pg_stat_activity
                    WHERE pg_stat_activity.datname = @databaseName
                    AND pid <> pg_backend_pid()
                """;

            command.Parameters.AddWithValue("@databaseName", metadata.DatabaseName);
            await command.ExecuteNonQueryAsync(ct);

            command.Parameters.Clear();
            command.CommandText = $"DROP DATABASE IF EXISTS \"{metadata.DatabaseName}\"";
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to drop PostgreSQL database.", ex);
        }
    }
}
