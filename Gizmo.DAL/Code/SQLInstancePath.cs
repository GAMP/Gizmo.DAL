using System;
using System.Runtime.Serialization;

namespace Gizmo.DAL
{
    /// <summary>
    /// SQL Instance path representation class.
    /// </summary>
    [Serializable()]
    [DataContract()]
    public class SQLInstancePath
    {
        #region PROPERTIES

        [DataMember()]
        public string DataPath
        {
            get; set;
        }

        [DataMember()]
        public string LogPath
        {
            get; set;
        }

        #endregion
    }
}
