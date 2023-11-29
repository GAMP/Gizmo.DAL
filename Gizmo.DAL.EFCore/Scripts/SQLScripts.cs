namespace Gizmo.DAL.Scripts
{
    /// <summary>
    /// SQL scripts.
    /// </summary>
    public static class SQLScripts
    {
        /// <summary>
        /// Depost payment refund migration script.
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
    }
}
