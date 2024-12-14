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

            builder.HasKey(x => x.Id);
            builder.HasIndex(t => t.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Id)
                .HasColumnName("DiscountId");

            builder.HasOne(x => x.Discount)
                .WithOne(x => x.Period)
                .HasForeignKey<DiscountPeriod>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
