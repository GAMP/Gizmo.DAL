using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class InvoiceFiscalReceiptMap : EntityTypeConfiguration<Entities.InvoiceFiscalReceipt>
    {
        public InvoiceFiscalReceiptMap()
        {
            HasKey(x=>x.Id);

            Property(x => x.Id)
                .HasColumnName("InvoiceFiscalReceiptId");

            Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            HasRequired(x => x.Invoice)
                .WithMany(x => x.FiscalReceipts)
                .HasForeignKey(x => x.InvoiceId);

            ToTable(nameof(Entities.InvoiceFiscalReceipt));
        }
    }
}
