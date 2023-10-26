using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductTaxMap : EntityTypeConfiguration<ProductTax>
    {
        public ProductTaxMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("ProductTaxId")
                .HasColumnOrder(0);

            Property(x => x.ProductId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_TaxProduct") { IsUnique = true, Order = 0 } }))
                .HasColumnOrder(1);

            Property(x => x.TaxId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_TaxProduct") { IsUnique = true, Order = 1 } }))
                .HasColumnOrder(2);

            Property(x => x.UseOrder)
                .HasColumnOrder(3);

            Property(x => x.IsEnabled)
                .HasColumnOrder(4);

            ToTable("ProductTax");

            HasRequired(x => x.Product)
                .WithMany(x => x.Taxes)
                .HasForeignKey(x => x.ProductId);

            HasRequired(x => x.Tax)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.TaxId)
                .WillCascadeOnDelete(false);
        }
    }
}
