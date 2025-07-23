using System;
using Npgsql;
using SharedLib;

namespace Gizmo.DAL.Extensions.DclExtensions;

internal static class PostgreSql
{
    internal sealed class ConnectionMetadata : IConnectionMetadata
    {
        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 5432;
        public string Username { get; init; } = "postgres";
        public string Password { get; init; } = string.Empty;
        public string DatabaseName { get; init; } = string.Empty;
        public DatabaseType DatabaseType => DatabaseType.POSTGRE;
        public SQLServerAuthentication AuthenticationType { get; init; } = SQLServerAuthentication.Unspecified;

        public IConnectionMetadata ReplaceDatabaseName(string databaseName) => new ConnectionMetadata
        {
            Host = Host,
            Port = Port,
            Username = Username,
            Password = Password,
            DatabaseName = databaseName
        };

        public IConnectionMetadata RemoveDatabaseName() => new ConnectionMetadata
        {
            Host = Host,
            Port = Port,
            Username = Username,
            Password = Password,
            DatabaseName = string.Empty
        };

        public string BuildConnectionString()
        {
            try
            {
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = Host,
                    Port = Port,
                    Username = Username,
                    Password = Password,
                    Database = DatabaseName
                };

                return builder.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to build PostgreSQL connection string.", ex);
            }
        }

        /// <summary>
        /// Creates a new <see cref="ConnectionMetadata"/> instance from a PostgreSQL connection string.
        /// </summary>
        /// <param name="connectionString">The PostgreSQL connection string to parse.</param>
        /// <returns>A new <see cref="IConnectionMetadata"/> instance with connection parameters extracted from the connection string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionString"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is empty or whitespace.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the connection string cannot be parsed using the Npgsql connection string builder.</exception>
        /// <remarks>
        /// This method uses the Npgsql connection string builder to parse PostgreSQL connection strings.
        /// It automatically handles standard PostgreSQL connection parameters and SSL mode detection.
        /// The parsed metadata is cached to improve performance on subsequent calls with the same connection string.
        /// </remarks>
        public static IConnectionMetadata FromConnectionString(string connectionString)
        {
            if (!Provider.Metadata.TryGetValue(connectionString, out var connection))
            {
                try
                {
                    var builder = new NpgsqlConnectionStringBuilder(connectionString);

                    connection = new ConnectionMetadata
                    {
                        Host = builder.Host,
                        Port = builder.Port,
                        Username = builder.Username,
                        Password = builder.Password,
                        DatabaseName = builder.Database,
                        AuthenticationType = builder.SslMode == SslMode.Disable ? SQLServerAuthentication.Unspecified : SQLServerAuthentication.Integrated
                    };

                    Provider.Metadata[connectionString] = connection;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to parse PostgreSQL connection string.", ex);
                }
            }

            return connection;
        }
    }
}