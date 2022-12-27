using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
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
            builder.HasIndex(t => t.FiscalReceiptId).HasDatabaseName("UQ_FiscalReceipt").IsUnique();

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.FiscalReceipts)
                .HasForeignKey(x => x.InvoiceId);

            builder.ToTable(nameof(Entities.InvoiceFiscalReceipt));
        }
    }
}
