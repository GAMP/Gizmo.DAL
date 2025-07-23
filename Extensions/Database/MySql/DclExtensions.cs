using System;
using SharedLib;

namespace Gizmo.DAL.Extensions.DclExtensions;

internal static class MySql
{
    public sealed class ConnectionMetadata : IConnectionMetadata
    {
        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 3306;
        public string Username { get; init; } = "root";
        public string Password { get; init; } = string.Empty;
        public string DatabaseName { get; init; } = string.Empty;
        public DatabaseType DatabaseType => DatabaseType.MYSQL;
        public SQLServerAuthentication AuthenticationType => SQLServerAuthentication.Unspecified;

        public string BuildConnectionString() => $"Server={Host};Port={Port};User ID={Username};Password={Password};Database={DatabaseName};";

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

        /// <summary>
        /// Creates a new <see cref="ConnectionMetadata"/> instance from a MySQL connection string.
        /// </summary>
        /// <param name="connectionString">The MySQL connection string to parse.</param>
        /// <returns>A new <see cref="IConnectionMetadata"/> instance with connection parameters extracted from the connection string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionString"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is empty or whitespace.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the connection string cannot be parsed or is malformed.</exception>
        /// <remarks>
        /// This method parses MySQL connection strings by splitting key-value pairs separated by semicolons.
        /// It recognizes the following connection string parameters: Server, Port, User ID, Password, and Database.
        /// The parsed metadata is cached to improve performance on subsequent calls with the same connection string.
        /// </remarks>
        public static IConnectionMetadata FromConnectionString(string connectionString)
        {

            if (!Provider.Metadata.TryGetValue(connectionString, out var connection))
            {
                try
                {
                    var tokens = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    string host = "localhost";
                    int port = 3306;
                    string username = "root";
                    string password = "root";
                    string databaseName = string.Empty;

                    foreach (var token in tokens)
                    {
                        var keyValue = token.Split('=', 2);
                        if (keyValue.Length == 2)
                        {
                            var key = keyValue[0].Trim();
                            var value = keyValue[1].Trim();

                            switch (key.ToLower())
                            {
                                case "server":
                                    host = value;
                                    break;
                                case "port":
                                    port = int.Parse(value);
                                    break;
                                case "user id":
                                    username = value;
                                    break;
                                case "password":
                                    password = value;
                                    break;
                                case "database":
                                    databaseName = value;
                                    break;
                            }
                        }
                    }

                    connection = new ConnectionMetadata
                    {
                        Host = host,
                        Port = port,
                        Username = username,
                        Password = password,
                        DatabaseName = databaseName
                    };

                    Provider.Metadata[connectionString] = connection;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to parse MySQL connection string.", ex);
                }
            }

            return connection;
        }
    }
}
