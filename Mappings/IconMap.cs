using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class IconMap : EntityTypeConfiguration<Icon>
    {
        public IconMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.Image)
                .HasMaxLength(SQLByteArraySize.MEDIUM)
                .IsRequired();

            // Table & Column Mappings
            ToTable("Icon");

            Property(x => x.Id)
                .HasColumnName("IconId");
        }
    }
}
