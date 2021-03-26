using GizmoDALV2.Entities;
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
    public class ProductHostHiddenMap : EntityTypeConfiguration<ProductHostHidden>
    {
        public ProductHostHiddenMap()
        {
            //key
            this.HasKey(x => x.Id);

            this.ToTable("ProductHostHidden");

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductHostHiddenId");

            this.Property(x => x.ProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductHostGroup") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.HostGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductHostGroup") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.IsHidden)
                .HasColumnOrder(3);

            this.HasRequired(x => x.Product)
                .WithMany(x => x.HiddenHostGroups)
                .HasForeignKey(x => x.ProductId);

            this.HasRequired(x => x.HostGroup)
                .WithMany(x => x.HiddenProducts)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
