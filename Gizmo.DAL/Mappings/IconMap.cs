using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class IconMap : EntityTypeConfiguration<Icon>
    {
        public IconMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.Image)
                .HasMaxLength(GizmoDALV2.SQLByteArraySize.MEDIUM)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Icon");

            this.Property(x => x.Id)
                .HasColumnName("IconId");
        }
    }
}
