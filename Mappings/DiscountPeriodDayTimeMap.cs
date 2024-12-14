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

            builder.HasKey(x => new { x.DiscountPeriodDayId, x.StartSecond, x.EndSecond });
            builder.HasIndex(t => t.DiscountPeriodDayId);

            builder.Property(x => x.DiscountPeriodDayId)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(x => x.StartSecond)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.EndSecond)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.DiscountPeriodDayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
