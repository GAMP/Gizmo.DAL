using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Fiscal receipt invoice map.
    /// </summary>
    public class InvoiceFiscalReceiptMap : IEntityTypeConfiguration<InvoiceFiscalReceipt>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<InvoiceFiscalReceipt> builder)
        {
            builder.ToTable(nameof(InvoiceFiscalReceipt));

            builder.HasKey(invoiceFiscalReceipt => invoiceFiscalReceipt.Id);

            builder.Property(invoiceFiscalReceipt => invoiceFiscalReceipt.Id)
                .HasColumnName("InvoiceFiscalReceiptId");

            builder.Property(invoiceFiscalReceipt => invoiceFiscalReceipt.InvoiceId)
                .HasColumnOrder(1);

            builder.Property(invoiceFiscalReceipt => invoiceFiscalReceipt.FiscalReceiptId)
                .HasColumnOrder(2);

            builder.HasIndex(invoiceFiscalReceipt => invoiceFiscalReceipt.FiscalReceiptId).IsUnique();

            builder.HasOne(invoiceFiscalReceipt => invoiceFiscalReceipt.Invoice)
                .WithMany(invoice => invoice.FiscalReceipts)
                .HasForeignKey(invoiceFiscalReceipt => invoiceFiscalReceipt.InvoiceId);
        }
    }
}
