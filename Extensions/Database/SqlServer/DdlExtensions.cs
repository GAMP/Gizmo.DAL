using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions.DdlExtensions;

internal static class SqlServer
{
    public static async Task EnsureLoginExists(DatabaseFacade facade, string login, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login name cannot be null or empty.", nameof(login));

            //This connection should not be disposed if it was created by Entity Framework.
            var originalConnection = facade.GetDbConnection();

            await using var connection = new SqlConnection(originalConnection.ConnectionString);
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText =
                """
                    SELECT sp.name, sp.is_disabled
                    FROM sys.server_principals AS sp
                    WHERE sp.type = 'U' AND sp.name LIKE @loginName
                """;

            command.Parameters.AddWithValue("@loginName", $"%{Environment.MachineName}\\{login}");

            string existingLogin = null;
            bool loginDisabled = false;

            await using (var reader = await command.ExecuteReaderAsync(ct))
            {
                if (await reader.ReadAsync(ct))
                {
                    existingLogin = reader.GetString(0);
                    loginDisabled = reader.GetBoolean(1);
                }
            }

            command.Parameters.Clear();

            if (string.IsNullOrEmpty(existingLogin))
            {
                var loginName = $"{Environment.MachineName}\\{login}";

                command.CommandText =
                    $"""
                         CREATE LOGIN [{loginName}] FROM WINDOWS WITH DEFAULT_DATABASE = [master];
                         ALTER SERVER ROLE [sysadmin] ADD MEMBER [{loginName}];
                     """;

                await command.ExecuteNonQueryAsync(ct);
            }

            if (loginDisabled)
            {
                command.CommandText = $"ALTER LOGIN [{existingLogin}] ENABLE";
                await command.ExecuteNonQueryAsync(ct);
            }
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to create SQL Server login '{login}'. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create SQL Server login '{login}'.", ex);
        }
    }

    public static async Task<IEnumerable<string>> GetNonSystemDbNames(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
            //This connection should not be disposed if it was created by Entity Framework.
            var connection = facade.GetDbConnection();
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText =
                """
                    SELECT name 
                    FROM master.sys.databases 
                    WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')
                    ORDER BY name
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
            if (facade.GetDbConnection() is not SqlConnection connection)
                throw new InvalidOperationException("The database connection is not a valid SQL Server connection.");

            if (connection.Database.Equals("master", StringComparison.OrdinalIgnoreCase)
                || connection.Database.Equals("tempdb", StringComparison.OrdinalIgnoreCase)
                || connection.Database.Equals("model", StringComparison.OrdinalIgnoreCase)
                || connection.Database.Equals("msdb", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM master.sys.databases WHERE [name] = @databaseName";
            command.Parameters.AddWithValue("@databaseName", connection.Database);

            await using var reader = await command.ExecuteReaderAsync(ct);

            var found = false;

            if (await reader.ReadAsync(ct))
            {
                found = Convert.ToInt32(reader[0]) > 0;
            }

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
        try
        {
            if (string.IsNullOrWhiteSpace(backupFile))
                throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

            //This connection should not be disposed if it was created by Entity Framework.
            var originalConnection = facade.GetDbConnection();

            await using var connection = new SqlConnection(originalConnection.ConnectionString);
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText =
                $"""
                     BACKUP DATABASE [{originalConnection.Database}]
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
        try
        {
            if (string.IsNullOrWhiteSpace(backupFile))
                throw new ArgumentException("Backup file path cannot be null or empty.", nameof(backupFile));

            //This connection should not be disposed if it was created by Entity Framework.
            var originalConnection = facade.GetDbConnection();

            await using var connection = new SqlConnection(originalConnection.ConnectionString);
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            var backupFiles = await GetBackupFiles(connection, originalConnection.Database, backupFile, ct);

            var moveStatement = new StringBuilder();

            foreach (var file in backupFiles)
            {
                moveStatement.Append($"MOVE N'{file.LogicalName}' TO N'{file.TargetPath}',");
            }

            string restoreSql =
                $"""
                     RESTORE DATABASE [{originalConnection.Database}]
                     FROM DISK = @backupFile
                     WITH FILE = 1, {moveStatement} NOUNLOAD, STATS = 5, REPLACE
                 """;

            await using var command = connection.CreateCommand();
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
            var originalConnection = facade.GetDbConnection();

            if (string.IsNullOrWhiteSpace(originalConnection.Database)
                || originalConnection.Database.Equals("master", StringComparison.OrdinalIgnoreCase)
                || originalConnection.Database.Equals("tempdb", StringComparison.OrdinalIgnoreCase)
                || originalConnection.Database.Equals("model", StringComparison.OrdinalIgnoreCase)
                || originalConnection.Database.Equals("msdb", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Cannot drop system database '{originalConnection.Database}'.");
            }

            await using var connection = new SqlConnection(originalConnection.ConnectionString);
            await connection.ChangeDatabaseAsync("master", ct);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            // Force disconnect all users by setting the database to single user mode before dropping it
            command.CommandText = $"ALTER DATABASE [{originalConnection.Database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            await command.ExecuteNonQueryAsync(ct);

            command.CommandText = $"DROP DATABASE [{originalConnection.Database}]";
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

        const string Sql =
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
            await using var command = connection.CreateCommand();
            command.CommandText = Sql;

            await using var reader = await command.ExecuteReaderAsync(ct);

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

    private static async Task<List<BackupFile>> GetBackupFiles(SqlConnection connection, string dbName, string backupFile, CancellationToken ct)
    {
        try
        {
            var (dataDirectory, logDirectory) = await GetDirectories(connection, ct);

            await using var command = connection.CreateCommand();
            command.CommandText = "RESTORE FILELISTONLY FROM DISK = @backupFile";
            command.Parameters.AddWithValue("@backupFile", backupFile);

            var files = new List<BackupFile>();
            await using var reader = await command.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
            {
                var logicalName = reader["LogicalName"]?.ToString();
                var type = reader["Type"]?.ToString();

                if (string.IsNullOrWhiteSpace(logicalName))
                    continue;

                var targetPath = type == "L"
                    ? Path.Combine(logDirectory, $"{dbName}_log.ldf")
                    : Path.Combine(dataDirectory, $"{dbName}.mdf");

                files.Add(new BackupFile { LogicalName = logicalName, TargetPath = targetPath });
            }

            return files;
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException($"Failed to get SQL Server backup file list from '{backupFile}'. SQL Error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to get SQL Server backup files from '{backupFile}'.", ex);
        }
    }

    private record BackupFile
    {
        public string LogicalName { get; init; }
        public string TargetPath { get; init; }
    }
}
