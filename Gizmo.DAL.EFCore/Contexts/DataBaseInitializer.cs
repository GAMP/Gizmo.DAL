using Gizmo.DAL.Contexts;
using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;

using SharedLib;

using System;
using System.Globalization;
using System.Linq;

namespace Gizmo.DAL.EFCore.Contexts
{
    /// <summary>
    /// Database initializer.
    /// </summary>
    public sealed class DatabaseInitializer
    {
        private readonly DefaultDbContext _context;
        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="context">
        /// Database context.
        /// </param>
        public DatabaseInitializer(DefaultDbContext context) => _context = context;

        /// <summary>
        /// Initialize database.
        /// </summary>
        public void Initialize()
        {
            if (_context.Database.CanConnect())
            {
                var appliedMigrations = _context.Database.GetAppliedMigrations();

                if (!appliedMigrations.Any())
                {
                    _context.Database.EnsureDeleted();
                    _context.Database.Migrate();
                    SeedData(false);
                }
            }
            else
            {
                _context.Database.Migrate();
                SeedData(false);
            }
        }

        private void SeedData(bool withRefresh)
        {
            try
            {
                _context.Database.BeginTransaction();

                if (withRefresh)
                {
                    _context.PresetTimeSaleMoney.RemoveRange(_context.PresetTimeSaleMoney);
                    _context.PresetTimeSale.RemoveRange(_context.PresetTimeSale);
                    _context.HostEndpoint.RemoveRange(_context.HostEndpoint);
                    _context.HostGroups.RemoveRange(_context.HostGroups);
                    _context.UsersMember.RemoveRange(_context.UsersMember);
                    _context.UserGroups.RemoveRange(_context.UserGroups);
                    _context.BillRates.RemoveRange(_context.BillRates);
                    _context.BillProfiles.RemoveRange(_context.BillProfiles);
                    _context.HostLayoutGroups.RemoveRange(_context.HostLayoutGroups);
                    _context.ProductsTaxes.RemoveRange(_context.ProductsTaxes);
                    _context.ProductPeriodDays.RemoveRange(_context.ProductPeriodDays);
                    _context.ProductPeriods.RemoveRange(_context.ProductPeriods);
                    _context.BundleProducts.RemoveRange(_context.BundleProducts);
                    _context.ProductTimes.RemoveRange(_context.ProductTimes);
                    _context.ProductBundles.RemoveRange(_context.ProductBundles);
                    _context.Products.RemoveRange(_context.Products);
                    _context.ProductGroups.RemoveRange(_context.ProductGroups);
                    _context.Taxes.RemoveRange(_context.Taxes);
                    _context.UserPermissions.RemoveRange(_context.UserPermissions);
                    _context.Credentials.RemoveRange(_context.Credentials);
                    _context.UsersOperator.RemoveRange(_context.UsersOperator);
                    _context.MonetaryUnits.RemoveRange(_context.MonetaryUnits);
                    _context.PaymentMethods.RemoveRange(_context.PaymentMethods);
                    
                    _context.SaveChanges();
                }

                #region AddPaymentMethods

                _context.PaymentMethods.AddRange(new PaymentMethod[]
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
                    _context.MonetaryUnits.AddRange(new MonetaryUnit[]
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
                    _context.MonetaryUnits.AddRange(new MonetaryUnit[]
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

                _context.UsersOperator.AddRange(admin);
                _context.SaveChanges();

                var adminCredential = new UserCredential() { Id = 1, Salt = salt, Password = password };

                var adminPermissions = IntegrationLib.ClaimTypeBase.GetClaimTypes()
                    .Select(claim => new UserPermission() { UserId = admin.Id, Type = claim.Resource, Value = claim.Operation });

                _context.Credentials.AddRange(adminCredential);
                _context.UserPermissions.AddRange(adminPermissions);

                #endregion

                #region AddTaxes

                var taxes = new Tax[]
                {
                 new() { Name = "24%", Value = 23, UseOrder = 0 },
                 new() { Name = "16%", Value = 16, UseOrder = 1 },
                 new() { Name = "None", Value = 0, UseOrder = 2 }
                };

                _context.Taxes.AddRange(taxes);

                _context.SaveChanges();

                #endregion

                #region AddProducts

                var tax = taxes[0];

                var productGroupTime = new ProductGroup() { Name = "Time Offers", DisplayOrder = 0, Guid = Guid.NewGuid() };
                var productGroupFood = new ProductGroup() { Name = "Food", DisplayOrder = 1, Guid = Guid.NewGuid() };
                var productGroupDrinks = new ProductGroup() { Name = "Drinks", DisplayOrder = 2, Guid = Guid.NewGuid() };
                var productGroupSweets = new ProductGroup() { Name = "Sweets", DisplayOrder = 3, Guid = Guid.NewGuid() };

                var productGroups = new ProductGroup[] { productGroupTime, productGroupFood, productGroupDrinks, productGroupSweets };

                _context.ProductGroups.AddRange(productGroups);
                _context.SaveChanges();

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

                _context.Products.AddRange(products);
                _context.ProductBundles.AddRange(productBundlePizzaAndCola);
                _context.SaveChanges();

                var bundleProducts = new BundleProduct[]
                {
                 new(){ProductId = productCocaCola.Id, ProductBundleId = productBundlePizzaAndCola.Id, Price = 1, Quantity = 1 },
                 new(){ProductId = productPizza.Id, ProductBundleId = productBundlePizzaAndCola.Id, Price = 2, Quantity = 1 }
                };

                var productPeriod = new ProductPeriod()
                {
                    Id = 1,
                    Options = PeriodOptionType.None
                };
                var productPeriodDays = new ProductPeriodDay[]
                {
                 new(){ ProductPeriodId = productPeriod.Id, Day = DayOfWeek.Saturday },
                 new(){ ProductPeriodId = productPeriod.Id, Day = DayOfWeek.Sunday },
                };

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

                _context.ProductTimes.AddRange(productTimes);
                _context.SaveChanges();

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

                _context.BundleProducts.AddRange(bundleProducts);
                _context.ProductPeriods.AddRange(productPeriod);
                _context.ProductPeriodDays.AddRange(productPeriodDays);
                _context.ProductsTaxes.AddRange(productTaxes);

                #endregion

                #region AddLayoutGroups

                _context.HostLayoutGroups.Add(new HostLayoutGroup()
                {
                    Name = "Default",
                    DisplayOrder = 0
                });

                #endregion

                #region AddBillProfiles

                var billProfileMemberPrices = new BillProfile() { Name = "Member Prices" };
                var billProfileGuestsPrices = new BillProfile() { Name = "Guests Prices" };

                var billProfiles = new BillProfile[] { billProfileMemberPrices, billProfileGuestsPrices };

                _context.BillProfiles.AddRange(billProfiles);
                _context.SaveChanges();

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

                _context.BillRates.AddRange(billRates);

                #endregion

                #region AddUserGroups

                var userGroupMember = new UserGroup() { Name = "Members", BillProfileId = billProfileMemberPrices.Id, IsDefault = true };
                var userGroupGuest = new UserGroup() { Name = "Guests", BillProfileId = billProfileGuestsPrices.Id, Options = UserGroupOptionType.GuestUse };

                var userGroups = new UserGroup[] { userGroupMember, userGroupGuest };

                _context.UserGroups.AddRange(userGroups);
                _context.SaveChanges();

                #endregion

                #region AddUsers

                var userMember = new UserMember() { Username = "User", UserGroupId = userGroupMember.Id, Guid = Guid.NewGuid() };

                _context.UsersMember.AddRange(userMember);

                #endregion

                #region AddHostGroups

                var hostGroupComputers = new HostGroup() { Name = "Computers", DefaultGuestGroupId = userGroupGuest.Id };
                var hostGroupEndpoints = new HostGroup() { Name = "Endpoints", DefaultGuestGroupId = userGroupGuest.Id };

                var hostGroups = new HostGroup[] { hostGroupComputers, hostGroupEndpoints };

                _context.HostGroups.AddRange(hostGroups);
                _context.SaveChanges();

                #endregion

                #region AddHosts

                _context.HostEndpoint.AddRange(new HostEndpoint[]
                {
                     new() { Name = "XBOX-ONE-1", Number = 1, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() },
                     new() { Name = "XBOX-ONE-2", Number = 2, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() },
                     new() { Name = "PS4-1", Number = 3, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() },
                     new() { Name = "WII-1", Number = 4, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = Guid.NewGuid() }
                });

                #endregion

                #region AddPresetTime

                _context.PresetTimeSale.AddRange(new PresetTimeSale[]
                {
                    new() { Value = 1 },
                    new() { Value = 5 },
                    new() { Value = 15 },
                    new() { Value = 30 },
                    new() { Value = 60 }
                });

                _context.PresetTimeSaleMoney.AddRange(new PresetTimeSaleMoney[]
                {
                     new() { Value = 1 },
                     new() { Value = 2 },
                     new() { Value = 5 },
                     new() { Value = 10 },
                     new() { Value = 20 }
                });

                #endregion

                _context.SaveChanges();

                _context.Database.CommitTransaction();
            }
            catch
            {
                _context.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
