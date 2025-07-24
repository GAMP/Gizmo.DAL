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

        public string ToConnectionString()
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