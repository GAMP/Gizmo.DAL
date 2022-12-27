using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class AppGroupAppMap : IEntityTypeConfiguration<AppGroupApp>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppGroupApp> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.AppGroupId, t.AppId });

            // Properties
            builder.Property(x => x.AppGroupId)
                .HasColumnOrder(0);

            builder.Property(t => t.AppId)
                .HasColumnOrder(1);

            // Indexes
            builder.HasIndex(x => x.AppGroupId);

            // Table & Column Mappings
            builder.ToTable("AppGroupApp");

            builder.HasOne(x => x.AppGroup)
                .WithMany(x => x.Apps)
                .HasForeignKey(x => x.AppGroupId);

            builder.HasOne(x => x.App)
                .WithMany(x => x.AppGroups)
                .HasForeignKey(x => x.AppId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
