using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
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
