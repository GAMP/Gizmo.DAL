using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductBundleUserPriceMap : IEntityTypeConfiguration<ProductBundleUserPrice>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductBundleUserPrice> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ProductBundleUserPriceId")
                .HasColumnOrder(0);

            builder.Property(x => x.ProductBundleId)
                .HasColumnOrder(1);
                
            builder.Property(x => x.UserGroupId)
                .HasColumnOrder(2);

            builder.Property(x => x.Price)
                .HasColumnOrder(3);

            builder.Property(x => x.PointsPrice)
                .HasColumnOrder(4);

            builder.Property(x => x.PurchaseOptions)
                .HasColumnOrder(5);

            // Indexes
            builder.HasIndex(x => new { x.ProductBundleId, x.UserGroupId }).IsUnique();

            builder.ToTable(nameof(ProductBundleUserPrice));

            // Relations            
            builder.HasOne(x => x.ProductBundle)
                .WithMany(x => x.UserPrices)
                .HasForeignKey(x => x.ProductBundleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UserGroup)
                .WithMany()
                .HasForeignKey(x => x.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
