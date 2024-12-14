using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount targeted entity map.
    /// </summary>
    public sealed class DiscountTargetedMap : IEntityTypeConfiguration<DiscountTargeted>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountTargeted> builder)
        {
            builder.ToTable(nameof(DiscountTargeted))
                .HasBaseType<DiscountPeriodic>();

            builder.Property(discountTargeted => discountTargeted.Id)
                .HasColumnOrder(0);
        }
    }
}
