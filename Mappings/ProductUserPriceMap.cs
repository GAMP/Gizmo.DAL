using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductUserPriceMap : EntityTypeConfiguration<ProductUserPrice>
    {
        public ProductUserPriceMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("ProductUserPriceId")
                .HasColumnOrder(0);

            Property(x => x.ProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductUserGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductUserGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.Price)
                .HasColumnOrder(3);

            Property(x => x.PointsPrice)
                .HasColumnOrder(4);

            Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            ToTable("ProductUserPrice");

            // Relations            
            HasRequired(x => x.Product)
                .WithMany(x => x.UserGroupPrices)
                .HasForeignKey(x => x.ProductId);

            HasRequired(x => x.UserGroup)
                .WithMany(x => x.ProductPrices)
                .HasForeignKey(x => x.UserGroupId);
        }
    }
}
