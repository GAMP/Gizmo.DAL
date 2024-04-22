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
            SQLScripts.RESET_USERGUESTS => RESET_USERGUESTS,
            SQLScripts.GET_PAGINATED_PAYMENT_TRANSACTIONS => GET_PAGINATED_PAYMENT_TRANSACTIONS,
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
                UserSessionId IN (SELECT value FROM STRING_SPLIT(@JoinList, ','));
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
            DECLARE @LOG_LIMIT_SQL TABLE (RowsAffected INT);

            DECLARE @CurrentRecords INT;
            SELECT @CurrentRecords = COUNT(*) FROM [Log];

            IF (@CurrentRecords > @MAX_RECORDS)
            BEGIN
                DECLARE @OverLimit INT = @CurrentRecords - @MAX_RECORDS;
                IF (@OverLimit > 0)
                BEGIN
                    DELETE FROM [Log]
                    WHERE LogId IN (
                        SELECT TOP (@OverLimit) LogId 
                        FROM [Log] 
                        ORDER BY LogId ASC
                    );
                END
                ELSE
                BEGIN
                    DELETE FROM @LOG_LIMIT_SQL;
                END
            END
            ELSE
            BEGIN
                DELETE FROM @LOG_LIMIT_SQL;
            END
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
        private const string RESET_USERGUESTS = """
            UPDATE [dbo].[UserGuest] 
            SET IsReserved=0,ReservedHostId=NULL,ReservedSlot=NULL 
            WHERE (IsReserved=1 OR ReservedHostId IS NOT NULL OR ReservedSlot IS NOT NULL);
            """;
        private const string GET_PAGINATED_PAYMENT_TRANSACTIONS = """
            ;WITH PaymentTransactions AS (
                SELECT 
                    'InvoicePayment' AS Type,
                    NULL AS UserId,
                    ip.Amount,
                    ip.CreatedTime AS Date,
                    ip.CreatedById AS OperatorId,
                    ip.ShiftId,
                    ip.RegisterId,
                    ip.InvoiceId,
                    p.PaymentMethodId,
                    NULL AS DepositPaymentId,
                    NULL AS HostId
                FROM InvoicePayment AS ip
                JOIN Payment AS p ON ip.PaymentId = p.PaymentId
                WHERE 
                    ip.CreatedTime >= @DateFrom AND ip.CreatedTime <= @DateTo
                    AND (@ShiftId IS NULL OR ip.ShiftId = @ShiftId)
                    AND (@RegisterId IS NULL OR ip.RegisterId = @RegisterId)
                    AND (@OperatorId IS NULL OR ip.CreatedById = @OperatorId)
                    AND (@UserId IS NULL OR ip.UserId = @UserId)
                    AND (@PaymentMethodId IS NULL OR p.PaymentMethodId = @PaymentMethodId)

                UNION ALL

                SELECT
                    'DepositPayment' AS Type,
                    NULL AS UserId,
                    p.Amount,
                    dp.CreatedTime AS Date,
                    dp.CreatedById AS OperatorId,
                    dp.ShiftId,
                    dp.RegisterId,
                    NULL AS InvoiceId,
                    p.PaymentMethodId,
                    dp.DepositPaymentId,
                    NULL AS HostId
                FROM DepositPayment AS dp
                JOIN Payment AS p ON dp.PaymentId = p.PaymentId
                WHERE 
                    dp.CreatedTime >= @DateFrom AND dp.CreatedTime <= @DateTo
                    AND (@ShiftId IS NULL OR dp.ShiftId = @ShiftId)
                    AND (@RegisterId IS NULL OR dp.RegisterId = @RegisterId)
                    AND (@OperatorId IS NULL OR dp.CreatedById = @OperatorId)
                    AND (@UserId IS NULL OR dp.UserId = @UserId)
                    AND (@PaymentMethodId IS NULL OR p.PaymentMethodId = @PaymentMethodId)

                UNION ALL

                SELECT
                    'InvoicePaymentRefund' AS Type,
                    p.UserId,
                    p.Amount,
                    r.CreatedTime AS Date,
                    r.CreatedById AS OperatorId,
                    r.ShiftId,
                    r.RegisterId,
                    NULL AS InvoiceId,
                    p.PaymentMethodId,
                    NULL AS DepositPaymentId,
                    NULL AS HostId
                FROM RefundInvoicePayment AS rip
                JOIN Refund AS r ON rip.RefundId = r.RefundId
                JOIN Payment AS p ON r.PaymentId = p.PaymentId
                JOIN Invoice AS i ON rip.InvoiceId = i.InvoiceId
                WHERE 
                    r.CreatedTime >= @DateFrom AND r.CreatedTime <= @DateTo
                    AND (@ShiftId IS NULL OR r.ShiftId = @ShiftId)
                    AND (@RegisterId IS NULL OR r.RegisterId = @RegisterId)
                    AND (@OperatorId IS NULL OR r.CreatedById = @OperatorId)
                    AND (@UserId IS NULL OR i.UserId = @UserId)
                    AND (@PaymentMethodId IS NULL OR r.RefundMethodId = @PaymentMethodId)

                UNION ALL

                SELECT
                    'DepositPaymentRefund' AS Type,
                    CASE
                        WHEN p.UserId IS NULL THEN dt.UserId
                        ELSE NULL
                    END AS UserId,
                    r.Amount,
                    r.CreatedTime AS Date,
                    r.CreatedById AS OperatorId,
                    r.ShiftId,
                    r.RegisterId,
                    NULL AS InvoiceId,
                    r.RefundMethodId AS PaymentMethodId,
                    NULL AS DepositPaymentId,
                    NULL AS HostId
                FROM RefundDepositPayment AS rdp
                JOIN Refund AS r ON rdp.RefundId = r.RefundId
                JOIN Payment AS p ON r.PaymentId = p.PaymentId
                JOIN DepositTransaction AS dt ON r.DepositTransactionId = dt.DepositTransactionId
                JOIN DepositPayment AS dp ON rdp.DepositPaymentId = dp.DepositPaymentId
                WHERE 
                    r.CreatedTime >= @DateFrom AND r.CreatedTime <= @DateTo
                    AND (@ShiftId IS NULL OR r.ShiftId = @ShiftId)
                    AND (@RegisterId IS NULL OR r.RegisterId = @RegisterId)
                    AND (@OperatorId IS NULL OR r.CreatedById = @OperatorId)
                    AND (@UserId IS NULL OR dp.UserId = @UserId)
                    AND (@PaymentMethodId IS NULL OR r.RefundMethodId = @PaymentMethodId)

                UNION ALL

                SELECT
                    CASE
                        WHEN rt.Type = 1 THEN 'PayIn'
                        ELSE 'PayOut'
                    END AS Type,
                    NULL AS UserId,
                    rt.Amount,
                    rt.CreatedTime AS Date,
                    rt.CreatedById AS OperatorId,
                    rt.ShiftId,
                    rt.RegisterId,
                    NULL AS InvoiceId,
                    -1 AS PaymentMethodId,
                    NULL AS DepositPaymentId,
                    NULL AS HostId
                FROM RegisterTransaction AS rt
                WHERE 
                    rt.CreatedTime >= @DateFrom AND rt.CreatedTime <= @DateTo
                    AND (rt.Type = 1 OR rt.Type = 2)
                    AND (@ShiftId IS NULL OR rt.ShiftId = @ShiftId)
                    AND (@RegisterId IS NULL OR rt.RegisterId = @RegisterId)
                    AND (@OperatorId IS NULL OR rt.CreatedById = @OperatorId)
            )

            -- This is required to obtain the correct paginated items count

            SELECT 
                (SELECT COUNT(*) FROM PaymentTransactions) AS Total, -- Single subquery for total count
                ISNULL(
                    (
                        SELECT 
                            UserId, 
                            Amount, 
                            PaymentMethodId, 
                            Date, 
                            OperatorId, 
                            ShiftId, 
                            RegisterId, 
                            Type
                        FROM PaymentTransactions
                        ORDER BY Date
                        OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY
                        FOR JSON PATH
                    ),
                    '[]'
                ) AS Items
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
        
        """;
    }
}
