using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class VoidMap : EntityTypeConfiguration<Void>
    {
        public VoidMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("VoidId")
                .HasColumnOrder(0);

            // Table & Column Mappings
            ToTable(nameof(Void));
        }
    }

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
