using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class BundleProductUserPriceMap : EntityTypeConfiguration<BundleProductUserPrice>
    {
        public BundleProductUserPriceMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("BundleProductUserPriceId")
                .HasColumnOrder(0);

            Property(x => x.BundleProductId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BundleProductUserGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.UserGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_BundleProductUserGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.Price)
                .HasColumnOrder(3);

            ToTable(nameof(BundleProductUserPrice));

            // Relations            
            HasRequired(x => x.BundleProduct)
                .WithMany(x => x.UserPrices)
                .HasForeignKey(x => x.BundleProductId);

            HasRequired(x => x.UserGroup)
                .WithMany()
                .HasForeignKey(x => x.UserGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}
