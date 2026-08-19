#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Gizmo.DAL.Scripts;
using Gizmo.Server.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
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
            _logger.LogInformation("Initializing database.");

            MigrationResult? result = null;

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogInformation("Connected to existing database {databaseName}.", _dbContext.Database.GetConnectionMetadata().DatabaseName);

                //we will only reach this code in case that database already exist, its state or version is not know at this stage

                bool isCreate = await DetermineNewDatabaseAsync(_dbContext, cancellationToken);

                //attempt to update ef6 database
                var isUpgrade = await TryMigrateToEF6InitialAsync(cancellationToken);

                // update state based on upgrade, if upgrade executed then we are not considering this to be newly created database
                isCreate = isCreate && !isUpgrade;

                //gets currently pending migrations
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);

                //any pending migration should be applied
                if (pendingMigrations.Any())
                {
                    _logger.LogInformation("Migrating database to latest version.");
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }

                if (isUpgrade)
                {
                    // NOTE we can only execute this step (UTC conversion) once the v3 migrations are applied

                    using (var dbTransaction = _dbContext.Database.BeginTransaction())
                    {
                        // check if local time zone is not UTC
                        if (TimeZoneInfo.Local.BaseUtcOffset != TimeSpan.Zero)
                        {
                            var localTimeZone = TimeZoneInfo.Local;
                            _logger.LogInformation("Converting database time to UTC from {currentTimeZone}.", localTimeZone);

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
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Source time zone is already UTC.");
                        }

                        _logger.LogInformation("Populating payment intents due to incompatibility with v3.");
                        var paymentIntentsQuery = _dbContext.Set<PaymentIntentDeposit>().Where(paymentIntent => paymentIntent.State == Entities.PaymentIntentState.Completed)
                          .Where(paymentIntent => paymentIntent.DepositPaymentId != null);

                        await paymentIntentsQuery.ExecuteUpdateAsync(setters => setters.SetProperty(paymentIntent => paymentIntent.PaymentId,
                            paymentIntent => _dbContext.Set<PaymentIntentDeposit>().Where(depositIntent => depositIntent.Id == paymentIntent.DepositPaymentId)
                            .Select(depositIntent => depositIntent.PaymentId)
                            .Single()), cancellationToken);

                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                    }
                }

                result = new MigrationResult() { IsCreate = isCreate, IsMigrate = pendingMigrations.Any(), IsUpgrade = isUpgrade };
            }
            else
            {
                // we will end up here whether if no actual database exist OR if connection have failed
                // reaching this code does not automatically mean that the database is actually new although its probably very common          

                var existingDatabase = await _dbContext.Database.Exists(cancellationToken);
                if (!existingDatabase)
                    _logger.LogInformation("Creating new database {dbName}.", _dbContext.Database.GetConnectionMetadata().DatabaseName);

                bool isCreate = !existingDatabase || await DetermineNewDatabaseAsync(_dbContext, cancellationToken);
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);     

                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }

                // new database created, no upgrade happen, new migrations where applied
                result = new MigrationResult() { IsCreate = !existingDatabase, IsUpgrade = false, IsMigrate = pendingMigrations.Any() };
            }

            try
            {
                var dbName = _dbContext.Database.GetDbConnection().Database;
                var _ = await _dbContext.Database.ExecuteSqlScriptAsync(SQLScripts.APPLY_SPECIFIC_DATABASE_SETTINGS, new() { { "DbName", dbName } }, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to apply database specific settings.");
            }

            //create default data
            await ValidateDataAsync(result, cancellationToken);
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
                            _logger.LogInformation("Upgrading existing database {databaseName} from V2 to V3.", _dbContext.Database.GetConnectionMetadata().DatabaseName);

                            await migrationDbContext.Database.MigrateAsync(cancellationToken);
                            await MigrateEFValidateDataAsync(cancellationToken);

                            _logger.LogInformation("Existing database {databaseName} was upgraded from v2.", _dbContext.Database.GetConnectionMetadata().DatabaseName);

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
            // Same register names may exist in EF6 databases; v3 adds a unique index so we
            // must dedupe here. Group using SQL Server's default-collation comparison rules
            // (ANSI_PADDING ON → trailing spaces insignificant; default CI collation → case-insensitive)
            // otherwise pairs like "SM-01" / "SM-01 " slip through as separate groups under C#
            // string equality, but SQL Server rejects them as duplicates when the index is built.
            static string Normalized(string name) => name.TrimEnd().ToLowerInvariant();

            var registers = await _dbContext.Registers
                .Select(x => new { x.Id, x.Name })
                .ToListAsync(cancellationToken);

            var hasChanges = false;

            foreach (var group in registers.GroupBy(r => Normalized(r.Name)))
            {
                var items = group.ToList();

                if (items.Count > 1)
                {
                    int currentNumber = 1;
                    foreach (var register in items)
                    {
                        var trimmed = register.Name.TrimEnd();
                        var truncated = trimmed[..Math.Min(trimmed.Length, 40)];
                        var newName = $"{truncated} ({currentNumber})";

                        _logger.LogWarning("Renaming existing register {original} to {new} due to unique name incompatibility with v3.", register.Name, newName);

                        var registerEntity = new DAL.Entities.Register()
                        {
                            Id = register.Id,
                            Name = newName,
                        };

                        _dbContext.Entry(registerEntity).Property(register => register.Name).IsModified = true;

                        currentNumber++;
                    }
                    hasChanges = true;
                }
                else
                {
                    // Single entry: trim trailing whitespace so it doesn't collide with a future insert.
                    var register = items[0];
                    var trimmed = register.Name.TrimEnd();
                    if (trimmed != register.Name)
                    {
                        _dbContext.Entry(new DAL.Entities.Register { Id = register.Id, Name = trimmed })
                            .Property(r => r.Name).IsModified = true;
                        hasChanges = true;
                    }
                }
            }

            if (hasChanges)
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Validates and create default required data entities.
        /// </summary>
        /// <param name="result">Migration result.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        private async Task ValidateDataAsync(MigrationResult result, CancellationToken cancellationToken)
        {
            try
            {
                // this service is only registered in bootstrap mode, needs care!
                var optionsService = _serviceProvider.GetRequiredService<IDatabaseOptionsInitializeAccess>();

                _logger.LogInformation("Initializing-validating default data.");

                await using (var dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
                {
                    #region Branch

                    // attempt to obtain single branch, status is irrelevant at this stage
                    int? targetBranchId = await _dbContext.Branches.Select(branch => (int?)branch.Id).FirstOrDefaultAsync(cancellationToken);

                    if (targetBranchId == null)
                    {
                        var branch = new Branch()
                        {
                            Id = targetBranchId ?? 0,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.BRANCH_DEFAULT_NAME),
                        };

                        _dbContext.Branches.Add(branch);
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        targetBranchId = branch.Id;
                    }
                    else
                    {
                        // simply update the branch name based on same localization values we would have used with the new database
                        if (result.IsUpgrade)
                        {
                            var branch = new Branch()
                            {
                                Id = targetBranchId.Value,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.BRANCH_DEFAULT_NAME),
                            };

                            _dbContext.Entry(branch).Property(branch => branch.Name).IsModified = true;
                            await _dbContext.SaveChangesAsync(cancellationToken);
                        }
                    }

                    #endregion                    

                    #region Operator                

                    if (result.IsCreate || result.IsUpgrade)
                    {
                        #region Permission sets

                        DAL.Entities.UserPermissionSet? ownerPermissionSet = null;

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
                            var localizedName = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(policySet);
                            if (!string.IsNullOrEmpty(localizedName))
                            {
                                var setPermissions = attributes.Where(a => a.Description!.DefaultSets.Contains(policySet))
                                    .ToArray();

                                var permissionSet = new DAL.Entities.UserPermissionSet()
                                {
                                    Name = localizedName,
                                    Permissions = setPermissions.Select(s => new UserPermissionSetPermission()
                                    {
                                        Type = s.Description!.Resource,
                                        Value = s.Description.Operation
                                    }).ToHashSet()
                                };

                                // keep permission set reference to be assigned to newly created admin account
                                if (policySet == GizmoPolicySet.Owner)
                                    ownerPermissionSet = permissionSet;

                                _dbContext.PermissionSets.Add(permissionSet);
                            }
                        }

                        await _dbContext.SaveChangesAsync(cancellationToken);

                        #endregion

                        if (result.IsCreate)
                        {
                            #region AddDefaultOperator

                            byte[] salt = _dbContext.GetNewSalt();
                            byte[] password = _dbContext.GetHashedPassword("admin", salt);

                            DAL.Entities.UserOperator? defaultOperator = new UserOperator
                            {
                                Username = "Admin",
                                CreatedTime = DateTimeOffset.UtcNow.DateTime,
                                Guid = Guid.NewGuid(),
                                PermissionSetId = ownerPermissionSet?.Id,
                            };

                            _dbContext.UsersOperator.Add(defaultOperator);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            var adminCredential = new UserCredential()
                            {
                                Id = defaultOperator.Id,
                                Salt = salt,
                                Password = password
                            };

                            _dbContext.Credentials.Add(adminCredential);
                            _dbContext.UserOperatorBranches.Add(new UserOperatorBranch()
                            {
                                BranchId = targetBranchId!.Value,
                                OperatorId = defaultOperator.Id,
                            });

                            await _dbContext.SaveChangesAsync(cancellationToken);

                            #endregion
                        }
                        else
                        {
                            // upgrade case, since we don't really know current permissions configuration in existing ef6 database we cant practically map them to the 
                            // new permission sets, its up to user
                            // validating existing operator and restoring permissions is also not part of initialization, its better to use other tools for such cases

                            // Existing operators are attached to the default branch by the EF6->Core
                            // migration script (Scripts.EF_6_BRANCH_SET) so the assignment is atomic with
                            // the schema upgrade and recorded in __EFMigrationsHistory. Doing it here instead
                            // relied on the transient IsUpgrade flag (true for a single process run only): if
                            // anything threw after the migration committed, operators were stranded with no
                            // UserOperatorBranch rows and no second chance. Do not reintroduce that here.
                        }
                    }

                    #endregion

                    #region Stock

                    if (result.IsCreate || result.IsUpgrade)
                    {
                        int? targetStockId = await _dbContext.Stocks.Select(stock => (int?)stock.Id).FirstOrDefaultAsync(cancellationToken);

                        var stock = new Stock()
                        {
                            BranchId = targetBranchId.Value,
                            Id = targetStockId ?? 0,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.STOCK_SELLING_POINT_DEFAULT_NAME),
                            IsDeleted = false,
                            Type = StockType.SellingPoint
                        };

                        if (targetStockId == null)
                        {
                            _dbContext.Stocks.Add(stock);
                            await _dbContext.SaveChangesAsync(cancellationToken);
                        }
                        else
                        {
                            // simply update the stock name based on same localization values we would have used with the new database
                            // we will only end up here in case of ef6 database upgrade where an single stock created automatically by migration sql script

                            _dbContext.Entry(stock).Property(stock => stock.Name).IsModified = true;
                            await _dbContext.SaveChangesAsync(cancellationToken);
                        }

                        #region Warehouse Stock

                        _dbContext.Stocks.Add(new Stock()
                        {
                            BranchId = targetBranchId,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.STOCK_WAREHOUSE_DEFAULT_NAME),
                            IsDeleted = false,
                            Type = StockType.Warehouse
                        });

                        #endregion
                    }

                    #endregion

                    #region Register
                    // always create at least one register no matter of the database migration result
                    if (!await _dbContext.Registers.AnyAsync(cancellationToken))
                    {
                        _logger.LogInformation("Creating default register.");
                        var defaultRegister = new Register()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.REGISTER_DEFAULT_NAME),
                            StartCash = 0,
                            IdleTimeout = null,
                            MacAddress = null,
                            BranchId = targetBranchId!.Value,
                        };

                        _dbContext.Registers.Add(defaultRegister);
                    }
                    #endregion

                    #region Document types
                    foreach (var documentType in Enum.GetValues<DocumentTypes>().Cast<DocumentTypes>())
                    {
                        if (!await _dbContext.DocumentTypes.AnyAsync(dt => dt.Id == (int)documentType, cancellationToken))
                        {
                            _logger.LogInformation("Creating default document type {DocumentType}.", documentType);
                            var documentTypeEntity = new DocumentType()
                            {
                                Id = (int)documentType,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(documentType),
                            };
                            _dbContext.DocumentTypes.Add(documentTypeEntity);
                        }
                    }
                    #endregion

                    #region Transfer reason
                    foreach (var transferReason in Enum.GetValues<InventoryTransferReasons>().Cast<InventoryTransferReasons>())
                    {
                        if (!await _dbContext.Set<InventoryTransferReason>().AnyAsync(dt => dt.Id == (int)transferReason, cancellationToken))
                        {
                            _logger.LogInformation("Creating default transfer reason {TransferReason}.", transferReason);
                            var transferReasonEntity = new InventoryTransferReason()
                            {
                                Id = (int)transferReason,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(transferReason),
                            };
                            _dbContext.Set<InventoryTransferReason>().Add(transferReasonEntity);
                        }
                    }
                    #endregion

                    #region Adjustment rason
                    foreach (var adjustmentReason in Enum.GetValues<InventoryAdjustmentReasons>().Cast<InventoryAdjustmentReasons>())
                    {
                        if (!await _dbContext.Set<InventoryAdjustmentReason>().AnyAsync(dt => dt.Id == (int)adjustmentReason, cancellationToken))
                        {
                            _logger.LogInformation("Creating default adjustment reason {AdjustmentReason}.", adjustmentReason);
                            var adjustmentReasonEntity = new InventoryAdjustmentReason()
                            {
                                Id = (int)adjustmentReason,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(adjustmentReason),
                            };
                            _dbContext.Set<InventoryAdjustmentReason>().Add(adjustmentReasonEntity);
                        }
                    }
                    #endregion

                    #region Skin Options

                    if (result.IsCreate || result.IsUpgrade)
                    {
                        var clientSkinOptions = new
                        {
                            UserLoginDisabled = false,
                            HomeDisabled = false,
                            QuickLaunchMaxItems = 6,

                            HomePageMaxItemsPerRow = 8,
                            AppsPageMaxItemsPerRow = 8,
                            ProductsPageMaxItemsPerRow = 8,

                            MaxPopularProducts = 8,
                            MaxPopularApplications = 8,

                            ShopDisabled = false,
                        };
                        _dbContext.ClientOptions.Add(new DAL.Entities.ClientOptions()
                        {
                            Data = System.Text.Json.JsonSerializer.Serialize(clientSkinOptions),
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.CLIENT_OPTIONS_DEFAULT_NAME),
                            IsDefault = true,
                        });
                    }

                    #endregion

                    if (result.IsCreate)
                    {
                        // only new database seeding

                        #region AddPaymentMethods

                        _dbContext.PaymentMethods.AddRange(
                            [
                            new()
                            {
                                Id = (int)PaymentMethodType.Cash,
                                Name =  _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.PAYMENT_METHOD_CASH_NAME),
                                DisplayOrder = 0,
                                IsEnabled = true,
                                IsClient = true,
                                IsManager = true
                            },
                            new()
                            {
                                Id = (int)PaymentMethodType.CreditCard,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.PAYMENT_METHOD_CREDIT_CARD_NAME),
                                DisplayOrder = 1,
                                IsEnabled = true,
                                IsClient = true,
                                IsManager = true
                            },
                            new()
                            {
                                Id = (int)PaymentMethodType.Deposit,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.PAYMENT_METHOD_DEPOSIT_NAME),
                                DisplayOrder = 2,
                                IsEnabled = true,
                                IsClient = true,
                                IsManager = true
                            },
                            new()
                            {
                                Id = (int)PaymentMethodType.Points,
                                Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.PAYMENT_METHOD_POINTS_NAME),
                                DisplayOrder = 3,
                                IsEnabled = true,
                                IsClient = true,
                                IsManager = true
                            }
                            ]);

                        #endregion

                        #region AddLayoutGroups

                        _dbContext.HostLayoutGroups.Add(new HostLayoutGroup()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.HOST_LAYOUT_GROUPED_DEFAULT_NAME),
                            DisplayOrder = 0
                        });

                        #endregion

                        #region AddBillProfiles

                        var computersBillingProfile = new BillProfile()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.BILL_PROFILE_COMPUTERS_DEFAULT_NAME)
                        };
                        var endpointsBillingProfile = new BillProfile()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.BILL_PROFILE_ENDPOINTS_DEFAULT_NAME)
                        };

                        var billProfiles = new BillProfile[] { computersBillingProfile, endpointsBillingProfile };

                        var billRates = new BillRate[]
                        {
                            new()
                            {
                                BillProfile = computersBillingProfile,
                                IsDefault = true,
                                MinimumFee = 2,
                                ChargeAfter = 1,
                                ChargeEvery = 5,
                                Rate = 2,
                                StartFee = 1
                            },
                            new()
                            {
                                BillProfile = endpointsBillingProfile,
                                IsDefault = true,
                                MinimumFee = 2,
                                ChargeAfter = 1,
                                ChargeEvery = 5,
                                Rate = 2,
                                StartFee = 1
                            }
                        };

                        _dbContext.BillProfiles.AddRange(billProfiles);
                        _dbContext.BillRates.AddRange(billRates);

                        await _dbContext.SaveChangesAsync(cancellationToken);

                        #endregion

                        #region AddUserGroups

                        var userGroupMember = new UserGroup()
                        {
                            Name = "Members",
                            IsDefault = true
                        };
                        var userGroupGuest = new UserGroup()
                        {
                            Name = "Guests",
                            Options = Entities.UserGroupOptionType.GuestUse
                        };

                        var userGroups = new UserGroup[] { userGroupMember, userGroupGuest };

                        _dbContext.UserGroups.AddRange(userGroups);

                        #endregion

                        #region AddHostGroups

                        var hostGroupComputers = new HostGroup()
                        {
                            BranchId = targetBranchId.Value,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.HOST_GROUP_COMPUERS_DEFAULT_NAME),
                            DefaultGuestGroup = userGroupGuest,
                            BillProfileId = computersBillingProfile.Id,
                        };
                        var hostGroupEndpoints = new HostGroup()
                        {
                            BranchId = targetBranchId.Value,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Gizmo.Server.DefaultNames.HOST_GROUP_ENDPOINTS_DEFAULT_NAME),
                            DefaultGuestGroup = userGroupGuest,
                            BillProfileId = endpointsBillingProfile.Id,
                        };

                        var hostGroups = new HostGroup[]
                        {
                            hostGroupComputers,
                            hostGroupEndpoints
                        };

                        _dbContext.HostGroups.AddRange(hostGroups);

                        #endregion

                        #region PresetTimeSale

                        _dbContext.PresetTimeSale.AddRange(
                            [
                            new() { Value = 1 },
                            new() { Value = 5 },
                            new() { Value = 15 },
                            new() { Value = 30 },
                            new() { Value = 60 }
                            ]);

                        #endregion

                        #region PresetTimeSaleMoney

                        _dbContext.PresetTimeSaleMoney.AddRange(
                            [
                            new() { Value = 1 },
                            new() { Value = 2 },
                            new() { Value = 5 },
                            new() { Value = 10 },
                            new() { Value = 20 }
                            ]);
                        #endregion 

                        #region Notifications

                        _dbContext.Notifications.Add(new DAL.Entities.NotificationTimedRemaining()
                        {
                            Minute = 5,
                            Type = NotificationType.Visual,
                        });

                        #endregion

                        #region Assistance requests

                        _dbContext.AssistanceRequestTypes.Add(new AssistanceRequestType()
                        {
                            Title = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.ASSISTANCE_REQUEST_TYPE_DEFAULT_NAME),
                            DisplayOrder = 0,
                        });

                        #endregion

                        #region App categories

                        _dbContext.Categories.Add(new AppCategory()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.APP_CATEGORY_APPLICATIONS_DEFAULT_NAME)
                        });
                        _dbContext.Categories.Add(new AppCategory()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.APP_CATEGORY_GAMES_DEFAULT_NAME)
                        });
                        _dbContext.Categories.Add(new AppCategory()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.APP_CATEGORY_LAUNCHERS_DEFAULT_NAME)
                        });

                        #endregion

                        #region App profile

                        _dbContext.AppGroups.Add(new AppGroup()
                        {
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.APP_GROUP_DEFAULT_NAME)
                        });

                        #endregion

                        #region Product groups

                        _dbContext.ProductGroups.Add(new ProductGroup()
                        {
                            DisplayOrder = 0,
                            SortOption = ProductSortOptionType.Name,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.PRODUCT_GROUP_TIME_OFFERS_DEFAULT_NAME)
                        });

                        _dbContext.ProductGroups.Add(new ProductGroup()
                        {
                            DisplayOrder = 1,
                            SortOption = ProductSortOptionType.Name,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.PRODUCT_GROUP_FOOD_DEFAULT_NAME)
                        });

                        _dbContext.ProductGroups.Add(new ProductGroup()
                        {
                            DisplayOrder = 2,
                            SortOption = ProductSortOptionType.Name,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.PRODUCT_GROUP_DRINKS_DEFAULT_NAME)
                        });

                        _dbContext.ProductGroups.Add(new ProductGroup()
                        {
                            DisplayOrder = 3,
                            SortOption = ProductSortOptionType.Name,
                            Name = _assemblyResourcesLocalizationService.GetLocalizedStringValueOrName(Server.DefaultNames.PRODUCT_GROUP_SWEETS_DEFAULT_NAME)
                        });

                        #endregion
                    }

                    if (result.IsCreate || result.IsUpgrade)
                    {
                        #region Reservation notification
                        _dbContext.Notifications.Add(new DAL.Entities.NotificationTimedReservation()
                        {
                            Minute = 15,
                            Type = NotificationType.Visual,
                        });
                        #endregion
                    }

                    #region Options

                    // cases for newly created or upgraded databases
                    if (result.IsCreate || result.IsUpgrade)
                    {
                        if (result.IsCreate)
                        {
                            // process cases that for newly created databases

                            await optionsService.WriteAsync(_dbContext, new Server.Options.UserSessionsOptions()
                            {
                                TerminatePending = true,
                                LogoutDisconnected = true,
                                PendingTimeout = 180 
                            }, cancellationToken);

                            await optionsService.WriteAsync(_dbContext, new Server.Options.UserRegistrationOptions()
                            {
                                IsClientEnabled = true,
                                IsPortalEnabled = false,
                                VerificationMethod = Server.RegistrationVerificationMethod.None,
                            }, cancellationToken);

                            await optionsService.WriteAsync(_dbContext, new Server.Options.InvoicingOptions()
                            {
                                AutoInvoiceGuest = true,
                                AutoInvoiceMember = true,
                                AutoInvoicePaymentGuest = true,
                                AutoInvoicePaymentMember = true,
                            }, cancellationToken);

                            await optionsService.WriteAsync(_dbContext, new Server.Options.UserBalanceOptions()
                            {
                                WithholdUnpaidUsageSessionDeposits = true,
                            }, cancellationToken);

                            await optionsService.WriteAsync(_dbContext, new Server.Options.POSAutomationOptions()
                            {
                                AutoDelivery = true,
                                AutoGuestLogin = true,
                                AutoPrepare = true,
                                DisablePrintReceiptByDefault = false,

                            }, cancellationToken);

                            await optionsService.WriteAsync(_dbContext, new Server.Options.PaymentProcessingOptions(), cancellationToken);

                            await optionsService.WriteAsync(_dbContext, new Server.Options.TopUpOptions()
                            {
                                IsCustomValueAllowed = true,
                                MinimumValue = 1
                            }, cancellationToken);
                        }

                        await optionsService.WriteAsync(_dbContext, new Server.Options.NetworkOptions()
                        {
                            HostName = "gizmo.local",
                            HttpProtocols = Server.HttpProtocols.HttpHttps
                        }, cancellationToken);
                        await optionsService.WriteAsync(_dbContext, new Server.Options.SkinOptions() { DefaultSkin = "Next" }, cancellationToken);
                        await optionsService.WriteAsync(_dbContext, new Server.Options.ReservationsOptions()
                        {
                            EnableLoginBlockBefore = true,
                            LoginBlockBeforeTime = 15,

                            EnableLoginBlockAfter = true,
                            LoginBlockAfterTime = 15,

                            EnableExpiration = true,
                            ExpireAfter = 20,

                            TimeSourceType = Web.Api.Models.ReservationTimeSourceType.TimeOfferFixedTime,
                            PaymentExpireAfter = 15,

                            CancellationGracePeriod = 1440,
                            CancellationRefundPercentage = 100,

                        }, cancellationToken);
                    }

                    #endregion

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

        /// <summary>
        /// Executes logic to determine if the database is considered as a new.
        /// </summary>
        /// <param name="dbContext">DbContext.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><see langword="true"/> or <see langword="false"/>.</returns>
        /// <remarks>
        /// This function is used to determine logical database creation state, currently we are considering the database a newly created as long as none of expected migrations are applied.<br></br>
        /// The main usage of this result is to be able to trigger data seeding in some scenarios where database might be already created but not migrated.
        /// </remarks>
        private static async Task<bool> DetermineNewDatabaseAsync(DefaultDbContext dbContext, CancellationToken cancellationToken)
        {
            //gets currently pending migrations
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            var totalMigrationsCount = dbContext.Database.GetMigrations().Count();

            // treat as new database if pending migrations count is equal to total migrations count
           return pendingMigrations.Count() == totalMigrationsCount;
        }

        sealed class MigrationResult
        {
            /// <summary>
            /// Indicates upgrade from ef6 to ef core.
            /// </summary>
            public bool IsUpgrade { get; init; }

            /// <summary>
            /// Indicates a new database creation.
            /// </summary>
            public bool IsCreate { get; init; }

            /// <summary>
            /// Indicates that new migrations where applied.
            /// </summary>
            public bool IsMigrate { get; init; }
        }
    }
}
