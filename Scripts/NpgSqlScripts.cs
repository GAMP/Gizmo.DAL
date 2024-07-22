using System;

namespace Gizmo.DAL.Scripts
{
    internal static class NpgSqlScripts
    {
        internal static string GetScript(string scriptName) => scriptName switch
        {
            SQLScripts.APPLY_SPECIFIC_DATABASE_SETTINGS => APPLY_SPECIFIC_SETTINGS,
            SQLScripts.SESSION_UPDATE_SQL => SESSION_UPDATE_SQL,
            SQLScripts.LOG_LIMIT_SQL => LOG_LIMIT_SQL,
            SQLScripts.SESSION_BILLED_SPAN_UPDATE => SESSION_BILLED_SPAN_UPDATE,
            SQLScripts.HAS_TABLE_BY_NAME => HAS_TABLE_BY_NAME,
            SQLScripts.TRUNCATE_LOGS => TRUNCATE_LOGS,
            SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERID => DELETE_USERAGREEMENTSTATE_BY_USERID,
            SQLScripts.DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID => DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID,
            SQLScripts.CREATE_DEPOSIT_PAYMENT_REFUNDS => CREATE_DEPOSIT_PAYMENT_REFUNDS,
            SQLScripts.RESET_USERGUESTS => RESET_USERGUESTS,
            SQLScripts.GET_PAGINATED_PAYMENT_TRANSACTIONS => GET_PAGINATED_PAYMENT_TRANSACTIONS,
            SQLScripts.USERS_HARD_DELETE => USERS_HARD_DELETE,
            _ => throw new NotSupportedException($"Script name {scriptName} is not supported for this database provider."),
        };

