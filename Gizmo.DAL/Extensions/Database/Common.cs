using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SharedLib;

namespace Gizmo.DAL.Extensions;

internal static class Provider
{
    public enum Type
    {
        SqlServer,
        PostgreSql,
        MySql,
        Sqlite,
    }

    public static readonly Dictionary<string, IConnectionMetadata> Metadata = [];

    private static readonly Dictionary<string, Type> Providers = new()
    {
        ["Microsoft.EntityFrameworkCore.SqlServer"] = Type.SqlServer,
        ["Npgsql.EntityFrameworkCore.PostgreSQL"] = Type.PostgreSql,
        ["Pomelo.EntityFrameworkCore.MySql"] = Type.MySql,
        ["Microsoft.EntityFrameworkCore.Sqlite"] = Type.Sqlite
    };
    
    /// <summary>
    /// Gets the provider type from a database facade.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> to examine.</param>
    /// <returns>The <see cref="Type"/> corresponding to the database provider.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the database provider name is not set.</exception>
    /// <exception cref="NotSupportedException">Thrown when the provider is not supported.</exception>
    public static Type GetProviderType(this DatabaseFacade facade)
    {
        if (string.IsNullOrEmpty(facade.ProviderName))
            throw new InvalidOperationException("Database provider name is not set.");

        return Providers.TryGetValue(facade.ProviderName, out var provider)
            ? provider
            : throw new NotSupportedException($"Provider '{facade.ProviderName}' is not supported for the current operation.");
    }
}

/// <summary>
/// Represents a database connection configuration with methods for connection string management.
/// </summary>
/// <remarks>
/// This interface defines the contract for database connection objects that can be used across different database providers.
/// It provides connection parameters, authentication details, and utility methods for connection string manipulation.
/// </remarks>
public interface IConnectionMetadata
{
    /// <summary>
    /// Gets the database server host name or IP address.
    /// </summary>
    /// <value>The host name or IP address of the database server. Default is typically "localhost".</value>
    string Host { get; }

    /// <summary>
    /// Gets the database server port number.
    /// </summary>
    /// <value>The port number used to connect to the database server. If null, the provider will use its default port or instance resolution.</value>
    int? Port { get; }

    /// <summary>
    /// Gets the username for database authentication.
    /// </summary>
    /// <value>The username used for connecting to the database. May be null for integrated authentication scenarios.</value>
    string Username { get; }

    /// <summary>
    /// Gets the password for database authentication.
    /// </summary>
    /// <value>The password used for connecting to the database. May be null for integrated authentication scenarios.</value>
    string Password { get; }

    /// <summary>
    /// Gets the name of the target database.
    /// </summary>
    /// <value>The name of the database to connect to. May be empty for connections to the default database.</value>
    string DatabaseName { get; }

    /// <summary>
    /// Gets the authentication type used for database connection.
    /// </summary>
    /// <value>The authentication method (Integrated, SQL Server, or Unspecified) used for the connection.</value>
    SQLServerAuthentication AuthenticationType { get; }

    /// <summary>
    /// Gets the type of database system being used.
    /// </summary>
    /// <value>The database type (such as SQLServer, PostgreSQL, MySQL, etc.) that this connection represents.</value>
    DatabaseType DatabaseType { get; }

    /// <summary>
    /// Builds a connection string using the current connection parameters.
    /// </summary>
    /// <returns>A properly formatted connection string for the database provider.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the connection string cannot be built with the current parameters.</exception>
    /// <remarks>
    /// The format of the returned connection string depends on the specific database provider implementation.
    /// </remarks>
    string ToConnectionString();

    /// <summary>
    /// Creates a new connection metadata instance with a different database name.
    /// </summary>
    /// <param name="databaseName">The name of the database to use in the new connection.</param>
    /// <returns>A new <see cref="IConnectionMetadata"/> instance with the updated database name.</returns>
    IConnectionMetadata ChangeDatabaseTo(string databaseName);
}
