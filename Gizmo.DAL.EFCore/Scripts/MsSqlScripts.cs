using System;

namespace Gizmo.DAL.Scripts
{
    internal static class MsSqlScripts
    {
        internal static string GetScript(string scriptName) => scriptName switch
        {
            SQLScripts.CREATE_DEPOSIT_PAYMENT_REFUNDS => CREATE_DEPOSIT_PAYMENT_REFUNDS,
            SQLScripts.SESSION_BILLED_SPAN_UPDATE => SESSION_BILLED_SPAN_UPDATE,
            SQLScripts.SESSION_UPDATE_SQL => SESSION_UPDATE_SQL,
            SQLScripts.LOG_LIMIT_SQL => LOG_LIMIT_SQL,
            SQLScripts.TRUNCATE_LOGS => TRUNCATE_LOGS,
            SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERID => DELETE_USERAGREEMENTSTATE_BY_USERID,
            SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID => DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID,
            SQLScripts.HAS_EF6_MIGRATION_BY_MIGRATIONID => HAS_EF6_MIGRATION_BY_MIGRATIONID,
            SQLScripts.HAS_TABLE_BY_NAME => HAS_TABLE_BY_NAME,
            _ => throw new NotSupportedException($"Script name {scriptName} is not supported for this database provider."),
        };

        private const string CREATE_DEPOSIT_PAYMENT_REFUNDS = """
            BEGIN TRANSACTION;
                
                INSERT INTO [dbo].[Refund] 
                (
                    Amount
                    , DepositTransactionId
                    , RefundMethodId
                    , CreatedTime
                )
                SELECT 
                    [Amount]
                    , [DepositTransactionId]
                    , -1
                    , [CreatedTime] 
                FROM [dbo].[DepositTransaction] 
                WHERE 
                    Type =1;
            
                INSERT INTO [dbo].[RefundDepositPayment] 
                (
                    FiscalReceiptStatus
                    ,RefundId
                )
                SELECT 
                    0
                    ,RefundId 
                FROM [dbo].[Refund] _refunds 
                WHERE 
                    NOT EXISTS (SELECT RefundId FROM [dbo].[RefundInvoicePayment] _refundInvoice WHERE _refunds.RefundId= _refundInvoice.RefundId);
            
            COMMIT;
        """;
        private const string SESSION_BILLED_SPAN_UPDATE = """
            UPDATE UserSession 
            SET 
                BilledSpan=BilledSpan+@BILL_CYCLE_SECONDS 
            WHERE 
                UserSessionId IN (@JoinList);
            """;
        private const string SESSION_UPDATE_SQL = """
            UPDATE UserSession 
            SET
                Span=Span+@SPAN,
                PendSpan = CASE State WHEN 5 THEN PendSpan+@SPAN ELSE PendSpan END, PendSpanTotal = CASE State WHEN 5 THEN PendSpanTotal+@SPAN ELSE PendSpanTotal END,
                PauseSpan = CASE State WHEN 9 THEN PauseSpan+@SPAN ELSE PauseSpan END, PauseSpanTotal = CASE State WHEN 9 THEN PauseSpanTotal+@SPAN ELSE PauseSpanTotal END,
                GraceSpan = CASE State WHEN 33 THEN GraceSpan+@SPAN ELSE GraceSpan END, GraceSpanTotal = CASE State WHEN 33 THEN GraceSpanTotal+@SPAN ELSE GraceSpanTotal END
            OUTPUT 
                INSERTED.UserSessionId
                ,INSERTED.UserId
                ,INSERTED.HostId
                ,INSERTED.State
                ,INSERTED.Span
                ,INSERTED.BilledSpan
                ,INSERTED.PendTime
                ,INSERTED.PendSpan
                ,INSERTED.EndTime
                ,INSERTED.CreatedById
                ,INSERTED.CreatedTime
                ,INSERTED.Slot
                ,INSERTED.PendSpanTotal
                ,INSERTED.PauseSpan
                ,INSERTED.PauseSpanTotal
                ,INSERTED.GraceTime
                ,INSERTED.GraceSpan
                ,INSERTED.GraceSpanTotal
            WHERE 
                State & 1 = 1;
            """;
        private const string LOG_LIMIT_SQL = """
            DECLARE @CURRENT_RECORDS AS INT = 0;
            
            SELECT @CURRENT_RECORDS = COUNT(*) FROM Log;
            
            IF(@CURRENT_RECORDS > @MAX_RECORDS)
            BEGIN;
                DECLARE @OVER_LIMIT AS INT = @CURRENT_RECORDS - @MAX_RECORDS;
                IF @OVER_LIMIT > 0
                WITH LOG_EXPRESSION AS (SELECT TOP (@OVER_LIMIT) * FROM [Log] ORDER BY LogId ASC) DELETE FROM LOG_EXPRESSION;
                SELECT @OVER_LIMIT;
            END;
        """;
        private const string TRUNCATE_LOGS = """
            TRUNCATE TABLE [LogException];
            ALTER TABLE [LogException] DROP CONSTRAINT  [FK_dbo.LogException_dbo.Log_LogId];
            TRUNCATE TABLE [Log];
            ALTER TABLE [LogException] ADD CONSTRAINT  ""FK_dbo.LogException_dbo.Log_LogId"" FOREIGN KEY(LogId) REFERENCES [Log] (LogId) ON DELETE CASCADE;
            """;
        private const string DELETE_USERAGREEMENTSTATE_BY_USERID = "DELETE FROM UserAgreementState WHERE UserId = @UserId";
        private const string DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID = "DELETE FROM UserAgreementState WHERE UserAgreementId = @UserAgreementId";
        private const string HAS_EF6_MIGRATION_BY_MIGRATIONID = """
            DECLARE @HAS_EF6_MIGRATION_BY_MIGRATIONID TABLE (result BIT);
            IF EXISTS (
                SELECT 1 
                FROM __MigrationHistory 
                WHERE MigrationId = @migrationId
            )
                INSERT INTO @HAS_EF6_MIGRATION_BY_MIGRATIONID VALUES (1);
            ELSE
                DELETE FROM @HAS_EF6_MIGRATION_BY_MIGRATIONID
            """;
        private const string HAS_TABLE_BY_NAME = """
            DECLARE @HAS_TABLE_BY_NAME TABLE (result BIT);
            IF EXISTS (
                SELECT 1 
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = @name
            )
                INSERT INTO @HAS_TABLE_BY_NAME VALUES (1);
            ELSE
                DELETE FROM @HAS_TABLE_BY_NAME
            """;
    }
}
