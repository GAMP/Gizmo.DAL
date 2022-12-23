using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Indexes
            builder.HasIndex(t => t.Id);

            builder.ToTable("Product");
        }        
    }
}
