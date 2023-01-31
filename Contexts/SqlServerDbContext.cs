using GizmoDALV2;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Sql server context.
    /// </summary>
    public sealed class SqlServerDbContext : DefaultDbContext
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public SqlServerDbContext(string connectionString) : base(connectionString) { }
    }
}
