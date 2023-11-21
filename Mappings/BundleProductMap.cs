using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class BundleProductMap : EntityTypeConfiguration<BundleProduct>
    {
        public BundleProductMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("BundleProductId");

            Property(x => x.ProductBundleId)
                .HasColumnOrder(1);

            Property(x => x.ProductId)
                .HasColumnOrder(2);

            Property(x => x.Quantity)
                .HasColumnOrder(3);
            
            // Relations
            ToTable("BundleProduct");

            HasRequired(x => x.ProductBundle)
                .WithMany(x => x.BundledProducts)
                .HasForeignKey(x => x.ProductBundleId);

            HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
        }
    }
}