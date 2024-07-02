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
            SQLScripts.USERS_HARD_DELETE => USERS_HARD_DELETE,
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
                    0 AS Type, --'InvoicePayment'
                    ip.UserId,
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
                    AND (COALESCE(@IncludeInvoicePayments, 1) = 1) --true or default to true if NULL
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 1) --PaymentTransactionDirection.Out

                UNION ALL

                SELECT
                    1 AS Type, --'DepositPayment'
                    dp.UserId,
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
                    AND (COALESCE(@IncludeDepositPayments, 1) = 1) --true or default to true if NULL
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 1) --PaymentTransactionDirection.Out

                UNION ALL

                SELECT
                    3 AS Type, --'RefundInvoicePayment'
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
                    AND (COALESCE(@IncludeInvoiceRefunds, 1) = 1) --true or default to true if NULL
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 0) --PaymentTransactionDirection.In

                UNION ALL

                SELECT
                    4 AS Type, --'RefundDepositPayment'
                    p.UserId,
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
                    AND (COALESCE(@IncludeDepositRefunds, 1) = 1) --true or default to true if NULL
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 0) --PaymentTransactionDirection.In

                UNION ALL

                SELECT
                    CASE
                        WHEN rt.Type = 1    --RegisterTransactionType.PayIn
                            THEN 2              --PaymentTransactionType.PayIn
                            ELSE 5               --PaymentTransactionType.PayOut
                    END AS Type,
                    NULL AS UserId,
                    rt.Amount,
                    rt.CreatedTime AS Date,
                    rt.CreatedById AS OperatorId,
                    rt.ShiftId,
                    rt.RegisterId,
                    NULL AS InvoiceId,
                    -1 AS PaymentMethodId, --register transactions are always made in cash
                    NULL AS DepositPaymentId,
                    NULL AS HostId
                FROM RegisterTransaction AS rt
                WHERE 
                    rt.CreatedTime >= @DateFrom AND rt.CreatedTime <= @DateTo
                    AND (rt.Type = 1 OR rt.Type = 2)
                    AND (@ShiftId IS NULL OR rt.ShiftId = @ShiftId)
                    AND (@RegisterId IS NULL OR rt.RegisterId = @RegisterId)
                    AND (@OperatorId IS NULL OR rt.CreatedById = @OperatorId)
                    AND (COALESCE(@PaymentMethodId, -1) = -1 -- cash or default to cash if NULL
                            AND @UserId IS NOT NULL
                            AND (COALESCE(@IncludePayIns, 1) = 1 -- true
                                    OR COALESCE(@IncludePayOuts, 1) = 1)) -- true
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
                        ORDER BY
                            CASE WHEN @SortBy = 'Date' AND @SortOrder = 'ASC' THEN Date END ASC,
                            CASE WHEN @SortBy = 'Date' AND @SortOrder = 'DESC' THEN Date END DESC,
                            CASE WHEN @SortBy = 'Amount' AND @SortOrder = 'ASC' THEN Amount END ASC,
                            CASE WHEN @SortBy = 'Amount' AND @SortOrder = 'DESC' THEN Amount END DESC,
                            CASE WHEN @SortBy = 'UserId' AND @SortOrder = 'ASC' THEN UserId END ASC,
                            CASE WHEN @SortBy = 'UserId' AND @SortOrder = 'DESC' THEN UserId END DESC,
                            CASE WHEN @SortBy = 'PaymentMethodId' AND @SortOrder = 'ASC' THEN PaymentMethodId END ASC,
                            CASE WHEN @SortBy = 'PaymentMethodId' AND @SortOrder = 'DESC' THEN PaymentMethodId END DESC,
                            CASE WHEN @SortBy = 'OperatorId' AND @SortOrder = 'ASC' THEN OperatorId END ASC,
                            CASE WHEN @SortBy = 'OperatorId' AND @SortOrder = 'DESC' THEN OperatorId END DESC,
                            CASE WHEN @SortBy = 'ShiftId' AND @SortOrder = 'ASC' THEN ShiftId END ASC,
                            CASE WHEN @SortBy = 'ShiftId' AND @SortOrder = 'DESC' THEN ShiftId END DESC,
                            CASE WHEN @SortBy = 'RegisterId' AND @SortOrder = 'ASC' THEN RegisterId END ASC,
                            CASE WHEN @SortBy = 'RegisterId' AND @SortOrder = 'DESC' THEN RegisterId END DESC,
                            CASE WHEN @SortBy = 'Type' AND @SortOrder = 'ASC' THEN Type END ASC,
                            CASE WHEN @SortBy = 'Type' AND @SortOrder = 'DESC' THEN Type END DESC
                        OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY
                        FOR JSON PATH
                    ),
                    '[]'
                ) AS Items
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
        
        """;
        private const string USERS_HARD_DELETE =
        """
            -- if compatibility level is less than 130, set it to 130 to use STRING_SPLIT function
            --check--
            --SELECT compatibility_level
            --FROM sys.databases
            --WHERE name = 'Gizmo';
            --alter--
            --ALTER DATABASE Gizmo
            --SET COMPATIBILITY_LEVEL = 130;

            DECLARE @UserIdList TABLE (UserId INT);

            INSERT INTO @UserIdList (UserId)
            SELECT value
            FROM STRING_SPLIT(@UserIds, ',');

            BEGIN TRANSACTION;
                BEGIN TRY

                    DELETE FROM UsageSession
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE ucl
                    FROM UserCreditLimit AS ucl
                    INNER JOIN UserMember AS u ON ucl.UserId = u.UserId
                    WHERE u.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE r
                    FROM Refund AS r
                    INNER JOIN DepositTransaction AS dt ON r.DepositTransactionId = dt.DepositTransactionId
                    WHERE dt.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE r
                    FROM Refund AS r
                    INNER JOIN Payment AS p ON r.PaymentId = p.PaymentId
                    WHERE p.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE vmp
                    FROM VerificationMobilePhone AS vmp
                    INNER JOIN Verification AS v ON vmp.VerificationId = v.VerificationId
                    WHERE v.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE ve
                    FROM VerificationEmail AS ve
                    INNER JOIN Verification AS v ON ve.VerificationId = v.VerificationId
                    WHERE v.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM Verification
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE ut
                    FROM UsageTime AS ut
                    INNER JOIN Usage AS u ON ut.UsageId = u.UsageId
                    WHERE u.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE utf
                    FROM UsageTimeFixed AS utf
                    INNER JOIN Usage AS u ON utf.UsageId = u.UsageId
                    WHERE u.UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM Usage
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM InvoicePayment
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM PaymentIntent
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM Payment
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM AssistanceRequest
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM HostGroupWaitingLineEntry
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserAgreementState
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserAttribute
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserPermission
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserNote
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM ReservationUser
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM ReservationHost
                    WHERE PreferedUserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM Reservation
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM Token
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM AppRating
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM AppStat
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM AssetTransaction
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM DepositTransaction
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM PointTransaction
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserSessionChange
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserSession
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM ProductOrder
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM InvoiceLine
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM Invoice
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    DELETE FROM UserMember
                        OUTPUT DELETED.UserId
                        ,OUTPUT DELETED.UserName
                        ,OUTPUT DELETED.Email
                        ,OUTPUT DELETED.UserGroupId
                        ,OUTPUT DELETED.IsNegativeBalanceAllowed
                        ,OUTPUT DELETED.IsPersonalInfoRequested
                        ,OUTPUT DELETED.BillingOptions
                        ,OUTPUT DELETED.EnableDate
                        ,OUTPUT DELETED.DisableDate
                    WHERE UserId IN (SELECT UserId FROM @UserIdList);

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                    THROW;
                END CATCH;
        """;
        private const string USERS_HARD_DELETE_DATA = """
            DECLARE @UserId INT = 12;

            SELECT TableName, UserId, COUNT(*) AS DeletingCount
            FROM (
        	    SELECT 'UsageSession' AS TableName, UserId 
        	    FROM UsageSession 

        	    UNION ALL

        	    SELECT 'UserCreditLimit' AS TableName, ucl.UserId 
        	    FROM UserCreditLimit AS ucl
        	    INNER JOIN UserMember AS u ON ucl.UserId = u.UserId

        	    UNION ALL

        	    SELECT 'Refund_DepositTransaction' AS TableName, dt.UserId
        	    FROM Refund AS r
        	    INNER JOIN DepositTransaction AS dt ON r.DepositTransactionId = dt.DepositTransactionId

        	    UNION ALL

        	    SELECT 'Refund_Payment' AS TableName, p.UserId
        	    FROM Refund AS r
        	    INNER JOIN Payment AS p ON r.PaymentId = p.PaymentId

        	    UNION ALL

        	    SELECT 'VerificationMobilePhone' AS TableName, v.UserId
        	    FROM VerificationMobilePhone AS vmp
        	    INNER JOIN Verification AS v ON vmp.VerificationId = v.VerificationId

        	    UNION ALL

        	    SELECT 'VerificationEmail' AS TableName, v.UserId
        	    FROM VerificationEmail AS ve
        	    INNER JOIN Verification AS v ON ve.VerificationId = v.VerificationId

        	    UNION ALL

        	    SELECT 'Verification' AS TableName, UserId 
        	    FROM Verification 

        	    UNION ALL

        	    SELECT 'UsageTime' AS TableName, u.UserId
        	    FROM UsageTime AS ut
        	    INNER JOIN Usage AS u ON ut.UsageId = u.UsageId

        	    UNION ALL

        	    SELECT 'UsageTimeFixed' AS TableName, u.UserId
        	    FROM UsageTimeFixed AS utf
        	    INNER JOIN Usage AS u ON utf.UsageId = u.UsageId

        	    UNION ALL

        	    SELECT 'Usage' AS TableName, UserId 
        	    FROM Usage 

        	    UNION ALL

        	    SELECT 'InvoicePayment' AS TableName, UserId 
        	    FROM InvoicePayment 

        	    UNION ALL

        	    SELECT 'PaymentIntent' AS TableName, UserId 
        	    FROM PaymentIntent 

        	    UNION ALL

        	    SELECT 'Payment' AS TableName, UserId 
        	    FROM Payment 

        	    UNION ALL

        	    SELECT 'AssistanceRequest' AS TableName, UserId 
        	    FROM AssistanceRequest 

        	    UNION ALL

        	    SELECT 'HostGroupWaitingLineEntry' AS TableName, UserId 
        	    FROM HostGroupWaitingLineEntry 

        	    UNION ALL

        	    SELECT 'UserAgreementState' AS TableName, UserId 
        	    FROM UserAgreementState 

        	    UNION ALL

        	    SELECT 'UserAttribute' AS TableName, UserId 
        	    FROM UserAttribute 

        	    UNION ALL

        	    SELECT 'UserPermission' AS TableName, UserId 
        	    FROM UserPermission 

        	    UNION ALL

        	    SELECT 'UserNote' AS TableName, UserId 
        	    FROM UserNote 

        	    UNION ALL

        	    SELECT 'ReservationUser' AS TableName, UserId 
        	    FROM ReservationUser 

        	    UNION ALL

        	    SELECT 'ReservationHost' AS TableName, PreferedUserId AS UserId 
        	    FROM ReservationHost 

        	    UNION ALL

        	    SELECT 'Reservation' AS TableName, UserId 
        	    FROM Reservation 

        	    UNION ALL

        	    SELECT 'Token' AS TableName, UserId 
        	    FROM Token 

        	    UNION ALL

        	    SELECT 'AppRating' AS TableName, UserId 
        	    FROM AppRating 

        	    UNION ALL

        	    SELECT 'AppStat' AS TableName, UserId 
        	    FROM AppStat 

        	    UNION ALL

        	    SELECT 'AssetTransaction' AS TableName, UserId 
        	    FROM AssetTransaction 

        	    UNION ALL

        	    SELECT 'DepositTransaction' AS TableName, UserId 
        	    FROM DepositTransaction 

        	    UNION ALL

        	    SELECT 'PointTransaction' AS TableName, UserId 
        	    FROM PointTransaction 

        	    UNION ALL

        	    SELECT 'UserSessionChange' AS TableName, UserId 
        	    FROM UserSessionChange 

        	    UNION ALL

        	    SELECT 'UserSession' AS TableName, UserId 
        	    FROM UserSession 

        	    UNION ALL

        	    SELECT 'ProductOrder' AS TableName, UserId 
        	    FROM ProductOrder 

        	    UNION ALL

        	    SELECT 'InvoiceLine' AS TableName, UserId 
        	    FROM InvoiceLine 

        	    UNION ALL

        	    SELECT 'Invoice' AS TableName, UserId 
        	    FROM Invoice 

        	    UNION ALL

        	    SELECT 'UserMember' AS TableName, UserId 
        	    FROM UserMember 

            ) AS DeletedRecords
            WHERE 
            (@UserId IS NULL OR UserId = @UserId)
            GROUP BY TableName, UserId
            ORDER BY DeletingCount;
        """;
    }
}
