using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Invoice line reservation fee map.
    /// </summary>
    public sealed class InvoiceLineReservationFeeMap : IEntityTypeConfiguration<InvoiceLineReservationFee>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InvoiceLineReservationFee> builder)
        {
            builder.ToTable(nameof(InvoiceLineReservationFee))
                .HasBaseType<InvoiceLine>();

            builder.Property(productOrderLine => productOrderLine.Id)
                .IsRequired()
                .HasColumnOrder(0)
                .HasColumnName("InvoiceLineId");

            builder.Property(invoiceLineReservationFee => invoiceLineReservationFee.OrderLineId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(invoiceLineReservationFee => invoiceLineReservationFee.Type)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(invoiceLineReservationFee => invoiceLineReservationFee.Fee)
                .IsRequired()
                .HasColumnOrder(3);

            builder.HasIndex(invoiceLineReservationFee => invoiceLineReservationFee.OrderLineId)
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(invoiceLineReservationFee => invoiceLineReservationFee.OrderLine)
                .WithOne()
                .HasForeignKey<InvoiceLineReservationFee>(invoiceLineReservationFee => invoiceLineReservationFee.OrderLineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
