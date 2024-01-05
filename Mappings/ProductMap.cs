using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
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
