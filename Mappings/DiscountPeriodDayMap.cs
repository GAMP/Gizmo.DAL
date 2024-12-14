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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("DiscountPeriodDayId")
                .HasColumnOrder(0);

            builder.Property(x => x.DiscountPeriodId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.Day)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(t => new { t.DiscountPeriodId, t.Day })
                .IsUnique();

            builder.HasOne(x => x.Period)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.DiscountPeriodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
