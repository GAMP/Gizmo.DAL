using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Product extended entity map.
    /// </summary>
    public class ProductOLExtendedMap : IEntityTypeConfiguration<ProductOLExtended>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductOLExtended> builder)
        {
            builder.ToTable(nameof(ProductOLExtended))
                .HasBaseType<ProductOL>();

            builder.Property(productOlExtended => productOlExtended.BundleLineId)
                .IsRequired(false);

            builder.Property(productOlExtended => productOlExtended.ReservationId)
               .IsRequired(false);

            builder.Property(productOlExtended => productOlExtended.ReservationHostId)
                .IsRequired(false);

            builder.HasOne(x => x.BundleLine)
                .WithMany()
                .HasForeignKey(x => x.BundleLineId);

            builder.HasOne(productOlExtended => productOlExtended.Reservation)
                .WithMany()
                .HasForeignKey(productOlExtended => productOlExtended.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(productOlExtended => productOlExtended.ReservationHost)
                .WithMany()
                .HasForeignKey(productOlExtended => productOlExtended.ReservationHostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
