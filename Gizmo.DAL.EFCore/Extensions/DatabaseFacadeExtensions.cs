using Gizmo.DAL.Scripts;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
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
        /// Executes sql script from file depending on the database provider.
        /// </summary>
        /// <param name="dbFacade">
        /// Database facade.
        /// </param>
        /// <param name="scriptName">
        /// Sql script name from the Gizmo.DAL.Scripts namespace.
        /// </param>
        /// <param name="sqlParameter">
        /// Sql parameter.
        /// </param>
        /// <returns>
        /// Number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this sql command.
        /// </exception>
        public static int ExecuteSqlScript(this DatabaseFacade dbFacade, string scriptName, SqlParameter sqlParameter = null)
            => dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRaw(MsSqlScripts.GetScript(scriptName), sqlParameter),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRaw(NpgSqlScripts.GetScript(scriptName), sqlParameter),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

        /// <summary>
        /// Executes sql script from file depending on the database provider.
        /// </summary>
        /// <param name="dbFacade">
        /// Database facade.
        /// </param>
        /// <param name="scriptName">
        /// Sql script name from the Gizmo.DAL.Scripts namespace.
        /// </param>
        /// <param name="sqlParameter">
        /// Sql parameter.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        ///  Task with number of rows affected.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this sql command.
        /// </exception>
        public static Task<int> ExecuteSqlScriptAsync(this DatabaseFacade dbFacade, string scriptName, SqlParameter sqlParameter = null, CancellationToken cToken = default)
            => dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbFacade.ExecuteSqlRawAsync(MsSqlScripts.GetScript(scriptName), sqlParameter, cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbFacade.ExecuteSqlRawAsync(NpgSqlScripts.GetScript(scriptName), sqlParameter, cToken),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };

        /// <summary>
        /// Executes sql script from file depending on the database provider.
        /// </summary>
        /// <typeparam name="T">
        /// Result type.
        /// </typeparam>
        /// <param name="dbFacade">
        /// Database facade.
        /// </param>
        /// <param name="dbSet">
        /// Database set.
        /// </param>
        /// <param name="scriptName">
        /// Sql script name from the Gizmo.DAL.Scripts namespace.
        /// </param>
        /// <param name="sqlParameter">
        /// Sql parameter.
        /// </param>
        /// <returns>
        /// Queryable.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this sql command.
        /// </exception>
        public static IQueryable<T> FromSqlScript<T>(this DatabaseFacade dbFacade, DbSet<T> dbSet, string scriptName, SqlParameter sqlParameter = null) where T : class
            => dbFacade.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbSet.FromSqlRaw(MsSqlScripts.GetScript(scriptName), sqlParameter),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbSet.FromSqlRaw(NpgSqlScripts.GetScript(scriptName), sqlParameter),
                _ => throw new NotSupportedException($"Database provider {dbFacade.ProviderName} is not supported for this sql command."),
            };
    }
}
