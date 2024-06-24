namespace Gizmo.DAL.Scripts
{
    /// <summary>
    /// SQL scripts.
    /// </summary>
    public static class SQLScripts
    {
        /// <summary>
        /// Deposit payment refund migration script.
        /// </summary>
        public const string CREATE_DEPOSIT_PAYMENT_REFUNDS = nameof(CREATE_DEPOSIT_PAYMENT_REFUNDS);

        /// <summary>
        /// Session billed span update script.
        /// </summary>
        public const string SESSION_BILLED_SPAN_UPDATE = nameof(SESSION_BILLED_SPAN_UPDATE);

        /// <summary>
        /// Session span update script.
        /// </summary>
        public const string SESSION_UPDATE_SQL = nameof(SESSION_UPDATE_SQL);

        /// <summary>
        /// Log limiting sql script.
        /// </summary>
        public const string LOG_LIMIT_SQL = nameof(LOG_LIMIT_SQL);

        /// <summary>
        /// Log truncate sql script.
        /// </summary>
        public const string TRUNCATE_LOGS = nameof(TRUNCATE_LOGS);

        /// <summary>
        /// Delete user agreement state by user id.
        /// </summary>
        public const string DELETE_USERAGREEMENTSTATE_BY_USERID = nameof(DELETE_USERAGREEMENTSTATE_BY_USERID);

        /// <summary>
        /// Delete user agreement state by user agreement id.
        /// </summary>
        public const string DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID = nameof(DELETE_USERAGREEMENTSTATE_BY_USERAGREEMENTID);

        /// <summary>
        /// Check if migration exists.
        /// </summary>
        public const string HAS_EF6_MIGRATION_BY_MIGRATIONID = nameof(HAS_EF6_MIGRATION_BY_MIGRATIONID);

        /// <summary>
        /// Check if table exists (case sensitive matters).
        /// </summary>
        public const string HAS_TABLE_BY_NAME = nameof(HAS_TABLE_BY_NAME);

        /// <summary>
        /// Reset user guests data.
        /// </summary>
        public const string RESET_USERGUESTS = nameof(RESET_USERGUESTS);

        /// <summary>
        /// Get payment transactions.
        /// </summary>
        public const string GET_PAGINATED_PAYMENT_TRANSACTIONS = nameof(GET_PAGINATED_PAYMENT_TRANSACTIONS);
    }
}
