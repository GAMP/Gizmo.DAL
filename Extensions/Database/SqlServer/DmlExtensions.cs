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
        this DefaultDbContext cx,
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

    public static async Task CleanupUsers(this DefaultDbContext cx, CancellationToken ct)
    {
        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            cx.Database.SetCommandTimeout(int.MaxValue);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[AssetTransaction] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[AppStat] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[AppRating] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[AssistanceRequest] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ReservationUser] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Reservation] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UsageTime] WHERE UsageId IN (SELECT UsageId FROM [dbo].[Usage] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UsageTimeFixed] WHERE UsageId IN (SELECT UsageId FROM [dbo].[Usage] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UsageRate] WHERE UsageId IN (SELECT UsageId FROM [dbo].[Usage] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UsageUserSession] WHERE UsageId IN (SELECT UsageId FROM [dbo].[Usage] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UsageSession] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Usage] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UserSessionChange] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UserSessionChange] WHERE CreatedById IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UserSession] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[RefundInvoicePayment] WHERE InvoicePaymentId IN (SELECT InvoicePaymentId FROM [dbo].[InvoicePayment] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[RefundDepositPayment] WHERE DepositPaymentId IN (SELECT DepositPaymentId FROM [dbo].[DepositPayment] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Refund] WHERE PaymentId IN (SELECT PaymentId FROM [dbo].[Payment] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[RefundDepositPayment] WHERE RefundId IN (SELECT RefundId FROM [dbo].[Refund] WHERE DepositTransactionId IN (SELECT DepositTransactionId FROM [dbo].[DepositTransaction] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL)))",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Refund] WHERE DepositTransactionId IN (SELECT DepositTransactionId FROM [dbo].[DepositTransaction] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoicePayment] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[PaymentIntentDeposit] WHERE PaymentIntentId IN (SELECT PaymentIntentId FROM [dbo].[PaymentIntent] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[PaymentIntent] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[DepositPayment] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Payment] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"UPDATE InvoiceLineExtended SET BundleLineId=NULL WHERE InvoiceLineId IN (SELECT InvoiceLineId FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoiceLineProduct] WHERE InvoiceLineId IN (SELECT InvoiceLineId FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoiceLineSession] WHERE InvoiceLineId IN (SELECT InvoiceLineId FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoiceLineTime] WHERE InvoiceLineId IN (SELECT InvoiceLineId FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoiceLineTimeFixed] WHERE InvoiceLineId IN (SELECT InvoiceLineId FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoiceLineExtended] WHERE InvoiceLineId IN (SELECT InvoiceLineId FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[InvoiceLine] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Invoice] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"UPDATE ProductOLExtended SET BundleLineId=NULL WHERE ProductOLId IN (SELECT ProductOLId FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOLTimeFixed] WHERE ProductOLId IN (SELECT ProductOLId FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOLTime] WHERE ProductOLId IN (SELECT ProductOLId FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOLSession] WHERE ProductOLId IN (SELECT ProductOLId FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOLProduct] WHERE ProductOLId IN (SELECT ProductOLId FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOLExtended] WHERE ProductOLId IN (SELECT ProductOLId FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOL] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[ProductOrder] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[DepositTransaction] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[PointTransaction] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[HostGroupWaitingLineEntry] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UserCreditLimit] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UserAttribute] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            //await cx.Database.ExecuteSqlCommandAsync("DELETE FROM [dbo].[Note] WHERE NoteId IN (SELECT NoteId FROM [dbo].[UserNote] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL));", ct);
            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[UserNote] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Verification] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [dbo].[Token] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            //await cx.Database.ExecuteSqlCommandAsync("DELETE FROM [UserGuest] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);", ct);
            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [UserMember] WHERE UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            await cx.Database.ExecuteSqlAsync(
                $"DELETE FROM [User] WHERE IsDeleted=1 AND UserId IN (SELECT A.UserId FROM [User] AS A LEFT OUTER JOIN [UserGuest] AS B ON A.UserId=B.UserId LEFT OUTER JOIN [UserOperator] AS C ON A.UserId=C.UserId WHERE A.IsDeleted=1 AND B.UserId IS NULL AND C.UserId IS NULL);",
                ct);

            //detect any changes made
            cx.ChangeTracker.DetectChanges();

            //save any changes made
            await cx.SaveChangesAsync(ct);

            //commit changes
            await trx.CommitAsync(ct);
        }
    }
}
