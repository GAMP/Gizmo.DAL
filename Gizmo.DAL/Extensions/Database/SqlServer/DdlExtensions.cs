using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions.DdlExtensions;

internal static class SqlServer
{
    public static async Task<bool> LoginExists(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

        await using var connection = new SqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT COUNT(*) FROM sys.server_principals WHERE name = @loginName";
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
        var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

        await using var connection = new SqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = """
            SELECT COUNT(*) 
            FROM sys.server_role_members srm
            JOIN sys.server_principals sp ON srm.member_principal_id = sp.principal_id
            JOIN sys.server_principals sr ON srm.role_principal_id = sr.principal_id
            WHERE sp.name = @loginName AND sr.name = 'sysadmin'
        """;

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
        var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

        await using var connection = new SqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = $"ALTER LOGIN [{loginName}] DISABLE";
        await command.ExecuteNonQueryAsync(ct);
    }

    public static async Task<bool> IsLoginDisabled(DatabaseFacade facade, string loginName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(loginName))
            throw new ArgumentException("Login name cannot be null or empty.", nameof(loginName));

        var metadata = facade.GetConnectionMetadata();
        var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

        await using var connection = new SqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT is_disabled FROM sys.server_principals WHERE name = @loginName";
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
        var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

        await using var connection = new SqlConnection(cs);
        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();

