using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount target group entity map.
    /// </summary>
    public sealed class TargetGroupMap : IEntityTypeConfiguration<TargetGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroup> builder)
        {
            builder.ToTable(nameof(TargetGroup));

            builder.HasKey(targetGroup => targetGroup.Id);

            builder.Property(targetGroup => targetGroup.Id)
                .HasColumnName("TargetGroupId");

            builder.Property(targetGroup => targetGroup.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(targetGroup => targetGroup.DiscountId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(targetGroup => targetGroup.Requirement)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(targetGroup => targetGroup.Value)
                .IsRequired(false)
                .HasColumnOrder(3);

            builder.Property(targetGroup => targetGroup.IncludeAll)
                .IsRequired()
                .HasColumnOrder(4);
        }
    }
}
