using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class AppExeImageMap : EntityTypeConfiguration<AppExeImage>
    {
        public AppExeImageMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.Image)
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            ToTable("AppExeImage");

            Property(x => x.Id)
                .HasColumnName("AppExeId");

            HasRequired(x => x.AppExe)
                .WithRequiredDependent(x => x.Image)
                .WillCascadeOnDelete(true);
        }
    }
}
