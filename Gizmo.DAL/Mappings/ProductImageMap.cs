using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class ProductImageMap : EntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.Image)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLByteArraySize.MEDIUM);

            // Table & Column Mappings
            this.ToTable(nameof(ProductImage));

            this.Property(x => x.Id)
                .HasColumnName("ProductImageId");

            this.HasRequired(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x=>x.ProductId);
        }
    }
}