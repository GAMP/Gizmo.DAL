using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class InvoiceFiscalReceiptMap : EntityTypeConfiguration<Entities.InvoiceFiscalReceipt>
    {
        public InvoiceFiscalReceiptMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("InvoiceFiscalReceiptId");

            Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            Property(x => x.FiscalReceiptId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_FiscalReceipt") { IsUnique = true }
                }));

            HasRequired(x => x.Invoice)
                .WithMany(x => x.FiscalReceipts)
                .HasForeignKey(x => x.InvoiceId);

            ToTable(nameof(Entities.InvoiceFiscalReceipt));
        }
    }
}
