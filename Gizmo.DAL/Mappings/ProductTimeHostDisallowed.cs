using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductTimeHostDisallowedMap : EntityTypeConfiguration<ProductTimeHostDisallowed>
    {
        public ProductTimeHostDisallowedMap()
        {
            //key
            HasKey(x => x.Id);

            ToTable("ProductTimeHostDisallowed");

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductTimeHostDisallowedId");

            Property(x => x.ProductTimeId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimeHostGroup") { IsUnique = true, Order = 0 } }));

            Property(x => x.HostGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimeHostGroup") { IsUnique = true, Order = 1 } }));

            Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            HasRequired(x => x.ProductTime)
                .WithMany(x => x.DisallowedHostsGroup)
                .HasForeignKey(x => x.ProductTimeId);

            HasRequired(x => x.HostGroup)
                .WithMany(x => x.DisallowedTimeOffers)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
