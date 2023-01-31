using System.ComponentModel.Composition;

using Gizmo.DAL.Interfaces;

using GizmoDALV2;

namespace Gizmo.DAL.Contexts.Providers
{
    /// <summary>
    /// DAL Initialization code.
    /// </summary>
    [Export(typeof(IGizmoDbContextProvider))]
    [Export(typeof(IMySqlContextProvider))]
    public class MySqlContextProvider : IGizmoDbContextProvider, IMySqlContextProvider
    {
        private readonly string _connectionString;
        public MySqlContextProvider(string connectionString) => _connectionString = connectionString;

        #region IGizmoDbContextProvider

        IGizmoDBContext IGizmoDbContextProvider.GetDbContext() => GetDbContext();
        IGizmoDBContext IGizmoDbContextProvider.GetDbNonProxyContext() => GetDbNonProxyContext();

        #endregion

        #region IGizmoDqlSqlServerContextProvider

        public MySqlDbContext GetDbContext()
        {
            return new MySqlDbContext(_connectionString);
        }

        public MySqlDbContext GetDbNonProxyContext()
        {
            var context = GetDbContext();
            return context;
        }

        #endregion
    }
}
