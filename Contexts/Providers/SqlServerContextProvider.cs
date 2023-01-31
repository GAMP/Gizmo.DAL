using System.ComponentModel.Composition;

using Gizmo.DAL.Interfaces;

using GizmoDALV2;

namespace Gizmo.DAL.Contexts.Providers
{
    /// <summary>
    /// DAL Initialization code.
    /// </summary>
    [Export(typeof(IGizmoDbContextProvider))]
    [Export(typeof(ISqlServerContextProvider))]
    public class SqlServerContextProvider : IGizmoDbContextProvider, ISqlServerContextProvider
    {
        private readonly string _connectionString;
        public SqlServerContextProvider(string connectionString) => _connectionString = connectionString;

        #region IGizmoDbContextProvider

        IGizmoDBContext IGizmoDbContextProvider.GetDbContext() => GetDbContext();
        IGizmoDBContext IGizmoDbContextProvider.GetDbNonProxyContext() => GetDbNonProxyContext();

        #endregion

        #region IGizmoDqlSqlServerContextProvider

        public SqlServerDbContext GetDbContext()
        {
            return new SqlServerDbContext(_connectionString);
        }

        public SqlServerDbContext GetDbNonProxyContext()
        {

            var context = GetDbContext();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            return context;
        }

        #endregion
    }
}
