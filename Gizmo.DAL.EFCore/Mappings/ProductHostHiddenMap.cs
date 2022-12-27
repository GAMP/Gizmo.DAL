using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class ProductHostHiddenMap : IEntityTypeConfiguration<ProductHostHidden>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductHostHidden> builder)
        {
            //Primary key
            builder.HasKey(x => x.Id);            

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("ProductHostHiddenId");

            builder.Property(x => x.ProductId)
                .HasColumnOrder(1);

            builder.Property(x => x.HostGroupId)
                .HasColumnOrder(2);
                
            builder.Property(x => x.IsHidden)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => new { x.ProductId, x.HostGroupId }).HasDatabaseName("UQ_ProductHostGroup").IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(ProductHostHidden));

            builder.HasOne(x => x.Product)
                .WithMany(x => x.HiddenHostGroups)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.HostGroup)
                .WithMany(x => x.HiddenProducts)
                .HasForeignKey(x => x.HostGroupId);
        }
    }
}
