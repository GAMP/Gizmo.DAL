using System;
using Gizmo.DAL.Extensions.DclExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions;

/// <summary>
/// Provides extension methods for Data Control Language (DCL) operations.
/// </summary>
/// <remarks>
/// DCL commands are used to control access to data in the database. Typical DCL commands include:
/// <list type="bullet">
///   <item><description><c>GRANT</c> – Gives a privilege to user.</description></item>
///   <item><description><c>REVOKE</c> – Withdraws a privilege from user.</description></item>
/// </list>
/// </remarks>
public static class DclOperations
{
    /// <summary>
    /// Creates connection metadata for the specified database provider using individual connection parameters.
    /// </summary>
    /// <param name="type">The type of database provider (SQL Server, PostgreSQL, MySQL, etc.).</param>
    /// <param name="host">The host name or IP address of the database server.</param>
    /// <param name="port">The port number of the database server. If null, the provider will use its default port or instance resolution.</param>
    /// <param name="databaseName">The name of the database to connect to.</param>
    /// <param name="username">The username for authentication. Optional for integrated authentication scenarios.</param>
    /// <param name="password">The password for authentication. Optional for integrated authentication scenarios.</param>
    /// <param name="authentication">The authentication type for SQL Server connections. Defaults to <see cref="SQLServerAuthentication.Unspecified"/>.</param>
    /// <returns>An <see cref="IConnectionMetadata"/> instance appropriate for the specified database provider.</returns>
    /// <exception cref="ArgumentNullException">Thrown when required parameters are null.</exception>
    /// <exception cref="ArgumentException">Thrown when required parameters are invalid.</exception>
    /// <exception cref="NotSupportedException">Thrown when the specified database type is not supported.</exception>
    /// <remarks>
    /// This method creates provider-specific connection metadata objects for SQL Server (including LocalDB and Express), 
    /// PostgreSQL, and MySQL databases. The authentication parameter is only applicable to SQL Server variants.
    /// </remarks>
    public static IConnectionMetadata CreateConnectionMetadata(DatabaseType type,
            string host,
            int? port,
            string databaseName,
            string username = null,
            string password = null,
            SQLServerAuthentication authentication = SQLServerAuthentication.Unspecified) => type switch
            {
                DatabaseType.MSSQL or
                DatabaseType.LOCALDB or
                DatabaseType.MSSQLEXPRESS => new SqlServer.ConnectionMetadata
                {
                    Host = host,
                    Port = port,
                    DatabaseName = databaseName,
                    DatabaseType = type,
                    Username = username,
                    Password = password,
                    AuthenticationType = authentication
                },
                DatabaseType.POSTGRE => new PostgreSql.ConnectionMetadata
                {
                    Host = host,
                    Port = port,
                    DatabaseName = databaseName,
                    Username = username,
                    Password = password
                },
                DatabaseType.MYSQL => new MySql.ConnectionMetadata
                {
                    Host = host,
                    Port = port,
                    DatabaseName = databaseName,
                    Username = username,
                    Password = password
                },
                _ => throw new NotSupportedException($"Database type '{type}' is not supported for creating connection metadata.")
            };

    /// <summary>
    /// Creates connection metadata for the specified database provider using a connection string.
    /// </summary>
    /// <param name="type">The type of database provider (SQL Server, PostgreSQL, MySQL, etc.).</param>
    /// <param name="connectionString">The connection string containing all necessary connection information including host, port, database name, and authentication details.</param>
    /// <returns>An <see cref="IConnectionMetadata"/> instance appropriate for the specified database provider, parsed from the connection string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionString"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is empty, whitespace, or malformed.</exception>
    /// <exception cref="NotSupportedException">Thrown when the specified database type is not supported.</exception>
    /// <remarks>
    /// This method parses an existing connection string to extract connection parameters and creates appropriate 
    /// provider-specific metadata objects. Supports SQL Server (including LocalDB and Express), PostgreSQL, and MySQL databases.
    /// </remarks>
    public static IConnectionMetadata CreateConnectionMetadata(DatabaseType type, string connectionString) => type switch
    {
        DatabaseType.MSSQL or
        DatabaseType.LOCALDB or
        DatabaseType.MSSQLEXPRESS => SqlServer.ConnectionMetadata.FromConnectionString(connectionString),
        DatabaseType.POSTGRE => PostgreSql.ConnectionMetadata.FromConnectionString(connectionString),
        DatabaseType.MYSQL => MySql.ConnectionMetadata.FromConnectionString(connectionString),
        _ => throw new NotSupportedException($"Database type '{type}' is not supported for creating connection metadata.")
    };

    /// <summary>
    /// Gets the connection metadata from the database facade.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance to extract connection metadata from.</param>
    /// <returns>An <see cref="IConnectionMetadata"/> instance containing the connection details for the database provider.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="facade"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set or connection string is invalid.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for metadata extraction.</exception>
    /// <remarks>
    /// This method extracts connection information from an active Entity Framework database context and 
    /// returns provider-specific metadata. Supports SQL Server, PostgreSQL, and MySQL providers.
    /// </remarks>
    public static IConnectionMetadata GetConnectionMetadata(this DatabaseFacade facade) => facade.GetProviderType() switch
    {
        Provider.Type.SqlServer => SqlServer.ConnectionMetadata.FromConnectionString(facade.GetConnectionString()),
        Provider.Type.PostgreSql => PostgreSql.ConnectionMetadata.FromConnectionString(facade.GetConnectionString()),
        Provider.Type.MySql => MySql.ConnectionMetadata.FromConnectionString(facade.GetConnectionString()),
        _ => throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for checking connection metadata.")
    };
}
