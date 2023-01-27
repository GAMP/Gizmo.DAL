using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class VoidInvoiceMap : IEntityTypeConfiguration<VoidInvoice>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<VoidInvoice> builder)
        {
            //// Primary Key
            //builder.HasKey(t => t.Id);

            builder.Property(t => t.InvoiceId)
                .HasColumnOrder(1);

            // Indexes
            builder.HasIndex(t => t.InvoiceId).HasDatabaseName("UQ_Invoice").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(t => t.Invoice)
                .WithMany(t => t.Voids)
                .HasForeignKey(t => t.InvoiceId);

            // Table & Column Mappings
            builder.ToTable(nameof(VoidInvoice));
        }
    }
}
