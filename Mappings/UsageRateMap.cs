using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Usage rate map.
    /// </summary>
    public class UsageRateMap : IEntityTypeConfiguration<UsageRate>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageRate> builder)
        {
            builder.ToTable(nameof(UsageRate));

            builder.Property(usageRate => usageRate.Id)
                .HasColumnOrder(0);

            builder.Property(usageRate => usageRate.BillRateId)
                .HasColumnOrder(1);

            builder.Property(usageRate => usageRate.Total)
                .HasColumnOrder(2);

            builder.Property(usageRate => usageRate.Rate)
                .HasColumnOrder(3);

            builder.Property(usageRate => usageRate.BillProfileStamp)
                .HasColumnOrder(4);

            builder.Property(usageRate => usageRate.DiscountId)
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.Property(usageRate => usageRate.DiscountCalculationType)
                .IsRequired(false)
                .HasColumnOrder(6);

            builder.Property(usageRate => usageRate.DiscountValue)
                .IsRequired(false)
                .HasColumnOrder(7);

            builder.Property(usageRate => usageRate.DiscountAmount)
                .IsRequired()
                .HasColumnOrder(8);

            builder.HasIndex(usageRate => usageRate.Id);

            builder.HasOne(usageRate => usageRate.BillRate)
                .WithMany(usageRate => usageRate.Usage)
                .HasForeignKey(usageRate => usageRate.BillRateId);

            builder.HasOne(billRate => billRate.Discount)
                .WithMany()
                .HasForeignKey(billRate => billRate.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
