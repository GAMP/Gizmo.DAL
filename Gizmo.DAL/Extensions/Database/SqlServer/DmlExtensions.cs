using System.Text;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class SqlServer
{
    public static string CleanupScript(bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts)
    {
        string DeleteTable(string tableName) => $"DELETE FROM [{tableName}];";
        string DeleteTableWithReseed(string tableName) => $"DELETE FROM [{tableName}];\nDBCC CHECKIDENT ('{tableName}', RESEED, 1);";
        string SetColumnNull(string tableName, string columnName) => $"UPDATE [{tableName}] SET {columnName} = NULL;";
        string SetColumnNullWithCondition(string tableName, string columnName, string condition) => $"UPDATE [{tableName}] SET {columnName} = NULL WHERE {condition};";

        var script = new StringBuilder();

        // Tables that couple users and hosts — cleared when either is being deleted.
        // AppStat: app usage logged per user/host.
        // HostGroupWaitingLineEntry: queue state linking a user to a host group.
        if (deleteUsers || deleteHosts)
        {
            script.AppendLine("-- Cross-dependency cleanup for users and hosts");
            script.AppendLine(DeleteTable("AppStat"));
            script.AppendLine(DeleteTable("HostGroupWaitingLineEntry"));
        }

        if (deleteHosts && !deleteUsers)
        {
            script.AppendLine("-- Reset user guests when deleting hosts but not users");
            script.AppendLine(SetColumnNullWithCondition("UserGuest", "ReservedHostId", "ReservedHostId IS NOT NULL"));
        }

        // Financial data cleanup always runs (matching original implementation).
        // Reservations are grouped here too: financial tables (InvoiceLine, ProductOL) carry
        // ReservationHostId FKs, so reservation rows can only be removed once those are gone.
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
        script.AppendLine(DeleteTableWithReseed("Usage"));

        script.AppendLine(DeleteTable("UserSessionChange"));
        script.AppendLine(DeleteTable("UserSession"));
        script.AppendLine(DeleteTableWithReseed("UsageSession"));

        script.AppendLine(DeleteTable("RefundInvoicePayment"));
        script.AppendLine(DeleteTable("RefundDepositPayment"));
        script.AppendLine(DeleteTableWithReseed("Refund"));

        script.AppendLine(DeleteTable("VoidInvoice"));
        script.AppendLine(DeleteTable("VoidDepositPayment"));
        script.AppendLine(DeleteTableWithReseed("Void"));

        script.AppendLine(DeleteTableWithReseed("InvoicePayment"));

        script.AppendLine(DeleteTable("PaymentIntentDeposit"));
        script.AppendLine(DeleteTable("PaymentIntentOrder"));
        script.AppendLine(DeleteTableWithReseed("PaymentIntent"));

        script.AppendLine(DeleteTableWithReseed("DepositPayment"));
        script.AppendLine(DeleteTableWithReseed("Payment"));

        script.AppendLine(DeleteTable("InvoiceLineProduct"));
        script.AppendLine(DeleteTable("InvoiceLineSession"));
        script.AppendLine(DeleteTable("InvoiceLineTime"));
        script.AppendLine(DeleteTable("InvoiceLineTimeFixed"));
        script.AppendLine(DeleteTable("InvoiceLineExtended"));
        script.AppendLine(DeleteTableWithReseed("InvoiceLine"));

        script.AppendLine(DeleteTable("InvoiceFiscalReceipt"));
        script.AppendLine(DeleteTableWithReseed("Invoice"));

        script.AppendLine(DeleteTable("ProductOLTimeFixed"));
        script.AppendLine(DeleteTable("ProductOLTime"));
        script.AppendLine(DeleteTable("ProductOLSession"));
        script.AppendLine(DeleteTable("ProductOLProduct"));
        script.AppendLine(DeleteTable("ProductOLExtended"));
        script.AppendLine(DeleteTable("ProductOrderDiscount"));
        script.AppendLine(DeleteTableWithReseed("ProductOL"));

        script.AppendLine(DeleteTable("ReservationProductOrder"));
        script.AppendLine(DeleteTableWithReseed("ProductOrder"));
        script.AppendLine(DeleteTableWithReseed("DepositTransaction"));
        script.AppendLine(DeleteTableWithReseed("PointTransaction"));

        script.AppendLine(DeleteTable("InventoryEntry"));
        script.AppendLine(DeleteTable("InventoryDocument"));
        // Inventory has a restrict FK to Shift (Inventory.ShiftId) — must go before Shift below.
        // Its Cascade children (InventoryEntry, InventoryDocument, InventoryTransfer) are handled above/via cascade.
        script.AppendLine(DeleteTable("Inventory"));
        script.AppendLine(DeleteTable("StockTransaction"));
        script.AppendLine(DeleteTable("ShiftCount"));
        script.AppendLine(DeleteTable("RegisterTransaction"));
        script.AppendLine(DeleteTable("FiscalReceipt"));
        script.AppendLine(DeleteTable("Shift"));
        script.AppendLine(DeleteTable("Register"));
        // Stock: blockers are cleared above — Inventory/StockTransaction/InventoryEntry deleted,
        // InventoryTransfer cascades from Inventory, Register.StockId is SetNull, StockCount cascades from Stock.
        script.AppendLine(DeleteTableWithReseed("Stock"));

        // Reservations — moved here from the top cross-dep section so that financial tables
        // carrying ReservationHostId (InvoiceLine, ProductOL) are already gone by this point.
        script.AppendLine("-- Reservation cleanup (runs after financial tables that reference ReservationHost)");
        script.AppendLine(DeleteTable("ReservationUser"));
        script.AppendLine(DeleteTable("ReservationHost"));
        script.AppendLine(DeleteTable("Reservation"));

        // Products cleanup
        if (deleteProducts)
        {
            script.AppendLine("-- Product cleanup in dependency order");
            var productTables = new[]
            {
                "ProductImage", "ProductTax", "ProductPeriod", "ProductPeriodDayTime",
                "ProductPeriodDay", "ProductTimeHostDisallowed", "ProductUserDisallowed",
                "ProductUserPrice", "BundleProduct", "ProductTimePeriod", "ProductTimePeriodDayTime",
                "ProductTimePeriodDay", "ProductBundle", "Product", "ProductTime", "ProductBaseExtended"
            };

            foreach (var table in productTables)
                script.AppendLine(DeleteTable(table));
            script.AppendLine(DeleteTableWithReseed("ProductBase"));
        }

        if (deleteProducts || deleteHosts)
            script.AppendLine(DeleteTable("ProductHostHidden"));

        // Users cleanup
        if (deleteUsers)
        {
            script.AppendLine("-- User-related data cleanup");
            // HostGroupWaitingLineEntry moved to the cross-dep section — it couples users+hosts
            // and should be wiped whenever either is cleaned, not just on the user path.
            var userTables = new[]
            {
                "AssetTransaction", "AppRating", "UserCreditLimit",
                "UserAttribute", "UserNote", "Note", "VerificationEmail", "VerificationMobilePhone",
                "Verification", "Token"
            };

            foreach (var table in userTables)
                script.AppendLine(DeleteTable(table));
        }

        // Hosts cleanup
        if (deleteHosts)
        {
            script.AppendLine("-- Reset user guests when deleting hosts to avoid foreign key constraint violations");
            script.AppendLine(SetColumnNullWithCondition("UserGuest", "ReservedHostId", "ReservedHostId IS NOT NULL"));
            script.AppendLine();

            script.AppendLine("-- Host cleanup");
            // LicenseKey.AssignedHostId is a restrict FK to HostComputer — null it first.
            script.AppendLine(SetColumnNullWithCondition("LicenseKey", "AssignedHostId", "AssignedHostId IS NOT NULL"));
            script.AppendLine(DeleteTable("HostComputer"));
            script.AppendLine(DeleteTable("HostEndpoint"));
            script.AppendLine(DeleteTableWithReseed("Host"));
        }

        // Operators cleanup with proper foreign key handling
        if (deleteOperators)
        {
            script.AppendLine("-- Clear ALL foreign key references to operators systematically");
            script.AppendLine();

            // Null every CreatedById/ModifiedById FK to [User]/[UserOperator] by querying
            // sys.foreign_keys at runtime. This replaces a hardcoded table list that drifted as
            // new migrations added tables (e.g. HostLayoutGroupImage), causing FK violations
            // when EF later deletes UserOperator rows in SaveChangesAsync.
            script.AppendLine("-- Null CreatedById/ModifiedById on every table that FKs to [User] or [UserOperator]");
            script.AppendLine("""
                DECLARE @nullOperatorRefsSql NVARCHAR(MAX) = N'';
                SELECT @nullOperatorRefsSql = @nullOperatorRefsSql +
                    N'UPDATE [' + OBJECT_SCHEMA_NAME(fk.parent_object_id) + N'].[' + OBJECT_NAME(fk.parent_object_id) +
                    N'] SET [' + c.name + N'] = NULL WHERE [' + c.name + N'] IS NOT NULL;' + CHAR(13) + CHAR(10)
                FROM sys.foreign_keys AS fk
                INNER JOIN sys.foreign_key_columns AS fkc
                    ON fk.object_id = fkc.constraint_object_id
                INNER JOIN sys.columns AS c
                    ON fkc.parent_object_id = c.object_id
                   AND fkc.parent_column_id = c.column_id
                WHERE OBJECT_NAME(fk.referenced_object_id) IN (N'User', N'UserOperator')
                  AND c.name IN (N'CreatedById', N'ModifiedById');
                EXEC sp_executesql @nullOperatorRefsSql;
            """);

            script.AppendLine();
            script.AppendLine("-- Clear specific foreign key references before deleting related entities");
            script.AppendLine(SetColumnNullWithCondition("Host", "HostGroupId", "HostGroupId IS NOT NULL"));
            script.AppendLine(SetColumnNullWithCondition("User", "PermissionSetId", "PermissionSetId IS NOT NULL"));
            script.AppendLine(SetColumnNullWithCondition("AssetTransaction", "CheckedInById", "CheckedInById IS NOT NULL"));
            script.AppendLine();

            script.AppendLine("-- Delete any tokens owned by operators (any type) before deleting the operators themselves");
            script.AppendLine("DELETE FROM [Token] WHERE [UserId] IN (SELECT [UserId] FROM [UserOperator]);");
            script.AppendLine();

            // Clean up entities that genuinely block the operator delete. Only ScheduleReportRecipient
            // has a Restrict FK to UserOperator (via User) that would prevent EF's
            // cx.UsersOperator.RemoveRange(...) from succeeding. Everything else (AgeRestriction,
            // AssistanceRequestType, ClientOptions, Companion, Notification, UserPermissionSet,
            // Branch, Payment, AssetTransaction, HostGroupWaitingLineEntry) is either config/template
            // data that should survive an operator wipe, or already handled by the schema-driven
            // null block above / deleted in other sections.
            script.AppendLine("-- Clean up entities with Restrict FKs to UserOperator");
            script.AppendLine(DeleteTable("ScheduleReportRecipient"));
        }

        return script.ToString();
    }

    public static string CleanupUsersScript()
    {
        static string DeleteFromTableForCleanup(string tableName, string subquery, string columnName = "UserId") => $"""
            DELETE FROM [{tableName}] 
            WHERE {columnName} IN (
                {subquery}
            );
        """;

        static string DeleteFromTableWithJoinForCleanup(string tableName, string joinTableName, string joinColumnName, string subquery, string whereColumnName = "UserId") => $"""
            DELETE FROM [{tableName}] 
            WHERE {joinColumnName} IN (
                SELECT {joinColumnName} 
                FROM [{joinTableName}] 
                WHERE {whereColumnName} IN (
                    {subquery}
                )
            );
        """;

        static string UpdateTableSetNullForCleanup(string tableName, string columnToSetNull, string joinTableName, string joinColumnName, string subquery, string whereColumnName = "UserId") => $"""
            UPDATE [{tableName}] 
            SET {columnToSetNull} = NULL 
            WHERE {joinColumnName} IN (
                SELECT {joinColumnName} 
                FROM [{joinTableName}] 
                WHERE {whereColumnName} IN (
                    {subquery}
                )
            );
        """;

        const string DeletedUsersSubquery = """
            SELECT A.UserId 
            FROM [User] AS A 
            LEFT OUTER JOIN UserGuest AS B ON A.UserId = B.UserId 
            LEFT OUTER JOIN UserOperator AS C ON A.UserId = C.UserId 
            WHERE A.IsDeleted = 1
            AND B.UserId IS NULL 
            AND C.UserId IS NULL
        """;

        var script = new StringBuilder();

        // Pre-delete child rows that reference UserSession/UserMember via restrict FKs,
        // otherwise subsequent DELETE FROM [UserSession]/[UserMember] fails.
        script.AppendLine("-- Pre-delete UserSessionChange rows (restrict FKs to UserSession and UserMember)");
        script.AppendLine($"""
            DELETE FROM [UserSessionChange]
            WHERE UserSessionId IN (
                SELECT UserSessionId
                FROM [UserSession]
                WHERE UserId IN (
                    {DeletedUsersSubquery}
                )
            );
        """);
        script.AppendLine($"""
            DELETE FROM [UserSessionChange]
            WHERE UserId IN (
                {DeletedUsersSubquery}
            );
        """);

        // Null out self-referential bundle-line columns BEFORE deleting InvoiceLine/ProductOL rows,
        // otherwise those rows can't be touched (Extended subtypes reference other InvoiceLine/ProductOL rows).
        script.AppendLine();
        script.AppendLine("-- Null self-referential BundleLineId columns before parent deletes");
        script.AppendLine(UpdateTableSetNullForCleanup("InvoiceLineExtended", "BundleLineId", "InvoiceLine", "InvoiceLineId", DeletedUsersSubquery));
        script.AppendLine(UpdateTableSetNullForCleanup("ProductOLExtended", "BundleLineId", "ProductOL", "ProductOLId", DeletedUsersSubquery));

        // Nested DELETE operations (joined tables) — MUST run before the direct parent deletes
        // below, otherwise parent deletes hit restrict FKs from these children (e.g.
        // RefundInvoicePayment → InvoicePayment, InvoiceLineProduct → InvoiceLine, etc).
        // Subqueries use the parent's UserId to find rows, so parents must still be intact here.
        var joinDeleteMappings = new[]
        {
            ("UsageTime", "Usage", "UsageId"),
            ("UsageTimeFixed", "Usage", "UsageId"),
            ("UsageRate", "Usage", "UsageId"),
            ("UsageUserSession", "Usage", "UsageId"),
            ("RefundInvoicePayment", "InvoicePayment", "InvoicePaymentId"),
            ("RefundDepositPayment", "DepositPayment", "DepositPaymentId"),
            ("Refund", "Payment", "PaymentId"),
            ("Refund", "DepositTransaction", "DepositTransactionId"),
            ("PaymentIntentDeposit", "PaymentIntent", "PaymentIntentId"),
            ("InvoiceLineProduct", "InvoiceLine", "InvoiceLineId"),
            ("InvoiceLineSession", "InvoiceLine", "InvoiceLineId"),
            ("InvoiceLineTime", "InvoiceLine", "InvoiceLineId"),
            ("InvoiceLineTimeFixed", "InvoiceLine", "InvoiceLineId"),
            ("InvoiceLineExtended", "InvoiceLine", "InvoiceLineId"),
            ("ProductOLTimeFixed", "ProductOL", "ProductOLId"),
            ("ProductOLTime", "ProductOL", "ProductOLId"),
            ("ProductOLSession", "ProductOL", "ProductOLId"),
            ("ProductOLProduct", "ProductOL", "ProductOLId"),
            ("ProductOLExtended", "ProductOL", "ProductOLId")
        };

        script.AppendLine();
        script.AppendLine("-- Nested DELETE operations (joined tables)");
        foreach (var (tableName, joinTableName, joinColumnName) in joinDeleteMappings)
            script.AppendLine(DeleteFromTableWithJoinForCleanup(tableName, joinTableName, joinColumnName, DeletedUsersSubquery));

        // Special cases with custom conditions — also run before direct parent deletes.
        script.AppendLine();
        script.AppendLine("-- Special DELETE operations with custom conditions");
        script.AppendLine($"""
            DELETE FROM [UserSessionChange]
            WHERE CreatedById IN (
                {DeletedUsersSubquery}
            );
        """);

        script.AppendLine($"""
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
            );
        """);

        // Basic DELETE operations (direct user ID reference) — runs LAST (before final User delete)
        // so that children from the join/special sections above are already gone and don't block.
        var directDeleteTables = new[]
        {
            "AssetTransaction", "AppStat", "AppRating", "AssistanceRequest",
            "ReservationUser", "Reservation", "UsageSession", "Usage",
            "UserSession", "InvoicePayment", "IntentOrderDeposit", "PaymentIntent",
            "DepositPayment", "Payment", "InvoiceLine", "Invoice", "ProductOL",
            "ProductOrder", "DepositTransaction", "PointTransaction",
            "HostGroupWaitingLineEntry", "UserCreditLimit", "UserAttribute",
            "UserNote", "Verification", "Token", "UserMember"
        };

        script.AppendLine();
        script.AppendLine("-- Basic DELETE operations (direct user ID reference)");
        foreach (var tableName in directDeleteTables)
            script.AppendLine(DeleteFromTableForCleanup(tableName, DeletedUsersSubquery));

        // Final user deletion
        script.AppendLine();
        script.AppendLine("-- Final user deletion");
        script.AppendLine($"""
            DELETE FROM [User] 
            WHERE IsDeleted = 1 
            AND UserId IN (
                {DeletedUsersSubquery}
            );
        """);

        return script.ToString();
    }
}
