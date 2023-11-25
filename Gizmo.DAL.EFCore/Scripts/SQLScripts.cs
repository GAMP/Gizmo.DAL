using System;

namespace Gizmo.DAL.Scripts
{
    public static class SQLScripts
    {
        /// <summary>
        /// Depost payment refund migration script.
        /// </summary>
        public const string CREATE_DEPOSIT_PAYMENT_REFUNDS = "BEGIN TRANSACTION;" +
            "INSERT INTO [dbo].[Refund] (Amount,DepositTransactionId, RefundMethodId,CreatedTime)" +
            "SELECT [Amount],[DepositTransactionId],-1,[CreatedTime] FROM [dbo].[DepositTransaction] WHERE Type =1;" +
            "INSERT INTO [dbo].[RefundDepositPayment] (FiscalReceiptStatus,RefundId)" +
            "SELECT 0,RefundId FROM [dbo].[Refund] _refunds WHERE NOT EXISTS (SELECT RefundId FROM [dbo].[RefundInvoicePayment] _refundIvoice WHERE _refunds.RefundId= _refundIvoice.RefundId);" +
            "COMMIT;";

        /// <summary>
        /// Session billed span update script.
        /// </summary>
        public const string SESSION_BILLED_SPAN_UPDATE = @"UPDATE UserSession SET BilledSpan=BilledSpan+{0} WHERE UserSessionId IN ({1});";

        /// <summary>
        /// Session span update script.
        /// </summary>
        public const string SESSION_UPDATE_SQL =
            "UPDATE UserSession SET" + " " +
            "Span=Span+@SPAN," + " " +
            "PendSpan = CASE State WHEN 5 THEN PendSpan+@SPAN ELSE PendSpan END, PendSpanTotal = CASE State WHEN 5 THEN PendSpanTotal+@SPAN ELSE PendSpanTotal END," + " " +
            "PauseSpan = CASE State WHEN 9 THEN PauseSpan+@SPAN ELSE PauseSpan END, PauseSpanTotal = CASE State WHEN 9 THEN PauseSpanTotal+@SPAN ELSE PauseSpanTotal END," + " " +
            "GraceSpan = CASE State WHEN 33 THEN GraceSpan+@SPAN ELSE GraceSpan END, GraceSpanTotal = CASE State WHEN 33 THEN GraceSpanTotal+@SPAN ELSE GraceSpanTotal END" + " " +
            "OUTPUT INSERTED.UserSessionId as Id,INSERTED.UserId,INSERTED.HostId,INSERTED.State,INSERTED.Span,INSERTED.BilledSpan,INSERTED.PendTime,INSERTED.PendSpan,INSERTED.EndTime,INSERTED.CreatedById,INSERTED.CreatedTime,INSERTED.Slot,INSERTED.PendSpanTotal,INSERTED.PauseSpan,INSERTED.PauseSpanTotal,INSERTED.GraceTime,INSERTED.GraceSpan,INSERTED.GraceSpanTotal" + " " +
            "WHERE State & 1 = 1;";

        /// <summary>
        /// Log limiting sql script.
        /// </summary>
        public const string LOG_LIMIT_SQL = "DECLARE @CURRENT_RECORDS AS INT = 0;" + " " +
          "SELECT @CURRENT_RECORDS = COUNT(*) FROM Log;" +
          "IF(@CURRENT_RECORDS > @MAX_RECORDS)" + " " +
          "BEGIN;" +
          "DECLARE @OVER_LIMIT AS INT = @CURRENT_RECORDS - @MAX_RECORDS;" +
          "IF @OVER_LIMIT > 0" + " " +
          "WITH LOG_EXPRESSION AS (SELECT TOP (@OVER_LIMIT) * FROM [Log] ORDER BY LogId ASC) DELETE FROM LOG_EXPRESSION;" +
          "SELECT @OVER_LIMIT;" +
          "END;";
    }
}
