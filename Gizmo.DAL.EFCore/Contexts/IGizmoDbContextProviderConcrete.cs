using SharedLib.Configuration;

namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Gizmo.DALl default db context provider.
    /// </summary>
    public interface IGizmoDbContextProviderConcrete
    {
        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        DefaultDbContext GetDbContext();

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        DefaultDbContext GetDbNonProxyContext();

        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <param name="dbConfig">Database configuration.</param>
        /// <returns>New context instance.</returns>
        DefaultDbContext GetDbContext(ServiceDatabaseConfig dbConfig);

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <param name="dbConfig">Database configuration.</param>
        /// <returns>New context instance.</returns>
        DefaultDbContext GetDbNonProxyContext(ServiceDatabaseConfig dbConfig);
    }
}
