using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Reservation order entity map.
    /// </summary>
    public sealed class ReservationProductOrderMap : IEntityTypeConfiguration<ReservationProductOrder>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ReservationProductOrder> builder)
        {
            builder.ToTable(nameof(ReservationProductOrder));

            builder.HasKey(reservationOrder => reservationOrder.Id);

            builder.Property(reservationOrder => reservationOrder.Id)
                .HasColumnName("ReservationProductOrderId")
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(reservationOrder => reservationOrder.ReservationId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(reservationOrder => reservationOrder.ProductOrderId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(reservationOrder => reservationOrder.ProductOrderId)
                .IsUnique()
                .HasFilter(null);

            builder.HasIndex(reservationOrder => reservationOrder.ReservationId)
                .IsUnique()
                .HasFilter(null);
        }
    }
}
