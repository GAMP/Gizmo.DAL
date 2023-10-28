using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ProductOLTimeFixedMap : IEntityTypeConfiguration<ProductOLTimeFixed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOLTimeFixed> builder)
        {
            builder.ToTable(nameof(ProductOLTimeFixed));
        }
    }
}
