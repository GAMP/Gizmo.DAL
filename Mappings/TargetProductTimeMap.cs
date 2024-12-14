using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group target product time entity map.
    /// </summary>
    public sealed class TargetProductTimeMap : IEntityTypeConfiguration<TargetProductTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetProductTime> builder)
        {
            builder.ToTable(nameof(TargetProductTime))
                .HasBaseType<Target>();

            builder.Property(targetProductTime => targetProductTime.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(targetProductTime => targetProductTime.TargetGroupProductTimeId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(targetProductTime => targetProductTime.ProductTimeId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(target => new { target.TargetGroupProductTimeId, target.ProductTimeId })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(target => target.ProductTime)
                .WithMany()
                .HasForeignKey(target => target.ProductTimeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupProductTime)
                .WithMany(targetGroup => targetGroup.ProductTimes)
                .HasForeignKey(target => target.TargetGroupProductTimeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
