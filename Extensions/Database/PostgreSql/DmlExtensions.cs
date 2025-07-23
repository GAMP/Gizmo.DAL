using System;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class PostgreSql
{
    public static Task Cleanup(this DefaultDbContext cx, bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts, CancellationToken ct)
    {
        throw new NotImplementedException("PostgreSQL cleanup is not implemented.");
    }

    public static Task CleanupUsers(this DefaultDbContext cx, CancellationToken ct)
    {
        throw new NotImplementedException("PostgreSQL user cleanup is not implemented.");
    }
}