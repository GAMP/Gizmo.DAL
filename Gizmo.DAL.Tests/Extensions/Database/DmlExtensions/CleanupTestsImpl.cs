using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database.DmlExtensions;

public static class CleanupTestsImpl
{
    public static async Task Cleanup_CreatesDefaultAdminOperator(DefaultDbContext context)
    {
        var testOperator = new Entities.UserOperator
        {
            Username = "TestOperator",
            CreatedTime = DateTime.UtcNow,
            UserCredential = new Entities.UserCredential()
        };
        context.UsersOperator.Add(testOperator);
        await context.SaveChangesAsync(CancellationToken.None);

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: true,
            deleteProducts: false,
            CancellationToken.None
        );

        var adminOperator = await context.UsersOperator
            .Include(u => u.UserCredential)
            .Include(u => u.Permissions)
            .FirstOrDefaultAsync(u => u.Username == "Admin");

        Assert.NotNull(adminOperator);
        Assert.Equal("Admin", adminOperator.Username);
        Assert.NotNull(adminOperator.UserCredential);
        Assert.NotNull(adminOperator.UserCredential.Salt);
        Assert.NotNull(adminOperator.UserCredential.Password);
        Assert.True(adminOperator.Permissions.Any(), "Admin should have permissions");
    }

    public static async Task Cleanup_HandlesReservedHostReferences(DefaultDbContext context)
    {
        // Get the default user group from seeded data
        var defaultUserGroup = await context.UserGroups.FirstAsync();
        
        // Create a host
        var testHost = new Entities.Host
        {
            Name = "TestHost",
            CreatedTime = DateTime.UtcNow
        };
        context.Hosts.Add(testHost);
        await context.SaveChangesAsync(CancellationToken.None);

        // Create a guest user with ReservedHostId pointing to the host
        var guestUser = new Entities.UserGuest
        {
            Username = "TestGuest",
            UserGroupId = defaultUserGroup.Id,
            ReservedHostId = testHost.Id,
            CreatedTime = DateTime.UtcNow
        };
        context.UsersGuest.Add(guestUser);
        await context.SaveChangesAsync(CancellationToken.None);

        // This should not throw a foreign key constraint error
        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: true,
            deleteOperators: false,
            deleteProducts: false,
            CancellationToken.None
        );

        var hostExists = await context.Hosts.AnyAsync(h => h.Id == testHost.Id);
        var guestExists = await context.UsersGuest.AnyAsync(u => u.Id == guestUser.Id);
        
        Assert.False(hostExists, "Host should be deleted");
        
        if (guestExists)
        {
            var updatedGuest = await context.UsersGuest.FindAsync(guestUser.Id);
            Assert.Null(updatedGuest?.ReservedHostId);
        }
    }

    public static async Task Cleanup_HandlesAssetTransactionCheckedInBy(DefaultDbContext context)
    {
        // Get the default entities from seeded data
        var defaultAssetType = await context.AssetTypes.FirstAsync();
        var defaultAsset = await context.Assets.FirstAsync();
        var defaultUserMember = await context.UsersMember.FirstAsync();
        
        // Create a separate branch for this test to avoid conflicts with seeded branch during cleanup
        var testBranch = new Entities.Branch
        {
            Name = "Test Branch for AssetTransaction",
            IsDeleted = false,
            IsDisabled = false
        };
        context.Branches.Add(testBranch);
        await context.SaveChangesAsync(CancellationToken.None);
        
        // Create an operator
        var testOperator = new Entities.UserOperator
        {
            Username = "TestOperator",
            CreatedTime = DateTime.UtcNow,
            UserCredential = new Entities.UserCredential()
        };
        context.UsersOperator.Add(testOperator);
        await context.SaveChangesAsync(CancellationToken.None);

        // Create an asset transaction with CheckedInById reference
        var assetTransaction = new Entities.AssetTransaction
        {
            AssetTypeId = defaultAssetType.Id,
            AssetTypeName = defaultAssetType.Name,
            AssetId = defaultAsset.Id,
            UserId = defaultUserMember.Id,
            BranchId = testBranch.Id,
            CheckedInById = testOperator.Id,
            CreatedTime = DateTime.UtcNow
        };
        context.AssetTransactions.Add(assetTransaction);
        await context.SaveChangesAsync(CancellationToken.None);

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: true,
            deleteProducts: false,
            CancellationToken.None
        );

        var operatorExists = await context.UsersOperator.AnyAsync(o => o.Id == testOperator.Id && o.Username != "Admin");
        var transactionExists = await context.AssetTransactions.AnyAsync(t => t.Id == assetTransaction.Id);
        
        Assert.False(operatorExists, "Test operator should be deleted");
        
        if (transactionExists)
        {
            var updatedTransaction = await context.AssetTransactions.FindAsync(assetTransaction.Id);
            Assert.Null(updatedTransaction?.CheckedInById);
        }
    }

    public static async Task Cleanup_HandlesEmptyDatabase(DefaultDbContext context)
    {
        // Get initial counts
        var initialUserCount = await context.Users.CountAsync();
        var initialHostCount = await context.Hosts.CountAsync();
        var initialProductCount = await context.Products.CountAsync();

        await context.Cleanup(
            deleteUsers: true,
            deleteHosts: true,
            deleteOperators: true,
            deleteProducts: true,
            CancellationToken.None
        );

        var finalUserCount = await context.Users.CountAsync();
        var finalHostCount = await context.Hosts.CountAsync();
        var finalProductCount = await context.Products.CountAsync();
        var adminOperator = await context.UsersOperator.FirstOrDefaultAsync(u => u.Username == "Admin");

        Assert.True(finalUserCount <= initialUserCount);
        Assert.True(finalHostCount <= initialHostCount);
        Assert.True(finalProductCount <= initialProductCount);
        Assert.NotNull(adminOperator); // Admin should be created even in empty database
    }

    public static async Task Cleanup_RespectsCancellationToken(DefaultDbContext context)
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel(); // Cancel immediately

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () =>
        {
            await context.Cleanup(
                deleteUsers: true,
                deleteHosts: true,
                deleteOperators: true,
                deleteProducts: true,
                cts.Token
            );
        });
    }

    #region Helper Methods

    private static async Task<int> GetDeleteUsersCount(DefaultDbContext context)
    {
        var appStatsCount = await context.AppStats.CountAsync();
        var reservationUsersCount = await context.ReservationUsers.CountAsync();
        var reservationHostsCount = await context.ReservationHosts.CountAsync();
        var reservationsCount = await context.Reservations.CountAsync();
        var assetTransactionCount = await context.AssetTransactions.CountAsync();
        var appRatingCount = await context.AppRatings.CountAsync();
        var userCreditLimitCount = await context.UserCreditLimits.CountAsync();
        var userAttributeCount = await context.UserAttribute.CountAsync();
        var userNoteCount = await context.UserNotes.CountAsync();
        var verificationEmailCount = await context.EmailVerifications.CountAsync();
        var verificationMobilePhoneCount = await context.MobilePhoneVerifications.CountAsync();
        var verificationCount = await context.Verifications.CountAsync();
        var usersGuestCount = await context.UsersGuest.CountAsync();
        var usersMemberCount = await context.UsersMember.CountAsync();

        return appRatingCount
            + appStatsCount
            + reservationUsersCount
            + reservationHostsCount
            + reservationsCount
            + assetTransactionCount
            + userCreditLimitCount
            + userAttributeCount
            + userNoteCount
            + verificationEmailCount
            + verificationMobilePhoneCount
            + verificationCount
            + usersGuestCount
            + usersMemberCount;
    }

    private static async Task<int> GetDeleteHostsCount(DefaultDbContext context)
    {
        var appStatsCount = await context.AppStats.CountAsync();
        var reservationUsersCount = await context.ReservationUsers.CountAsync();
        var reservationHostsCount = await context.ReservationHosts.CountAsync();
        var reservationsCount = await context.Reservations.CountAsync();
        var hostComputerCount = await context.HostComputers.CountAsync();
        var hostEndpointCount = await context.HostEndpoint.CountAsync();
        var hostCount = await context.Hosts.CountAsync();
        var productHostHiddenCount = await context.ProductHostGroupHidden.CountAsync();

        return appStatsCount
            + reservationUsersCount
            + reservationHostsCount
            + reservationsCount
            + hostComputerCount
            + hostEndpointCount
            + hostCount
            + productHostHiddenCount;
    }

    private static async Task<int> GetDeleteOperatorsCount(DefaultDbContext context)
    {
        var admin = await context.UsersOperator.FirstOrDefaultAsync(x => x.Username == "Admin");
        var usersOperatorCount = await context.UsersOperator.CountAsync();

        if (admin != null)
            usersOperatorCount--; // Exclude the admin operator

        return usersOperatorCount;
    }

    private static async Task<int> GetDeleteProductsCount(DefaultDbContext context)
    {
        var productImageCount = await context.ProductImages.CountAsync();
        var productTaxCount = await context.ProductsTaxes.CountAsync();
        var productPeriodCount = await context.ProductPeriods.CountAsync();
        var productPeriodDayCount = await context.ProductPeriodDays.CountAsync();
        var productTimeHostDisallowedCount = await context.ProductTimeHostDisallowed.CountAsync();
        var productUserDisallowedCount = await context.ProductUserGroupDisallowed.CountAsync();
        var productUserPriceCount = await context.ProductUserPrices.CountAsync();
        var bundleProductCount = await context.BundleProducts.CountAsync();
        var productTimePeriodCount = await context.ProductTimePeriods.CountAsync();
        var productTimePeriodDayCount = await context.ProductTimePeriodDays.CountAsync();
        var productBundleCount = await context.ProductBundles.CountAsync();
        var productCount = await context.Products.CountAsync();
        var productTimeCount = await context.ProductTimes.CountAsync();
        var productHostHiddenCount = await context.ProductHostGroupHidden.CountAsync();

        return productImageCount
            + productTaxCount
            + productPeriodCount
            + productPeriodDayCount
            + productTimeHostDisallowedCount
            + productUserDisallowedCount
            + productUserPriceCount
            + bundleProductCount
            + productTimePeriodCount
            + productTimePeriodDayCount
            + productBundleCount
            + productCount
            + productTimeCount
            + productHostHiddenCount;
    }

    private static async Task<int> GetDeleteGeneralCount(DefaultDbContext context)
    {
        // USAGE SESSION
        var usageRateCount = await context.UsageRate.CountAsync();
        var usageTimeFixedCount = await context.UsageFixed.CountAsync();
        var usageTimeCount = await context.UsageTime.CountAsync();
        var usageUserSessionCount = await context.UsageUserSession.CountAsync();
        var usageCount = await context.Usage.CountAsync();
        var usageSessionCount = await context.UsageSessions.CountAsync();

        // USER SESSION
        var userSessionChangeCount = await context.SessionsChanges.CountAsync();
        var userSessionCount = await context.UsageUserSession.CountAsync();

        // REFUNDS
        var refundInvoicePaymentCount = await context.InvoicePaymentRefund.CountAsync();
        var refundDepositPaymentCount = await context.DepositPaymentRefunds.CountAsync();
        var refundCount = await context.Refunds.CountAsync();

        // VOIDS
        var voidInvoiceCount = await context.InvoiceVoids.CountAsync();
        var voidDepositPaymentCount = await context.DepositPaymentVoids.CountAsync();
        var voidCount = await context.Voids.CountAsync();

        // INVOICE PAYMENTS
        var invoicePaymentCount = await context.InvoicePayments.CountAsync();

        // DEPOSIT PAYMENT
        var paymentIntentDepositCount = await context.PaymentIntents.CountAsync();
        var paymentIntentCount = await context.PaymentIntents.CountAsync();
        var depositPaymentCount = await context.DepositPayments.CountAsync();

        // PAYMENTS
        var paymentCount = await context.Payments.CountAsync();

        // INVOICE
        var invoiceLineProductCount = await context.InvoiceLineProduct.CountAsync();
        var invoiceLineSessionCount = await context.InvoiceLineSession.CountAsync();
        var invoiceLineTimeCount = await context.InvoiceLineTime.CountAsync();
        var invoiceLineTimeFixedCount = await context.InvoiceLineTimeFixed.CountAsync();
        var invoiceLineExtendedCount = await context.InvoiceLinesExtended.CountAsync();
        var invoiceLineCount = await context.InvoiceLines.CountAsync();
        var invoiceFiscalReceiptCount = await context.InvoiceFiscalReceipts.CountAsync();
        var invoiceCount = await context.Invoices.CountAsync();

        // ORDER
        var productOLTimeFixedCount = await context.OrderLinesTimeFixed.CountAsync();
        var productOLTimeCount = await context.OrderLinesTime.CountAsync();
        var productOLSessionCount = await context.OrderLineSession.CountAsync();
        var productOLProductCount = await context.OrderLinesProduct.CountAsync();
        var productOLExtendedCount = await context.OrderLinesExtended.CountAsync();
        var productOLCount = await context.OrderLines.CountAsync();
        var productOrderCount = await context.Orders.CountAsync();

        // DEPOSIT TRANSACTION
        var depositTransactionCount = await context.DepositTransactions.CountAsync();

        // POINT TRANSACTION
        var pointTransactionCount = await context.PointsTransaction.CountAsync();

        // STOCK TRANSACTION
        var stockTransactionCount = await context.StockTransactions.CountAsync();

        // SHIFT COUNT
        var shiftCountCount = await context.ShiftCounts.CountAsync();

        // REGISTER TRANSACTION
        var registerTransactionCount = await context.RegisterTransactions.CountAsync();

        // FISCAL RECEIPTS
        var fiscalReceiptCount = await context.FiscalReceipts.CountAsync();

        // SHIFT
        var shiftCount = await context.Shifts.CountAsync();

        // REGISTER
        var registerCount = await context.Registers.CountAsync();

        return usageRateCount
            + usageTimeFixedCount
            + usageTimeCount
            + usageUserSessionCount
            + usageCount
            + usageSessionCount
            + userSessionChangeCount
            + userSessionCount
            + refundInvoicePaymentCount
            + refundDepositPaymentCount
            + refundCount
            + voidInvoiceCount
            + voidDepositPaymentCount
            + voidCount
            + invoicePaymentCount
            + paymentIntentDepositCount
            + paymentIntentCount
            + depositPaymentCount
            + paymentCount
            + invoiceLineProductCount
            + invoiceLineSessionCount
            + invoiceLineTimeCount
            + invoiceLineTimeFixedCount
            + invoiceLineExtendedCount
            + invoiceLineCount
            + invoiceFiscalReceiptCount
            + invoiceCount
            + productOLTimeFixedCount
            + productOLTimeCount
            + productOLSessionCount
            + productOLProductCount
            + productOLExtendedCount
            + productOLCount
            + productOrderCount
            + depositTransactionCount
            + pointTransactionCount
            + stockTransactionCount
            + shiftCountCount
            + registerTransactionCount
            + fiscalReceiptCount
            + shiftCount
            + registerCount;
    }

    #endregion

    #region Complex Cleanup Tests

    public static async Task Cleanup_All(DefaultDbContext context)
    {
        // Get initial counts
        var initialGeneralCount = await GetDeleteGeneralCount(context);
        var initialUserCount = await GetDeleteUsersCount(context);
        var initialHostCount = await GetDeleteHostsCount(context);
        var initialOperatorCount = await GetDeleteOperatorsCount(context);

        if(initialOperatorCount == 0)
        {
            // If there are no operators, we cannot test this case
            return; // Skip the test by returning early
        }

        var initialProductCount = await GetDeleteProductsCount(context);

        var initialTotalCount = initialGeneralCount
            + initialUserCount
            + initialHostCount
            + initialOperatorCount
            + initialProductCount;

        // Verify initial counts are greater than zero
        Assert.True(initialTotalCount > 0, "Initial counts should be greater than zero.");

        await context.Cleanup(
            deleteUsers: true,
            deleteHosts: true,
            deleteOperators: true,
            deleteProducts: true,
            CancellationToken.None
        );

        var finalGeneralCount = await GetDeleteGeneralCount(context);
        var finalUserCount = await GetDeleteUsersCount(context);
        var finalProductCount = await GetDeleteProductsCount(context);
        var finalHostCount = await GetDeleteHostsCount(context);
        var finalOperatorCount = await GetDeleteOperatorsCount(context);

        Assert.Equal(0, finalGeneralCount);
        Assert.Equal(0, finalUserCount);
        Assert.Equal(0, finalHostCount);
        Assert.Equal(0, finalOperatorCount);
        Assert.Equal(0, finalProductCount);
    }

    public static async Task Cleanup_DeleteUsersOnly(DefaultDbContext context)
    {
        // Get initial counts
        var initialUserCount = await GetDeleteUsersCount(context);
        var initialHostCount = await GetDeleteHostsCount(context);
        var initialOperatorCount = await GetDeleteOperatorsCount(context);
        var initialProductCount = await GetDeleteProductsCount(context);

        // Verify initial user count is greater than zero
        Assert.True(initialUserCount > 0, "Initial user count should be greater than zero.");

        await context.Cleanup(
            deleteUsers: true,
            deleteHosts: false,
            deleteOperators: false,
            deleteProducts: false,
            CancellationToken.None
        );

        var finalUserCount = await GetDeleteUsersCount(context);
        var finalHostCount = await GetDeleteHostsCount(context);
        var finalOperatorCount = await GetDeleteOperatorsCount(context);
        var finalProductCount = await GetDeleteProductsCount(context);

        Assert.Equal(0, finalUserCount);
        Assert.Equal(initialHostCount, finalHostCount);
        Assert.Equal(initialOperatorCount, finalOperatorCount);
        Assert.Equal(initialProductCount, finalProductCount);
    }

    public static async Task Cleanup_DeleteHostsOnly(DefaultDbContext context)
    {
        // Get initial counts
        var initialHostCount = await GetDeleteHostsCount(context);
        var initialUserCount = await GetDeleteUsersCount(context);
        var initialOperatorCount = await GetDeleteOperatorsCount(context);
        var initialProductCount = await GetDeleteProductsCount(context);

        // Verify initial host count is greater than zero
        Assert.True(initialHostCount > 0, "Initial host count should be greater than zero.");

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: true,
            deleteOperators: false,
            deleteProducts: false,
            CancellationToken.None
        );

        var finalHostCount = await GetDeleteHostsCount(context);
        var finalUserCount = await GetDeleteUsersCount(context);
        var finalOperatorCount = await GetDeleteOperatorsCount(context);
        var finalProductCount = await GetDeleteProductsCount(context);

        Assert.Equal(0, finalHostCount);
        Assert.Equal(initialUserCount, finalUserCount);
        Assert.Equal(initialOperatorCount, finalOperatorCount);
        Assert.Equal(initialProductCount, finalProductCount);
    }

    public static async Task Cleanup_DeleteOperatorsOnly(DefaultDbContext context)
    {
        // Get initial counts
        var initialOperatorCount = await GetDeleteOperatorsCount(context);

        if (initialOperatorCount == 0)
        {
            // If there are no operators, we cannot test this case
            return; // Skip the test by returning early
        }

        var initialUserCount = await GetDeleteUsersCount(context);
        var initialHostCount = await GetDeleteHostsCount(context);
        var initialProductCount = await GetDeleteProductsCount(context);

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: true,
            deleteProducts: false,
            CancellationToken.None
        );

        var finalOperatorCount = await GetDeleteOperatorsCount(context);
        var finalUserCount = await GetDeleteUsersCount(context);
        var finalHostCount = await GetDeleteHostsCount(context);
        var finalProductCount = await GetDeleteProductsCount(context);

        Assert.Equal(0, finalOperatorCount);
        Assert.Equal(initialUserCount, finalUserCount);
        Assert.Equal(initialHostCount, finalHostCount);
        Assert.Equal(initialProductCount, finalProductCount);
    }

    public static async Task Cleanup_DeleteProductsOnly(DefaultDbContext context)
    {
        // Get initial counts
        var initialProductCount = await GetDeleteProductsCount(context);
        var initialUserCount = await GetDeleteUsersCount(context);
        var initialHostCount = await GetDeleteHostsCount(context);
        var initialOperatorCount = await GetDeleteOperatorsCount(context);

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: false,
            deleteProducts: true,
            CancellationToken.None
        );

        var finalProductCount = await GetDeleteProductsCount(context);
        var finalUserCount = await GetDeleteUsersCount(context);
        var finalHostCount = await GetDeleteHostsCount(context);
        var finalOperatorCount = await GetDeleteOperatorsCount(context);

        Assert.True(finalProductCount <= initialProductCount);
        Assert.Equal(initialUserCount, finalUserCount);
        Assert.Equal(initialHostCount, finalHostCount);
        Assert.Equal(initialOperatorCount, finalOperatorCount);
    }

    public static async Task Cleanup_DeleteAllFalse(DefaultDbContext context)
    {
        // Get initial counts
        var initialUserCount = await GetDeleteUsersCount(context);
        var initialHostCount = await GetDeleteHostsCount(context);
        var initialOperatorCount = await GetDeleteOperatorsCount(context);
        var initialProductCount = await GetDeleteProductsCount(context);

        // Verify initial counts are greater than zero
        Assert.True(initialUserCount > 0, "Initial user count should be greater than zero.");
        Assert.True(initialHostCount > 0, "Initial host count should be greater than zero.");
        Assert.True(initialProductCount > 0, "Initial product count should be greater than zero.");

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: false,
            deleteProducts: false,
            CancellationToken.None
        );

        var finalUserCount = await GetDeleteUsersCount(context);
        var finalHostCount = await GetDeleteHostsCount(context);
        var finalOperatorCount = initialOperatorCount == 0
            ? 0
            : await GetDeleteOperatorsCount(context);
        var finalProductCount = await GetDeleteProductsCount(context);

        Assert.Equal(initialUserCount, finalUserCount);
        Assert.Equal(initialHostCount, finalHostCount);
        Assert.Equal(initialOperatorCount, finalOperatorCount);
        Assert.Equal(initialProductCount, finalProductCount);
    }

    #endregion
}
