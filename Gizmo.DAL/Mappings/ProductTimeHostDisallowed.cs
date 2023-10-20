using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ProductTimeHostDisallowedMap : EntityTypeConfiguration<ProductTimeHostDisallowed>
    {
        public ProductTimeHostDisallowedMap()
        {
            //key
            this.HasKey(x => x.Id);

            this.ToTable("ProductTimeHostDisallowed");

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductTimeHostDisallowedId");

            this.Property(x => x.ProductTimeId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimeHostGroup") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.HostGroupId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_ProductTimeHostGroup") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.IsDisallowed)
                .HasColumnOrder(3);

            this.HasRequired(x => x.ProductTime)
                .WithMany(x => x.DisallowedHostsGroup)
                .HasForeignKey(x => x.ProductTimeId);

            this.HasRequired(x => x.HostGroup)
                .WithMany(x => x.DisallowedTimeOffers)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
