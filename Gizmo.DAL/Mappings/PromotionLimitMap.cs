using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion limit map.
    /// </summary>
    public sealed class PromotionLimitMap : IEntityTypeConfiguration<PromotionLimit>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionLimit> builder)
        {
            builder.ToTable(nameof(PromotionLimit));

            builder.HasKey(promotionLimit => new { promotionLimit.PromotionId, promotionLimit.Type });

            builder.HasOne(promotionLimit => promotionLimit.Promotion)
                .WithMany(promotion => promotion.Limits)
                .HasForeignKey(promotionLimit => promotionLimit.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
