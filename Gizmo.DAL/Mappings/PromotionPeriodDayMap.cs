using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion period day entity map.
    /// </summary>
    public sealed class PromotionPeriodDayMap : IEntityTypeConfiguration<PromotionPeriodDay>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionPeriodDay> builder)
        {
            builder.ToTable(nameof(PromotionPeriodDay));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("PromotionPeriodDayId")
                .HasColumnOrder(0);

            builder.Property(x => x.PromotionPeriodId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.Day)
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasIndex(t => new { t.PromotionPeriodId, t.Day })
                .IsUnique();

            builder.HasOne(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.PromotionPeriodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
