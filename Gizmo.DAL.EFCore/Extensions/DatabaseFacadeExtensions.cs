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
    }
}
