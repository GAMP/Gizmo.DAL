using GizmoDALV2;

namespace Gizmo.DAL.Interfaces
{
    /// <summary>
    /// Database context provider interface.
    /// </summary>
    /// <typeparam name="TContext">IGizmoDbContext</typeparam>
    public interface IDbContextProvider<TContext> where TContext : class, IGizmoDBContext //TODO: IDisposable?
    {
        #region FUNCTIONS

        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        TContext GetDbContext();

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        TContext GetDbNonProxyContext();

        #endregion
    }
}
