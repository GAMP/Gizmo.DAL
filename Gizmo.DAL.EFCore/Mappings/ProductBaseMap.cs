using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductBaseMap : IEntityTypeConfiguration<ProductBase>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductBase> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("ProductId")
                .HasColumnOrder(0);

            builder.Property(x => x.ProductGroupId)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Description)
                .HasMaxLength(SQLStringSize.NORMAL)
                .HasColumnOrder(3);

            builder.Property(x => x.Price)
                .HasColumnOrder(4);

            builder.Property(x => x.Cost)
                .HasColumnOrder(5);

            builder.Property(x => x.Points)
                .HasColumnOrder(6);

            builder.Property(x => x.PointsPrice)
                .HasColumnOrder(7);

            builder.Property(x => x.Barcode)
                .HasColumnOrder(8)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();
            builder.HasIndex(t => t.Barcode).HasDatabaseName("UQ_Barcode").IsUnique();

            // Relations
            builder.ToTable("ProductBase");

            builder.HasOne(x => x.ProductGroup)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductGroupId);
        }
    }
}
