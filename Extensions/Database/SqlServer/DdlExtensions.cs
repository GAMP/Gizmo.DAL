using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions.DdlExtensions;

internal static class SqlServer
{
    public static async Task EnsureLoginExists(DatabaseFacade facade, string name, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            var originalDbName = connection.Database;

            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                    SELECT sp.name, sp.is_disabled
                    FROM sys.server_principals AS sp
                    WHERE sp.type = 'U' AND sp.name LIKE @loginName
                """;
            command.Parameters.AddWithValue("@loginName", $"%{Environment.MachineName}\\{name}");

            string login = null;
            bool loginDisabled = false;

            using (var reader = await command.ExecuteReaderAsync(ct))
            {
                if (await reader.ReadAsync(ct))
                {
                    login = reader.GetString(0);
                    loginDisabled = reader.GetBoolean(1);
                }
            }

            command.Parameters.Clear();

            if (!string.IsNullOrEmpty(login))
            {
                if (loginDisabled)
                {
                    command.CommandText = $"ALTER LOGIN [{login}] ENABLE";
                    await command.ExecuteNonQueryAsync(ct);
                }
            }
            else
            {
                command.CommandText =
                    $"""
                        USE [master];
                        CREATE LOGIN [{Environment.MachineName}\{name}] FROM WINDOWS WITH DEFAULT_DATABASE = [master];
                        ALTER SERVER ROLE [sysadmin] ADD MEMBER [{Environment.MachineName}\{name}];
                    """;
                await command.ExecuteNonQueryAsync(ct);
            }

            // Restore the original database context
            await connection.ChangeDatabaseAsync(originalDbName, ct);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to create SQL Server login '{name}'. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create SQL Server login '{name}'.", ex);
        }
    }

    public static async Task<IEnumerable<string>> GetNonSystemDbNames(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            var originalDbName = connection.Database;
            
            // Ensure we're in master database context
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText =
                """
                    SELECT name 
                    FROM sys.databases 
                    WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')
                    ORDER BY name
                """;

            var dbNames = new List<string>();

            using var reader = await command.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
            {
                dbNames.Add(reader.GetString(0));
            }

            // Restore original database context
            await connection.ChangeDatabaseAsync(originalDbName, ct);

            return dbNames;
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to retrieve SQL Server database names. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve SQL Server database names.", ex);
        }
    }

    public static async Task<bool> Exists(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            var originalDbName = connection.Database;
            
            // Ensure we're in master database context to check if target database exists
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE [name] = @databaseName";
            command.Parameters.AddWithValue("@databaseName", originalDbName);

            using var reader = await command.ExecuteReaderAsync(ct);

            var found = false;

            if (await reader.ReadAsync(ct))
            {
                found = Convert.ToInt32(reader[0]) > 0;
            }

            // Restore original database context
            await connection.ChangeDatabaseAsync(originalDbName, ct);

            return found;
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to check if SQL Server database exists. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to check if SQL Server database exists.", ex);
        }
    }

    public static async Task Backup(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(backupFile))
            throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            await connection.OpenAsync(ct);

            var dbName = connection.Database.Replace("]", "]]");

            using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                    BACKUP DATABASE [{dbName}]
                    TO DISK = @backupFile
                    WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD, STATS = 5
                """;
            
            command.Parameters.AddWithValue("@backupFile", backupFile);
            command.CommandTimeout = 1000;
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to backup SQL Server database to '{backupFile}'. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to backup SQL Server database to '{backupFile}'.", ex);
        }
    }

    public static async Task Restore(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(backupFile))
            throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            await connection.OpenAsync(ct);

            var backupFiles = await GetBackupFiles(connection, backupFile, ct);

            if (backupFiles.Count == 0)
                throw new InvalidOperationException("No files found in the backup to restore.");

            var moveStatements = new List<string>(backupFiles.Count);

            foreach (var file in backupFiles)
            {
                moveStatements.Add($"MOVE N'{file.LogicalName.Replace("'", "''")}' TO N'{file.TargetPath.Replace("'", "''")}'");
            }

            var dbName = connection.Database.Replace("]", "]]");

            string restoreSql =
                $"""
                    RESTORE DATABASE [{dbName}]
                    FROM DISK = @backupFile
                    WITH FILE = 1, {string.Join(", ", moveStatements)}, NOUNLOAD, STATS = 5, REPLACE
                """;

            using var command = connection.CreateCommand();
            command.CommandText = restoreSql;
            command.Parameters.AddWithValue("@backupFile", backupFile);
            command.CommandTimeout = 600;
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to restore SQL Server database from '{backupFile}'. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to restore SQL Server database from '{backupFile}'.", ex);
        }
    }

    public static async Task Drop(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            string targetDbName = connection.Database;
            
            if (string.IsNullOrWhiteSpace(targetDbName) || 
                targetDbName.Equals("master", StringComparison.OrdinalIgnoreCase) ||
                targetDbName.Equals("tempdb", StringComparison.OrdinalIgnoreCase) ||
                targetDbName.Equals("model", StringComparison.OrdinalIgnoreCase) ||
                targetDbName.Equals("msdb", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Cannot drop system database '{targetDbName}'.");
            }

            targetDbName = targetDbName.Replace("]", "]]");
            
            // We need to connect to master database to drop the current database
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();

            // Force disconnect all users by setting the database to single user mode before dropping it
            command.CommandText = $"ALTER DATABASE [{targetDbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            await command.ExecuteNonQueryAsync(ct);

            command.CommandText = $"DROP DATABASE [{targetDbName}]";
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to drop SQL Server database. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to drop SQL Server database.", ex);
        }
    }

    private static async Task<(string dataDirectory, string logDirectory)> GetDirectories(SqlConnection connection, CancellationToken ct)
    {
        var dataDirectory = string.Empty;
        var logDirectory = string.Empty;

        var sql =
            """
                declare @DefaultData nvarchar(512)
                exec master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultData', @DefaultData output
                declare @DefaultLog nvarchar(512)
                exec master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultLog', @DefaultLog output
                declare @MasterData nvarchar(512)
                exec master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer\Parameters', N'SqlArg0', @MasterData output
                select @MasterData=substring(@MasterData, 3, 255)
                select @MasterData=substring(@MasterData, 1, len(@MasterData) - charindex('\', reverse(@MasterData)))
                declare @MasterLog nvarchar(512)
                exec master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer\Parameters', N'SqlArg2', @MasterLog output
                select @MasterLog=substring(@MasterLog, 3, 255)
                select @MasterLog=substring(@MasterLog, 1, len(@MasterLog) - charindex('\', reverse(@MasterLog)))
                select isnull(@DefaultData, @MasterData) DefaultData, isnull(@DefaultLog, @MasterLog) DefaultLog
            """;

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            using var reader = await command.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
            {
                dataDirectory = reader["DefaultData"].ToString();
                logDirectory = reader["DefaultLog"].ToString();
            }
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to get SQL Server default directories. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get SQL Server default directories.", ex);
        }

        if (string.IsNullOrEmpty(dataDirectory) || string.IsNullOrEmpty(logDirectory))
        {
            throw new InvalidOperationException("Could not determine SQL Server default data and log directories.");
        }

        return (dataDirectory, logDirectory);
    }

    private static async Task<List<BackupFile>> GetBackupFiles(SqlConnection connection, string backupFile, CancellationToken ct)
    {
        var files = new List<BackupFile>();

        try
        {
            var (dataDirectory, logDirectory) = await GetDirectories(connection, ct);

            using var command = connection.CreateCommand();
            command.CommandText = "RESTORE FILELISTONLY FROM DISK = @backupFile";
            command.Parameters.AddWithValue("@backupFile", backupFile);

            using var reader = await command.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
            {
                var logicalName = reader["LogicalName"]?.ToString();
                var type = reader["Type"]?.ToString();
                
                if (string.IsNullOrWhiteSpace(logicalName))
                    continue;

                var targetPath = type == "L" 
                    ? Path.Combine(logDirectory, $"{connection.Database}_log.ldf")
                    : Path.Combine(dataDirectory, $"{connection.Database}.mdf");

                files.Add(new BackupFile
                {
                    LogicalName = logicalName,
                    TargetPath = targetPath
                });
            }
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to get SQL Server backup file list from '{backupFile}'. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to get SQL Server backup files from '{backupFile}'.", ex);
        }

        return files;
    }

    private record BackupFile
    {
        public string LogicalName { get; init; }
        public string TargetPath { get; init; }
    }
}
