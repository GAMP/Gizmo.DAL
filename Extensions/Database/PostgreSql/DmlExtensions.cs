using System.Text;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class PostgreSql
{
    public static string CleanupScript(bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts)
    {
        static string DeleteTable(string tableName) => $"DELETE FROM \"{tableName}\";";
        static string DeleteTableWithSequenceReset(string tableName, string sequenceName = null)
        {
            sequenceName ??= GetConventionalSequenceName(tableName);
            return $"DELETE FROM \"{tableName}\";\nALTER SEQUENCE \"{sequenceName}\" RESTART WITH 1;";
        }
        static string GetConventionalSequenceName(string tableName) => tableName switch
        {
            "Usage" => "Usage_UsageId_seq",
            "UsageSession" => "UsageSession_UsageSessionId_seq",
            "Refund" => "Refund_RefundId_seq",
            "Void" => "Void_VoidId_seq",
            "InvoicePayment" => "InvoicePayment_InvoicePaymentId_seq",
            "PaymentIntent" => "PaymentIntent_PaymentIntentId_seq",
            "DepositPayment" => "DepositPayment_DepositPaymentId_seq",
            "Payment" => "Payment_PaymentId_seq",
            "InvoiceLine" => "InvoiceLine_InvoiceLineId_seq",
            "Invoice" => "Invoice_InvoiceId_seq",
            "ProductOL" => "ProductOL_ProductOLId_seq",
            "ProductOrder" => "ProductOrder_ProductOrderId_seq",
            "DepositTransaction" => "DepositTransaction_DepositTransactionId_seq",
            "PointTransaction" => "PointTransaction_PointTransactionId_seq",
            "ProductBase" => "ProductBase_ProductId_seq",
            "Host" => "Host_HostId_seq",
            _ => $"{tableName}_{tableName}Id_seq"
        };
        static string DeleteFromTableWithCondition(string tableName, string condition) => $"DELETE FROM \"{tableName}\" WHERE {condition};";
        static string SetColumnNull(string tableName, string columnName) => $"UPDATE \"{tableName}\" SET \"{columnName}\" = NULL;";
        static string SetColumnNullWithCondition(string tableName, string columnName, string condition) => $"UPDATE \"{tableName}\" SET \"{columnName}\" = NULL WHERE {condition};";
        static string SetOperatorColumnsNull(string tableName) => $"UPDATE \"{tableName}\" SET \"CreatedById\" = NULL, \"ModifiedById\" = NULL WHERE \"CreatedById\" IS NOT NULL OR \"ModifiedById\" IS NOT NULL;";
        static string SetOperatorColumnsNullWithExtraColumn(string tableName, string additionalColumn) => $"UPDATE \"{tableName}\" SET \"CreatedById\" = NULL, \"ModifiedById\" = NULL, \"{additionalColumn}\" = NULL WHERE \"CreatedById\" IS NOT NULL OR \"ModifiedById\" IS NOT NULL OR \"{additionalColumn}\" IS NOT NULL;";

        var script = new StringBuilder();

        // Handle cross-dependencies first
        if (deleteUsers || deleteHosts)
        {
            var commonTables = new[] { "AppStat", "ReservationUser", "ReservationHost", "Reservation" };
            script.AppendLine("-- Cross-dependency cleanup for users and hosts");
            foreach (var table in commonTables)
                script.AppendLine(DeleteTable(table));
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
                "ProductImage", "ProductTax", "ProductPeriod", "ProductPeriodDayTime",
                "ProductPeriodDay", "ProductTimeHostDisallowed", "ProductUserDisallowed",
                "ProductUserPrice", "BundleProduct", "ProductTimePeriod", "ProductTimePeriodDayTime",
                "ProductTimePeriodDay", "ProductBundle", "Product", "ProductTime", "ProductBaseExtended"
            };

            foreach (var table in productTables)
                script.AppendLine(DeleteTable(table));
            script.AppendLine(DeleteTableWithSequenceReset("ProductBase"));
        }

        if (deleteProducts || deleteHosts)
            script.AppendLine(DeleteTable("ProductHostHidden"));

        // Users cleanup
        if (deleteUsers)
        {
            script.AppendLine("-- User-related data cleanup");
            var userTables = new[]
            {
                "HostGroupWaitingLineEntry", "AssetTransaction", "AppRating", "UserCreditLimit",
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
                "App", "AppCategory", "AppExe", "AppGroup", "Attribute", "BillProfile",
                "Device", "DeviceHost", "Feed", "Host", "HostGroup", "MonetaryUnit",
                "News", "PaymentMethod", "PluginLibrary", "ProductBase", "ProductGroup",
                "ProductHostHidden", "ProductImage", "ProductUserDisallowed", "Reservation",
                "ReservationHost", "ReservationUser", "SecurityProfile", "Setting", "Tax",
                "Token", "User", "UserAgreement", "UserAttribute", "UserCredential",
                "UserCreditLimit", "UserGroup", "UserPermissionSet", "UserPicture", "Variable"
            };

            foreach (var table in operatorTables)
            {
                if (table == "AssetTransaction")
                    script.AppendLine(SetOperatorColumnsNullWithExtraColumn(table, "CheckedInById"));
                else
                    script.AppendLine(SetOperatorColumnsNull(table));
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
                "AgeRestriction", "AssistanceRequestType", "Stock", "Branch",
                "ClientOptions", "Companion", "Notification", "UserPermissionSet"
            };

            foreach (var table in operatorSpecificTables)
                script.AppendLine(DeleteTable(table));
        }

        return script.ToString();
    }

    public static string CleanupUsersScript()
    {
        static string DeleteFromTableForCleanup(string tableName, string subquery, string columnName = "UserId") =>
            $"""
            DELETE FROM "{tableName}" 
            WHERE "{columnName}" IN (
                {subquery}
            );
            """;

        static string DeleteFromTableWithJoinForCleanup(string tableName, string joinTableName, string joinColumnName, string subquery, string whereColumnName = "UserId") =>
            $"""
            DELETE FROM "{tableName}" 
            WHERE "{joinColumnName}" IN (
                SELECT "{joinColumnName}" 
                FROM "{joinTableName}" 
                WHERE "{whereColumnName}" IN (
                    {subquery}
                )
            );
            """;

        static string UpdateTableSetNullForCleanup(string tableName, string columnToSetNull, string joinTableName, string joinColumnName, string subquery, string whereColumnName = "UserId") =>
            $"""
            UPDATE "{tableName}" 
            SET "{columnToSetNull}" = NULL 
            WHERE "{joinColumnName}" IN (
                SELECT "{joinColumnName}" 
                FROM "{joinTableName}" 
                WHERE "{whereColumnName}" IN (
                    {subquery}
                )
            );
            """;

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

        var script = new StringBuilder();

        // Basic DELETE operations (direct user ID reference)
        var directDeleteTables = new[]
        {
            "AssetTransaction", "AppStat", "AppRating", "AssistanceRequest", 
            "ReservationUser", "Reservation", "UsageSession", "Usage", 
            "UserSessionChange", "UserSession", "InvoicePayment", "PaymentIntent", 
            "DepositPayment", "Payment", "InvoiceLine", "Invoice", "ProductOL", 
            "ProductOrder", "DepositTransaction", "PointTransaction", 
            "HostGroupWaitingLineEntry", "UserCreditLimit", "UserAttribute", 
            "UserNote", "Verification", "Token", "UserMember"
        };

        script.AppendLine("-- Basic DELETE operations (direct user ID reference)");
        foreach (var tableName in directDeleteTables)
            script.AppendLine(DeleteFromTableForCleanup(tableName, DeletedUsersSubquery));

        // Nested DELETE operations (joined tables)
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

        // Special cases with custom conditions
        script.AppendLine();
        script.AppendLine("-- Special DELETE operations with custom conditions");
        script.AppendLine($"""
            DELETE FROM "UserSessionChange" 
            WHERE "CreatedById" IN (
                {DeletedUsersSubquery}
            );
            """);

        script.AppendLine($"""
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
            );
            """);

        // Update operations setting NULL values
        script.AppendLine();
        script.AppendLine("-- Update operations setting NULL values");
        script.AppendLine(UpdateTableSetNullForCleanup("InvoiceLineExtended", "BundleLineId", "InvoiceLine", "InvoiceLineId", DeletedUsersSubquery));
        script.AppendLine(UpdateTableSetNullForCleanup("ProductOLExtended", "BundleLineId", "ProductOL", "ProductOLId", DeletedUsersSubquery));

        // Final user deletion
        script.AppendLine();
        script.AppendLine("-- Final user deletion");
        script.AppendLine($"""
            DELETE FROM "User" 
            WHERE "IsDeleted" = true 
            AND "UserId" IN (
                {DeletedUsersSubquery}
            );
            """);

        return script.ToString();
    }
}
