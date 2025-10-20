#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Gizmo.DAL.Scripts;
using Gizmo.Server;
using Gizmo.Server.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Database initializer.
    /// </summary>
    public sealed class DbInitializer
    {
        private readonly DefaultDbContext _dbContext;
        private readonly TickerQDbContext _tickerQDbContext;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAssemblyResourcesLocalizationService _assemblyResourcesLocalizationService;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="dbContext">Default database context.</param>
        /// <param name="tickerQDbContext">TickerQ database context.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="assemblyResourcesLocalizationService">Localization service.</param>
        /// <param name="serviceProvider">Service provider.</param>
        public DbInitializer(
            DefaultDbContext dbContext,
            TickerQDbContext tickerQDbContext,
            ILogger<DbInitializer> logger,
            IServiceProvider serviceProvider,
            IAssemblyResourcesLocalizationService assemblyResourcesLocalizationService)
        {
            _dbContext = dbContext;
            _tickerQDbContext = tickerQDbContext;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _assemblyResourcesLocalizationService = assemblyResourcesLocalizationService;
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

            bool seedData = false;

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogTrace("Connected to existing database.");

                //we will only reach this code in case that database already exist, its state or version is not know at this stage

                //attempt to update ef6 database
                var isMigrated = await TryMigrateToEF6InitialAsync(cancellationToken);

                if (isMigrated)
                    _logger.LogTrace("Existing database was migrated from EF6.");

                //will contain currently applied migrations count, zero will mean that this is initial database
                var appliedMigrations = await _dbContext.Database.GetAppliedMigrationsAsync(cancellationToken);

                //gets currently pending migrations
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                //any pending migration should be applied
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken);

                //if there are no applied migrations then this is a new database so we need to seed data
                seedData = !appliedMigrations.Any();

                if (isMigrated)
                {
                    // check if local time zone is not UTC
                    if (TimeZoneInfo.Local.BaseUtcOffset != TimeSpan.Zero)
                    {
                        var localTimeZone = TimeZoneInfo.Local;
                        _logger.LogInformation("Converting database time to UTC from {currentTimeZone}.", localTimeZone);

                        using (var dbTransaction = _dbContext.Database.BeginTransaction())
                        {
                            //check if conversion where previously completed
                            var hasConvertedFrom = await _dbContext.Settings.Where(setting => setting.GroupName == "UPGRADE" && setting.Name == "UTC_CONVERTED_FROM")
                                .AnyAsync(cancellationToken: cancellationToken);

                            if (hasConvertedFrom == false)
                            {
                                var converter = new Gizmo.DAL.DateTimeTimeZoneConverter(_dbContext, localTimeZone);
                                await converter.ConvertToUtcAsync(cancellationToken);

                                _dbContext.Settings.Add(new Setting()
                                {
                                    GroupName = "UPGRADE",
                                    Name = "UTC_CONVERTED_FROM",
                                    Value = localTimeZone.Id
                                });

                                await _dbContext.SaveChangesAsync(cancellationToken);
                                await dbTransaction.CommitAsync(cancellationToken);
                            }
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Source time zone is already UTC.");
                    }

                    // update v2 deposit payment intents PaymentId based on DepositPayment associated payment
                    using (var dbTransaction = _dbContext.Database.BeginTransaction())
                    {
                        var paymentIntentsQuery = _dbContext.Set<PaymentIntentDeposit>().Where(paymentIntent => paymentIntent.State == Entities.PaymentIntentState.Completed)
                          .Where(paymentIntent => paymentIntent.DepositPaymentId != null);

                        await paymentIntentsQuery.ExecuteUpdateAsync(setters => setters.SetProperty(paymentIntent => paymentIntent.PaymentId,
                            paymentIntent => _dbContext.Set<PaymentIntentDeposit>().Where(depositIntent => depositIntent.Id == paymentIntent.DepositPaymentId)
                            .Select(depositIntent => depositIntent.PaymentId)
                            .Single()), cancellationToken);

                        await dbTransaction.CommitAsync(cancellationToken);
                    }

                    using (var dbTransaction = _dbContext.Database.BeginTransaction())
                    {

                    }
                }
            }
            else
            {
                _logger.LogTrace("Connected to new database.");

                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(cancellationToken);

                //since a new database created we should seed data
                seedData = true;
            }

            //check if data seeding is required
            if (seedData)
                await _dbContext.AddSeedDataAsync(_serviceProvider, cancellationToken);

            //create default data
            await CreateDefaultDataAsync(cancellationToken);
        }

        /// <summary>
        /// Initialize TickerQ database.
        /// </summary>
        /// <param name="cancellationToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task InitializeTickerQAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("Initializing TickerQ database.");

            var pendingMigrations = await _tickerQDbContext.Database.GetPendingMigrationsAsync(cancellationToken);

            if (pendingMigrations.Any())
            {
                _logger.LogTrace("Applying TickerQ database migrations.");
                await _tickerQDbContext.Database.MigrateAsync(cancellationToken);
            }

            _logger.LogTrace("TickerQ database initialized.");
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
                            await MigrateEFValidateDataAsync(cancellationToken);

                            return true;
                        }
                        else
                            throw new NotSupportedException("Current database version cannot be upgraded.");
                    }
                }
            }

            return false;
        }

        private async Task MigrateEFValidateDataAsync(CancellationToken cancellationToken)
        {
            //there is an potential of same register names being used in EF6 database
            //we will need to generate new unique names for each register

            //get all registers grouped by name
            var registerNameGroup = await _dbContext.Registers
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).GroupBy(x => x.Name).ToListAsync(cancellationToken);

            if (registerNameGroup.Count > 0)
            {
                foreach (var nameGroup in registerNameGroup)
                {
                    //each name group will start from 1
                    int currentNumber = 1;
                    foreach (var register in nameGroup)
                    {
                        //take up to 40 characters from existing name and append an register number to it
                        var existingNameTruncated = register.Name[..Math.Min(register.Name.Length, 40)];
                        var newName = $"{existingNameTruncated} ({currentNumber})";

                        var registerEntity = new DAL.Entities.Register()
                        {
                            Id = register.Id,
                            Name = newName,
                        };

                        _dbContext.Entry(registerEntity).Property(register => register.Name).IsModified = true;

                        currentNumber++;
                    }
                }

                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Validates and create default required data entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        private async Task CreateDefaultDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogTrace("Initializing default data.");

                using (var dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
                {
                    #region PermissionSets

                    //this could be done in seeding BUT since we have two potential database states ef6 and new ef core we might already have seeded the initial data in the ef6
                    //making it harder to distinguish what data should be seeded

                    DAL.Entities.UserPermissionSet? adminPermissionSet = null;
                    bool permissionsSeeded = false;

                    //get permission set setting reflecting previous seeding state
                    var currentSettingEntity = await _dbContext.Settings.Where(setting => setting.GroupName == "SEEDING" && setting.Name == "PERMISSION_SET")
                        .Select(setting => new { setting.Value, setting.Id })
                        .FirstOrDefaultAsync(cancellationToken);

                    if (currentSettingEntity == null || bool.TryParse(currentSettingEntity.Value, out bool hasSeeded) && !hasSeeded)
                    {
                        //add or update seeding state
                        var settingEntity = new Setting()
                        {
                            Id = currentSettingEntity?.Id ?? 0,
                            GroupName = "SEEDING",
                            Name = "PERMISSION_SET",
                            Value = true.ToString(),
                        };

                        _dbContext.Entry(settingEntity).State = settingEntity.Id == 0 ? EntityState.Added : EntityState.Modified;

                        //gets all system policy sets
                        var policySets = Enum.GetValues<GizmoPolicySet>();

                        //get all system policies
                        var attributes = Enum.GetValues<GizmoPolicies>().Cast<GizmoPolicies>()
                            .Select(policy => new
                            {
                                Description = policy.GetAttribute<PolicyDescriptionAttribute>()
                            })
                            .Where(policy => policy.Description != null && policy.Description.IsAssignable)
                            .ToList();

                        foreach (var policySet in policySets)
                        {
                            //localize policy name
                            var localizedName = _assemblyResourcesLocalizationService.GetLocalizedStringValue(policySet);
                            if (!string.IsNullOrEmpty(localizedName))
                            {
                                if (await _dbContext.PermissionSets.Where(permissionSet => permissionSet.Name.ToLower() == localizedName.ToLower()).AnyAsync(cancellationToken) == false)
                                {
                                    var setPermissions = attributes.Where(a => a.Description.DefaultSets.Contains(policySet))
                                        .ToArray();

                                    var permissionSet = new DAL.Entities.UserPermissionSet()
                                    {
                                        Name = localizedName,
                                        Permissions = setPermissions.Select(s => new UserPermissionSetPermission()
                                        {
                                            Type = s.Description.Resource,
                                            Value = s.Description.Operation
                                        }).ToHashSet()
                                    };

                                    // keep permission set reference to be assigned to newly created admin account
                                    if (policySet == GizmoPolicySet.Owner)
                                    {
                                        adminPermissionSet = permissionSet;
                                    }

                                    _dbContext.PermissionSets.Add(permissionSet);
                                }
                            }
                        }

                        await _dbContext.SaveChangesAsync(cancellationToken);

                        permissionsSeeded = true;
                    }

                    #endregion

                    //check if admin account exists
                    var defaultOperator = await _dbContext.UsersOperator.Where(userOperator => userOperator.Username.ToLower() == "admin")
                        .FirstOrDefaultAsync(cancellationToken);

                    if (defaultOperator == null)
                    {
                        // create default operator account
                        // currently is expected that admin will be added by SeedDataMethod (will need to review)
                    }
                    else
                    {
                        // assign permissions set to existing operator in case of seeding
                        if (permissionsSeeded)
                        {
                            defaultOperator.PermissionSetId = adminPermissionSet!.Id;
                            _dbContext.Entry(defaultOperator).Property(entity => entity.PermissionSetId).IsModified = true;
                        }
                    }

                    //get existing default branch id
                    int? usableBranchId = await _dbContext.Branches
                        .Where(branch => !branch.IsDisabled && !branch.IsDeleted)
                        .Select(branch => (int?)branch.Id)
                        .FirstOrDefaultAsync(cancellationToken);

                    //check if any default branches exists
                    if (usableBranchId == null)
                    {
                        _logger.LogTrace("Creating default branch.");
                        var defaultBranch = new Branch()
                        {
                            Name = "Default",
                            IsDisabled = false,
                            IsDeleted = false,
                        };

                        _dbContext.Branches.Add(defaultBranch);

                        //save changes so we can receive branch id
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        //use id of default branch
                        usableBranchId = defaultBranch.Id;
                    }

                    //check if any branches exists
                    if (!await _dbContext.Registers.AnyAsync(cancellationToken))
                    {
                        _logger.LogTrace("Creating default register.");
                        var defaultRegister = new Register()
                        {
                            Name = "Default",
                            StartCash = 0,
                            IdleTimeout = null,
                            BranchId = usableBranchId!.Value,
                        };

                        _dbContext.Registers.Add(defaultRegister);
                    }

                    //check if one found
                    if (defaultOperator != null)
                    {
                        if (!await _dbContext.UserOperatorBranches.Where(operatorBranch => operatorBranch.OperatorId == defaultOperator.Id).AnyAsync(cancellationToken: cancellationToken))
                        {
                            _logger.LogInformation("Adding admin operator to default branch.");
                            _dbContext.UserOperatorBranches.Add(new UserOperatorBranch()
                            {
                                BranchId = usableBranchId!.Value,
                                OperatorId = defaultOperator.Id,
                            });
                        }
                    }

                    foreach (var documentType in Enum.GetValues<DocumentTypes>().Cast<DocumentTypes>())
                    {
                        if (!_dbContext.DocumentTypes.Any(dt => dt.Id == (int)documentType))
                        {
                            _logger.LogTrace("Creating default document type {DocumentType}.", documentType);
                            var documentTypeEntity = new DocumentType()
                            {
                                Id = (int)documentType,
                                Name = documentType.ToString(),
                            };
                            _dbContext.DocumentTypes.Add(documentTypeEntity);
                        }
                    }

                    foreach (var transferReason in Enum.GetValues<InventoryTransferReasons>().Cast<InventoryTransferReasons>())
                    {
                        if (!_dbContext.Set<InventoryTransferReason>().Any(dt => dt.Id == (int)transferReason))
                        {
                            _logger.LogTrace("Creating default transfer reason {TransferReason}.", transferReason);
                            var transferReasonEntity = new InventoryTransferReason()
                            {
                                Id = (int)transferReason,
                                Name = transferReason.ToString(),
                            };
                            _dbContext.Set<InventoryTransferReason>().Add(transferReasonEntity);
                        }
                    }

                    foreach (var adjustmentReason in Enum.GetValues<InventoryAdjustmentReasons>().Cast<InventoryAdjustmentReasons>())
                    {
                        if (!_dbContext.Set<InventoryAdjustmentReason>().Any(dt => dt.Id == (int)adjustmentReason))
                        {
                            _logger.LogTrace("Creating default adjustment reason {AdjustmentReason}.", adjustmentReason);
                            var adjustmentReasonEntity = new InventoryAdjustmentReason()
                            {
                                Id = (int)adjustmentReason,
                                Name = adjustmentReason.ToString(),
                            };
                            _dbContext.Set<InventoryAdjustmentReason>().Add(adjustmentReasonEntity);
                        }
                    }

                    //save any changes made
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    //commit any changes made
                    await dbTransaction.CommitAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error creating default data.");
            }
        }
    }
}
