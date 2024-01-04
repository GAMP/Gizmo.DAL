using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductBundleMap : IEntityTypeConfiguration<ProductBundle>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductBundle> builder)
        {
            // Indexes
            builder.HasIndex(t => t.Id);

            // Table
            builder.ToTable("ProductBundle");
        }
    }
}