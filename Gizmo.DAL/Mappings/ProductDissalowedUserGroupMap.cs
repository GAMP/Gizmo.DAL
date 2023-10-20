using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ProductUserDisallowedMap : EntityTypeConfiguration<ProductUserDisallowed>
    {
        public ProductUserDisallowedMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("ProductUserDisallowed");

            this.Property(x => x.Id)
                .HasColumnName("ProductUserDisallowedId")
                .HasColumnOrder(0);

            this.Property(x => x.ProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductUserGroup") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductUserGroup") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            this.HasRequired(x => x.Product)
                .WithMany(x => x.DisallowedUserGroups)
                .HasForeignKey(x => x.ProductId);

            this.HasRequired(x => x.UserGroup)
                .WithMany(x => x.DissalowedProducts)
                .HasForeignKey(x => x.UserGroupId);
        }
    }
}
