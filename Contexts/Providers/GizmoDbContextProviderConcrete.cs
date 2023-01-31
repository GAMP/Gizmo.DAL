using System;
using System.ComponentModel.Composition;

using GizmoDALV2;

using Microsoft.Extensions.DependencyInjection;

namespace Gizmo.DAL.Contexts.Providers
{
    /// <summary>
    /// Gizmo.DAL default db context ptovider.
    /// </summary>
    public sealed class GizmoDbContextProviderConcrete : IGizmoDbContextProviderConcrete, IGizmoDbContextProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// Gizmo.DAL default db context ptovider initializer
        /// </summary>
        /// <param name="serviceProvider">DI Service provider</param>
        public GizmoDbContextProviderConcrete(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

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
            return _serviceProvider.GetRequiredService<DefaultDbContext>();
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
