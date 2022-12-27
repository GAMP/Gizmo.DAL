using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class ProductBaseExtendedMap : IEntityTypeConfiguration<ProductBaseExtended>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductBaseExtended> builder)
        {
            // Indexes
            builder.HasIndex(t => t.Id);

            builder.ToTable("ProductBaseExtended");
        }        
    }
}
