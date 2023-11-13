#nullable enable

namespace Gizmo.DAL.EFCore
{
    /// <summary>
    /// Database connection options.
    /// </summary>
    /// <remarks>
    /// This will be directly mapped to an application.json or any other source configuration file.<br></br>
    /// <b>This structure mirrors current Gizmo server connection options class.</b>
    /// </remarks>
    public class DatabaseConnectionOptions
    {
        /// <summary>
        /// Type of the database.
        /// </summary>
        public SharedLib.DatabaseType DbType { get; init; }

        /// <summary>
        /// Database connection string.
        /// </summary>
        public string DbConnectionString { get; init; } = string.Empty;

        /// <summary>
        /// Optional command timeout.
        /// </summary>
        public int? CommandTimeout { get; init; }
    }
}
