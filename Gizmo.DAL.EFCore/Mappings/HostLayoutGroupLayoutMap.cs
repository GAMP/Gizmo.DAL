using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class HostLayoutGroupLayoutMap : IEntityTypeConfiguration<HostLayoutGroupLayout>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostLayoutGroupLayout> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("HostLayoutGroupLayoutId")
                .HasColumnOrder(0);

            builder.Property(x => x.HostLayoutGroupId)
                .HasColumnOrder(1);
                
            builder.Property(x => x.HostId)
                .HasColumnOrder(2);

            builder.Property(x => x.X)
                .HasColumnOrder(3);

            builder.Property(x => x.Y)
                .HasColumnOrder(4);

            builder.Property(x => x.Height)
                .HasColumnOrder(5);

            builder.Property(x => x.Width)
                .HasColumnOrder(6);

            builder.Property(x => x.IsHidden)
                .HasColumnOrder(7);

            // Indexes
            builder.HasIndex(x => new { x.HostLayoutGroupId, x.HostId }).HasDatabaseName("UQ_HostLayoutGroupHost").IsUnique();

            // Table & Column Mappings
            builder.ToTable("HostLayoutGroupLayout");

            builder.HasOne(x => x.HostLayoutGroup)
                .WithMany(x => x.Layouts)
                .HasForeignKey(x => x.HostLayoutGroupId);

            builder.HasOne(x => x.Host)
                .WithMany(x => x.Layouts)
                .HasForeignKey(x => x.HostId);
        }
    }
}
