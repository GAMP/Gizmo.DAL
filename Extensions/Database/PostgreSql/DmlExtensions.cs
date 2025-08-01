using System;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class PostgreSql
{
    // We can use the same cleanup logic as SQL Server, because the logic is written as database-agnostic.
    public static Task Cleanup(this DefaultDbContext cx, bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts, CancellationToken ct) => 
        SqlServer.Cleanup(cx, deleteUsers, deleteHosts, deleteOperators, deleteProducts, ct);

    public static Task CleanupUsers(this DefaultDbContext cx, CancellationToken ct)
    {
        throw new NotImplementedException("PostgreSQL user cleanup is not implemented.");
    }
}
