using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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

            //This connection should not be disposed if it was created by Entity Framework.
            var originalConnection = facade.GetDbConnection();

            using var connection = new NpgsqlConnection(originalConnection.ConnectionString);
            await connection.ChangeDatabaseAsync("postgres", ct);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
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
        catch (NpgsqlException ex)
        {
            throw new InvalidOperationException($"Failed to create PostgreSQL login '{login}'. PostgreSQL Error: {ex.Message}", ex);
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
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection();
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText = 
                """
                    SELECT datname 
                    FROM pg_database 
                    WHERE datistemplate = false 
                    AND datname NOT IN ('postgres')
                    ORDER BY datname
                """;

            var dbNames = new List<string>();

            using (var reader = await command.ExecuteReaderAsync(ct))
            {
                while (await reader.ReadAsync(ct))
                {
                    dbNames.Add(reader.GetString(0));
                }
            }

            return dbNames;
        }
        catch (NpgsqlException ex)
        {
            throw new InvalidOperationException($"Failed to retrieve PostgreSQL database names. PostgreSQL Error: {ex.Message}", ex);
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
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as NpgsqlConnection;

            if (connection.Database.Equals("postgres", StringComparison.OrdinalIgnoreCase) ||
                connection.Database.Equals("template0", StringComparison.OrdinalIgnoreCase) ||
                connection.Database.Equals("template1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM pg_database WHERE datname = @databaseName";
            command.Parameters.AddWithValue("@databaseName", connection.Database);

            var found = false;

            using (var reader = await command.ExecuteReaderAsync(ct))
            {
                if (await reader.ReadAsync(ct))
                {
                    found = Convert.ToInt32(reader[0]) > 0;
                }
            }

            return found;
        }
        catch (NpgsqlException ex)
        {
            throw new InvalidOperationException($"Failed to check if PostgreSQL database exists. PostgreSQL Error: {ex.Message}", ex);
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

            var pgHome = Environment.GetEnvironmentVariable("POSTGRESQL_HOME");
            string commandFile = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? pgHome is not null
                    ? Path.Combine(pgHome, "bin", "pg_dump.exe")
                    : @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe"
                : "pg_dump";

            var processInfo = new System.Diagnostics.ProcessStartInfo
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

            using var process = System.Diagnostics.Process.Start(processInfo);
            var output = await process.StandardOutput.ReadToEndAsync(ct);
            var error = await process.StandardError.ReadToEndAsync(ct);

            await process.WaitForExitAsync(ct);
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"pg_dump failed: {error}");
            }
        }
        catch (NpgsqlException ex)
        {
            throw new InvalidOperationException($"Failed to backup PostgreSQL database to '{backupFile}'. PostgreSQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            var error = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @" Ensure that the PostgreSQL home directory is set correctly in the environment variable 'POSTGRESQL_HOME' overwise the default path 'C:\Program Files\PostgreSQL\17\' will be used."
                : string.Empty;

            throw new InvalidOperationException($"Failed to backup PostgreSQL database to '{backupFile}'.{error}", ex);
        }
    }

    public static async Task Restore(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(backupFile))
                throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

            var metadata = ConnectionMetadata.FromConnectionString(facade.GetConnectionString());

            var pgHome = Environment.GetEnvironmentVariable("POSTGRESQL_HOME");
            string commandFile = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? pgHome is not null
                    ? Path.Combine(pgHome, "bin", "pg_restore.exe")
                    : @"C:\Program Files\PostgreSQL\17\bin\pg_restore.exe"
                : "pg_restore";

            var processInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = commandFile,
                Arguments = $"--dbname=\"{metadata.DatabaseName}\" --no-owner \"{backupFile}\"",
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

            using var process = System.Diagnostics.Process.Start(processInfo);

            string output = await process.StandardOutput.ReadToEndAsync(ct);
            string error = await process.StandardError.ReadToEndAsync(ct);

            await process.WaitForExitAsync(ct);

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"pg_restore failed: {error}");
            }
        }
        catch (NpgsqlException ex)
        {
            throw new InvalidOperationException($"Failed to restore PostgreSQL database from '{backupFile}'. PostgreSQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            var error = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @" Ensure that the PostgreSQL home directory is set correctly in the environment variable 'POSTGRESQL_HOME' overwise the default path 'C:\Program Files\PostgreSQL\17\' will be used."
                : string.Empty;

            throw new InvalidOperationException($"Failed to restore PostgreSQL database from '{backupFile}'.{error}", ex);
        }
    }

    public static async Task Drop(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var originalConnection = facade.GetDbConnection();

            if (string.IsNullOrWhiteSpace(originalConnection.Database) ||
                originalConnection.Database.Equals("postgres", StringComparison.OrdinalIgnoreCase) ||
                originalConnection.Database.Equals("template0", StringComparison.OrdinalIgnoreCase) ||
                originalConnection.Database.Equals("template1", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Cannot drop system database '{originalConnection.Database}'.");
            }

            using var connection = new NpgsqlConnection(originalConnection.ConnectionString);
            await connection.ChangeDatabaseAsync("postgres", ct);
            await connection.OpenAsync(ct);

            // Disconnect all users
            using var command = connection.CreateCommand();
            command.CommandText =
                """
                    SELECT pg_terminate_backend(pg_stat_activity.pid)
                    FROM pg_stat_activity
                    WHERE pg_stat_activity.datname = @databaseName
                    AND pid <> pg_backend_pid()
                """;

            command.Parameters.AddWithValue("@databaseName", originalConnection.Database);
            await command.ExecuteNonQueryAsync(ct);

            command.Parameters.Clear();
            command.CommandText = $"DROP DATABASE IF EXISTS \"{originalConnection.Database}\"";
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (NpgsqlException ex)
        {
            throw new InvalidOperationException($"Failed to drop PostgreSQL database. PostgreSQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to drop PostgreSQL database.", ex);
        }
    }
}
