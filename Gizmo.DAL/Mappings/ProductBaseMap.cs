using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ProductBaseMap : EntityTypeConfiguration<ProductBase>
    {
        public ProductBaseMap()
        {
            // Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("ProductId")
                .HasColumnOrder(0);

            Property(x => x.ProductGroupId)
                .HasColumnOrder(1);

            Property(x => x.Name)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.Description)
                .HasMaxLength(SQLStringSize.NORMAL)
                .HasColumnOrder(3);

            Property(x => x.Price)
                .HasColumnOrder(4);

            Property(x => x.Cost)
                .HasColumnOrder(5);

            Property(x => x.Points)
                .HasColumnOrder(6);

            Property(x => x.PointsPrice)
                .HasColumnOrder(7);

            Property(x => x.Barcode)
                .HasColumnOrder(8)
                .IsOptional()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Barcode") { IsUnique = true }
                }));

            // Relations
            ToTable("ProductBase");

            HasRequired(x => x.ProductGroup)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductGroupId);
        }
    }
}
