using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class ProductTaxMap : EntityTypeConfiguration<ProductTax>
    {
        public ProductTaxMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnName("ProductTaxId")
                .HasColumnOrder(0);

            this.Property(x => x.ProductId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_TaxProduct") { IsUnique = true, Order = 0 } }))
                .HasColumnOrder(1);

            this.Property(x => x.TaxId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_TaxProduct") { IsUnique = true, Order = 1 } }))
                .HasColumnOrder(2);

            this.Property(x => x.UseOrder)
                .HasColumnOrder(3);

            this.Property(x => x.IsEnabled)
                .HasColumnOrder(4);

            this.ToTable("ProductTax");

            this.HasRequired(x => x.Product)
                .WithMany(x => x.Taxes)
                .HasForeignKey(x => x.ProductId);

            this.HasRequired(x => x.Tax)
                .WithMany(x=>x.Products)
                .HasForeignKey(x => x.TaxId)
                .WillCascadeOnDelete(false);
        }
    }
}
