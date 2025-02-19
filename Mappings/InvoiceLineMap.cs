using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Invoice line entity map.
    /// </summary>
    public class InvoiceLineMap : IEntityTypeConfiguration<InvoiceLine>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.HasKey(invoiceLine => invoiceLine.Id);

            builder.ToTable(nameof(InvoiceLine));

            builder.Property(invoiceLine => invoiceLine.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoiceLineId");

            builder.Property(invoiceLine => invoiceLine.InvoiceId)
                .HasColumnOrder(1);

            builder.Property(invoiceLine => invoiceLine.UserId)
                .HasColumnOrder(2);

            builder.Property(invoiceLine => invoiceLine.ProductName)
                .HasColumnOrder(3);

            builder.Property(invoiceLine => invoiceLine.Quantity)
                .HasColumnOrder(4);

            builder.Property(invoiceLine => invoiceLine.UnitPrice)
                .HasColumnOrder(5);

            builder.Property(invoiceLine => invoiceLine.UnitListPrice)
                .HasColumnOrder(6);

            builder.Property(invoiceLine => invoiceLine.UnitPointsPrice)
                .HasColumnOrder(7);

            builder.Property(invoiceLine => invoiceLine.UnitPointsListPrice)
                .HasColumnOrder(8);

            builder.Property(invoiceLine => invoiceLine.UnitCost)
                .HasColumnOrder(9);

            builder.Property(invoiceLine => invoiceLine.Cost)
                .HasColumnOrder(10);

            builder.Property(invoiceLine => invoiceLine.TaxRate)
                .HasColumnOrder(11);

            builder.Property(invoiceLine => invoiceLine.PreTaxTotal)
                .HasColumnOrder(12);

            builder.Property(invoiceLine => invoiceLine.Total)
                .HasColumnOrder(13);

            builder.Property(invoiceLine => invoiceLine.PointsTotal)
                .HasColumnOrder(14);

            builder.Property(invoiceLine => invoiceLine.Points)
                .HasColumnOrder(15);

            builder.Property(invoiceLine => invoiceLine.PointsAward)
                .HasColumnOrder(16);

            builder.Property(invoiceLine => invoiceLine.TaxTotal)
                .HasColumnOrder(17);

            builder.Property(invoiceLine => invoiceLine.PayType)
                .HasColumnOrder(18);

            builder.Property(invoiceLine => invoiceLine.PointsTransactionId)
                .HasColumnOrder(19);

            builder.Property(invoiceLine => invoiceLine.IsDeleted)
                .HasColumnOrder(20);

            builder.Property(invoiceLine => invoiceLine.IsVoided)
                .HasColumnOrder(21);

            builder.Property(invoiceLine => invoiceLine.ReservationId)
                .IsRequired(false)
                .HasColumnOrder(22);

            builder.Property(invoiceLine => invoiceLine.ReservationHostId)
                .IsRequired(false)
                .HasColumnOrder(23);

            builder.Property(invoiceLine => invoiceLine.ReservationSlot)
                .IsRequired(false)
                .HasColumnOrder(24);

            // Indexes
            builder.HasIndex(invoiceLine => invoiceLine.PointsTransactionId)
                .IsUnique();

            builder.HasOne(invoiceLine => invoiceLine.Invoice)
                .WithMany(invoiceLine => invoiceLine.InvoiceLines)
                .HasForeignKey(invoiceLine => invoiceLine.InvoiceId);

            builder.HasOne(invoiceLine => invoiceLine.User)
                .WithMany(invoiceLine => invoiceLine.InvoiceLines)
                .HasForeignKey(invoiceLine => invoiceLine.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(invoiceLine => invoiceLine.PointsTransaction)
                .WithMany()
                .HasForeignKey(invoiceLine => invoiceLine.PointsTransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(invoiceLine => invoiceLine.Reservation)
                .WithMany(reservation => reservation.InvoiceLines)
                .HasForeignKey(invoiceLine => invoiceLine.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(invoiceLine => invoiceLine.ReservationHost)
                .WithMany(reservationHost => reservationHost.InvoiceLines)
                .HasForeignKey(invoiceLine => invoiceLine.ReservationHostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
