using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion entity map.
    /// </summary>
    public sealed class PromotionDiscountGroupMap : IEntityTypeConfiguration<PromotionDiscountGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionDiscountGroup> builder)
        {
            builder.ToTable(nameof(PromotionDiscountGroup))
                .HasBaseType<Promotion>();

            builder.HasOne(promotionDiscount => promotionDiscount.DiscountGroup)
                .WithMany()
                .HasForeignKey(promotionDiscount => promotionDiscount.DiscountGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
