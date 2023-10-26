using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductHostHiddenMap : EntityTypeConfiguration<ProductHostHidden>
    {
        public ProductHostHiddenMap()
        {
            //Primary key
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductHostHiddenId");

            Property(x => x.ProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductHostGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.HostGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductHostGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.IsHidden)
                .HasColumnOrder(3);

            // Table & Column Mappings
            ToTable(nameof(ProductHostHidden));

            HasRequired(x => x.Product)
                .WithMany(x => x.HiddenHostGroups)
                .HasForeignKey(x => x.ProductId);

            HasRequired(x => x.HostGroup)
                .WithMany(x => x.HiddenProducts)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
