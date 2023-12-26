using System;

namespace Gizmo.DAL.Scripts
{
    internal static class NpgSqlScripts
    {
        internal static string GetScript(string scriptName) => scriptName switch
        {
            SQLScripts.SESSION_UPDATE_SQL => SESSION_UPDATE_SQL,
            SQLScripts.LOG_LIMIT_SQL => LOG_LIMIT_SQL,
            SQLScripts.SESSION_BILLED_SPAN_UPDATE => SESSION_BILLED_SPAN_UPDATE,
            SQLScripts.HAS_TABLE_BY_NAME => HAS_TABLE_BY_NAME,
            SQLScripts.TRUNCATE_LOGS => TRUNCATE_LOGS,
            SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERID => DELETE_USERAGREEMENTSTATE_BY_USERID,
            SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID => DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID,
            SQLScripts.CREATE_DEPOSIT_PAYMENT_REFUNDS => CREATE_DEPOSIT_PAYMENT_REFUNDS,
            _ => throw new NotSupportedException($"Script name {scriptName} is not supported for this database provider."),
        };

        private const string CREATE_DEPOSIT_PAYMENT_REFUNDS = """
            BEGIN;

                INSERT INTO "Refund" 
                (
                    "Amount",
                    "DepositTransactionId",
                    "RefundMethodId",
                    "CreatedTime"
                )
                SELECT 
                    "Amount",
                    "DepositTransactionId",
                    -1,
                    "CreatedTime" 
                FROM "DepositTransaction" 
                WHERE 
                    "Type" = 1;

                INSERT INTO "RefundDepositPayment" 
                (
                    "FiscalReceiptStatus",
                    "RefundId"
                )
                SELECT 
                    0,
                    "RefundId" 
                FROM "Refund" AS _refunds 
                WHERE 
                    NOT EXISTS (
                        SELECT "RefundId" 
                        FROM "RefundInvoicePayment" AS _refundInvoice 
                        WHERE _refunds."RefundId" = _refundInvoice."RefundId"
                    );

            COMMIT;
            """;
        private const string SESSION_BILLED_SPAN_UPDATE = """
            UPDATE "UserSession"
            SET 
                "BilledSpan" = "BilledSpan" + @BILL_CYCLE_SECONDS
            WHERE 
                "UserSessionId" = ANY(STRING_TO_ARRAY(@JoinList, ',')::int[]);
            """;
        private const string SESSION_UPDATE_SQL = """
            WITH updated AS (
                UPDATE "UserSession" 
                SET
                    "Span" = "Span" + @SPAN,
                    "PendSpan" = CASE WHEN "State" = 5 THEN "PendSpan" + @SPAN ELSE "PendSpan" END, 
                    "PendSpanTotal" = CASE WHEN "State" = 5 THEN "PendSpanTotal" + @SPAN ELSE "PendSpanTotal" END,
                    "PauseSpan" = CASE WHEN "State" = 9 THEN "PauseSpan" + @SPAN ELSE "PauseSpan" END, 
                    "PauseSpanTotal" = CASE WHEN "State" = 9 THEN "PauseSpanTotal" + @SPAN ELSE "PauseSpanTotal" END,
                    "GraceSpan" = CASE WHEN "State" = 33 THEN "GraceSpan" + @SPAN ELSE "GraceSpan" END, 
                    "GraceSpanTotal" = CASE WHEN "State" = 33 THEN "GraceSpanTotal" + @SPAN ELSE "GraceSpanTotal" END
                WHERE 
                    "State" & 1 = 1
                RETURNING
                    "UserSessionId", "UserId", "HostId", "State", "Span", "BilledSpan", "PendTime", "PendSpan", "EndTime", 
                    "CreatedById", "CreatedTime", "Slot", "PendSpanTotal", "PauseSpan", "PauseSpanTotal", "GraceTime", 
                    "GraceSpan", "GraceSpanTotal"
            )
            SELECT * FROM updated;
            """;
        private const string LOG_LIMIT_SQL = """
            DELETE FROM public."Log"
            WHERE "LogId" IN (
                SELECT "LogId"
                FROM public."Log"
                ORDER BY "LogId"
                LIMIT (
                    SELECT GREATEST(COUNT(*) - @MAX_RECORDS, 0)
                    FROM public."Log"
                )
            );
            """;
        private const string TRUNCATE_LOGS = """
            TRUNCATE TABLE "LogException", "Log" RESTART IDENTITY;
            """;
        private const string DELETE_USERAGREEMENTSTATE_BY_USERID = """
            DELETE FROM "UserAgreementState" WHERE "UserId" = @UserId;
            """;
        private const string DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID = """
            DELETE FROM "UserAgreementState" WHERE "UserAgreementId" = @UserAgreementId;
            """;
        private const string HAS_TABLE_BY_NAME = """
            DROP TABLE IF EXISTS has_table_by_name;
            CREATE TEMP TABLE has_table_by_name (table_name TEXT);
            
            INSERT INTO has_table_by_name (table_name) 
                SELECT table_name 
                FROM information_schema.tables 
                WHERE table_name = @name AND table_schema = 'public'
                LIMIT 1;
            """;
    }
}
