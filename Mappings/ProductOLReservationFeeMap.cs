using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Product order line reservation fee map.
    /// </summary>
    public sealed class ProductOLReservationFeeMap : IEntityTypeConfiguration<ProductOLReservationFee>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductOLReservationFee> builder)
        {
            builder.ToTable(nameof(ProductOLReservationFee))
                .HasBaseType<ProductOL>();

            builder.Property(productOrderLineReservationFee => productOrderLineReservationFee.ReservationId)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(productOrderLineReservationFee => productOrderLineReservationFee.Type)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(productOrderLineReservationFee => productOrderLineReservationFee.Fee)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(productOrderLineReservationFee => productOrderLineReservationFee.Reservation)
                .WithMany()
                .HasForeignKey(productOrderLineReservationFee => productOrderLineReservationFee.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
