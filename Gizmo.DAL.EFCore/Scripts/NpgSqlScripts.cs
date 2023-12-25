using System;

namespace Gizmo.DAL.Scripts
{
    internal static class NpgSqlScripts
    {
        internal static string GetScript(string scriptName) => scriptName switch
        {
            SQLScripts.SESSION_UPDATE_SQL => SESSION_UPDATE_SQL,
            SQLScripts.LOG_LIMIT_SQL => LOG_LIMIT_SQL,
            //SQLScripts.CREATE_DEPOSIT_PAYMENT_REFUNDS => CREATE_DEPOSIT_PAYMENT_REFUNDS,
            //SQLScripts.SESSION_BILLED_SPAN_UPDATE => SESSION_BILLED_SPAN_UPDATE,
            //SQLScripts.TRUNCATE_LOGS => TRUNCATE_LOGS,
            //SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERID => DELETE_USERAGREEMENTSTATE_BY_USERID,
            //SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID => DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID,
            //SQLScripts.HAS_EF6_MIGRATION_BY_MIGRATIONID => HAS_EF6_MIGRATION_BY_MIGRATIONID,
            //SQLScripts.HAS_TABLE_BY_NAME => HAS_TABLE_BY_NAME,
            _ => throw new NotSupportedException($"Script name {scriptName} is not supported for this database provider."),
        };

        private const string CREATE_DEPOSIT_PAYMENT_REFUNDS = """

            """;
        private const string SESSION_BILLED_SPAN_UPDATE = """

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

            """;
        private const string DELETE_USERAGREEMENTSTATE_BY_USERID = """

            """;
        private const string DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID = """

            """;
        private const string HAS_EF6_MIGRATION_BY_MIGRATIONID = """

            """;
        private const string HAS_TABLE_BY_NAME = """

            """;
    }
}
