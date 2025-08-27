using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using SharedLib;

namespace Gizmo.DAL.Extensions.DclExtensions;

internal static class SqlServer
{
    private static readonly string[] LocalDbKeys = ["127.0.0.1", "localhost", "localdb"];
    private static readonly string[] ExpressKeys = ["sqlexpress", @"\SQLEXPRESS"];

    public sealed class ConnectionMetadata : IConnectionMetadata
    {
        public string Host { get; init; } = "localhost";
        public int? Port { get; init; } = null;
        public string DatabaseName { get; init; } = string.Empty;
        public string Username { get; init; }
        public string Password { get; init; }
        public DatabaseType DatabaseType { get; init; } = DatabaseType.MSSQL;
        public SQLServerAuthentication AuthenticationType { get; init; } = SQLServerAuthentication.Integrated;

        public string ToConnectionString()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = DatabaseType == DatabaseType.LOCALDB
                        ? Host 
                        : Port.HasValue
                            ? $"{Host},{Port.Value}" 
                            : Host,
                    InitialCatalog = DatabaseName,
                    IntegratedSecurity = AuthenticationType == SQLServerAuthentication.Integrated
                };

                if (!builder.IntegratedSecurity)
                {
                    if (Username is not null)
                        builder.UserID = Username;

                    if (Password is not null)
                        builder.Password = Password;
                }

                builder.TrustServerCertificate = true;

                return builder.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to build SQL Server connection string.", ex);
            }
        }

        public static IConnectionMetadata FromConnectionString(string connectionString)
        {
            if (!Provider.Metadata.TryGetValue(connectionString, out var connection))
            {
                var builder = new SqlConnectionStringBuilder(connectionString);

                try
                {
                    var dataSource = builder.DataSource ?? string.Empty;
                    var split = dataSource.Split(',');

                    var host = split[0];
                    var port = split.Length > 1 && int.TryParse(split[1], out int parsedPort)
                        ? (int?)parsedPort
                        : null;

                    var dbType = DatabaseType.MSSQL;

                    if (port is null && LocalDbKeys.Any(key => dataSource.Contains(key, StringComparison.OrdinalIgnoreCase)))
                    {
                        dbType = DatabaseType.LOCALDB;
                    }
                    else if (ExpressKeys.Any(key => dataSource.Contains(key, StringComparison.OrdinalIgnoreCase)))
                    {
                        dbType = DatabaseType.MSSQLEXPRESS;
                    }

                    connection = new ConnectionMetadata
                    {
                        Host = host,
                        Port = port,
                        AuthenticationType = builder.IntegratedSecurity ? SQLServerAuthentication.Integrated : SQLServerAuthentication.Unspecified,
                        Username = builder.UserID,
                        Password = builder.Password,
                        DatabaseName = builder.InitialCatalog,
                        DatabaseType = dbType,
                    };

                    Provider.Metadata[connectionString] = connection;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to parse SQL Server connection string.", ex);
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
                AuthenticationType = AuthenticationType,
                DatabaseType = DatabaseType
            };
        }
    }
}
