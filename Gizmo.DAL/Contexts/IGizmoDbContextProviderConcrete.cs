namespace Gizmo.DAL.Contexts
{
    /// <summary>
    /// Gizmo.DALl default db context ptovider.
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
    }
}
