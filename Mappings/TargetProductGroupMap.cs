using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group target product group entity map.
    /// </summary>
    public sealed class TargetProductGroupMap : IEntityTypeConfiguration<TargetProductGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetProductGroup> builder)
        {
            builder.ToTable(nameof(TargetProductGroup))
                .HasBaseType<Target>();

            builder.Property(targetProductGroup => targetProductGroup.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(targetProductGroup => targetProductGroup.TargetGroupProductGroupId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(targetProductGroup => targetProductGroup.ProductGroupId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(target => target.ProductGroup)
                .WithMany()
                .HasForeignKey(target => target.ProductGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(target => target.TargetGroupProductGroup)
                .WithMany(targetGroup => targetGroup.ProductGroups)
                .HasForeignKey(target => target.TargetGroupProductGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(target => new { target.TargetGroupProductGroupId, target.ProductGroupId })
                .IsUnique()
                .HasFilter(null);
        }
    }
}
