using System;
using GizmoDALV2;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gizmo.DAL.EFCore
{
    /// <summary>
    /// Database transaction wrapper class.
    /// </summary>
    /// <remarks>
    /// Used to wrap the sql server transaction class and expose to code that dont have any reference to Entity Framework.
    /// </remarks>
    public class DatabaseTransaction : IDatabaseTransaction
    {
        #region CONSTRUCTOR
        public DatabaseTransaction(IDbContextTransaction inner)
        {
            InnerTransaction = inner ?? throw new ArgumentNullException(nameof(inner));
        }
        #endregion

        #region PROPERTIES
        private IDbContextTransaction InnerTransaction
        {
            get; set;
        }
        #endregion

        #region FUNCTIONS

        public void Commit()
        {
            InnerTransaction?.Commit();
        }

        public void Dispose()
        {
            InnerTransaction?.Dispose();
        }

        public void Rollback()
        {
            InnerTransaction?.Rollback();
        }

        #endregion
    }
}