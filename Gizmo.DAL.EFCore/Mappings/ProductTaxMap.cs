using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
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
                .WithMany(x=>x.Products)
                .HasForeignKey(x => x.TaxId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeds
            builder.HasData(new ProductTax() { Id = 1, ProductId = 1, TaxId = 1});
            builder.HasData(new ProductTax() { Id = 2, ProductId = 2, TaxId = 1});
            builder.HasData(new ProductTax() { Id = 3, ProductId = 3, TaxId = 1});
            builder.HasData(new ProductTax() { Id = 4, ProductId = 4, TaxId = 1});
            builder.HasData(new ProductTax() { Id = 5, ProductId = 5, TaxId = 1});
            builder.HasData(new ProductTax() { Id = 6, ProductId = 6, TaxId = 1});
            builder.HasData(new ProductTax() { Id = 7, ProductId = 7, TaxId = 1});
        }
    }
}
