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
    public static async Task EnsureLoginExists(DatabaseFacade facade, string name, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as NpgsqlConnection;
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();

            command.CommandText = $"SELECT 1 FROM pg_roles WHERE rolname = @loginName";
            command.Parameters.AddWithValue("@loginName", name);

            var exists = await command.ExecuteScalarAsync(ct) != null;

            if (!exists)
            {
                command.CommandText = $"CREATE ROLE \"{name}\" LOGIN SUPERUSER";
                await command.ExecuteNonQueryAsync(ct);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to create PostgreSQL login.", ex);
        }
    }

    public static async Task<IEnumerable<string>> GetNonSystemDbNames(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as NpgsqlConnection;
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT datname FROM pg_database WHERE datistemplate = false";

            var dbNames = new List<string>();

            using var reader = await command.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
            {
                dbNames.Add(reader.GetString(0));
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
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as NpgsqlConnection;
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM pg_database WHERE datname = @databaseName";
            command.Parameters.AddWithValue("@databaseName", connection.Database);

            using var reader = await command.ExecuteReaderAsync(ct);

            var found = false;
            if (await reader.ReadAsync(ct))
            {
                found = Convert.ToInt32(reader[0]) > 0;
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
            string output = await process.StandardOutput.ReadToEndAsync(ct);
            string error = await process.StandardError.ReadToEndAsync(ct);
            await process.WaitForExitAsync(ct);
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"pg_dump failed: {error}");
            }
        }
        catch (Exception ex)
        {
            var error = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @" Ensure that the PostgreSQL home directory is set correctly in the environment variable 'POSTGRESQL_HOME' overwise the default path 'C:\Program Files\PostgreSQL\17\' will be used."
                : string.Empty;

            throw new InvalidOperationException($"Failed to backup PostgreSQL database.{error}", ex);
        }
    }

    public static async Task Restore(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            // Ensure we're not connected to the database we're trying to restore
            var connection = facade.GetDbConnection() as NpgsqlConnection;
            string targetDatabaseName = connection.Database;

            await connection.ChangeDatabaseAsync("postgres", ct);
            await connection.OpenAsync(ct);
            
            // Terminate any existing connections to the target database
            using var terminateCommand = connection.CreateCommand();
            terminateCommand.CommandText =
                $"""
                    SELECT pg_terminate_backend(pg_stat_activity.pid)
                    FROM pg_stat_activity
                    WHERE pg_stat_activity.datname = @databaseName
                    AND pid <> pg_backend_pid()
                """;
            terminateCommand.Parameters.AddWithValue("@databaseName", targetDatabaseName);
            await terminateCommand.ExecuteNonQueryAsync(ct);
            
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
        catch (Exception ex)
        {
            var error = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @" Ensure that the PostgreSQL home directory is set correctly in the environment variable 'POSTGRESQL_HOME' overwise the default path 'C:\Program Files\PostgreSQL\17\' will be used."
                : string.Empty;

            throw new InvalidOperationException($"Failed to restore PostgreSQL database.{error}", ex);
        }
    }

    public static async Task Drop(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as NpgsqlConnection;
            string targetDatabaseName = connection.Database; // Store the database name before changing
            
            // We need to connect to postgres database to drop the current database
            await connection.ChangeDatabaseAsync("postgres", ct);
            await connection.OpenAsync(ct);

            // Disconnect all users
            using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                    SELECT pg_terminate_backend(pg_stat_activity.pid)
                    FROM pg_stat_activity
                    WHERE pg_stat_activity.datname = @databaseName
                    AND pid <> pg_backend_pid()
                """;

            command.Parameters.AddWithValue("@databaseName", targetDatabaseName);
            await command.ExecuteNonQueryAsync(ct);

            command.Parameters.Clear();
            command.CommandText = $"DROP DATABASE IF EXISTS \"{targetDatabaseName}\"";
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to drop PostgreSQL database.", ex);
        }
    }
}
