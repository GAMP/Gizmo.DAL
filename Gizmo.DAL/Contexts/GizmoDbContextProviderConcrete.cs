using System;

using Gizmo.DAL;

using Microsoft.Extensions.Options;

using SharedLib;
using SharedLib.Configuration;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Gizmo.DAL default db context ptovider.
    /// </summary>
    public sealed class GizmoDbContextProviderConcrete : IGizmoDbContextProviderConcrete, IGizmoDbContextProvider
    {
        private readonly ServiceDatabaseConfig _dbConfig;
        /// <summary>
        /// Gizmo.DAL default db context ptovider initializer.
        /// </summary>
        /// <param name="options">DI options.</param>
        public GizmoDbContextProviderConcrete(IOptions<ServiceDatabaseConfig> options) => _dbConfig = options.Value;

        #region IGizmoDbContextProvider

        IGizmoDBContext IGizmoDbContextProvider.GetDbContext() => GetDbContext();
        IGizmoDBContext IGizmoDbContextProvider.GetDbNonProxyContext() => GetDbNonProxyContext();

        #endregion

        #region IGizmoDbContextProviderConcrete

        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public DefaultDbContext GetDbContext()
        {
            return _dbConfig.DbType switch
            {
                DatabaseType.LOCALDB or DatabaseType.MSSQLEXPRESS or DatabaseType.MSSQL => new(_dbConfig.DbConnectionString),
                _ => throw new NotImplementedException(nameof(GetDbContext))
            };
        }

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public DefaultDbContext GetDbNonProxyContext()
        {
            var context = GetDbContext();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            return context;
        }

        #endregion
    }
}
