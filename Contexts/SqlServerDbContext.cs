using GizmoDALV2;

namespace Gizmo.DAL.Contexts
{
    public class SqlServerDbContext : DefaultDbContext
    {
        public SqlServerDbContext(string connectionString) : base(connectionString) { }
    }
}
