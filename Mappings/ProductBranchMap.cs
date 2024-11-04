using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductBranchMap : IEntityTypeConfiguration<ProductBranch>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductBranch> builder)
        {
            builder.HasKey(t => new { t.ProductId, t.BranchId });

            builder.Property(e => e.ProductId)
                .HasColumnOrder(0);
            builder.Property(e => e.BranchId)
                .HasColumnOrder(1);
            builder.Property(e => e.IsEnabled)
                .HasColumnOrder(3);

            builder.HasIndex(t => new { t.ProductId, t.BranchId }).IsUnique().HasFilter(null);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BranchId);

            builder.ToTable(nameof(ProductBranch));
        }
    }
}
