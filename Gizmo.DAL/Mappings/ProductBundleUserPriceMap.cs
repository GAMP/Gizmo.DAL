using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ProductBundleUserPriceMap : EntityTypeConfiguration<ProductBundleUserPrice>
    {
        public ProductBundleUserPriceMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("ProductBundleUserPriceId")
                .HasColumnOrder(0);

            Property(x => x.ProductBundleId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductBundlePriceUserGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductBundlePriceUserGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.Price)
                .HasColumnOrder(3);

            Property(x => x.PointsPrice)
                .HasColumnOrder(4);

            Property(x => x.PurchaseOptions)
                .HasColumnOrder(5);

            ToTable(nameof(ProductBundleUserPrice));

            // Relations            
            HasRequired(x => x.ProductBundle)
                .WithMany(x => x.UserPrices)
                .HasForeignKey(x => x.ProductBundleId);

            HasRequired(x => x.UserGroup)
                .WithMany()
                .HasForeignKey(x => x.UserGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}
