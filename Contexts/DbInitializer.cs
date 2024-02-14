using Gizmo.DAL.Extensions;
using Gizmo.DAL.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<DefaultDbContext> _logger;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <param name="logger">Logger.</param>
        public DbInitializer(DefaultDbContext dbContext, ILogger<DefaultDbContext> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

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
            _logger.LogTrace("Initializing database.");

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogTrace("Connected to existing database.");

                //we will only reach this code in case that database already exist, its state or version is not know at this stage

                //attempt to update ef6 database
                var isMigrated = await TryMigrateToEF6InitialAsync(cancellationToken);

                if(isMigrated)
                    _logger.LogTrace("Existing database was migrated from EF6.");

                //will conaint currently applied migrations count, zero will mean that this is initial database
                var appliedMigrations = await _dbContext.Database.GetAppliedMigrationsAsync(cancellationToken);

                //gets currently pending migrations
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                //any pending migration should be applied
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken);

                if (!appliedMigrations.Any())
                    await _dbContext.AddSeedDataAsync(cancellationToken);
            }
            else
            {
                _logger.LogTrace("Connected to new database.");

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
                var hasEF6MigrationHistoryTable = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.HAS_TABLE_BY_NAME, new Dictionary<string, object> 
                {
                    { "name", "__MigrationHistory" } 
                }, cancellationToken) == 1;

                if (hasEF6MigrationHistoryTable)
                {
                    var hasEFCoreMigrationHistoryTable = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.HAS_TABLE_BY_NAME, new Dictionary<string, object>
                    {
                        { "name", "__EFMigrationsHistory" }
                    }, cancellationToken) == 1;

                    if (!hasEFCoreMigrationHistoryTable)
                    {
                        var hasEF6Migration_Update17 = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.HAS_EF6_MIGRATION_BY_MIGRATIONID, new Dictionary<string, object>
                        {
                            { "migrationId", "202309121624325_Update17" }
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
