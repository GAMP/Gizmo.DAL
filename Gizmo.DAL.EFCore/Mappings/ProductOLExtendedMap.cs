using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ProductOLExtendedMap : IEntityTypeConfiguration<ProductOLExtended>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOLExtended> builder)
        {
            builder.ToTable(nameof(ProductOLExtended));

            builder.HasOne(x => x.BundleLine)
                .WithMany()
                .HasForeignKey(x => x.BundleLineId);
        }
    }
}
