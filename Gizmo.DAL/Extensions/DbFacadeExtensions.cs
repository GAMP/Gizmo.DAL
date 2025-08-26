using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Scripts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions
{
    /// <summary>
    /// Database facade extensions.
    /// </summary>
    public static class DbFacadeExtensions
    {
        /// <summary>
        /// Executes the SQL against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// </summary>
        /// <param name="dbFacade">
        /// Provides access to database related information and operations for a context.
        /// </param>
        /// <param name="scriptName">
        /// SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        public static int ExecuteSqlScript(this DatabaseFacade dbFacade, string scriptName, Dictionary<string, object> parameters)
        {
            var result = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRaw(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRaw(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            return result == -1
                ? throw new InvalidOperationException($"Error executing sql script {scriptName}.")
                : result;
        }

        /// <summary>
        /// Executes the SQL against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// </summary>
        /// <param name="dbFacade">
        /// Provides access to database related information and operations for a context.
        /// </param>
        /// <param name="scriptName">
        /// SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Task that represents the asynchronous operation with the number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        public static async Task<int> ExecuteSqlScriptAsync(this DatabaseFacade dbFacade, string scriptName, Dictionary<string, object> parameters = null, CancellationToken cToken = default)
        {
            var (script, sqlParameters) = dbFacade.GetProviderType() switch
            {
                Provider.Type.SqlServer =>
                    (MsSqlScripts.GetScript(scriptName),
                    parameters is null or { Count: 0 }
                        ? Enumerable.Empty<IDbDataParameter>()
                        : parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                Provider.Type.PostgreSql =>
                    (NpgSqlScripts.GetScript(scriptName),
                    parameters is null or { Count: 0 }
                        ? Enumerable.Empty<IDbDataParameter>()
                        : parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            var result = await dbFacade.ExecuteSqlRawAsync(script, sqlParameters, cToken);

            return result == -1
                ? throw new InvalidOperationException($"Error executing sql script {scriptName}.")
                : result;
        }

        /// <summary>
        /// Executes the SQL against the database to delete all rows from the table.
        /// </summary>
        /// <param name="dbFacade">
        /// Provides access to database related information and operations for a context.
        /// </param>
        /// <param name="tableName">
        /// Table name.
        /// </param>
        /// <param name="withReseed">
        /// If true, reseed identity column to 1.
        /// </param>
        /// <param name="where">
        /// Sql where clause. Key is column name, value is column value.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Error executing sql script to delete from table.
        /// </exception>
        public static async Task<int> DeleteFromAsync(this DatabaseFacade dbFacade, string tableName, bool withReseed, Dictionary<string, string> where = null, CancellationToken cToken = default)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

            var providerType = dbFacade.GetProviderType();

            var whereClause = BuildDictionaryClause(providerType, where, "AND");

            var sql = providerType switch
            {
                Provider.Type.SqlServer => withReseed
                    ? whereClause.Length > 0
                        ? $"DELETE FROM [dbo].[{tableName}] WHERE {whereClause}; DBCC CHECKIDENT ('{tableName}', RESEED, 1);"
                        : $"DELETE FROM [dbo].[{tableName}]; DBCC CHECKIDENT ('{tableName}', RESEED, 1);"
                    : whereClause.Length > 0
                        ? $"DELETE FROM [dbo].[{tableName}] WHERE {whereClause};"
                        : $"DELETE FROM [dbo].[{tableName}];",
                Provider.Type.PostgreSql => withReseed
                    ? whereClause.Length > 0
                        ? $"DELETE FROM \"{tableName}\" WHERE {whereClause}; ALTER SEQUENCE \"{tableName}_{tableName}Id_seq\" RESTART WITH 1;"
                        : $"DELETE FROM \"{tableName}\"; ALTER SEQUENCE \"{tableName}_{tableName}Id_seq\" RESTART WITH 1;"
                    : whereClause.Length > 0
                        ? $"DELETE FROM \"{tableName}\" WHERE {whereClause};"
                        : $"DELETE FROM \"{tableName}\";",
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            var result = await dbFacade.ExecuteSqlRawAsync(sql, cToken);

            return result == -1
               ? throw new InvalidOperationException($"Error executing sql script to delete from {tableName}.")
               : result;
        }

        /// <summary>
        /// Executes the SQL against the database to update rows from the table.
        /// </summary>
        /// <param name="dbFacade">
        /// Provides access to database related information and operations for a context.
        /// </param>
        /// <param name="tableName">
        /// Table name.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <param name="where">
        /// Sql where clause. Key is column name, value is column value.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Error executing sql script to update table.
        /// </exception>
        public static async Task<int> UpdateAsync(this DatabaseFacade dbFacade, string tableName, IDictionary<string, string> parameters, Dictionary<string, string> where = null, CancellationToken cToken = default)
        {
            if (parameters is null || parameters.Count == 0)
                throw new ArgumentException("Parameters cannot be null or empty.", nameof(parameters));

            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

            var providerType = dbFacade.GetProviderType();

            var setClause = BuildDictionaryClause(providerType, parameters, ",");
            var whereClause = BuildDictionaryClause(providerType, where, "AND");

            var sql = providerType switch
            {
                Provider.Type.SqlServer => whereClause.Length > 0
                    ? $"UPDATE [dbo].[{tableName}] SET {setClause} WHERE {whereClause};"
                    : $"UPDATE [dbo].[{tableName}] SET {setClause};",
                Provider.Type.PostgreSql => whereClause.Length > 0
                    ? $"UPDATE \"{tableName}\" SET {setClause} WHERE {whereClause};"
                    : $"UPDATE \"{tableName}\" SET {setClause};",
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            var result = await dbFacade.ExecuteSqlRawAsync(sql, cToken);

            return result == -1
               ? throw new InvalidOperationException($"Error executing sql script to update {tableName}.")
               : result;
        }

        private static string BuildDictionaryClause(Provider.Type providerType, IDictionary<string, string> items, string joiner)
        {
            if (items is null || items.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in items)
            {
                if (sb.Length > 0)
                {
                    sb.Append(' ');
                    sb.Append(joiner);
                    sb.Append(' ');
                }

                sb.Append(providerType switch
                {
                    Provider.Type.SqlServer => string.IsNullOrWhiteSpace(item.Value)
                        ? $"[{item.Key}] = NULL"
                        : $"[{item.Key}] = {item.Value}",
                    Provider.Type.PostgreSql => string.IsNullOrWhiteSpace(item.Value)
                        ? $"\"{item.Key}\" = NULL"
                        : $"\"{item.Key}\" = {item.Value}",
                    _ => throw new NotSupportedException($"Database provider {providerType} is not supported for this sql command."),
                });
            }

            return sb.ToString();
        }
    }
}
