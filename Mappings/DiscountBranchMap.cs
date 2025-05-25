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

            builder.HasKey(discountBranch => new { discountBranch.DiscountId, discountBranch.BranchId });

            builder.Property(discountBranch => discountBranch.DiscountId)
                .HasColumnOrder(0);

            builder.Property(discountBranch => discountBranch.BranchId)
                .HasColumnOrder(1);

            builder.Property(discountBranch => discountBranch.IsEnabled)
                .HasColumnOrder(2);

            builder.HasIndex(discountBranch => new { discountBranch.DiscountId, discountBranch.BranchId })
                .IsUnique();

            builder.HasOne(discountBranch => discountBranch.Branch)
                .WithMany(branch => branch.Discounts)
                .HasForeignKey(discountBranch => discountBranch.BranchId);
        }
    }
}
