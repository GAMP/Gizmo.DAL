using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class PostgreSql
{
    private static string DeleteTable(string tableName) =>
        $"DELETE FROM \"{tableName}\";";

    private static string DeleteTableWithSequenceReset(string tableName)
    {
        var sequenceName = tableName switch
        {
            "ProductBase" => "ProductBase_ProductId_seq",
            _ => $"{tableName}_{tableName}Id_seq"
        };
        return $"DELETE FROM \"{tableName}\";\nALTER SEQUENCE \"{sequenceName}\" RESTART WITH 1;";
    }

    private static string DeleteFromTableWithCondition(string tableName, string condition) =>
        $"DELETE FROM \"{tableName}\" WHERE {condition};";

    private static string SetColumnNull(string tableName, string columnName) =>
        $"UPDATE \"{tableName}\" SET \"{columnName}\" = NULL;";

    private static string SetColumnNullWithCondition(string tableName, string columnName, string condition) =>
        $"UPDATE \"{tableName}\" SET \"{columnName}\" = NULL WHERE {condition};";

    private static string SetOperatorColumnsNull(string tableName) =>
        $"UPDATE \"{tableName}\" SET \"CreatedById\" = NULL, \"ModifiedById\" = NULL WHERE \"CreatedById\" IS NOT NULL OR \"ModifiedById\" IS NOT NULL;";

    private static string SetOperatorColumnsNullWithExtraColumn(string tableName, string additionalColumn) =>
        $"UPDATE \"{tableName}\" SET \"CreatedById\" = NULL, \"ModifiedById\" = NULL, \"{additionalColumn}\" = NULL WHERE \"CreatedById\" IS NOT NULL OR \"ModifiedById\" IS NOT NULL OR \"{additionalColumn}\" IS NOT NULL;";

    public static string CleanupScript(bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts)
    {
        var script = new StringBuilder();

        // Handle cross-dependencies first
        if (deleteUsers || deleteHosts)
        {
            var commonTables = new[]
            {
                "AppStat",
                "ReservationUser",
                "ReservationHost",
                "Reservation"
            };

            script.AppendLine("-- Cross-dependency cleanup for users and hosts");
            foreach (var table in commonTables)
            {
                script.AppendLine(DeleteTable(table));
            }
        }

        // Always clean up financial data when any category is being deleted
        if (deleteUsers || deleteHosts || deleteOperators || deleteProducts)
        {
            script.AppendLine("-- Nullify foreign key references that need to be handled before deletion");
            script.AppendLine(SetColumnNull("InvoiceLineExtended", "BundleLineId"));
            script.AppendLine(SetColumnNull("UsageSession", "CurrentUsageId"));
            script.AppendLine(SetColumnNull("ProductOLExtended", "BundleLineId"));
            script.AppendLine();

            script.AppendLine("-- Financial data cleanup in dependency order");
            script.AppendLine(DeleteTable("UsageRate"));
            script.AppendLine(DeleteTable("UsageTimeFixed"));
            script.AppendLine(DeleteTable("UsageTime"));
            script.AppendLine(DeleteTable("UsageUserSession"));
            script.AppendLine(DeleteTableWithSequenceReset("Usage"));

            script.AppendLine(DeleteTable("UserSessionChange"));
            script.AppendLine(DeleteTable("UserSession"));
            script.AppendLine(DeleteTableWithSequenceReset("UsageSession"));

            script.AppendLine(DeleteTable("RefundInvoicePayment"));
            script.AppendLine(DeleteTable("RefundDepositPayment"));
            script.AppendLine(DeleteTableWithSequenceReset("Refund"));

            script.AppendLine(DeleteTable("VoidInvoice"));
            script.AppendLine(DeleteTable("VoidDepositPayment"));
            script.AppendLine(DeleteTableWithSequenceReset("Void"));

            script.AppendLine(DeleteTableWithSequenceReset("InvoicePayment"));

            script.AppendLine(DeleteTable("PaymentIntentDeposit"));
            script.AppendLine(DeleteTable("PaymentIntentOrder"));
            script.AppendLine(DeleteTableWithSequenceReset("PaymentIntent"));

            script.AppendLine(DeleteTableWithSequenceReset("DepositPayment"));
            script.AppendLine(DeleteTableWithSequenceReset("Payment"));

            script.AppendLine(DeleteTable("InvoiceLineProduct"));
            script.AppendLine(DeleteTable("InvoiceLineSession"));
            script.AppendLine(DeleteTable("InvoiceLineTime"));
            script.AppendLine(DeleteTable("InvoiceLineTimeFixed"));
            script.AppendLine(DeleteTable("InvoiceLineExtended"));
            script.AppendLine(DeleteTableWithSequenceReset("InvoiceLine"));

            script.AppendLine(DeleteTable("InvoiceFiscalReceipt"));
            script.AppendLine(DeleteTableWithSequenceReset("Invoice"));

            script.AppendLine(DeleteTable("ProductOLTimeFixed"));
            script.AppendLine(DeleteTable("ProductOLTime"));
            script.AppendLine(DeleteTable("ProductOLSession"));
            script.AppendLine(DeleteTable("ProductOLProduct"));
            script.AppendLine(DeleteTable("ProductOLExtended"));
            script.AppendLine(DeleteTableWithSequenceReset("ProductOL"));

            script.AppendLine(DeleteTableWithSequenceReset("ProductOrder"));
            script.AppendLine(DeleteTableWithSequenceReset("DepositTransaction"));
            script.AppendLine(DeleteTableWithSequenceReset("PointTransaction"));

            script.AppendLine(DeleteTable("StockTransaction"));
            script.AppendLine(DeleteTable("ShiftCount"));
            script.AppendLine(DeleteTable("RegisterTransaction"));
            script.AppendLine(DeleteTable("FiscalReceipt"));
            script.AppendLine(DeleteTable("Shift"));
            script.AppendLine(DeleteTable("Register"));
        }

        // Products cleanup
        if (deleteProducts)
        {
            script.AppendLine("-- Product cleanup in dependency order");
            var productTables = new[]
            {
                "ProductImage",
                "ProductTax",
                "ProductPeriod",
                "ProductPeriodDayTime",
                "ProductPeriodDay",
                "ProductTimeHostDisallowed",
                "ProductUserDisallowed",
                "ProductUserPrice",
                "BundleProduct",
                "ProductTimePeriod",
                "ProductTimePeriodDayTime",
                "ProductTimePeriodDay",
                "ProductBundle",
                "Product",
                "ProductTime",
                "ProductBaseExtended"
            };

            foreach (var table in productTables)
            {
                script.AppendLine(DeleteTable(table));
            }
            script.AppendLine(DeleteTableWithSequenceReset("ProductBase"));
        }

        if (deleteProducts || deleteHosts)
        {
            script.AppendLine(DeleteTable("ProductHostHidden"));
        }

        // Users cleanup
        if (deleteUsers)
        {
            script.AppendLine("-- User-related data cleanup");
            var userTables = new[]
            {
                "HostGroupWaitingLineEntry",
                "AssetTransaction",
                "AppRating",
                "UserCreditLimit",
                "UserAttribute",
                "UserNote",
                "Note",
                "VerificationEmail",
                "VerificationMobilePhone",
                "Verification",
                "Token"
            };

            foreach (var table in userTables)
            {
                script.AppendLine(DeleteTable(table));
            }
        }

        // Hosts cleanup
        if (deleteHosts)
        {
            script.AppendLine("-- Reset user guests when deleting hosts to avoid foreign key constraint violations");
            script.AppendLine(SetColumnNullWithCondition("UserGuest", "ReservedHostId", "\"ReservedHostId\" IS NOT NULL"));
            script.AppendLine();

            script.AppendLine("-- Host cleanup");
            script.AppendLine(DeleteTable("HostComputer"));
            script.AppendLine(DeleteTable("HostEndpoint"));
            script.AppendLine(DeleteTableWithSequenceReset("Host"));
        }

        // Operators cleanup with proper foreign key handling
        if (deleteOperators)
        {
            script.AppendLine("-- Clear ALL foreign key references to operators systematically");
            script.AppendLine();

            script.AppendLine("-- Core entity updates (CreatedById/ModifiedById columns)");
            var operatorTables = new[]
            {
                "App",
                "AppCategory",
                "AppExe",
                "AppGroup",
                "Attribute",
                "BillProfile",
                "Device",
                "DeviceHost",
                "Feed",
                "Host",
                "HostGroup",
                "MonetaryUnit",
                "News",
                "PaymentMethod",
                "PluginLibrary",
                "ProductBase",
                "ProductGroup",
                "ProductHostHidden",
                "ProductImage",
                "ProductUserDisallowed",
                "Reservation",
                "ReservationHost",
                "ReservationUser",
                "SecurityProfile",
                "Setting",
                "Tax",
                "Token",
                "User",
                "UserAgreement",
                "UserAttribute",
                "UserCredential",
                "UserCreditLimit",
                "UserGroup",
                "UserPermissionSet",
                "UserPicture",
                "Variable"
            };

            foreach (var table in operatorTables)
            {
                if (table == "AssetTransaction")
                {
                    script.AppendLine(SetOperatorColumnsNullWithExtraColumn(table, "CheckedInById"));
                }
                else
                {
                    script.AppendLine(SetOperatorColumnsNull(table));
                }
            }

            script.AppendLine();
            script.AppendLine("-- Clear specific foreign key references before deleting related entities");
            script.AppendLine(SetColumnNullWithCondition("Host", "HostGroupId", "\"HostGroupId\" IS NOT NULL"));
            script.AppendLine(SetColumnNullWithCondition("User", "PermissionSetId", "\"PermissionSetId\" IS NOT NULL"));
            script.AppendLine();

            script.AppendLine("-- Clean up dependent records that would cause foreign key constraint violations");
            script.AppendLine(DeleteTable("HostGroupWaitingLineEntry"));
            script.AppendLine();

            script.AppendLine("-- Delete operator tokens (type 0)");
            script.AppendLine(DeleteFromTableWithCondition("Token", "\"Type\" = 0"));
            script.AppendLine();

            script.AppendLine("-- Clean up operator-specific entities");
            var operatorSpecificTables = new[]
            {
                "AgeRestriction",
                "AssistanceRequestType",
                "Stock",
                "Branch",
                "ClientOptions",
                "Companion",
                "Notification",
                "UserPermissionSet"
            };

            foreach (var table in operatorSpecificTables)
            {
                script.AppendLine(DeleteTable(table));
            }
        }

        return script.ToString();
    }

    public static async Task CleanupUsers(DefaultDbContext cx, CancellationToken ct)
    {
        const string DeletedUsersSubquery =
            """
               SELECT A."UserId" 
               FROM "User" AS A 
               LEFT OUTER JOIN "UserGuest" AS B ON A."UserId" = B."UserId" 
               LEFT OUTER JOIN "UserOperator" AS C ON A."UserId" = C."UserId" 
               WHERE A."IsDeleted" = true
               AND B."UserId" IS NULL 
               AND C."UserId" IS NULL
            """;

        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            // Use a reasonable timeout value for PostgreSQL
            cx.Database.SetCommandTimeout(3600);

            // Basic DELETE operations (direct user ID reference)
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AssetTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AppStat"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AppRating"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AssistanceRequest"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("ReservationUser"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Reservation"), ct);

            // Nested DELETE operations (joined tables)
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageTime", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageTimeFixed", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageRate", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageUserSession", "Usage", "UsageId"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UsageSession"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Usage"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserSessionChange"), ct);

            // Delete based on CreatedById
            await cx.Database.ExecuteSqlRawAsync(
                $"""
                      DELETE FROM "UserSessionChange" 
                      WHERE "CreatedById" IN (
                          {DeletedUsersSubquery}
                      )
                 """, ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserSession"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("RefundInvoicePayment", "InvoicePayment", "InvoicePaymentId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("RefundDepositPayment", "DepositPayment", "DepositPaymentId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("Refund", "Payment", "PaymentId"), ct);

            // Complex join with multiple levels
            await cx.Database.ExecuteSqlRawAsync(
                $"""
                     DELETE FROM "RefundDepositPayment" 
                     WHERE "RefundId" IN (
                         SELECT "RefundId" 
                         FROM "Refund" 
                         WHERE "DepositTransactionId" IN (
                             SELECT "DepositTransactionId" 
                             FROM "DepositTransaction" 
                             WHERE "UserId" IN (
                                 {DeletedUsersSubquery}
                             )
                         )
                     )
                 """, ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("Refund", "DepositTransaction", "DepositTransactionId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("InvoicePayment"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("PaymentIntentDeposit", "PaymentIntent", "PaymentIntentId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("PaymentIntent"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("DepositPayment"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Payment"), ct);

            // Update operations setting NULL values
            await cx.Database.ExecuteSqlRawAsync(
                UpdateTableSetNull(
                    "InvoiceLineExtended",
                    "BundleLineId",
                    "InvoiceLine",
                    "InvoiceLineId"),
                ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineProduct", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineSession", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineTime", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineTimeFixed", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineExtended", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("InvoiceLine"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Invoice"), ct);

            await cx.Database.ExecuteSqlRawAsync(
                UpdateTableSetNull(
                    "ProductOLExtended",
                    "BundleLineId",
                    "ProductOL",
                    "ProductOLId"),
                ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLTimeFixed", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLTime", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLSession", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLProduct", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLExtended", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("ProductOL"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("ProductOrder"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("DepositTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("PointTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("HostGroupWaitingLineEntry"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserCreditLimit"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserAttribute"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("Note", "UserNote", "NoteId"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserNote"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Verification"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Token"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserGuest"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserMember"), ct);

            // Final user deletion
            await cx.Database.ExecuteSqlRawAsync(
                $"""
                    DELETE FROM "User" 
                    WHERE "IsDeleted" = true
                    AND "UserId" IN (
                        {DeletedUsersSubquery}
                    )
                 """, ct);

            cx.ChangeTracker.DetectChanges();
            await cx.SaveChangesAsync(ct);
            await trx.CommitAsync(ct);
        }

        return;

        static string UpdateTableSetNull(
            string tableName,
            string columnToSetNull,
            string joinTableName,
            string joinColumnName,
            string whereColumnName = "UserId") =>
            $"""
                UPDATE "{tableName}" 
                SET "{columnToSetNull}" = NULL 
                WHERE "{joinColumnName}" IN (
                    SELECT "{joinColumnName}" 
                    FROM "{joinTableName}" 
                    WHERE "{whereColumnName}" IN (
                        {DeletedUsersSubquery}
                    )
                )
            """;

        static string DeleteFromTableWithJoin(string tableName, string joinTableName, string joinColumnName, string whereColumnName = "UserId") =>
            $"""
                 DELETE FROM "{tableName}" 
                 WHERE "{joinColumnName}" IN (
                     SELECT "{joinColumnName}" 
                     FROM "{joinTableName}" 
                     WHERE "{whereColumnName}" IN (
                         {DeletedUsersSubquery}
                     )
                 )
             """;

        static string DeleteFromTable(string tableName, string columnName = "UserId") =>
            $"""
                DELETE FROM "{tableName}" 
                WHERE "{columnName}" IN (
                    {DeletedUsersSubquery}
                )
            """;
    }
}
