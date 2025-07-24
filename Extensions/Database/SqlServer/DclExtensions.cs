using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using SharedLib;

namespace Gizmo.DAL.Extensions.DclExtensions;

internal static class SqlServer
{
    public sealed class ConnectionMetadata : IConnectionMetadata
    {
        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 1433;
        public string DatabaseName { get; init; } = string.Empty;
        public string Username { get; init; } = null;
        public string Password { get; init; } = null;
        public DatabaseType DatabaseType { get; init; } = DatabaseType.MSSQL;
        public SQLServerAuthentication AuthenticationType { get; init; } = SQLServerAuthentication.Integrated;

        public string ToConnectionString()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = $"{Host},{Port}",
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
                    var dbType = DatabaseType.MSSQL;
                    var host = "localhost";
                    var port = 1433;

                    var dataSource = builder.DataSource ?? "";
                    var dataSourceLower = dataSource.ToLowerInvariant();

                    if (dataSourceLower.Contains("(localdb)"))
                    {
                        dbType = DatabaseType.LOCALDB;
                        host = dataSource; // Keep full identifier for LocalDB
                        port = 0; // LocalDB doesn't use TCP/IP ports
                    }
                    else if (dataSourceLower.Contains("sqlexpress") || dataSource.Contains("\\SQLEXPRESS"))
                    {
                        dbType = DatabaseType.MSSQLEXPRESS;
                        
                        // Parse host from named instance (ServerName\SQLEXPRESS)
                        if (dataSource.Contains('\\'))
                        {
                            host = dataSource.Split('\\')[0];
                            if (string.IsNullOrEmpty(host))
                                host = "localhost";
                        }
                        else if (dataSource.Contains(','))
                        {
                            host = dataSource.Split(',')[0];
                            port = int.TryParse(dataSource.Split(',').LastOrDefault(), out int p) ? p : 1433;
                        }
                        else
                        {
                            host = dataSource;
                        }
                    }
                    else if (dataSource.Contains(','))
                    {
                        host = dataSource.Split(',')[0];
                        port = int.TryParse(dataSource.Split(',').LastOrDefault(), out int p) ? p : 1433;
                    }
                    else if (!string.IsNullOrEmpty(dataSource))
                    {
                        host = dataSource;
                    }

                    connection = new ConnectionMetadata
                    {
                        Host = host,
                        Port = port,
                        AuthenticationType = builder.IntegratedSecurity ? SQLServerAuthentication.Integrated : SQLServerAuthentication.Unspecified,
                        Username = builder.UserID,
                        Password = builder.Password,
                        DatabaseName = builder.InitialCatalog,
                        DatabaseType = dbType
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
    }
}
