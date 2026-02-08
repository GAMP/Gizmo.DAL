using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion entity map.
    /// </summary>
    public sealed class PromotionDiscountMap : IEntityTypeConfiguration<PromotionDiscount>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionDiscount> builder)
        {
            builder.ToTable(nameof(PromotionDiscount))
                .HasBaseType<Promotion>();

            builder.HasOne(promotionDiscount => promotionDiscount.Discount)
                .WithMany()
                .HasForeignKey(promotionDiscount => promotionDiscount.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
