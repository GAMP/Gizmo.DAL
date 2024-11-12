using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion period day time entity map.
    /// </summary>
    public sealed class PromotionPeriodDayTimeMap : IEntityTypeConfiguration<PromotionPeriodDayTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionPeriodDayTime> builder)
        {
            builder.ToTable(nameof(PromotionPeriodDayTime));

            builder.HasKey(x => new { x.PromotionPeriodDayId, x.StartSecond, x.EndSecond });
            builder.HasIndex(t => t.PromotionPeriodDayId);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PromotionPeriodDayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
