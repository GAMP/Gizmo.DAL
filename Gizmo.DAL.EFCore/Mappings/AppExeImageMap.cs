using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class AppExeImageMap : IEntityTypeConfiguration<AppExeImage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeImage> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .ValueGeneratedNever();

            builder.Property(x => x.Image)
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Indexes
            builder.HasIndex(x => x.Id);

            // Table & Column Mappings
            builder.ToTable("AppExeImage");

            builder.Property(x => x.Id)
                .HasColumnName("AppExeId");

            builder.HasOne(x => x.AppExe)
                .WithOne(x => x.Image)
                .HasForeignKey<AppExeImage>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
