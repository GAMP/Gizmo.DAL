using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion entity map.
    /// </summary>
    public sealed class PromotionPeriodMap : IEntityTypeConfiguration<PromotionPeriod>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionPeriod> builder)
        {
            builder.ToTable(nameof(PromotionPeriod));

            builder.HasKey(promotionPeriod => promotionPeriod.Id);

            builder.Property(promotionPeriod => promotionPeriod.Id)
                .HasColumnName("PromotionId")
                .ValueGeneratedNever();

            builder.HasIndex(t => t.Id);

            builder.HasOne(promotionPeriod => promotionPeriod.Promotion)
                .WithOne(promotion => promotion.Period)
                .HasForeignKey<PromotionPeriod>(promotionPeriod => promotionPeriod.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
