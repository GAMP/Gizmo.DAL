using System.Collections.Generic;

namespace Gizmo.DAL
{
    /// <summary>
    /// External export class.
    /// </summary>
    public class ExternalExport
    {
        #region PROPERTIES
        /// <summary>
        /// Gets exported users.
        /// </summary>
        public IEnumerable<DTO.UserImportInfo> Users { get; set; } 
        #endregion
    }
}