        command.CommandText = $"DROP LOGIN [{loginName}]";
        await command.ExecuteNonQueryAsync(ct);
    }

    public static async Task EnsureAdminExists(DatabaseFacade facade, string login, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login name cannot be null or empty.", nameof(login));

            // POSTGRES_DOCKER is set by the TestContainers fixture.
            var isDocker = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_DOCKER"));
            var useSqlAuth = OperatingSystem.IsLinux() || isDocker;

            // Windows logins require a qualified NT name (DOMAIN\user or MACHINE\user),
            // otherwise SQL Server rejects the CREATE LOGIN with a cryptic error.
            if (!useSqlAuth && !login.Contains('\\'))
                throw new ArgumentException(
                    $"Login '{login}' must be a qualified Windows name (DOMAIN\\user or MACHINE\\user).",
                    nameof(login));

            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

            await using var connection = new SqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT sp.name, sp.is_disabled, sp.type
                FROM sys.server_principals AS sp
                WHERE sp.name = @loginName
            """;

            command.Parameters.AddWithValue("@loginName", login);

            string existingLogin = null;
            bool loginDisabled = false;
            string loginType = null;

            await using (var reader = await command.ExecuteReaderAsync(ct))
            {
                if (await reader.ReadAsync(ct))
                {
                    existingLogin = reader.GetString(0);
                    loginDisabled = reader.GetBoolean(1);
                    loginType = reader.GetString(2);
                }
            }

            // Type codes: 'S' = SQL login, 'U' = Windows user, 'G' = Windows group.
            var existingMatchesIntent = useSqlAuth
                ? loginType == "S"
                : loginType is "U" or "G";

            if (!string.IsNullOrEmpty(existingLogin) && existingMatchesIntent)
            {
                if (loginDisabled)
                {
                    command.Parameters.Clear();
                    command.CommandText = $"ALTER LOGIN [{existingLogin}] ENABLE";
                    await command.ExecuteNonQueryAsync(ct);
                }
                return;
            }

            command.Parameters.Clear();

            string createLoginCommand;
            if (useSqlAuth)
            {
                // CREATE LOGIN cannot parameterize the password, so escape embedded quotes.
                // The new SQL login reuses the admin connection's password — only intended for the
                // TestContainers/Linux path where the same password is shared across the container.
                var escapedPassword = (metadata.Password ?? string.Empty).Replace("'", "''");
                createLoginCommand = $"""
                    CREATE LOGIN [{login}] WITH PASSWORD = N'{escapedPassword}', DEFAULT_DATABASE = [master], CHECK_POLICY = OFF;
                    ALTER SERVER ROLE [sysadmin] ADD MEMBER [{login}];
                """;
            }
            else
            {
                createLoginCommand = $"""
                    CREATE LOGIN [{login}] FROM WINDOWS;
                    ALTER SERVER ROLE [sysadmin] ADD MEMBER [{login}];
                """;
            }

            if (!string.IsNullOrEmpty(existingLogin))
            {
                // Existing principal's auth type doesn't match intent — drop and recreate.
                command.CommandText = $"DROP LOGIN [{existingLogin}]";
                await command.ExecuteNonQueryAsync(ct);
            }

            command.CommandText = createLoginCommand;
            await command.ExecuteNonQueryAsync(ct);
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
            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

            await using var connection = new SqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText = """
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
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve SQL Server database names.", ex);
        }
    }

    public static async Task<bool> Exists(DatabaseFacade facade, CancellationToken ct)
    {
        try
        {
           var metadata = facade.GetConnectionMetadata();
           var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

            await using var connection = new SqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE [name] = @databaseName";
            command.Parameters.AddWithValue("@databaseName", metadata.DatabaseName);

            await using var reader = await command.ExecuteReaderAsync(ct);

            var found = false;

            if (await reader.ReadAsync(ct))
            {
                found = Convert.ToInt32(reader[0]) > 0;
            }

            return found;
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
            var metadata = facade.GetConnectionMetadata();
            var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

            await using var connection = new SqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            command.CommandText = $"""
                BACKUP DATABASE [{metadata.DatabaseName}]
                TO DISK = @backupFile
                WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD, STATS = 10
            """;

            command.Parameters.AddWithValue("@backupFile", backupFile);
            command.CommandTimeout = 1000;
            await command.ExecuteNonQueryAsync(ct);
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

            if (!await Exists(facade, ct))
            {
                await Create(facade, ct);
            }

            var metadata = facade.GetConnectionMetadata();

            await using var originalConnection = new SqlConnection(metadata.ToConnectionString());
            await originalConnection.OpenAsync(ct);
            var backupFiles = await originalConnection.GetBackupFiles(backupFile, ct);

            var masterConnectionString = metadata.ChangeDatabaseTo("master").ToConnectionString();
            await using var masterConnection = new SqlConnection(masterConnectionString);
            await masterConnection.OpenAsync(ct);


            var moveStatement = new StringBuilder();

            foreach (var file in backupFiles)
            {
                moveStatement.Append($"MOVE N'{file.LogicalName}' TO N'{file.TargetPath}',");
            }

            string restoreSql = $"""
                ALTER DATABASE [{metadata.DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE [{metadata.DatabaseName}]
                FROM DISK = @backupFile
                WITH FILE = 1, {moveStatement} NOUNLOAD, STATS = 10, REPLACE, RECOVERY
                ALTER DATABASE [{metadata.DatabaseName}] SET MULTI_USER;
            """;

            await using var command = masterConnection.CreateCommand();
            command.CommandText = restoreSql;
            command.Parameters.AddWithValue("@backupFile", backupFile);
            command.CommandTimeout = 600;
            await command.ExecuteNonQueryAsync(ct);
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
            var metadata = facade.GetConnectionMetadata();

            if (metadata.DatabaseName.Equals("master", StringComparison.OrdinalIgnoreCase)
                || metadata.DatabaseName.Equals("tempdb", StringComparison.OrdinalIgnoreCase)
                || metadata.DatabaseName.Equals("model", StringComparison.OrdinalIgnoreCase)
                || metadata.DatabaseName.Equals("msdb", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Cannot drop system database '{metadata.DatabaseName}'.");
            }

            var cs = metadata.ChangeDatabaseTo("master").ToConnectionString();

            await using var connection = new SqlConnection(cs);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            // Force disconnect all users by setting the database to single user mode before dropping it
            command.CommandText = $"ALTER DATABASE [{metadata.DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            await command.ExecuteNonQueryAsync(ct);

            command.CommandText = $"DROP DATABASE [{metadata.DatabaseName}]";
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to drop SQL Server database.", ex);
        }
    }

    public static async Task Create(DatabaseFacade facade, CancellationToken ct)
    {
        var metadata = facade.GetConnectionMetadata();

        try
        {
            var masterConnectionString = metadata.ChangeDatabaseTo("master").ToConnectionString();

            await using var connection = new SqlConnection(masterConnectionString);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();

            // Create the database with proper file locations
            command.CommandText = $"""
                CREATE DATABASE [{metadata.DatabaseName}];
            """;
            await command.ExecuteNonQueryAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create SQL Server database '{metadata.DatabaseName}'.", ex);
        }
    }

    private static async Task<(string dataDirectory, string logDirectory)> GetDirectories(SqlConnection connection, CancellationToken ct)
    {
        var dataDirectory = string.Empty;
        var logDirectory = string.Empty;

        const string Sql = """
            IF CHARINDEX('Linux', @@VERSION) > 0
                BEGIN
                    -- On Linux, get directories from system views
                    SELECT 
                        DefaultData = (SELECT physical_name FROM sys.master_files WHERE database_id = 1 AND file_id = 1),
                        DefaultLog = (SELECT physical_name FROM sys.master_files WHERE database_id = 1 AND file_id = 2)
                END
            ELSE
                BEGIN
                    -- On Windows, use registry
                    DECLARE @DefaultData nvarchar(512)
                    EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultData', @DefaultData OUTPUT
                    DECLARE @DefaultLog nvarchar(512)
                    EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultLog', @DefaultLog OUTPUT
                    DECLARE @MasterData nvarchar(512)
                    EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer\Parameters', N'SqlArg0', @MasterData OUTPUT
                    SELECT @MasterData=SUBSTRING(@MasterData, 3, 255)
                    SELECT @MasterData=SUBSTRING(@MasterData, 1, LEN(@MasterData) - CHARINDEX('\', REVERSE(@MasterData)))
                    DECLARE @MasterLog nvarchar(512)
                    EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer\Parameters', N'SqlArg2', @MasterLog OUTPUT
                    SELECT @MasterLog=SUBSTRING(@MasterLog, 3, 255)
                    SELECT @MasterLog=SUBSTRING(@MasterLog, 1, LEN(@MasterLog) - CHARINDEX('\', REVERSE(@MasterLog)))
                    SELECT ISNULL(@DefaultData, @MasterData) DefaultData, ISNULL(@DefaultLog, @MasterLog) DefaultLog
                END
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

    private static async Task<List<BackupFile>> GetBackupFiles(this SqlConnection connection, string backupFile, CancellationToken ct)
    {
        try
        {
            await using var command = connection.CreateCommand();

            var dbName = connection.Database;

            command.CommandText = "RESTORE FILELISTONLY FROM DISK = @backupFile";
            command.Parameters.AddWithValue("@backupFile", backupFile);

            var files = new List<BackupFile>();
            await using var reader = await command.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
            {
                var logicalName = reader["LogicalName"]?.ToString();
                var targetPath = reader["PhysicalName"]?.ToString();

                if (string.IsNullOrEmpty(logicalName) || string.IsNullOrEmpty(targetPath))
                    continue;

                var type = reader["Type"]?.ToString();
                var directory = Path.GetDirectoryName(targetPath);
                var extension = Path.GetExtension(targetPath);

                targetPath = type switch
                {
                    "D" => Path.Combine(directory, dbName + extension),
                    "L" => Path.Combine(directory, dbName + "_log" + extension),
                    _ => throw new NotSupportedException($"Unsupported file type: {type}"),
                };
                
                files.Add(new BackupFile { LogicalName = logicalName, TargetPath = targetPath });
            }

            return files;
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
