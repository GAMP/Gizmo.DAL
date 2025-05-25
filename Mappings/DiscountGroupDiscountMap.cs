using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount group discount entity map.
    /// </summary>
    public sealed class DiscountGroupDiscountMap : IEntityTypeConfiguration<DiscountGroupDiscount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountGroupDiscount> builder)
        {
            builder.HasKey(discountGroupDiscount => new { discountGroupDiscount.DiscountGroupId, discountGroupDiscount.DiscountId });

            builder.Property(discountGroup => discountGroup.DiscountGroupId)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(discountGroup => discountGroup.DiscountId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.HasOne(discountGroupDiscount => discountGroupDiscount.DiscountGroup)
                .WithMany(discountGroup => discountGroup.Discounts)
                .HasForeignKey(discountGroupDiscount => discountGroupDiscount.DiscountGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
