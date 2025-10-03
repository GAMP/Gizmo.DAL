using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Product branch entity map.
    /// </summary>
    public class ProductBranchMap : IEntityTypeConfiguration<ProductBranch>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductBranch> builder)
        {
            builder.ToTable(nameof(ProductBranch));

            builder.HasKey(productBranch => new { productBranch.ProductId, productBranch.BranchId });

            builder.Property(productBranch => productBranch.ProductId)
                .HasColumnOrder(0);

            builder.Property(productBranch => productBranch.BranchId)
                .HasColumnOrder(1);

            builder.Property(productBranch => productBranch.IsEnabled)
                .HasColumnOrder(2);

            builder.HasIndex(productBranch => new { productBranch.ProductId, productBranch.BranchId })
                .IsUnique();

            builder.HasOne(productBranch => productBranch.Branch)
                .WithMany(branch => branch.Products)
                .HasForeignKey(productBranch => productBranch.BranchId);

            builder.HasOne(productBranch => productBranch.Product)
                .WithMany(product => product.Branches)
                .HasForeignKey(productBranch => productBranch.ProductId);
        }
    }
}
