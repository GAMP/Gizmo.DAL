using System.ComponentModel.Composition;

using GizmoDALV2;

namespace Gizmo.DAL.Contexts.Providers
{
    /// <summary>
    /// Sql Server context provider
    /// </summary>
    [Export(typeof(IGizmoDbContextProvider))]
    public sealed class SqlServerContextProvider : IGizmoDbContextProvider
    {
        private readonly string _connectionString;
        /// <summary>
        /// Sql server context provider initializer
        /// </summary>
        /// <param name="connectionString">Sql Server name or connection string</param>
        public SqlServerContextProvider(string connectionString) => _connectionString = connectionString;

        #region IGizmoDbContextProvider

        IGizmoDBContext IGizmoDbContextProvider.GetDbContext() => GetDbContext();
        IGizmoDBContext IGizmoDbContextProvider.GetDbNonProxyContext() => GetDbNonProxyContext();

        #endregion

        #region IGizmoDqlSqlServerContextProvider

        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public SqlServerDbContext GetDbContext()
        {
            return new SqlServerDbContext(_connectionString);
        }

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
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
