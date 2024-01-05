using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class BundleProductMap : IEntityTypeConfiguration<BundleProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BundleProduct> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("BundleProductId");

            builder.Property(x => x.ProductBundleId)
                .HasColumnOrder(1);

            builder.Property(x => x.ProductId)
                .HasColumnOrder(2);

            builder.Property(x => x.Quantity)
                .HasColumnOrder(3);

            // Relations
            builder.ToTable("BundleProduct");

            builder.HasOne(x => x.ProductBundle)
                .WithMany(x => x.BundledProducts)
                .HasForeignKey(x => x.ProductBundleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
        }
    }
}
