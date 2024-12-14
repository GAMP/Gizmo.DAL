using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount bonus flat map.
    /// </summary>
    public sealed class DiscountBonusFlatMap : IEntityTypeConfiguration<DiscountBonusFlat>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountBonusFlat> builder)
        {
            builder.ToTable(nameof(DiscountBonusFlat))
                .HasBaseType<Discount>();

            builder.Property(discountBonusFlat => discountBonusFlat.Id)
                .HasColumnOrder(0);

            builder.Property(discountBonusFlat => discountBonusFlat.Value)
                .HasColumnOrder(1)
                .IsRequired();
        }
    }
}
