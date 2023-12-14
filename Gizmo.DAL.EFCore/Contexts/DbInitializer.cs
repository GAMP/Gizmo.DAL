using Gizmo.DAL.EFCore.Extensions;
using Gizmo.DAL.Scripts;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Database initializer.
    /// </summary>
    public sealed class DbInitializer
    {
        private readonly DefaultDbContext _dbContext;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        public DbInitializer(DefaultDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        /// Initialize database.
        /// </summary>
        /// <param name="cancellationToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                var isMigrated = await TryMigrateToEF6InitialAsync(cancellationToken);

                var appliedMigrations = await _dbContext.Database.GetAppliedMigrationsAsync(cancellationToken);
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                pendingMigrations = isMigrated 
                    ? pendingMigrations.Skip(1) 
                    : pendingMigrations;

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken);

                if (!appliedMigrations.Any())
                    await _dbContext.AddSeedDataAsync(cancellationToken);
            }
            else
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                else
                {
                    //working only with migrations
                    return;
                }

               await _dbContext.AddSeedDataAsync(cancellationToken);
            }
        }

        private async Task<bool> TryMigrateToEF6InitialAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Database.IsSqlServer())
            {
                var hasEF6MigrationHistoryTable = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.HAS_TABLE_BY_NAME, new SqlParameter[] 
                { 
                    new ("name", "__EFMigrationsHistory") 
                }, cancellationToken) == 1;

                if (hasEF6MigrationHistoryTable)
                {
                    var hasEFCoreMigrationHistoryTable = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.HAS_TABLE_BY_NAME, new SqlParameter[]
                    {
                        new ("name", "__EFMigrationsHistory") 
                    }, cancellationToken) == 1;

                    if (!hasEFCoreMigrationHistoryTable)
                    {
                        var hasEF6Migration_Update17 = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.HAS_EF6_MIGRATION_BY_MIGRATIONID, new SqlParameter[]
                        {
                            new ("migrationId", "202309121624325_Update17") 
                        }, cancellationToken) == 1;

                        if (!hasEF6Migration_Update17)
                        {
                            throw new NotSupportedException("Current database version cannot be upgraded.");
                        }

                        using var migrationDbContext = _dbContext.WithEF6Migrations();

                        var pendingMigrations = await migrationDbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                        if (pendingMigrations.Count() == 1)
                        {
                            await migrationDbContext.Database.MigrateAsync(cancellationToken);

                            return true;
                        }
                        else
                            throw new NotSupportedException("Current database version cannot be upgraded.");
                    }
                }
            }

            return false;
        }
    }
}
