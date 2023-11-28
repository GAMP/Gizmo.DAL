using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductImageMap : EntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.Image)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            ToTable(nameof(ProductImage));

            Property(x => x.Id)
                .HasColumnName("ProductImageId");

            HasRequired(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductId);
        }
    }
}