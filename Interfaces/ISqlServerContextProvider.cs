using Gizmo.DAL.Contexts;

namespace Gizmo.DAL.Interfaces
{
    public interface ISqlServerContextProvider : IDbContextProvider<SqlServerDbContext>
    {
    }
}
