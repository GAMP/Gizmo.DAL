using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductImageMap : IEntityTypeConfiguration<ProductImage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Image)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            builder.ToTable(nameof(ProductImage));

            builder.Property(x => x.Id)
                .HasColumnName("ProductImageId");

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x=>x.ProductId);
        }
    }
}