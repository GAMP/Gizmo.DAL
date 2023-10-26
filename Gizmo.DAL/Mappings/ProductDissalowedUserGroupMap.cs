using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductUserDisallowedMap : EntityTypeConfiguration<ProductUserDisallowed>
    {
        public ProductUserDisallowedMap()
        {
            HasKey(x => x.Id);

            ToTable("ProductUserDisallowed");

            Property(x => x.Id)
                .HasColumnName("ProductUserDisallowedId")
                .HasColumnOrder(0);

            Property(x => x.ProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductUserGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductUserGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            HasRequired(x => x.Product)
                .WithMany(x => x.DisallowedUserGroups)
                .HasForeignKey(x => x.ProductId);

            HasRequired(x => x.UserGroup)
                .WithMany(x => x.DissalowedProducts)
                .HasForeignKey(x => x.UserGroupId);
        }
    }
}
