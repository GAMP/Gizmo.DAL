using Gizmo.DAL.Contexts;

using GizmoDALV2;

namespace Gizmo.DAL.Interfaces
{
    public interface IMySqlContextProvider : IDbContextProvider<MySqlDbContext>
    {
    }
}
