using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductTaxMap : IEntityTypeConfiguration<ProductTax>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTax> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ProductTaxId")
                .HasColumnOrder(0);

            builder.Property(x => x.ProductId)
                .HasColumnOrder(1);

            builder.Property(x => x.TaxId)
                .HasColumnOrder(2);

            builder.Property(x => x.UseOrder)
                .HasColumnOrder(3);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(4);

            // Indexes
            builder.HasIndex(x => new { x.ProductId, x.TaxId }, "UQ_TaxProduct").IsUnique();

            builder.ToTable("ProductTax");

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Taxes)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Tax)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.TaxId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