        private const string APPLY_SPECIFIC_SETTINGS = """
            -- Create a temporary table to return success result
            CREATE TEMP TABLE temp_table (id INT);
            UPDATE temp_table SET id = id WHERE 1 = 0;
            DROP TABLE temp_table;
        """;
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
            DELETE FROM "Log"
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
        private const string RESET_USERGUESTS = """
            UPDATE "UserGuest"
            SET "IsReserved" = 0, "ReservedHostId" = NULL, "ReservedSlot" = NULL
            WHERE ("IsReserved" = 1 OR "ReservedHostId" IS NOT NULL OR "ReservedSlot" IS NOT NULL);
            """;
        private const string GET_PAGINATED_PAYMENT_TRANSACTIONS = """
            ------------------------------------------------------------------------
            WITH myconstants (
                DateFrom, DateTo, ShiftId, RegisterId, OperatorId, UserId, PaymentMethodId, 
                IncludeInvoicePayments, IncludeDepositPayments, IncludeInvoiceRefunds, IncludeDepositRefunds, 
                IncludePayIns, IncludePayOuts, PaymentDirection, SortBy, SortOrder, Offset1, Limit1
            ) AS (
                VALUES (
                    '1970-01-01 00:00:00'::timestamp, '9999-12-31 23:59:59'::timestamp, NULL::int, NULL::int, NULL::int, NULL::int, 
                    NULL::int, true, true, true, true, true, true, NULL::int, 'Date', 'ASC', 0, 100
                )
            ),
            pt AS (
                SELECT 
                    0 AS "Type", --'InvoicePayment'
                    ip."UserId",
                    ip."Amount",
                    ip."CreatedTime" AS "Date",
                    ip."CreatedById" AS "OperatorId",
                    ip."ShiftId",
                    ip."RegisterId",
                    ip."InvoiceId",
                    p."PaymentMethodId",
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "InvoicePayment" AS ip
                JOIN "Payment" AS p ON ip."PaymentId" = p."PaymentId"
                JOIN myconstants mc ON true
                WHERE 
                    ip."CreatedTime" BETWEEN mc.DateFrom AND mc.DateTo
                    AND (mc.ShiftId IS NULL OR ip."ShiftId" = mc.ShiftId)
                    AND (mc.RegisterId IS NULL OR ip."RegisterId" = mc.RegisterId)
                    AND (mc.OperatorId IS NULL OR ip."CreatedById" = mc.OperatorId)
                    AND (mc.UserId IS NULL OR ip."UserId" = mc.UserId)
                    AND (mc.PaymentMethodId IS NULL OR p."PaymentMethodId" = mc.PaymentMethodId)
                    AND (COALESCE(mc.IncludeInvoicePayments, true)) 
                    AND (mc.PaymentDirection IS NULL OR mc.PaymentDirection != 1) --PaymentTransactionDirection.Out

                UNION ALL

                SELECT
                    1 AS "Type", --'DepositPayment'
                    dp."UserId",
                    p."Amount",
                    dp."CreatedTime" AS "Date",
                    dp."CreatedById" AS "OperatorId",
                    dp."ShiftId",
                    dp."RegisterId",
                    NULL AS "InvoiceId",
                    p."PaymentMethodId",
                    dp."DepositPaymentId",
                    NULL AS "HostId"
                FROM "DepositPayment" AS dp
                JOIN "Payment" AS p ON dp."PaymentId" = p."PaymentId"
                JOIN myconstants mc ON true
                WHERE 
                    dp."CreatedTime" BETWEEN mc.DateFrom AND mc.DateTo
                    AND (mc.ShiftId IS NULL OR dp."ShiftId" = mc.ShiftId)
                    AND (mc.RegisterId IS NULL OR dp."RegisterId" = mc.RegisterId)
                    AND (mc.OperatorId IS NULL OR dp."CreatedById" = mc.OperatorId)
                    AND (mc.UserId IS NULL OR dp."UserId" = mc.UserId)
                    AND (mc.PaymentMethodId IS NULL OR p."PaymentMethodId" = mc.PaymentMethodId)
                    AND (COALESCE(mc.IncludeDepositPayments, true))
                    AND (mc.PaymentDirection IS NULL OR mc.PaymentDirection != 1) --PaymentTransactionDirection.Out

                UNION ALL

                SELECT
                    3 AS "Type", --'RefundInvoicePayment'
                    p."UserId",
                    p."Amount",
                    r."CreatedTime" AS "Date",
                    r."CreatedById" AS "OperatorId",
                    r."ShiftId",
                    r."RegisterId",
                    NULL AS "InvoiceId",
                    p."PaymentMethodId",
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "RefundInvoicePayment" AS rip
                JOIN "Refund" AS r ON rip."RefundId" = r."RefundId"
                JOIN "Payment" AS p ON r."PaymentId" = p."PaymentId"
                JOIN "Invoice" AS i ON rip."InvoiceId" = i."InvoiceId"
                JOIN myconstants mc ON true
                WHERE 
                    r."CreatedTime" BETWEEN mc.DateFrom AND mc.DateTo
                    AND (mc.ShiftId IS NULL OR r."ShiftId" = mc.ShiftId)
                    AND (mc.RegisterId IS NULL OR r."RegisterId" = mc.RegisterId)
                    AND (mc.OperatorId IS NULL OR r."CreatedById" = mc.OperatorId)
                    AND (mc.UserId IS NULL OR i."UserId" = mc.UserId)
                    AND (mc.PaymentMethodId IS NULL OR r."RefundMethodId" = mc.PaymentMethodId)
                    AND (COALESCE(mc.IncludeInvoiceRefunds, true))
                    AND (mc.PaymentDirection IS NULL OR mc.PaymentDirection != 0) --PaymentTransactionDirection.In

                UNION ALL

                SELECT
                    4 AS "Type", --'RefundDepositPayment'
                    p."UserId",
                    r."Amount",
                    r."CreatedTime" AS "Date",
                    r."CreatedById" AS "OperatorId",
                    r."ShiftId",
                    r."RegisterId",
                    NULL AS "InvoiceId",
                    r."RefundMethodId" AS "PaymentMethodId",
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "RefundDepositPayment" AS rdp
                JOIN "Refund" AS r ON rdp."RefundId" = r."RefundId"
                JOIN "Payment" AS p ON r."PaymentId" = p."PaymentId"
                JOIN "DepositTransaction" AS dt ON r."DepositTransactionId" = dt."DepositTransactionId"
                JOIN "DepositPayment" AS dp ON rdp."DepositPaymentId" = dp."DepositPaymentId"
                JOIN myconstants mc ON true
                WHERE 
                    r."CreatedTime" BETWEEN mc.DateFrom AND mc.DateTo
                    AND (mc.ShiftId IS NULL OR r."ShiftId" = mc.ShiftId)
                    AND (mc.RegisterId IS NULL OR r."RegisterId" = mc.RegisterId)
                    AND (mc.OperatorId IS NULL OR r."CreatedById" = mc.OperatorId)
                    AND (mc.UserId IS NULL OR dp."UserId" = mc.UserId)
                    AND (mc.PaymentMethodId IS NULL OR r."RefundMethodId" = mc.PaymentMethodId)
                    AND (COALESCE(mc.IncludeDepositRefunds, true))
                    AND (mc.PaymentDirection IS NULL OR mc.PaymentDirection != 0) --PaymentTransactionDirection.In

                UNION ALL

                SELECT
                    CASE
                        WHEN rt."Type" = 1 THEN 2 -- RegisterTransactionType.PayIn to PaymentTransactionType.PayIn
                        ELSE 5 -- RegisterTransactionType.PayOut to PaymentTransactionType.PayOut
                    END AS "Type",
                    NULL AS "UserId",
                    rt."Amount",
                    rt."CreatedTime" AS "Date",
                    rt."CreatedById" AS "OperatorId",
                    rt."ShiftId",
                    rt."RegisterId",
                    NULL AS "InvoiceId",
                    -1 AS "PaymentMethodId", -- register transactions are always made in cash
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "RegisterTransaction" AS rt
                JOIN myconstants mc ON true
                WHERE 
                    rt."CreatedTime" BETWEEN mc.DateFrom AND mc.DateTo
                    AND (rt."Type" = 1 OR rt."Type" = 2)
                    AND (mc.ShiftId IS NULL OR rt."ShiftId" = mc.ShiftId)
                    AND (mc.RegisterId IS NULL OR rt."RegisterId" = mc.RegisterId)
                    AND (mc.OperatorId IS NULL OR rt."CreatedById" = mc.OperatorId)
                    AND (COALESCE(mc.PaymentMethodId, -1) = -1 -- cash or default to cash if NULL
                        AND mc.UserId IS NOT NULL
                        AND (COALESCE(mc.IncludePayIns, true) OR COALESCE(mc.IncludePayOuts, true))))
            SELECT 
                "UserId", 
                "Amount", 
                "PaymentMethodId", 
                "Date", 
                "OperatorId", 
                "ShiftId", 
                "RegisterId", 
                "Type"
            FROM pt
            JOIN myconstants AS mc ON true
            ORDER BY
                CASE WHEN mc.SortBy = 'Date' AND mc.SortOrder = 'ASC' THEN "Date" END ASC,
                CASE WHEN mc.SortBy = 'Date' AND mc.SortOrder = 'DESC' THEN "Date" END DESC,
                CASE WHEN mc.SortBy = 'Amount' AND mc.SortOrder = 'ASC' THEN "Amount" END ASC,
                CASE WHEN mc.SortBy = 'Amount' AND mc.SortOrder = 'DESC' THEN "Amount" END DESC,
                CASE WHEN mc.SortBy = 'UserId' AND mc.SortOrder = 'ASC' THEN "UserId" END ASC,
                CASE WHEN mc.SortBy = 'UserId' AND mc.SortOrder = 'DESC' THEN "UserId" END DESC,
                CASE WHEN mc.SortBy = 'PaymentMethodId' AND mc.SortOrder = 'ASC' THEN "PaymentMethodId" END ASC,
                CASE WHEN mc.SortBy = 'PaymentMethodId' AND mc.SortOrder = 'DESC' THEN "PaymentMethodId" END DESC,
                CASE WHEN mc.SortBy = 'OperatorId' AND mc.SortOrder = 'ASC' THEN "OperatorId" END ASC,
                CASE WHEN mc.SortBy = 'OperatorId' AND mc.SortOrder = 'DESC' THEN "OperatorId" END DESC
            OFFSET (SELECT Offset1 FROM myconstants) LIMIT (SELECT Limit1 FROM myconstants)
            -------------------------------------------------------------------------------------------

               WITH PaymentTransactions AS (
                SELECT 
                    0 AS "Type", --'InvoicePayment'
                    ip."UserId",
                    ip."Amount",
                    ip."CreatedTime" AS "Date",
                    ip."CreatedById" AS "OperatorId",
                    ip."ShiftId",
                    ip."RegisterId",
                    ip."InvoiceId",
                    p."PaymentMethodId",
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "InvoicePayment" AS ip
                JOIN "Payment" AS p ON ip."PaymentId" = p."PaymentId"
                WHERE 
                    ip."CreatedTime" BETWEEN @DateFrom AND @DateTo
                    AND (@ShiftId IS NULL OR ip."ShiftId" = @ShiftId)
                    AND (@RegisterId IS NULL OR ip."RegisterId" = @RegisterId)
                    AND (@OperatorId IS NULL OR ip."CreatedById" = @OperatorId)
                    AND (@UserId IS NULL OR ip."UserId" = @UserId)
                    AND (@PaymentMethodId IS NULL OR p."PaymentMethodId" = @PaymentMethodId)
                    AND (COALESCE(@IncludeInvoicePayments, true)) 
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 1) --PaymentTransactionDirection.Out

                UNION ALL

                SELECT
                    1 AS "Type", --'DepositPayment'
                    dp."UserId",
                    p."Amount",
                    dp."CreatedTime" AS "Date",
                    dp."CreatedById" AS "OperatorId",
                    dp."ShiftId",
                    dp."RegisterId",
                    NULL AS "InvoiceId",
                    p."PaymentMethodId",
                    dp."DepositPaymentId",
                    NULL AS "HostId"
                FROM "DepositPayment" AS dp
                JOIN "Payment" AS p ON dp."PaymentId" = p."PaymentId"
                WHERE 
                    dp."CreatedTime" BETWEEN @DateFrom AND @DateTo
                    AND (@ShiftId IS NULL OR dp."ShiftId" = @ShiftId)
                    AND (@RegisterId IS NULL OR dp."RegisterId" = @RegisterId)
                    AND (@OperatorId IS NULL OR dp."CreatedById" = @OperatorId)
                    AND (@UserId IS NULL OR dp."UserId" = @UserId)
                    AND (@PaymentMethodId IS NULL OR p."PaymentMethodId" = @PaymentMethodId)
                    AND (COALESCE(@IncludeDepositPayments, true))
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 1) --PaymentTransactionDirection.Out

                UNION ALL

                SELECT
                    3 AS "Type", --'RefundInvoicePayment'
                    p."UserId",
                    p."Amount",
                    r."CreatedTime" AS "Date",
                    r."CreatedById" AS "OperatorId",
                    r."ShiftId",
                    r."RegisterId",
                    NULL AS "InvoiceId",
                    p."PaymentMethodId",
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "RefundInvoicePayment" AS rip
                JOIN "Refund" AS r ON rip."RefundId" = r."RefundId"
                JOIN "Payment" AS p ON r."PaymentId" = p."PaymentId"
                JOIN "Invoice" AS i ON rip."InvoiceId" = i."InvoiceId"
                WHERE 
                    r."CreatedTime" BETWEEN @DateFrom AND @DateTo
                    AND (@ShiftId IS NULL OR r."ShiftId" = @ShiftId)
                    AND (@RegisterId IS NULL OR r."RegisterId" = @RegisterId)
                    AND (@OperatorId IS NULL OR r."CreatedById" = @OperatorId)
                    AND (@UserId IS NULL OR i."UserId" = @UserId)
                    AND (@PaymentMethodId IS NULL OR r."RefundMethodId" = @PaymentMethodId)
                    AND (COALESCE(@IncludeInvoiceRefunds, true))
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 0) --PaymentTransactionDirection.In

                UNION ALL

                SELECT
                    4 AS "Type", --'RefundDepositPayment'
                    p."UserId",
                    r."Amount",
                    r."CreatedTime" AS "Date",
                    r."CreatedById" AS "OperatorId",
                    r."ShiftId",
                    r."RegisterId",
                    NULL AS "InvoiceId",
                    r."RefundMethodId" AS "PaymentMethodId",
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "RefundDepositPayment" AS rdp
                JOIN "Refund" AS r ON rdp."RefundId" = r."RefundId"
                JOIN "Payment" AS p ON r."PaymentId" = p."PaymentId"
                JOIN "DepositTransaction" AS dt ON r."DepositTransactionId" = dt."DepositTransactionId"
                JOIN "DepositPayment" AS dp ON rdp."DepositPaymentId" = dp."DepositPaymentId"
                WHERE 
                    r."CreatedTime" BETWEEN @DateFrom AND @DateTo
                    AND (@ShiftId IS NULL OR r."ShiftId" = @ShiftId)
                    AND (@RegisterId IS NULL OR r."RegisterId" = @RegisterId)
                    AND (@OperatorId IS NULL OR r."CreatedById" = @OperatorId)
                    AND (@UserId IS NULL OR dp."UserId" = @UserId)
                    AND (@PaymentMethodId IS NULL OR r."RefundMethodId" = @PaymentMethodId)
                    AND (COALESCE(@IncludeDepositRefunds, true))
                    AND (@PaymentDirection IS NULL OR @PaymentDirection != 0) --PaymentTransactionDirection.In

                UNION ALL

                SELECT
                    CASE
                        WHEN rt."Type" = 1          --RegisterTransactionType.PayIn
                            THEN 2                       --PaymentTransactionType.PayIn
                            ELSE 5                        --PaymentTransactionType.PayOut
                    END AS "Type",
                    NULL AS "UserId",
                    rt."Amount",
                    rt."CreatedTime" AS "Date",
                    rt."CreatedById" AS "OperatorId",
                    rt."ShiftId",
                    rt."RegisterId",
                    NULL AS "InvoiceId",
                    -1 AS "PaymentMethodId", --register transactions are always made in cash
                    NULL AS "DepositPaymentId",
                    NULL AS "HostId"
                FROM "RegisterTransaction" AS rt
                WHERE 
                    rt."CreatedTime" BETWEEN @DateFrom AND @DateTo
                    AND (rt."Type" = 1 OR rt."Type" = 2)
                    AND (@ShiftId IS NULL OR rt."ShiftId" = @ShiftId)
                    AND (@RegisterId IS NULL OR rt."RegisterId" = @RegisterId)
                    AND (@OperatorId IS NULL OR rt."CreatedById" = @OperatorId)
                    AND (COALESCE(@PaymentMethodId, -1) = -1 -- cash or default to cash if NULL
                            AND @UserId IS NOT NULL
                            AND (COALESCE(@IncludePayIns, true) OR COALESCE(@IncludePayOuts, true)))
            )

            SELECT jsonb_build_object(
                'Total', (SELECT COUNT(*) FROM "PaymentTransactions"),
                'Items', COALESCE(
                    (SELECT jsonb_agg(row_to_json(t))
                     FROM (
                         SELECT 
                            "UserId", 
                            "Amount", 
                            "PaymentMethodId", 
                            "Date", 
                            "OperatorId", 
                            "ShiftId", 
                            "RegisterId", 
                            "Type"
                         FROM "PaymentTransactions"
                         ORDER BY
                            CASE WHEN SortBy = 'Date' AND SortOrder = 'ASC' THEN "Date" END ASC,
                            CASE WHEN SortBy = 'Date' AND SortOrder = 'DESC' THEN "Date" END DESC,
                            CASE WHEN SortBy = 'Amount' AND SortOrder = 'ASC' THEN "Amount" END ASC,
                            CASE WHEN SortBy = 'Amount' AND SortOrder = 'DESC' THEN "Amount" END DESC,
                            CASE WHEN SortBy = 'UserId' AND SortOrder = 'ASC' THEN "UserId" END ASC,
                            CASE WHEN SortBy = 'UserId' AND SortOrder = 'DESC' THEN "UserId" END DESC,
                            CASE WHEN SortBy = 'PaymentMethodId' AND SortOrder = 'ASC' THEN "PaymentMethodId" END ASC,
                            CASE WHEN SortBy = 'PaymentMethodId' AND SortOrder = 'DESC' THEN "PaymentMethodId" END DESC,
                            CASE WHEN SortBy = 'OperatorId' AND SortOrder = 'ASC' THEN "OperatorId" END ASC,
                            CASE WHEN SortBy = 'OperatorId' AND SortOrder = 'DESC' THEN "OperatorId" END DESC,
                            CASE WHEN SortBy = 'ShiftId' AND SortOrder = 'ASC' THEN "ShiftId" END ASC,
                            CASE WHEN SortBy = 'ShiftId' AND SortOrder = 'DESC' THEN "ShiftId" END DESC,
                            CASE WHEN SortBy = 'RegisterId' AND SortOrder = 'ASC' THEN "RegisterId" END ASC,
                            CASE WHEN SortBy = 'RegisterId' AND SortOrder = 'DESC' THEN "RegisterId" END DESC,
                            CASE WHEN SortBy = 'Type' AND SortOrder = 'ASC' THEN "Type" END ASC,
                            CASE WHEN SortBy = 'Type' AND SortOrder = 'DESC' THEN "Type" END DESC
                         OFFSET @Offset LIMIT @Limit
                     ) t),
                    '[]'::jsonb
                )
            );
                
        """;
        private const string USERS_HARD_DELETE =
        """
            CREATE TEMP TABLE "UserIdList" ("UserId" INT);
            INSERT INTO "UserIdList" ("UserId")
            SELECT UNNEST(STRING_TO_ARRAY(@UserIds, ',')::INT[]);

            BEGIN;

                DELETE FROM "UsageSession" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserCreditLimit"
                USING "UserMember" AS u
                WHERE "UserCreditLimit"."UserId" = u."UserId"
                AND u."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Refund"
                USING "DepositTransaction" AS dt
                WHERE "Refund"."DepositTransactionId" = dt."DepositTransactionId"
                AND dt."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Refund"
                USING "Payment" AS p
                WHERE "Refund"."PaymentId" = p."PaymentId"
                AND p."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "VerificationMobilePhone"
                USING "Verification" AS v
                WHERE "VerificationMobilePhone"."VerificationId" = v."VerificationId"
                AND v."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "VerificationEmail"
                USING "Verification" AS v
                WHERE "VerificationEmail"."VerificationId" = v."VerificationId"
                AND v."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Verification" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UsageTime"
                USING "Usage" AS u
                WHERE "UsageTime"."UsageId" = u."UsageId"
                AND u."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UsageTimeFixed"
                USING "Usage" AS u
                WHERE "UsageTimeFixed"."UsageId" = u."UsageId"
                AND u."UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Usage" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "InvoicePayment" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "PaymentIntent" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Payment" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "AssistanceRequest" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "HostGroupWaitingLineEntry" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserAgreementState" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserAttribute" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserPermission" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserNote" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "ReservationUser" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "ReservationHost" WHERE "PreferedUserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Reservation" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Token" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "AppRating" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "AppStat" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "AssetTransaction" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "DepositTransaction" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "PointTransaction" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserSessionChange" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserSession" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "ProductOrder" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "InvoiceLine" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "Invoice" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "UserMember" WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList");

                DELETE FROM "User" 
                WHERE "UserId" IN (SELECT "UserId" FROM "UserIdList")
                RETURNING "UserId";

            COMMIT;
        """;
    }
}
