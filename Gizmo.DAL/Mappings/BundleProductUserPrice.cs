using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class BundleProductUserPriceMap : EntityTypeConfiguration<BundleProductUserPrice>
    {
        public BundleProductUserPriceMap()
        {
            // Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("BundleProductUserPriceId")
                .HasColumnOrder(0);

            this.Property(x => x.BundleProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BundleProductUserGroup") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BundleProductUserGroup") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.Price)
                .HasColumnOrder(3);

            this.ToTable(nameof(BundleProductUserPrice));

            // Relations            
            this.HasRequired(x => x.BundleProduct)
                .WithMany(x => x.UserPrices)
                .HasForeignKey(x => x.BundleProductId);

            this.HasRequired(x => x.UserGroup)
                .WithMany()
                .HasForeignKey(x => x.UserGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}
