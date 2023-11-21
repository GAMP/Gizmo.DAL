using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class VoidInvoiceMap : EntityTypeConfiguration<VoidInvoice>
    {
        public VoidInvoiceMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.InvoiceId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Invoice") { IsUnique = true }
                }));

            HasRequired(t => t.Invoice)
                .WithMany(t => t.Voids)
                .HasForeignKey(t => t.InvoiceId);

            // Table & Column Mappings
            ToTable(nameof(VoidInvoice));
        }
    }
}
