using System;
using System.Collections.Generic;
using System.Text;
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
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to create SQL Server login.", ex);
        }
    }

    public static async Task<IEnumerable<string>> GetNonSystemDbNames(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText =
                """
                    SELECT name 
                    FROM master.sys.databases 
                    WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')
                """;

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
            throw new InvalidOperationException("Failed to retrieve SQL Server database names.", ex);
        }
    }

    public static async Task<bool> Exists(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT COUNT(*) FROM sys.databases WHERE [name] = @databaseName";
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
            throw new InvalidOperationException("Failed to check if SQL Server database exists. ", ex);
        }
    }

    public static async Task Backup(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            await connection.OpenAsync(ct);

            string safeBackupFile = backupFile.Replace("'", "''");
            string backupSql =
                $"""
                    BACKUP DATABASE [{connection.Database}]
                    TO DISK = N'{safeBackupFile}'
                    WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD, STATS = 5
                """;

            using var command = connection.CreateCommand();
            command.CommandText = backupSql;
            command.CommandTimeout = 1000;
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to backup SQL Server database.", ex);
        }
    }

    public static async Task Restore(DatabaseFacade facade, string backupFile, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            string targetDatabaseName = connection.Database; // Store the database name before changing
            
            // We need to connect to master database to restore the current database
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            var backupFiles = await GetBackupFiles(connection, targetDatabaseName, backupFile, ct);

            var moveSql = new StringBuilder(backupFiles.Count);

            foreach (var file in backupFiles)
            {
                moveSql.Append($"MOVE N'{file.LogicalName}' TO N'{file.TargetPath}',");
            }

            string safeBackupFile = backupFile.Replace("'", "''");

            string restoreSql =
                $"""
                    RESTORE DATABASE [{targetDatabaseName}]
                    FROM DISK = N'{safeBackupFile}'
                    WITH FILE = 1, {moveSql} NOUNLOAD, STATS = 5
                """;

            using var command = connection.CreateCommand();
            command.CommandText = restoreSql;
            command.CommandTimeout = 600;
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to restore SQL Server database.", ex);
        }
    }

    public static async Task Drop(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection() as SqlConnection;
            string targetDatabaseName = connection.Database; // Store the database name before changing
            
            // We need to connect to master database to drop the current database
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            using var command = connection.CreateCommand();

            // Force disconnect all users by setting the database to single user mode before dropping it
            command.CommandText = $"ALTER DATABASE [{targetDatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            await command.ExecuteNonQueryAsync(ct);

            command.CommandText = $"DROP DATABASE [{targetDatabaseName}]";
            await command.ExecuteNonQueryAsync(ct);
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
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get SQL Server default directories.", ex);
        }

        return (dataDirectory, logDirectory);
    }

    private static async Task<List<BackupFile>> GetBackupFiles(SqlConnection connection, string databaseName, string backupFile, CancellationToken ct)
    {
        var files = new List<BackupFile>();

        try
        {
            var (dataDirectory, logDirectory) = await GetDirectories(connection, ct);

            using var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_restorefilelistonly @backupFile";
            command.Parameters.AddWithValue("@backupFile", backupFile);

            using var reader = await command.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
            {
                files.Add(new()
                {
                    LogicalName = reader["LogicalName"]?.ToString(),
                    TargetPath = reader["Type"]?.ToString() == "L" ? $"{logDirectory}\\{databaseName}_log.ldf" : $"{dataDirectory}\\{databaseName}.mdf"
                });
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get SQL Server backup files.", ex);
        }

        return files;
    }

    private record BackupFile
    {
        public string LogicalName { get; init; }
        public string TargetPath { get; init; }
    }
}
