using System;
using System.ComponentModel.Composition;

using GizmoDALV2;

namespace Gizmo.DAL.Contexts.Providers
{
    /// <summary>
    /// MySql context ptovider.
    /// </summary>
    [Export(typeof(IGizmoDbContextProvider))]
    public sealed class MySqlContextProvider : IGizmoDbContextProvider
    {
        /// <summary>
        /// Gets database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public IGizmoDBContext GetDbContext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets non-proxy database context.
        /// </summary>
        /// <returns>New context instance.</returns>
        public IGizmoDBContext GetDbNonProxyContext()
        {
            throw new NotImplementedException();
        }
    }
}
