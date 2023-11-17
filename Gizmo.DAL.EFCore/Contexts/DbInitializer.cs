using Gizmo.DAL.EFCore.Extensions;

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
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task InitializeAsync(CancellationToken cToken = default)
        {
            if (await _dbContext.Database.CanConnectAsync(cToken))
            {
                var isMigrated = await TryMigrateToEF6InitialAsync(cToken);

                var appliedMigrations = await _dbContext.Database.GetAppliedMigrationsAsync(cToken);
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cToken);

                pendingMigrations = isMigrated 
                    ? pendingMigrations.Skip(1) 
                    : pendingMigrations;

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cToken);

                if (!appliedMigrations.Any())
                    _dbContext.AddSeedData();
            }
            else
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cToken);

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cToken);
                else
                {
                    //working only with migrations
                    return;
                }

                _dbContext.AddSeedData();
            }
        }

        private async Task<bool> TryMigrateToEF6InitialAsync(CancellationToken cToken = default)
        {
            if (_dbContext.Database.IsSqlServer())
            {
                if (await _dbContext.Database.TableExistsAsync("__MigrationHistory", cToken))
                {
                    if (!await _dbContext.Database.TableExistsAsync("__EFMigrationsHistory", cToken))
                    {
                        if (!await _dbContext.Database.MigrationExistAsync("202309121624325_Update17", cToken))
                        {
                            throw new NotSupportedException("Current database version cannot be upgraded.");
                        }

                        using var migrationDbContext = _dbContext.WithEF6Migrations();

                        var pendingMigrations = await migrationDbContext.Database.GetPendingMigrationsAsync(cToken);

                        if (pendingMigrations.Count() == 1)
                        {
                            await migrationDbContext.Database.MigrateAsync(cToken);

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
