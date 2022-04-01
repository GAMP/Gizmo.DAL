using System;

namespace Gizmo.DAL.Scripts
{
    public static class SQLScripts
    {
        public const string CREATE_DEPOSIT_PAYMENT_REFUNDS = "BEGIN TRANSACTION;" +
            "INSERT INTO [dbo].[Refund] (Amount,DepositTransactionId, RefundMethodId,CreatedTime)" +
            "SELECT [Amount],[DepositTransactionId],-1,[CreatedTime] FROM [dbo].[DepositTransaction] WHERE Type =1;" +
            "INSERT INTO [dbo].[RefundDepositPayment] (FiscalReceiptStatus,RefundId)" +
            "SELECT 0,RefundId FROM [dbo].[Refund] _refunds WHERE NOT EXISTS (SELECT RefundId FROM [dbo].[RefundInvoicePayment] _refundIvoice WHERE _refunds.RefundId= _refundIvoice.RefundId);" +
            "COMMIT;";

        public static string CreateUniqueNullableIndex(string indexName,string tableName,string columnName)
        {
            if(string.IsNullOrEmpty(indexName))
                throw new ArgumentNullException(nameof(indexName));

            if(string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if(string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException(nameof(columnName));

            return $"CREATE UNIQUE NONCLUSTERED INDEX[{indexName}] ON[dbo].[{tableName}]({columnName}) WHERE {columnName} IS NOT NULL";
        }

        public static string CreateNonUniqueNullableIndex(string indexName, string tableName, string columnName)
        {
            if (string.IsNullOrEmpty(indexName))
                throw new ArgumentNullException(nameof(indexName));

            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException(nameof(columnName));

            return $"CREATE NONCLUSTERED INDEX[{indexName}] ON[dbo].[{tableName}]({columnName}) WHERE {columnName} IS NOT NULL";
        }
    }
}
