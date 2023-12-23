using Gizmo.DAL.Scripts;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Data.Common;
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
        /// <param name="sqlParameters">
        /// Sql parameters for the script.
        /// </param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        public static int ExecuteSqlScript(this DatabaseFacade dbFacade, string scriptName, params DbParameter[] sqlParameters)
        {
            var result =  dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRaw(MsSqlScripts.GetScript(scriptName), sqlParameters),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRaw(NpgSqlScripts.GetScript(scriptName), sqlParameters),
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
        /// <param name="sqlParameters">
        /// Sql parameters for the script.
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
        public static async Task<int> ExecuteSqlScriptAsync(this DatabaseFacade dbFacade, string scriptName, IEnumerable<DbParameter> sqlParameters = null, CancellationToken cToken = default)
        {
            var result = dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => await dbFacade.ExecuteSqlRawAsync(MsSqlScripts.GetScript(scriptName), sqlParameters, cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => await dbFacade.ExecuteSqlRawAsync(NpgSqlScripts.GetScript(scriptName), sqlParameters, cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

            return result == -1
                ? throw new InvalidOperationException($"Error executing sql script {scriptName}.")
                : result;
        }
    }
}
