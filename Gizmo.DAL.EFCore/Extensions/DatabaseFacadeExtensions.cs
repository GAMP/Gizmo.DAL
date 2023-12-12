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
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// Checks if specified table exists in the database.
        /// </summary>
        /// <param name="databaseFacade">Facade.</param>
        /// <param name="tableName">Table name.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True or false.</returns>
        public static async Task<bool> TableExistsAsync(this DatabaseFacade databaseFacade, string tableName, CancellationToken cancellationToken = default)
        {
            SqlParameter tableNameParameter = new("tableName", tableName);

            if(databaseFacade.IsSqlServer())
            {
                string tableExistQuery = """
                        IF (EXISTS (SELECT *
                           FROM INFORMATION_SCHEMA.TABLES
                           WHERE TABLE_SCHEMA = 'dbo'
                           AND TABLE_NAME = @tableName))
                           BEGIN
                              SELECT CAST(1 AS BIT)
                           END;
                        ELSE
                           BEGIN
                              SELECT CAST(0 AS BIT)
                           END;
                        """;

                return (await databaseFacade.SqlQueryRaw<bool>(tableExistQuery,tableNameParameter)
                    .ToListAsync(cancellationToken)).Single();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Checks if EF6 migration exists.
        /// </summary>
        /// <param name="databaseFacade">Facade.</param>
        /// <param name="migrationId">Migration name.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if migration exists, false if migration table or migration id does not exist.</returns>
        public static async Task<bool> MigrationExistAsync(this DatabaseFacade databaseFacade, string migrationId, CancellationToken cancellationToken = default)
        {
            if(!await TableExistsAsync(databaseFacade, "__MigrationHistory",cancellationToken))
                return false;

            if (databaseFacade.IsSqlServer())
            {
                SqlParameter migrationIdParameter = new("migrationId", migrationId);

                string queryString = """"
                IF (EXISTS (SELECT *
                   FROM __MigrationHistory WHERE MigrationId = @migrationId))
                   BEGIN
                      SELECT CAST(1 AS BIT)
                   END;
                ELSE
                   BEGIN
                      SELECT CAST(0 AS BIT)
                   END;
                """";

                return (await databaseFacade.SqlQueryRaw<bool>(queryString, migrationIdParameter)
                   .ToListAsync(cancellationToken)).Single();
            }
            else
            {
                throw new NotSupportedException(); 
            }
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
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        public static int ExecuteSqlScript(this DatabaseFacade dbFacade, string scriptName, params SqlParameter[] sqlParameters)
            => dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRaw(MsSqlScripts.GetScript(scriptName), sqlParameters),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRaw(NpgSqlScripts.GetScript(scriptName), sqlParameters),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

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
        public static Task<int> ExecuteSqlScriptAsync(this DatabaseFacade dbFacade, string scriptName, IEnumerable<SqlParameter> sqlParameters = null, CancellationToken cToken = default)
            => dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRawAsync(MsSqlScripts.GetScript(scriptName), sqlParameters, cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRawAsync(NpgSqlScripts.GetScript(scriptName), sqlParameters, cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };
    }
}
