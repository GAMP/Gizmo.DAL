using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount period entity map.
    /// </summary>
    public sealed class DiscountPeriodMap : IEntityTypeConfiguration<DiscountPeriod>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DiscountPeriod> builder)
        {
            builder.ToTable(nameof(DiscountPeriod));

            builder.HasKey(discountPeriod => discountPeriod.Id);
            builder.HasIndex(discountPeriod => discountPeriod.Id);
            builder.Property(discountPeriod => discountPeriod.Id)
                .ValueGeneratedNever();

            builder.Property(discountPeriod => discountPeriod.Id)
                .HasColumnName("DiscountId");

            builder.HasOne(discountPeriod => discountPeriod.Discount)
                .WithOne(discount => discount.Period)
                .HasForeignKey<DiscountPeriod>(discountPeriod => discountPeriod.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
