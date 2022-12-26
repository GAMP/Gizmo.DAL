using System;
using System.Runtime.Serialization;

namespace GizmoDALV2
{
    /// <summary>
    /// SQL Server file list representation class.
    /// </summary>
    [Serializable()]
    [DataContract()]
    public class SQLFileList
    {
        #region PROPERTIES

        [DataMember()]
        public string LogicalName
        {
            get; set;
        }

        [DataMember()]
        public string PhysicalName
        {
            get; set;
        }

        [DataMember()]
        public string Type
        {
            get; set;
        }

        [DataMember()]
        public string FileGroupName
        {
            get; set;
        }

        [DataMember()]
        public long Size
        {
            get; set;
        }

        [DataMember()]
        public long MaxSize
        {
            get; set;
        }

        [DataMember()]
        public int Field
        {
            get; set;
        }

        [DataMember()]
        public decimal CreateLSN
        {
            get; set;
        }

        [DataMember()]
        public decimal DropLSN
        {
            get; set;
        }

        [DataMember()]
        public Guid UniqueId
        {
            get; set;
        }

        [DataMember()]
        public decimal ReadOnlyLSN
        {
            get; set;
        }

        [DataMember()]
        public decimal ReadWriteLSN
        {
            get; set;
        }

        [DataMember()]
        public long BackupSizeInBytes
        {
            get; set;
        }

        [DataMember()]
        public int SourceBlockSize
        {
            get; set;
        }

        [DataMember()]
        public int FileGroupId
        {
            get; set;
        }

        [DataMember()]
        public Guid? LogGroupGUID
        {
            get; set;
        }

        [DataMember()]
        public Guid? DifferentialBaseGUID
        {
            get; set;
        }

        [DataMember()]
        public bool IsReadOnly
        {
            get; set;
        }

        [DataMember()]
        public bool IsPresent
        {
            get; set;
        }

        [DataMember()]
        public string TDEThumbprint
        {
            get; set;
        }

        [DataMember()]
        public string SnapshotUrl
        {
            get; set;
        }

        #endregion
    }
}
