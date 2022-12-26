using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class HostLayoutGroupImageMap : IEntityTypeConfiguration<HostLayoutGroupImage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostLayoutGroupImage> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.Image)
                .HasMaxLength(GizmoDALV2.SQLByteArraySize.MEDIUM);

            builder.Property(x => x.Id)
                .HasColumnName("HostLayoutGroupId");

            // Indexes
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("HostLayoutGroupImage");

            builder.HasOne(x => x.HostLayoutGroup)
                .WithOne(x => x.Image)
                .HasForeignKey<HostLayoutGroupImage>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
