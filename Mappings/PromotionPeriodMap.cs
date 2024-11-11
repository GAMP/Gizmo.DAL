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

            builder.HasKey(period => period.Id);

            builder.Property(period => period.Id)
                .HasColumnName("PromotionId")
                .ValueGeneratedNever();

            builder.HasIndex(t => t.Id);

            builder.HasOne(period => period.Promotion)
                .WithOne(promotion => promotion.Period)
                .HasForeignKey<PromotionPeriod>(period => period.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
