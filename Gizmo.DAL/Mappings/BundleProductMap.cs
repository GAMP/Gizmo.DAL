using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class BundleProductMap : EntityTypeConfiguration<BundleProduct>
    {
        public BundleProductMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("BundleProductId");

            this.Property(x => x.ProductBundleId)
                .HasColumnOrder(1);

            this.Property(x => x.ProductId)
                .HasColumnOrder(2);

            this.Property(x => x.Quantity)
                .HasColumnOrder(3);
            
            // Relations
            this.ToTable("BundleProduct");

            this.HasRequired(x => x.ProductBundle)
                .WithMany(x => x.BundledProducts)
                .HasForeignKey(x => x.ProductBundleId);

            this.HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
        }
    }
}