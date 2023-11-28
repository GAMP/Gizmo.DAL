using Gizmo.DAL;

using System;
using System.Data.Entity;

namespace Gizmo.DAL
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
        public DatabaseTransaction(DbContextTransaction inner)
        {
            InnerTransaction = inner ?? throw new ArgumentNullException(nameof(inner));
        }
        #endregion

        #region PROPERTIES
        private DbContextTransaction InnerTransaction
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
