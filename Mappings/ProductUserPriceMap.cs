using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductUserPriceMap : IEntityTypeConfiguration<ProductUserPrice>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductUserPrice> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ProductUserPriceId")
                .HasColumnOrder(0);

            builder.Property(x => x.ProductId)
                .HasColumnOrder(1);
                
            builder.Property(x => x.UserGroupId)
                .HasColumnOrder(2);

            builder.Property(x => x.Price)
                .HasColumnOrder(3);

            builder.Property(x => x.PointsPrice)
                .HasColumnOrder(4);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            // Indexes
            builder.HasIndex(x => new { x.ProductId, x.UserGroupId }).IsUnique();

            builder.ToTable("ProductUserPrice");

            // Relations            
            builder.HasOne(x => x.Product)
                .WithMany(x => x.UserGroupPrices)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.UserGroup)
                .WithMany(x => x.ProductPrices)
                .HasForeignKey(x => x.UserGroupId);
        }
    }
}
