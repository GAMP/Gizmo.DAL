﻿using Gizmo.DAL.EFCore;
using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLib;

using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Database initializer.
    /// </summary>
    public sealed class DatabaseInitializer
    {
        private readonly IOptions<DatabaseConnectionOptions> _connectionOptions;

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="connectionOptions">
        /// Connection options.
        /// </param>
        public DatabaseInitializer(IOptions<DatabaseConnectionOptions> connectionOptions)
        {
            _connectionOptions = connectionOptions;
        }

        /// <summary>
        /// Initialize database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            string connectionString = _connectionOptions.Value.DbConnectionString;
            var databaseType = _connectionOptions.Value.DbType;
            var commandTimeout = _connectionOptions.Value.CommandTimeout;

            //TODO validation logic could be added, possibly it will be done before this call

            //here we need to determine if the current database instance is an old EF6 based one
            //that have not yet been migrated to the new strcuture
            if (databaseType != DatabaseType.MSSQL || databaseType != DatabaseType.MSSQLEXPRESS)
            {
                optionsBuilder.UseSqlServer(connectionString, options =>
                {
                    options.CommandTimeout(_connectionOptions.Value.CommandTimeout);

                    //use ef6 migration assembly
                    options.MigrationsAssembly("Gizmo.DAL.EF6.Migrations.MSSQL");
                });

                using (DefaultDbContext migrateContext = new(optionsBuilder.Options))
                {
                    string tableExistQuery = """
                           IF (EXISTS (SELECT *
                           FROM INFORMATION_SCHEMA.TABLES
                           WHERE TABLE_SCHEMA = 'dbo'
                           AND TABLE_NAME = '__MigrationHistory'))
                           BEGIN
                              SELECT CAST(1 AS BIT)
                           END;
                        ELSE
                           BEGIN
                              SELECT CAST(0 AS BIT)
                           END;
                        """;

                    var result = (await migrateContext.Database.SqlQueryRaw<bool>(tableExistQuery)
                        .ToListAsync(cancellationToken)).Single();

                    await migrateContext.Database.MigrateAsync(cancellationToken);
                }
            }

            switch(databaseType)
            {
                case DatabaseType.MSSQL:
                case DatabaseType.MSSQLEXPRESS:
                    optionsBuilder.UseSqlServer(connectionString, options =>
                    {
                        options.CommandTimeout(commandTimeout);
                        options.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.MSSQL");
                    });
                    break;
                case DatabaseType.POSTGRE:
                    optionsBuilder.UseNpgsql(connectionString, options =>
                    {
                        options.CommandTimeout(commandTimeout);
                        options.MigrationsAssembly("Gizmo.DAL.EFCore.Migrations.Npgsql");
                    });
                    break;
                default:
                    throw new NotSupportedException();
            }

            using (DefaultDbContext dbContext = new(optionsBuilder.Options))
            {                
                //get currently pending migrations
                var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync(cancellationToken);

                //if none of migrations is still applied we should seed the data
                //as such case will indicate creation of a new database
                if (!appliedMigrations.Any())
                    AddSeedData(dbContext);

                //execute any pending migrations
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        private void AddSeedData(DefaultDbContext dbContext)
        {
            try
            {
                dbContext.Database.BeginTransaction();

                #region AddPaymentMethods

                dbContext.PaymentMethods.AddRange(new PaymentMethod[]
                {
                    new() { Id = (int)PaymentMethodType.Cash, Name = "Cash", DisplayOrder = 0, IsEnabled = true, IsClient = true, IsManager = true },
                    new() { Id = (int)PaymentMethodType.Points, Name = "Points", DisplayOrder = 2, IsEnabled = true, IsClient = true, IsManager = true },
                    new() { Id = (int)PaymentMethodType.Deposit, Name = "Deposit", DisplayOrder = 3, IsEnabled = true, IsClient = true, IsManager = true },
                    new() { Id = (int)PaymentMethodType.CreditCard, Name = "Credit Card", DisplayOrder = 4, IsEnabled = true, IsClient = true, IsManager = true },
                });

                #endregion

                #region AddMonetaryUnits

                if (RegionInfo.CurrentRegion.ISOCurrencySymbol == "EUR")
                {
                    dbContext.MonetaryUnits.AddRange(new MonetaryUnit[]
                    {
                        new() { DisplayOrder = 0, Name = "1 Cent", Value = 0.01M },
                        new() { DisplayOrder = 1, Name = "5 Cent", Value = 0.05M },
                        new() { DisplayOrder = 2, Name = "10 Cent", Value = 0.10M },
                        new() { DisplayOrder = 3, Name = "20 Cent", Value = 0.20M },
                        new() { DisplayOrder = 4, Name = "50 Cent", Value = 0.50M },
                        new() { DisplayOrder = 5, Name = "1 Euro", Value = 1.00M },
                        new() { DisplayOrder = 6, Name = "2 Euro", Value = 2.00M },
                        new() { DisplayOrder = 7, Name = "5 Euro", Value = 5.00M },
                        new() { DisplayOrder = 8, Name = "10 Euro", Value = 10.00M },
                        new() { DisplayOrder = 9, Name = "20 Euro", Value = 20.00M },
                        new() { DisplayOrder = 10, Name = "50 Euro", Value = 50.00M },
                        new() { DisplayOrder = 11, Name = "100 Euro", Value = 100.00M },
                        new() { DisplayOrder = 12, Name = "200 Euro", Value = 200.00M },
                        new() { DisplayOrder = 13, Name = "500 Euro", Value = 500.00M }
                    });
                }
                else
                {
                    dbContext.MonetaryUnits.AddRange(new MonetaryUnit[]
                    {
                        new() { DisplayOrder = 0, Name = "1 Cent", Value = 0.01M },
                        new() { DisplayOrder = 1, Name = "5 Cent", Value = 0.05M },
                        new() { DisplayOrder = 2, Name = "10 Cent", Value = 0.10M },
                        new() { DisplayOrder = 3, Name = "25 Cent", Value = 0.25M },
                        new() { DisplayOrder = 4, Name = "1 Dollar", Value = 1.00M },
                        new() { DisplayOrder = 5, Name = "2 Dollar", Value = 2.00M },
                        new() { DisplayOrder = 6, Name = "5 Dollar", Value = 5.00M },
                        new() { DisplayOrder = 7, Name = "10 Dollar", Value = 10.00M },
                        new() { DisplayOrder = 8, Name = "20 Dollar", Value = 20.00M },
                        new() { DisplayOrder = 9, Name = "50 Dollar", Value = 50.00M },
                        new() { DisplayOrder = 10, Name = "100 Dollar", Value = 100.00M }
                    });
                }

                #endregion

                #region AddDefaultOperator

                byte[] salt = DefaultDbContext.GetNewSalt();
                byte[] password = DefaultDbContext.GetHashedPassword("admin", salt);

                var admin = new UserOperator
                {
                    Username = "Admin",
                    CreatedTime = DateTimeOffset.UtcNow.DateTime,
                    Guid = Guid.NewGuid(),
                };

                dbContext.UsersOperator.AddRange(admin);
                dbContext.SaveChanges();

                var adminCredential = new UserCredential() { Id = admin.Id, Salt = salt, Password = password };

                var adminPermissions = IntegrationLib.ClaimTypeBase.GetClaimTypes()
                    .Select(claim => new UserPermission() { UserId = admin.Id, Type = claim.Resource, Value = claim.Operation });

                dbContext.Credentials.AddRange(adminCredential);
                dbContext.UserPermissions.AddRange(adminPermissions);

                #endregion

                #region AddTaxes

                var taxes = new Tax[]
                {
                 new() { Name = "24%", Value = 23, UseOrder = 0 },
                 new() { Name = "16%", Value = 16, UseOrder = 1 },
                 new() { Name = "None", Value = 0, UseOrder = 2 }
                };

                dbContext.Taxes.AddRange(taxes);

                dbContext.SaveChanges();

                #endregion

                #region AddProducts

                var tax = taxes[0];

                var productGroupTime = new ProductGroup() { Name = "Time Offers", DisplayOrder = 0, Guid = Guid.NewGuid() };
                var productGroupFood = new ProductGroup() { Name = "Food", DisplayOrder = 1, Guid = Guid.NewGuid() };
                var productGroupDrinks = new ProductGroup() { Name = "Drinks", DisplayOrder = 2, Guid = Guid.NewGuid() };
                var productGroupSweets = new ProductGroup() { Name = "Sweets", DisplayOrder = 3, Guid = Guid.NewGuid() };

                var productGroups = new ProductGroup[] { productGroupTime, productGroupFood, productGroupDrinks, productGroupSweets };

                dbContext.ProductGroups.AddRange(productGroups);
                dbContext.SaveChanges();

                var productMars = new Product()
                {
                    ProductGroupId = productGroupSweets.Id,
                    Name = "Mars Bar",
                    Cost = 0.90m,
                    Price = 1.10m,
                    Points = 10,
                    StockOptions = StockOptionType.EnableStock,
                    Guid = Guid.NewGuid()
                };
                var productSnickers = new Product()
                {
                    ProductGroupId = productGroupSweets.Id,
                    Name = "Snickers Bar",
                    Points = 15,
                    StockOptions = StockOptionType.EnableStock,
                    Cost = 1.20m,
                    Price = 2.0m,
                    Guid = Guid.NewGuid()
                };
                var productPizza = new Product()
                {
                    ProductGroupId = productGroupFood.Id,
                    Name = "Pizza (Small)",
                    Cost = 2.20m,
                    Price = 6.0m,
                    Guid = Guid.NewGuid()
                };
                var productCocaCola = new Product()
                {
                    ProductGroupId = productGroupDrinks.Id,
                    Name = "Coca Cola (Can)",
                    Points = 20,
                    StockOptions = StockOptionType.EnableStock,
                    Cost = 1.20m,
                    Price = 2.0m,
                    Guid = Guid.NewGuid()
                };

                var products = new Product[] { productMars, productSnickers, productCocaCola, productPizza };

                var productBundlePizzaAndCola = new ProductBundle()
                {
                    ProductGroupId = productGroupFood.Id,
                    Name = "Pizza and Cola",
                    StockOptions = StockOptionType.EnableStock,
                    Points = 200,
                    Price = 3.40m, //pizza plus cola
                    Guid = Guid.NewGuid()
                };

                dbContext.Products.AddRange(products);
                dbContext.ProductBundles.AddRange(productBundlePizzaAndCola);
                dbContext.SaveChanges();

                var bundleProducts = new BundleProduct[]
                {
                     new(){ProductId = productCocaCola.Id, ProductBundleId = productBundlePizzaAndCola.Id, Price = 1, Quantity = 1 },
                     new(){ProductId = productPizza.Id, ProductBundleId = productBundlePizzaAndCola.Id, Price = 2, Quantity = 1 }
                };

                var productPeriods = new ProductPeriod[]
                {
                    new ()
                    {
                        Id = productMars.Id,
                        Options = PeriodOptionType.None
                    },
                    new ()
                    {
                        Id = productSnickers.Id,
                        Options = PeriodOptionType.None
                    },
                    new ()
                    {
                        Id = productPizza.Id,
                        Options = PeriodOptionType.None
                    },
                    new ()
                    {
                        Id = productCocaCola.Id,
                        Options = PeriodOptionType.None
                    },
                    new ()
                    {
                        Id = productBundlePizzaAndCola.Id,
                        Options = PeriodOptionType.None
                    },
                };

                var productPeriodDays = productPeriods.SelectMany(x => new ProductPeriodDay[]
                {
                     new(){ ProductPeriodId = x.Id, Day = DayOfWeek.Saturday },
                     new(){ ProductPeriodId = x.Id, Day = DayOfWeek.Sunday },
                });

                var productTimeSixHours = new ProductTime()
                {
                    ProductGroupId = productGroupTime.Id,
                    Name = "Six Hours (6)",
                    Minutes = 360,
                    Price = 12,
                    WeekEndMaxMinutes = null,
                    Guid = Guid.NewGuid()
                };
                var productTimeSixHoursWeekends = new ProductTime()
                {
                    ProductGroupId = productGroupTime.Id,
                    Name = "Six Hours (6 Weekends)",
                    Minutes = 360,
                    Price = 16,
                    WeekEndMaxMinutes = null,
                    Guid = Guid.NewGuid()
                };

                var productTimes = new ProductTime[] { productTimeSixHours, productTimeSixHoursWeekends };

                dbContext.ProductTimes.AddRange(productTimes);
                dbContext.SaveChanges();

                var productTaxes = new ProductTax[]
                {
                 new() { ProductId = productMars.Id, TaxId = tax.Id },
                 new() { ProductId = productSnickers.Id, TaxId = tax.Id },
                 new() { ProductId = productPizza.Id, TaxId = tax.Id },
                 new() { ProductId = productCocaCola.Id, TaxId = tax.Id },
                 new() { ProductId = productBundlePizzaAndCola.Id, TaxId = tax.Id },
                 new() { ProductId = productTimeSixHours.Id, TaxId = tax.Id },
                 new() { ProductId = productTimeSixHoursWeekends.Id, TaxId = tax.Id },
                };

                dbContext.BundleProducts.AddRange(bundleProducts);
                dbContext.ProductPeriods.AddRange(productPeriods);
                dbContext.ProductPeriodDays.AddRange(productPeriodDays);
                dbContext.ProductsTaxes.AddRange(productTaxes);

                #endregion

                #region AddLayoutGroups

                dbContext.HostLayoutGroups.Add(new HostLayoutGroup()
                {
                    Name = "Default",
                    DisplayOrder = 0
                });

                #endregion

                #region AddBillProfiles

                var billProfileMemberPrices = new BillProfile() { Name = "Member Prices" };
                var billProfileGuestsPrices = new BillProfile() { Name = "Guests Prices" };

                var billProfiles = new BillProfile[] { billProfileMemberPrices, billProfileGuestsPrices };

                dbContext.BillProfiles.AddRange(billProfiles);
                dbContext.SaveChanges();

                var billRates = new BillRate[]
                {
                 new()
                 {
                     BillProfileId = billProfileMemberPrices.Id,
                     IsDefault = true,
                     MinimumFee = 2,
                     ChargeAfter = 1,
                     ChargeEvery = 5,
                     Rate = 2,
                     StartFee = 1
                 },
                 new()
                 {
                     BillProfileId = billProfileGuestsPrices.Id,
                     IsDefault = true,
                     MinimumFee = 2,
                     ChargeAfter = 1,
                     ChargeEvery = 5,
                     Rate = 2,
                     StartFee = 1
                 }
                };

                dbContext.BillRates.AddRange(billRates);

                #endregion

                #region AddUserGroups

                var userGroupMember = new UserGroup() { Name = "Members", BillProfileId = billProfileMemberPrices.Id, IsDefault = true };
                var userGroupGuest = new UserGroup() { Name = "Guests", BillProfileId = billProfileGuestsPrices.Id, Options = UserGroupOptionType.GuestUse };

                var userGroups = new UserGroup[] { userGroupMember, userGroupGuest };

                dbContext.UserGroups.AddRange(userGroups);
                dbContext.SaveChanges();

                #endregion

                #region AddUsers

                var userMember = new UserMember() { Username = "User", UserGroupId = userGroupMember.Id, Guid = Guid.NewGuid() };

                dbContext.UsersMember.AddRange(userMember);

                #endregion

                #region AddHostGroups

                var hostGroupComputers = new HostGroup() { Name = "Computers", DefaultGuestGroupId = userGroupGuest.Id };
                var hostGroupEndpoints = new HostGroup() { Name = "Endpoints", DefaultGuestGroupId = userGroupGuest.Id };

                var hostGroups = new HostGroup[] { hostGroupComputers, hostGroupEndpoints };

                dbContext.HostGroups.AddRange(hostGroups);
                dbContext.SaveChanges();

                #endregion

                #region AddHosts

                dbContext.HostEndpoint.AddRange(new HostEndpoint[]
                {
                     new() { Name = "XBOX-ONE-1", Number = 1, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() },
                     new() { Name = "XBOX-ONE-2", Number = 2, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() },
                     new() { Name = "PS4-1", Number = 3, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() },
                     new() { Name = "WII-1", Number = 4, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() }
                });

                #endregion

                #region AddPresetTime

                dbContext.PresetTimeSale.AddRange(new PresetTimeSale[]
                {
                    new() { Value = 1 },
                    new() { Value = 5 },
                    new() { Value = 15 },
                    new() { Value = 30 },
                    new() { Value = 60 }
                });

                dbContext.PresetTimeSaleMoney.AddRange(new PresetTimeSaleMoney[]
                {
                     new() { Value = 1 },
                     new() { Value = 2 },
                     new() { Value = 5 },
                     new() { Value = 10 },
                     new() { Value = 20 }
                });

                #endregion

                dbContext.SaveChanges();

                dbContext.Database.CommitTransaction();
            }
            catch
            {
                dbContext.Database.RollbackTransaction();
                throw;
            }
        }
        private void RemoveSeedData(DefaultDbContext dbContext)
        {
            try
            {
                dbContext.Database.BeginTransaction();

                dbContext.PresetTimeSaleMoney.RemoveRange(dbContext.PresetTimeSaleMoney);
                dbContext.PresetTimeSale.RemoveRange(dbContext.PresetTimeSale);
                dbContext.HostEndpoint.RemoveRange(dbContext.HostEndpoint);
                dbContext.HostGroups.RemoveRange(dbContext.HostGroups);
                dbContext.UsersMember.RemoveRange(dbContext.UsersMember);
                dbContext.UserGroups.RemoveRange(dbContext.UserGroups);
                dbContext.BillRates.RemoveRange(dbContext.BillRates);
                dbContext.BillProfiles.RemoveRange(dbContext.BillProfiles);
                dbContext.HostLayoutGroups.RemoveRange(dbContext.HostLayoutGroups);
                dbContext.ProductsTaxes.RemoveRange(dbContext.ProductsTaxes);
                dbContext.ProductPeriodDays.RemoveRange(dbContext.ProductPeriodDays);
                dbContext.ProductPeriods.RemoveRange(dbContext.ProductPeriods);
                dbContext.BundleProducts.RemoveRange(dbContext.BundleProducts);
                dbContext.ProductTimes.RemoveRange(dbContext.ProductTimes);
                dbContext.ProductBundles.RemoveRange(dbContext.ProductBundles);
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.ProductGroups.RemoveRange(dbContext.ProductGroups);
                dbContext.Taxes.RemoveRange(dbContext.Taxes);
                dbContext.UserPermissions.RemoveRange(dbContext.UserPermissions);
                dbContext.Credentials.RemoveRange(dbContext.Credentials);
                dbContext.UsersOperator.RemoveRange(dbContext.UsersOperator);
                dbContext.MonetaryUnits.RemoveRange(dbContext.MonetaryUnits);
                dbContext.PaymentMethods.RemoveRange(dbContext.PaymentMethods);

                dbContext.SaveChanges();

                dbContext.Database.CommitTransaction();
            }
            catch
            {
                dbContext.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
