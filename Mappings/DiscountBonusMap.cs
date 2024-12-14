using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount bonus map.
    /// </summary>
    public sealed class DiscountBonusMap : IEntityTypeConfiguration<DiscountBonus>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountBonus> builder)
        {
            builder.ToTable(nameof(DiscountBonus))
                .HasBaseType<DiscountTargeted>();

            builder.Property(discountBonus => discountBonus.Id)
                .HasColumnOrder(0);

            builder.Property(discountBonus => discountBonus.Value)
                .HasColumnOrder(1);
        }
    }
}
