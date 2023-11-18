using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class InvoiceFiscalReceiptMap : IEntityTypeConfiguration<InvoiceFiscalReceipt>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceFiscalReceipt> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("InvoiceFiscalReceiptId");

            builder.Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            builder.Property(x => x.FiscalReceiptId)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(t => t.FiscalReceiptId).IsUnique();

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.FiscalReceipts)
                .HasForeignKey(x => x.InvoiceId);

            builder.ToTable(nameof(InvoiceFiscalReceipt));
        }
    }
}
