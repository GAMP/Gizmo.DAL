using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Discount branch entity map.
    /// </summary>
    public class DiscountBranchMap : IEntityTypeConfiguration<DiscountBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<DiscountBranch> builder)
        {
            builder.ToTable(nameof(DiscountBranch));

            builder.HasKey(t => new { t.DiscountId, t.BranchId });

            builder.Property(e => e.DiscountId)
                .HasColumnOrder(0);

            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);

            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(2);

            builder.HasIndex(t => new { t.DiscountId, t.BranchId })
                .IsUnique();

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Discounts)
                .HasForeignKey(x => x.BranchId);
        }
    }
}
