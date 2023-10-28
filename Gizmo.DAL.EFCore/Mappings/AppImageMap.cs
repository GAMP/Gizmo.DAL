using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AppImageMap : IEntityTypeConfiguration<AppImage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppImage> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("AppId")
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.Image)
                .HasMaxLength(GizmoDALV2.SQLByteArraySize.MEDIUM);

            // Indexes
            builder.HasIndex(x => x.Id);

            // Table & Column Mappings
            builder.ToTable("AppImage");

            builder.HasOne(x => x.App)
                .WithOne(x => x.Image)
                .HasForeignKey<AppImage>(x => x.Id);
        }
    }
}
