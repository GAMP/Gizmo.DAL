using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class AppImageMap : EntityTypeConfiguration<AppImage>
    {
        public AppImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("AppId")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(x => x.Image)
                .HasMaxLength(GizmoDALV2.SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            this.ToTable("AppImage");

            this.HasRequired(x => x.App)
                .WithRequiredDependent(x => x.Image)
                .WillCascadeOnDelete(true);
        }
    }
}
