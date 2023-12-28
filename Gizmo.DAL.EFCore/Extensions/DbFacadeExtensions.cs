using Gizmo.DAL.Scripts;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gizmo.DAL.EFCore.Extensions
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
        public static int ExecuteSqlScript(this DatabaseFacade dbFacade, string scriptName, IDictionary<string, object> parameters)
        {
            var result =  dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRaw(
                    MsSqlScripts.GetScript(scriptName), 
                    parameters.Select(x => new SqlParameter(x.Key, x.Value))),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRaw(
                    NpgSqlScripts.GetScript(scriptName), 
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value)).ToArray()),
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
        public static async Task<int> ExecuteSqlScriptAsync(this DatabaseFacade dbFacade, string scriptName, IDictionary<string, object> parameters = null, CancellationToken cToken = default)
        {
            var result = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => await dbFacade.ExecuteSqlRawAsync(
                    MsSqlScripts.GetScript(scriptName), 
                    parameters.Select(x => new SqlParameter(x.Key, x.Value)),
                    cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => await dbFacade.ExecuteSqlRawAsync(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters?.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value)).ToArray(),
                    cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

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
        public static async Task<int> DeleteFromAsync(this DatabaseFacade dbFacade, string tableName, bool withReseed, CancellationToken cToken)
        {
            var result = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => withReseed
                    ? await dbFacade.ExecuteSqlRawAsync($"DELETE FROM [dbo].[{tableName}]; DBCC CHECKIDENT ({tableName}, RESEED, 1);",cToken)
                    : await dbFacade.ExecuteSqlRawAsync($"DELETE FROM [dbo].[{tableName}];", cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => withReseed 
                    ? await dbFacade.ExecuteSqlRawAsync($"DELETE FROM \"{tableName}\"; ALTER SEQUENCE \"{tableName}_{tableName}Id_seq\" RESTART;",cToken)
                    : await dbFacade.ExecuteSqlRawAsync($"DELETE FROM \"{tableName}\";", cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            return result == -1
               ? throw new InvalidOperationException($"Error executing sql script to delete from {tableName}.")
               : result;
        }
    }
}
