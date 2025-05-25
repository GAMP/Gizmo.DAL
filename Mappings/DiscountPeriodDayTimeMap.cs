using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount period day time entity map.
    /// </summary>
    public sealed class DiscountPeriodDayTimeMap : IEntityTypeConfiguration<DiscountPeriodDayTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountPeriodDayTime> builder)
        {
            builder.ToTable(nameof(DiscountPeriodDayTime));

            builder.HasKey(periodDayTime => new { periodDayTime.DiscountPeriodDayId, periodDayTime.StartSecond, periodDayTime.EndSecond });
            builder.HasIndex(periodDayTime => periodDayTime.DiscountPeriodDayId);

            builder.Property(periodDayTime => periodDayTime.DiscountPeriodDayId)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(periodDayTime => periodDayTime.StartSecond)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(periodDayTime => periodDayTime.EndSecond)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(periodDayTime => periodDayTime.Day)
                .WithMany(periodDay => periodDay.Times)
                .HasForeignKey(periodDayTime => periodDayTime.DiscountPeriodDayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
