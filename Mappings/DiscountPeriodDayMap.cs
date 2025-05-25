using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount period day entity map.
    /// </summary>
    public sealed class DiscountPeriodDayMap : IEntityTypeConfiguration<DiscountPeriodDay>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountPeriodDay> builder)
        {
            builder.ToTable(nameof(DiscountPeriodDay));

            builder.HasKey(periodDay => periodDay.Id);

            builder.Property(periodDay => periodDay.Id)
                .HasColumnName("DiscountPeriodDayId")
                .HasColumnOrder(0);

            builder.Property(periodDay => periodDay.DiscountPeriodId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(periodDay => periodDay.Day)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(periodDay => new { periodDay.DiscountPeriodId, periodDay.Day })
                .IsUnique();

            builder.HasOne(periodDay => periodDay.Period)
                .WithMany(period => period.Days)
                .HasForeignKey(periodDay => periodDay.DiscountPeriodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
