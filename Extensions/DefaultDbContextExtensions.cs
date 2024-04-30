using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Gizmo.DAL.Entities;
using SharedLib;
using System.Globalization;
using Gizmo.DAL.Scripts;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text.Json;

namespace Gizmo.DAL.Extensions
{
    /// <summary>
    /// Default database context extensions.
    /// </summary>
    public static class DefaultDbContextExtensions
    {
        /// <summary>
        /// Updates database to target migration.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <param name="migrationName">
        /// Target migration name.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static async Task MigrateToAsync(this DefaultDbContext dbContext, string migrationName, CancellationToken cToken = default)
        {
            var migrator = dbContext.Database.GetInfrastructure().GetRequiredService<IMigrator>();

            await migrator.MigrateAsync(migrationName, cToken);
        }

        /// <summary>
        /// Upgrades the database to the last state on Entity Framework 6.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static async Task UpgradeDatabaseToLastEF6StateAsync(this DefaultDbContext dbContext, CancellationToken cToken = default)
        {
            await dbContext.MigrateToAsync("Initial", cToken);

            var migrationDbContext = dbContext.WithEF6Migrations();

            var migrator = migrationDbContext.Database.GetInfrastructure().GetRequiredService<IMigrator>();

            await migrator.MigrateAsync(Migration.InitialDatabase, cToken);
        }

        /// <summary>
        /// Returns new database context with EF6 migrations.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <returns>
        /// New database context with EF6 migrations.
        /// </returns>
        public static DefaultDbContext WithEF6Migrations(this DefaultDbContext dbContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            var connectionString = dbContext.Database.GetConnectionString();
            var commandTimeout = dbContext.Database.GetCommandTimeout();

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout(commandTimeout);
                options.MigrationsAssembly("Gizmo.DAL.Migrations.EF6.MSSQL");
            });

            return new(optionsBuilder.Options);
        }

        /// <summary>
        /// Add seed data to a new database.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token.
        /// </param>
        public static async Task AddSeedDataAsync(this DefaultDbContext dbContext, CancellationToken cancellationToken = default)
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

                byte[] salt = dbContext.GetNewSalt();
                byte[] password = dbContext.GetHashedPassword("admin", salt);

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

                await dbContext.SaveChangesAsync(cancellationToken);

                dbContext.Database.CommitTransaction();
            }
            catch
            {
                dbContext.Database.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Executes the SQL against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// </summary>
        /// <typeparam name="T">
        /// Type of DbSet.
        /// </typeparam>
        /// <param name="dbContext">
        /// Default database context.
        /// </param>
        /// <param name="scriptName">
        /// SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <returns>
        /// IQueryable of T.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        public static IQueryable<T> FromSqlScript<T>(this DefaultDbContext dbContext, string scriptName, Dictionary<string, object> parameters) where T : class
            => dbContext.Database.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbContext.Set<T>().FromSqlRaw(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbContext.Set<T>().FromSqlRaw(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                _ => throw new NotSupportedException($"Database provider {dbContext.Database.ProviderName} is not supported for this sql command."),
            };

        private sealed class PaginatedResult<T>
        {
            public int Total { get; set; }
            public T[] Items { get; set; }
        }
        /// <summary>
        /// Executes the SQL with paginated result against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// Uses a classic pagination algorithm.
        /// </summary>
        /// <typeparam name="T">
        /// Any class to be returned.
        /// </typeparam>
        /// <param name="dbContext">
        /// Default database context.
        /// </param>
        /// <param name="scriptName">
        /// SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs.
        /// </param>
        /// <param name="pageNumber">
        /// Pagination page number.
        /// </param>
        /// <param name="pageSize">
        /// Pagination page size.
        /// </param>
        /// <param name="sortBy">
        /// Sort by column (REQUIRED).
        /// </param>
        /// <param name="isAsc">
        /// Sort direction.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Paginated array of T and total items.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Order by column is required for pagination.
        /// </exception>
        /// <exception cref="JsonException">
        /// Invalid json data from the SQL script.
        /// </exception>
        /// <remarks>
        /// THIS FUNCTION DOESN'T SUPPORT SortableAttribute.
        /// </remarks>
        public static async Task<(int Total, T[] Items)> FromPaginatedSqlScript<T>(
            this DefaultDbContext dbContext,
            string scriptName,
            int pageNumber,
            int pageSize,
            string sortBy,
            bool isAsc,
            Dictionary<string, object> parameters,
            CancellationToken cToken = default)
        where T : class
        {
            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException(nameof(sortBy), "Order by column is required for pagination.");

            if (pageNumber < 1)
                pageNumber = 1;
            else if (pageNumber == int.MaxValue)
                pageNumber = int.MaxValue - 1;

            if (pageSize < 1)
                pageSize = 10;
            else if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;
                        
            var offset = pageSize * (pageNumber - 1);

            if(offset < 0)
                offset = 0;

            var sortOrder = isAsc ? "ASC" : "DESC";
            
            parameters.Add("Limit", pageSize);
            parameters.Add("Offset", offset);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortOrder", sortOrder);

            var result = dbContext.Database.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => await dbContext.Database.SqlQueryRaw<string>(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => await dbContext.Database.SqlQueryRaw<string>(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                _ => throw new NotSupportedException($"Database provider {dbContext.Database.ProviderName} is not supported for this sql command."),
            };

            if (result.Length == 0)
                return (0, []);

            var data = result.Length > 1
                ? string.Join("", result)
                : result[0];

            var paginatedResult = JsonSerializer.Deserialize<PaginatedResult<T>>(data);

            return (paginatedResult.Total, paginatedResult.Items);
        }
    }
}
