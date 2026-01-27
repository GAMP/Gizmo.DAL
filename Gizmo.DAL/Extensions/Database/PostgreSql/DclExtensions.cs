using System;
using Npgsql;

namespace Gizmo.DAL.Extensions.DclExtensions;

internal static class PostgreSql
{
    internal sealed class ConnectionMetadata : IConnectionMetadata
    {
        public string Host { get; init; } = "localhost";
        public int? Port { get; init; } = null;
        public string Username { get; init; } = "postgres";
        public string Password { get; init; } = string.Empty;
        public string DatabaseName { get; init; } = string.Empty;
        public string BackupExtension { get; init; } = ".dump";
        public DatabaseType DatabaseType => DatabaseType.POSTGRE;
        public SQLServerAuthentication AuthenticationType { get; init; } = SQLServerAuthentication.Server;

        public string ToConnectionString()
        {
            try
            {
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = Host,
                    Username = Username,
                    Password = Password,
                    Database = DatabaseName
                };

                // When Port is null → uses default 5432
                if (Port.HasValue)
                    builder.Port = Port.Value;

                return builder.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to build PostgreSQL connection string.", ex);
            }
        }

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
                        AuthenticationType = SQLServerAuthentication.Server // PostgreSQL will use only standard authentication for us
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

        public IConnectionMetadata ChangeDatabaseTo(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException("Database name cannot be null or empty.", nameof(databaseName));
            }

            return new ConnectionMetadata
            {
                Host = Host,
                Port = Port,
                Username = Username,
                Password = Password,
                DatabaseName = databaseName,
                AuthenticationType = AuthenticationType
            };
        }
    }
}
