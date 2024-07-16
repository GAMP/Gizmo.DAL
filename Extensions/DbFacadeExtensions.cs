using Gizmo.DAL.Scripts;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var result =  dbFacade.ProviderName switch
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
            var (script, sqlParameters) = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" =>
                    (MsSqlScripts.GetScript(scriptName),
                    parameters is null or { Count: 0 } 
                        ? Enumerable.Empty<object>()
                        : parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => 
                    (NpgSqlScripts.GetScript(scriptName),
                    parameters is null or { Count: 0 }
                        ? Enumerable.Empty<object>()
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
            var result = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => withReseed
                    ? where is not null && where.Count > 0
                        ? await dbFacade.ExecuteSqlAsync($"DELETE FROM [dbo].[{tableName}] WHERE {string.Join(" AND ", where.Select(x => $"{x.Key} = {x.Value}"))}; DBCC CHECKIDENT ('{tableName}', RESEED, 1);", cToken)
                        : await dbFacade.ExecuteSqlAsync($"DELETE FROM [dbo].[{tableName}]; DBCC CHECKIDENT ('{tableName}', RESEED, 1);", cToken)
                    : where is not null && where.Count > 0
                        ? await dbFacade.ExecuteSqlAsync($"DELETE FROM [dbo].[{tableName}] WHERE {string.Join(" AND ", where.Select(x => $"{x.Key} = {x.Value}"))};", cToken)
                        : await dbFacade.ExecuteSqlAsync($"DELETE FROM [dbo].[{tableName}];", cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => withReseed 
                    ? where is not null && where.Count > 0
                        ? await dbFacade.ExecuteSqlAsync($"DELETE FROM \"{tableName}\" WHERE {string.Join(" AND ", where.Select(x => $"\"{x.Key}\" = {x.Value}"))}; ALTER SEQUENCE \"{tableName}_{tableName}Id_seq\" RESTART;", cToken)
                        : await dbFacade.ExecuteSqlAsync($"DELETE FROM \"{tableName}\"; ALTER SEQUENCE \"{tableName}_{tableName}Id_seq\" RESTART;", cToken)
                    : where is not null && where.Count > 0
                        ? await dbFacade.ExecuteSqlAsync($"DELETE FROM \"{tableName}\" WHERE {string.Join(" AND ", where.Select(x => $"\"{x.Key}\" = {x.Value}"))};", cToken)
                        : await dbFacade.ExecuteSqlAsync($"DELETE FROM \"{tableName}\";", cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

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
            if(parameters is null || parameters.Count == 0)
                throw new ArgumentException("Parameters cannot be null or empty.", nameof(parameters));

            var result = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => where is not null && where.Count > 0
                    ? await dbFacade.ExecuteSqlAsync($"UPDATE [dbo].[{tableName}] SET {string.Join(", ", parameters.Select(x => $"{x.Key} = {x.Value}"))} WHERE {string.Join(" AND ", where.Select(x => $"{x.Key} = {x.Value}"))};", cToken)
                    : await dbFacade.ExecuteSqlAsync($"UPDATE [dbo].[{tableName}] SET {string.Join(", ", parameters.Select(x => $"{x.Key} = {x.Value}"))};", cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" =>  where is not null && where.Count > 0
                    ? await dbFacade.ExecuteSqlAsync($"UPDATE \"{tableName}\" SET {string.Join(", ", parameters.Select(x => $"\"{x.Key}\" = {x.Value}"))} WHERE {string.Join(" AND ", where.Select(x => $"\"{x.Key}\" = {x.Value}"))};", cToken)
                    : await dbFacade.ExecuteSqlAsync($"UPDATE \"{tableName}\" SET {string.Join(", ", parameters.Select(x => $"\"{x.Key}\" = {x.Value}"))};", cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            return result == -1
               ? throw new InvalidOperationException($"Error executing sql script to update {tableName}.")
               : result;
        }
    }
}
