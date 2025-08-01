using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Scripts;
using IntegrationLib;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class SqlServer
{
    public static async Task Cleanup(
        DefaultDbContext cx,
        bool deleteUsers,
        bool deleteHosts,
        bool deleteOperators,
        bool deleteProducts,
        CancellationToken ct)
    {
        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            cx.Database.SetCommandTimeout(int.MaxValue);

            if (deleteHosts || deleteUsers)
            {
                await cx.Database.DeleteFromAsync("AppStat", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ReservationUser", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ReservationHost", true, cToken: ct);
                await cx.Database.DeleteFromAsync("Reservation", true, cToken: ct);
            }

            if (deleteHosts && !deleteUsers)
            {
                await cx.Database.ExecuteSqlScriptAsync(SQLScripts.RESET_USERGUESTS, cToken: ct);
            }

            #region FINANCIAL

            await cx.Database.UpdateAsync("InvoiceLineExtended", new Dictionary<string, string> { { "BundleLineId", "NULL" } }, cToken: ct);
            await cx.Database.UpdateAsync("UsageSession", new Dictionary<string, string> { { "CurrentUsageId", "NULL" } }, cToken: ct);
            await cx.Database.UpdateAsync("ProductOLExtended", new Dictionary<string, string> { { "BundleLineId", "NULL" } }, cToken: ct);

            //USAGE SESSION
            await cx.Database.DeleteFromAsync("UsageRate", false, cToken: ct);
            await cx.Database.DeleteFromAsync("UsageTimeFixed", false, cToken: ct);
            await cx.Database.DeleteFromAsync("UsageTime", false, cToken: ct);
            await cx.Database.DeleteFromAsync("UsageUserSession", false, cToken: ct);
            await cx.Database.DeleteFromAsync("Usage", true, cToken: ct);
            await cx.Database.DeleteFromAsync("UsageSession", true, cToken: ct);

            //USER SESSION
            await cx.Database.DeleteFromAsync("UserSessionChange", true, cToken: ct);
            await cx.Database.DeleteFromAsync("UserSession", true, cToken: ct);

            //REFUNDS
            await cx.Database.DeleteFromAsync("RefundInvoicePayment", false, cToken: ct);
            await cx.Database.DeleteFromAsync("RefundDepositPayment", false, cToken: ct);
            await cx.Database.DeleteFromAsync("Refund", true, cToken: ct);

            //VOIDS
            await cx.Database.DeleteFromAsync("VoidInvoice", false, cToken: ct);
            await cx.Database.DeleteFromAsync("VoidDepositPayment", false, cToken: ct);
            await cx.Database.DeleteFromAsync("Void", true, cToken: ct);

            //INVOICE PAYMENTS
            await cx.Database.DeleteFromAsync("InvoicePayment", true, cToken: ct);

            //DEPOSIT PAYMENT
            await cx.Database.DeleteFromAsync("PaymentIntentDeposit", false, cToken: ct);
            await cx.Database.DeleteFromAsync("PaymentIntentOrder", false, cToken: ct);
            await cx.Database.DeleteFromAsync("PaymentIntent", true, cToken: ct);
            await cx.Database.DeleteFromAsync("DepositPayment", true, cToken: ct);

            //PAYMENTS
            await cx.Database.DeleteFromAsync("Payment", true, cToken: ct);

            //INVOICE
            await cx.Database.DeleteFromAsync("InvoiceLineProduct", false, cToken: ct);
            await cx.Database.DeleteFromAsync("InvoiceLineSession", false, cToken: ct);
            await cx.Database.DeleteFromAsync("InvoiceLineTime", false, cToken: ct);
            await cx.Database.DeleteFromAsync("InvoiceLineTimeFixed", false, cToken: ct);
            await cx.Database.DeleteFromAsync("InvoiceLineExtended", false, cToken: ct);
            await cx.Database.DeleteFromAsync("InvoiceLine", true, cToken: ct);
            await cx.Database.DeleteFromAsync("InvoiceFiscalReceipt", true, cToken: ct);
            await cx.Database.DeleteFromAsync("Invoice", true, cToken: ct);

            //ORDER
            await cx.Database.DeleteFromAsync("ProductOLTimeFixed", false, cToken: ct);
            await cx.Database.DeleteFromAsync("ProductOLTime", false, cToken: ct);
            await cx.Database.DeleteFromAsync("ProductOLSession", false, cToken: ct);
            await cx.Database.DeleteFromAsync("ProductOLProduct", false, cToken: ct);
            await cx.Database.DeleteFromAsync("ProductOLExtended", false, cToken: ct);
            await cx.Database.DeleteFromAsync("ProductOL", true, cToken: ct);
            await cx.Database.DeleteFromAsync("ProductOrder", true, cToken: ct);

            //DEPOSIT TRANSACTION
            await cx.Database.DeleteFromAsync("DepositTransaction", true, cToken: ct);

            //POINT TRANSACTION
            await cx.Database.DeleteFromAsync("PointTransaction", true, cToken: ct);

            //STOCK TRANSACTION
            await cx.Database.DeleteFromAsync("StockTransaction", true, cToken: ct);

            //SHIFT COUNT
            await cx.Database.DeleteFromAsync("ShiftCount", true, cToken: ct);

            //REGISTER TRANSACTION
            await cx.Database.DeleteFromAsync("RegisterTransaction", true, cToken: ct);

            //FISCAL RECEIPTS
            await cx.Database.DeleteFromAsync("FiscalReceipt", true, cToken: ct);

            //SHIFT
            await cx.Database.DeleteFromAsync("Shift", true, cToken: ct);

            //REGISTER
            await cx.Database.DeleteFromAsync("Register", true, cToken: ct);

            #endregion

            #region PRODUCTS

            if (deleteProducts || deleteHosts)
            {
                await cx.Database.DeleteFromAsync("ProductHostHidden", true, cToken: ct);
            }

            if (deleteProducts)
            {
                await cx.Database.DeleteFromAsync("ProductImage", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTax", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductPeriod", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductPeriodDayTime", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductPeriodDay", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTimeHostDisallowed", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductUserDisallowed", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductUserPrice", true, cToken: ct);
                await cx.Database.DeleteFromAsync("BundleProduct", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTimeHostDisallowed", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTimePeriod", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTimePeriodDayTime", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTimePeriodDay", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductBundle", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Product", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductTime", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductBaseExtended", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductBase", true, cToken: ct);
            }

            #endregion

            #region USERS

            if (deleteUsers)
            {
                await cx.Database.DeleteFromAsync("HostGroupWaitingLineEntry", true, cToken: ct);
                await cx.Database.DeleteFromAsync("AssetTransaction", true, cToken: ct);
                await cx.Database.DeleteFromAsync("AppRating", false, cToken: ct);
                await cx.Database.DeleteFromAsync("UserCreditLimit", false, cToken: ct);
                await cx.Database.DeleteFromAsync("UserAttribute", true, cToken: ct);
                await cx.Database.DeleteFromAsync("UserNote", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Note", true, cToken: ct);
                await cx.Database.DeleteFromAsync("VerificationEmail", false, cToken: ct);
                await cx.Database.DeleteFromAsync("VerificationMobilePhone", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Verification", true, cToken: ct);
                await cx.Database.DeleteFromAsync("Token", true, cToken: ct);

                cx.UsersGuest.RemoveRange(cx.UsersGuest);
                cx.UsersMember.RemoveRange(cx.UsersMember);

                //detect any changes made
                cx.ChangeTracker.DetectChanges();

                //save any changes made
                await cx.SaveChangesAsync(ct);
            }

            #endregion

            #region HOSTS

            if (deleteHosts)
            {
                await cx.Database.DeleteFromAsync("HostComputer", false, cToken: ct);
                await cx.Database.DeleteFromAsync("HostEndpoint", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Host", true, cToken: ct);
            }

            #endregion

            #region OPERATORS

            if (deleteOperators)
            {
                if (!deleteUsers)
                {
                    cx.ChangeTracker.DetectChanges();

                    await cx.Database.UpdateAsync(
                        "UserCreditLimit",
                        new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                        cToken: ct);

                    await cx.Database.UpdateAsync(
                        "UserPicture",
                        new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                        cToken: ct);

                    await cx.Database.UpdateAsync(
                        "UserAttribute",
                        new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                        cToken: ct);

                    await cx.Database.UpdateAsync(
                        "AssetTransaction",
                        new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" }, { "CheckedInById", "NULL" } },
                        cToken: ct);
                }

                if (!deleteHosts)
                {
                    await cx.Database.UpdateAsync("Host", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                }

                if (!deleteHosts && !deleteProducts)
                {
                    await cx.Database.UpdateAsync(
                        "ProductHostHidden",
                        new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                        cToken: ct);
                }

                await cx.Database.UpdateAsync(
                    "DeviceHost",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("Device", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "ReservationUser",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ReservationHost",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "Reservation",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.DeleteFromAsync("Token", false, new Dictionary<string, string> { { "Type", "0" } }, ct);
                await cx.Database.UpdateAsync("Token", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("App", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppCategory",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppEnterprise",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("AppExe", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExeCdImage",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExeDeployment",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExeImage",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExeLicense",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExeMaxUser",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExePersonalFile",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "AppExeTask",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("AppGroup", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("AppImage", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("AppLink", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("Asset", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "AssetType",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "Attribute",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "BillProfile",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "BundleProduct",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "BundleProductUserPrice",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ClientTask",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "Deployment",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("Feed", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "HostGroup",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "HostGroupWaitingLine",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "HostGroupWaitingLineEntry",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "HostLayoutGroup",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "HostLayoutGroupImage",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "HostLayoutGroupLayout",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("Icon", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("License", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "LicenseKey",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("Mapping", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "MonetaryUnit",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("News", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("Note", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "PaymentMethod",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "PersonalFile",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "PluginLibrary",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "PresetTimeSale",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "PresetTimeSaleMoney",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductBase",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductBundleUserPrice",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductGroup",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductImage",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductTax",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductTimeHostDisallowed",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductUserDisallowed",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "ProductUserPrice",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "SecurityProfile",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "SecurityProfilePolicy",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "SecurityProfileRestriction",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("Setting", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("TaskBase", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("Tax", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "UserGroup",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "UserGroupHostDisallowed",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync("Variable", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("User", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                await cx.Database.UpdateAsync(
                    "UserCredential",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                await cx.Database.UpdateAsync(
                    "UserAgreement",
                    new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } },
                    cToken: ct);

                cx.UserPermissions.RemoveRange(cx.UserPermissions.Where(permission => permission.User is Entities.UserOperator));
                cx.UsersOperator.RemoveRange(cx.UsersOperator);

                var defaultOperator = new Entities.UserOperator();

                byte[] salt = cx.GetNewSalt();
                byte[] password = cx.GetHashedPassword("admin", salt);

                defaultOperator.UserCredential = new Entities.UserCredential();
                defaultOperator.Username = "Admin";

                defaultOperator.CreatedTime = DateTime.UtcNow;
                defaultOperator.UserCredential.Salt = salt;
                defaultOperator.UserCredential.Password = password;

                var allPermissions =
                    ClaimTypeBase
                        .GetClaimTypes()
                        .Select(claim => new Entities.UserPermission { Type = claim.Resource, Value = claim.Operation });

                defaultOperator.Permissions.UnionWith(allPermissions);

                cx.UsersOperator.Update(defaultOperator);
                await cx.SaveChangesAsync(ct);
            }

            #endregion

            //detect any changes made
            cx.ChangeTracker.DetectChanges();

            //save any changes made
            await cx.SaveChangesAsync(ct);

            //commit changes
            await trx.CommitAsync(ct);
        }
    }

    public static async Task CleanupUsers(DefaultDbContext cx, CancellationToken ct)
    {
        const string DeletedUsersSubquery =
            """
               SELECT A.UserId 
               FROM [User] AS A 
               LEFT OUTER JOIN [UserGuest] AS B ON A.UserId = B.UserId 
               LEFT OUTER JOIN [UserOperator] AS C ON A.UserId = C.UserId 
               WHERE A.IsDeleted = 1 
               AND B.UserId IS NULL 
               AND C.UserId IS NULL
            """;

        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            cx.Database.SetCommandTimeout(int.MaxValue);

            // Basic DELETE operations (direct user ID reference)
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AssetTransaction"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AppStat"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AppRating"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AssistanceRequest"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("ReservationUser"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Reservation"), ct);

            // Nested DELETE operations (joined tables)
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageTime", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageTimeFixed", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageRate", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageUserSession", "Usage", "UsageId"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UsageSession"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Usage"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserSessionChange"), ct);

            // Delete based on CreatedById
            await cx.Database.ExecuteSqlAsync(
                $"""
                      DELETE FROM [UserSessionChange] 
                      WHERE CreatedById IN (
                          {DeletedUsersSubquery}
                      )
                 """, ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserSession"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("RefundInvoicePayment", "InvoicePayment", "InvoicePaymentId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("RefundDepositPayment", "DepositPayment", "DepositPaymentId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("Refund", "Payment", "PaymentId"), ct);

            // Complex join with multiple levels
            await cx.Database.ExecuteSqlAsync(
                $"""
                     DELETE FROM [RefundDepositPayment] 
                     WHERE RefundId IN (
                         SELECT RefundId 
                         FROM [Refund] 
                         WHERE DepositTransactionId IN (
                             SELECT DepositTransactionId 
                             FROM [DepositTransaction] 
                             WHERE UserId IN (
                                 {DeletedUsersSubquery}
                             )
                         )
                     )
                 """, ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("Refund", "DepositTransaction", "DepositTransactionId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("InvoicePayment"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("PaymentIntentDeposit", "PaymentIntent", "PaymentIntentId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("PaymentIntent"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("DepositPayment"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Payment"), ct);

            // Update operations setting NULL values
            await cx.Database.ExecuteSqlAsync(
                UpdateTableSetNull(
                    "InvoiceLineExtended",
                    "BundleLineId",
                    "InvoiceLine",
                    "InvoiceLineId"),
                ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineProduct", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineSession", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineTime", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineTimeFixed", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineExtended", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("InvoiceLine"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Invoice"), ct);

            await cx.Database.ExecuteSqlAsync(
                UpdateTableSetNull(
                    "ProductOLExtended",
                    "BundleLineId",
                    "ProductOL",
                    "ProductOLId"),
                ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLTimeFixed", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLTime", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLSession", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLProduct", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLExtended", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("ProductOL"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("ProductOrder"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("DepositTransaction"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("PointTransaction"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("HostGroupWaitingLineEntry"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserCreditLimit"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserAttribute"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("Note", "UserNote", "NoteId"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserNote"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Verification"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Token"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserGuest"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserMember"), ct);

            // Final user deletion
            await cx.Database.ExecuteSqlAsync(
                $"""
                    DELETE FROM [User] 
                    WHERE IsDeleted = 1 
                    AND UserId IN (
                        {DeletedUsersSubquery}
                    )
                 """, ct);

            cx.ChangeTracker.DetectChanges();
            await cx.SaveChangesAsync(ct);
            await trx.CommitAsync(ct);
        }

        return;

        static FormattableString UpdateTableSetNull(
            string tableName,
            string columnToSetNull,
            string joinTableName,
            string joinColumnName,
            string whereColumnName = "UserId") =>
            $"""
                UPDATE [{tableName}] 
                SET {columnToSetNull} = NULL 
                WHERE {joinColumnName} IN (
                    SELECT {joinColumnName} 
                    FROM [{joinTableName}] 
                    WHERE {whereColumnName} IN (
                        {DeletedUsersSubquery}
                    )
                )
            """;

        static FormattableString DeleteFromTableWithJoin(string tableName, string joinTableName, string joinColumnName, string whereColumnName = "UserId") =>
            $"""
                 DELETE FROM [{tableName}] 
                 WHERE {joinColumnName} IN (
                     SELECT {joinColumnName} 
                     FROM [{joinTableName}] 
                     WHERE {whereColumnName} IN (
                         {DeletedUsersSubquery}
                     )
                 )
             """;

        static FormattableString DeleteFromTable(string tableName, string columnName = "UserId") =>
            $"""
                DELETE FROM [{tableName}] 
                WHERE {columnName} IN (
                    {DeletedUsersSubquery}
                )
            """;
    }
}
 