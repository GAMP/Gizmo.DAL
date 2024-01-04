using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class AppExeCdImageMap : IEntityTypeConfiguration<AppExeCdImage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeCdImage> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.AppExeId)
                .HasColumnOrder(1);

            builder.Property(t => t.Path)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.MountOptions)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.DeviceId)
                .HasColumnOrder(4);

            builder.Property(x => x.CheckExitCode)
                .HasColumnOrder(5);

            builder.Property(t => t.Guid)
                .HasColumnOrder(6);

            // Table & Column Mappings
            builder.ToTable("AppExeCdImage");

            builder.Property(t => t.Id)
                .HasColumnName("AppExeCdImageId");

            // Indexes
            builder.HasIndex(x => x.Guid).IsUnique();
            builder.HasIndex(x => x.AppExeId);

            // Relationships
            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.AppExeCdImages)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
