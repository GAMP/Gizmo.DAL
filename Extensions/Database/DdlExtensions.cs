using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Extensions.DdlExtensions;
using Gizmo.DAL.Scripts;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions;

/// <summary>
/// Provides extension methods for Data Definition Language (DDL) operations.
/// </summary>
/// <remarks>
/// DDL commands are used to define and modify the structure of database objects. Typical DDL commands include:
/// <list type="bullet">
///   <item><description><c>CREATE</c> – Creates a new table, a view of a table, or other object in database.</description></item>
///   <item><description><c>ALTER</c> – Modifies an existing database object, such as a table.</description></item>
///   <item><description><c>DROP</c> – Deletes an entire table, a view of a table or other object in the database.</description></item>
///   <item><description><c>TRUNCATE</c> – Removes all records from a table, including all spaces allocated for the records are removed.</description></item>
/// </list>
/// </remarks>
public static class DdlOperations
{
    /// <summary>
    /// Checks if the database exists on the current <see cref="DatabaseFacade"/> instance.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a boolean indicating whether the database exists.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for checking database existence.</exception>
    /// <remarks>
    /// This method supports SQL Server and PostgreSQL database providers. The operation is provider-specific and uses appropriate system queries to determine database existence.
    /// </remarks>
    public static Task<bool> Exists(this DatabaseFacade facade, CancellationToken ct = default) =>
        facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.Exists(facade, ct),
            Provider.Type.PostgreSql => PostgreSql.Exists(facade, ct),
            _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for checking database existence.")
        };

    /// <summary>
    /// Restores a database from the specified backup file using the current <see cref="DatabaseFacade"/> instance.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="path">The file path to the backup file to restore from.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous restore operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> or <paramref name="path"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="path"/> is empty or whitespace.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the backup file specified by <paramref name="path"/> does not exist.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set or restore operation fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for restore operation.</exception>
    /// <remarks>
    /// This method supports SQL Server and PostgreSQL database providers. The restore operation is provider-specific:
    /// <list type="bullet">
    /// <item><description>SQL Server: Uses RESTORE DATABASE command with automatic file relocation.</description></item>
    /// <item><description>PostgreSQL: Uses pg_restore command-line utility.</description></item>
    /// </list>
    /// </remarks>
    public static Task Restore(this DatabaseFacade facade, string path, CancellationToken ct = default) =>
        facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.Restore(facade, path, ct),
            Provider.Type.PostgreSql => PostgreSql.Restore(facade, path, ct),
            _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for restore operation.")
        };

    /// <summary>
    /// Generates an appropriate backup file name based on the database provider type of the current <see cref="DatabaseFacade"/> instance.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <returns>
    /// A string containing the backup file name with the appropriate extension:
    /// <list type="bullet">
    /// <item><description>.bak for SQL Server variants (MSSQL, MSSQLEXPRESS, LOCALDB)</description></item>
    /// <item><description>.dump for PostgreSQL</description></item>
    /// <item><description>.sql for MySQL</description></item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set or connection metadata cannot be retrieved.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for backup file name generation.</exception>
    /// <remarks>
    /// This method extracts the database name from the connection metadata and generates a backup file name
    /// with the appropriate extension for the database provider. The generated name can be used
    /// for backup and restore operations.
    /// </remarks>
    public static string GenerateBackupName(this DatabaseFacade facade)
    {
        var metadata = facade.GetConnectionMetadata();

        return facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => $"{metadata.DatabaseName}.bak",
            Provider.Type.PostgreSql => $"{metadata.DatabaseName}.dump",
            Provider.Type.MySql => $"{metadata.DatabaseName}.sql",
            _ => throw new NotSupportedException($"Database type '{metadata.DatabaseType}' is not supported.")
        };
    }

    /// <summary>
    /// Creates a backup of the current database of <see cref="DatabaseFacade"/> to the specified location
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="path">The file path where the backup will be stored.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous backup operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> or <paramref name="path"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="path"/> is empty or whitespace.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown when the directory specified in <paramref name="path"/> does not exist.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when access to the backup file location is denied.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set or backup operation fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for backup operation.</exception>
    /// <remarks>
    /// This method supports SQL Server and PostgreSQL database providers. The backup operation is provider-specific:
    /// <list type="bullet">
    /// <item><description>SQL Server: Uses BACKUP DATABASE command to create a .bak file.</description></item>
    /// <item><description>PostgreSQL: Uses pg_dump command-line utility to create a dump file.</description></item>
    /// </list>
    /// </remarks>
    public static Task Backup(this DatabaseFacade facade, string path, CancellationToken ct = default) =>
        facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.Backup(facade, path, ct),
            Provider.Type.PostgreSql => PostgreSql.Backup(facade, path, ct),
            _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported.")
        };

    /// <summary>
    /// This operation will permanently delete the entire database and all its contents of the current <see cref="DatabaseFacade"/> instance.
    /// After this operation, the database context will be switched to the system database.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous drop operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set or drop operation fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for drop operation.</exception>
    /// <remarks>
    /// This method supports SQL Server and PostgreSQL database providers. The drop operation is provider-specific and will permanently delete the entire database.
    /// <para><strong>Warning:</strong> This operation is irreversible and will result in complete data loss.</para>
    /// <list type="bullet">
    /// <item><description>SQL Server: Sets database to single-user mode before dropping to force disconnect all users.</description></item>
    /// <item><description>PostgreSQL: Terminates all active connections before dropping the database.</description></item>
    /// </list>
    /// </remarks>
    public static Task Drop(this DatabaseFacade facade, CancellationToken ct = default) =>
        facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.Drop(facade, ct),
            Provider.Type.PostgreSql => PostgreSql.Drop(facade, ct),
            _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for drop operation.")
        };

    /// <summary>
    /// Truncates logs and dependent tables for the current <see cref="DatabaseFacade"/> instance.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the number of rows affected by the truncate operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the truncate operation fails.</exception>
    /// <remarks>
    /// This method executes a predefined SQL script to truncate log tables and their dependent tables. 
    /// The operation removes all records from the specified tables but preserves the table structure.
    /// This is useful for clearing historical data while maintaining database schema integrity.
    /// </remarks>
    public static Task<int> TruncateLogs(this DatabaseFacade facade, CancellationToken ct = default) =>
        facade.ExecuteSqlScriptAsync(SQLScripts.TRUNCATE_LOGS, cToken: ct);

    /// <summary>
    /// Retrieves the names of all non-system databases from the server.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a collection of database names.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for retrieving non-system database names.</exception>
    /// <remarks>
    /// This method supports SQL Server and PostgreSQL database providers. It filters out system databases to return only user-created databases.
    /// </remarks>
    public static Task<IEnumerable<string>> GetNonSystemDbNames(this DatabaseFacade facade, CancellationToken ct = default) =>
        facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.GetNonSystemDbNames(facade, ct),
            Provider.Type.PostgreSql => PostgreSql.GetNonSystemDbNames(facade, ct),
            _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for retrieving non-system database names.")
        };

    /// <summary>
    /// Ensures that a database login with the specified name exists.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="name">The name of the login to ensure exists.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> or <paramref name="name"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set or the operation fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for this operation.</exception>
    /// <remarks>
    /// This method supports SQL Server and PostgreSQL database providers. It creates the login if it doesn't already exist.
    /// </remarks>
    public static Task EnsureLoginExists(this DatabaseFacade facade, string name, CancellationToken ct = default) =>
        facade.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.EnsureLoginExists(facade, name, ct),
            Provider.Type.PostgreSql => PostgreSql.EnsureLoginExists(facade, name, ct),
            _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for ensuring login existence.")
        };
}

