using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using static Gizmo.DAL.Extensions.DclExtensions.PostgreSql;

namespace Gizmo.DAL.Extensions.DdlExtensions;

internal static class PostgreSql
{
    public static async Task<bool> LoginExists(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ToConnectionString();

        await using var connection = new NpgsqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT COUNT(*) FROM pg_roles WHERE rolname = @loginName";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@loginName";
        parameter.Value = loginName;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(ct);
        return Convert.ToInt32(result) > 0;
    }

    public static async Task<bool> HasAdminPermissions(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ToConnectionString();

        await using var connection = new NpgsqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT COUNT(*) FROM pg_roles WHERE rolname = @loginName AND rolsuper = true";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@loginName";
        parameter.Value = loginName;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(ct);
        return Convert.ToInt32(result) > 0;
    }

    public static async Task DisableLogin(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ToConnectionString();

        await using var connection = new NpgsqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = $"ALTER ROLE \"{loginName}\" NOLOGIN";
        await command.ExecuteNonQueryAsync(ct);
    }

    public static async Task<bool> IsLoginDisabled(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ToConnectionString();

        await using var connection = new NpgsqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT NOT rolcanlogin FROM pg_roles WHERE rolname = @loginName";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@loginName";
        parameter.Value = loginName;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(ct);
        return result != null && Convert.ToBoolean(result);
    }

    public static async Task DropLogin(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ToConnectionString();

        await using var connection = new NpgsqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = $"DROP ROLE IF EXISTS \"{loginName}\"";
        await command.ExecuteNonQueryAsync(ct);
    }

    public static async Task EnsureAdminExists(DatabaseFacade facade, string login, CancellationToken ct)
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

            // Check if role exists with detailed info
            command.CommandText =
                """
                    SELECT r.rolname, 
                        NOT r.rolcanlogin as is_disabled,
                        CASE WHEN r.rolpassword IS NOT NULL THEN 'P' ELSE 'N' END as has_password
                    FROM pg_roles r 
                    WHERE r.rolname = @loginName
                """;

            command.Parameters.AddWithValue("@loginName", login);

            string existingLogin = null;
            bool loginDisabled = false;
            string hasPassword = null;

            await using (var reader = await command.ExecuteReaderAsync(ct))
            {
                if (await reader.ReadAsync(ct))
                {
                    existingLogin = reader.GetString(0);
                    loginDisabled = reader.GetBoolean(1);
                    hasPassword = reader.GetString(2);
                }
            }

            // If role exists and has password, no need to recreate
            if (!string.IsNullOrEmpty(existingLogin) && hasPassword == "P")
            {
                // Just ensure the role is enabled if it was disabled
                if (loginDisabled)
                {
                    command.Parameters.Clear();
                    command.CommandText = $"ALTER ROLE \"{existingLogin}\" LOGIN";
                    await command.ExecuteNonQueryAsync(ct);
                }
                return; // Role already exists with password
            }

            command.Parameters.Clear();

            if (string.IsNullOrEmpty(existingLogin))
            {
                command.CommandText = $"CREATE ROLE \"{login}\" LOGIN SUPERUSER PASSWORD '{metadata.Password}'";
                await command.ExecuteNonQueryAsync(ct);
            }
            else
            {
                command.CommandText = $"ALTER ROLE \"{existingLogin}\" LOGIN SUPERUSER PASSWORD '{metadata.Password}'";
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

            var docker = Environment.GetEnvironmentVariable("POSTGRES_DOCKER");
            var isDocker = !string.IsNullOrEmpty(docker);

            var cmd = "pg_dump";

            string fileName;
            var arguments = new List<string>
            {
                $"-U {metadata.Username}",
                $"-d \"{metadata.DatabaseName}\"",
                $"-f \"{backupFile}\"",
                "-Fc",
                "--no-owner",
                "--no-privileges"
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

            // Check for actual errors, not just warnings
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"{cmd} failed with error code {process.ExitCode}: {stderr}");
            }
            else if (!string.IsNullOrEmpty(stderr) && !stderr.Contains("warning:"))
            {
                // Only throw on non-warning messages
                throw new InvalidOperationException($"{cmd} generated error: {stderr}");
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

            if (!await Exists(facade, ct))
            {
                await Create(facade, ct);
            }

            var metadata = ConnectionMetadata.FromConnectionString(facade.GetConnectionString());

            var docker = Environment.GetEnvironmentVariable("POSTGRES_DOCKER");
            var isDocker = !string.IsNullOrEmpty(docker);

            var cmd = "pg_restore";

            string fileName;
            var arguments = new List<string>
            {
                $"-d {metadata.DatabaseName}",
                $"-U {metadata.Username}",
                $"-v \"{backupFile}\"",
                "--clean",
                "--if-exists",
                "--no-owner",
                "--no-acl",      // Skip privilege (ACL) restoration
                "--no-comments", // Skip comment restoration
                "--disable-triggers" // Disable triggers during restore to avoid constraint issues
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

            // Check if there are errors (not just warnings)
            bool hasErrors = false;
            if (process.ExitCode != 0)
            {
                // pg_restore returns non-zero even with warnings, but we want to allow warnings
                if (stderr.Contains("errors ignored on restore:"))
                {
                    Console.WriteLine($"Warning: pg_restore had non-fatal errors that were ignored: {stderr}");
                }
                else
                {
                    hasErrors = true;
                }
            }

            if (hasErrors)
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

    public static async Task Create(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("postgres").ToConnectionString();

            await using var connection = new NpgsqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText = $"CREATE DATABASE \"{metadata.DatabaseName}\"";
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create PostgreSQL database '{facade.GetConnectionMetadata().DatabaseName}'.", ex);
        }
    }
}
