using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount basic entity map.
    /// </summary>
    public sealed class DiscountPeriodicMap : IEntityTypeConfiguration<DiscountPeriodic>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountPeriodic> builder)
        {
            builder.ToTable(nameof(DiscountPeriodic))
                .HasBaseType<Discount>();

            builder.Property(discountPeriodic => discountPeriodic.Id)
                .HasColumnOrder(0);
        }
    }
}
