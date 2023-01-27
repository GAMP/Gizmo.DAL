using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class InvoiceLineSessionMap : IEntityTypeConfiguration<InvoiceLineSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineSession> builder)
        {
            builder.ToTable(nameof(InvoiceLineSession));

            // Indexes
            builder.HasIndex(t => t.UsageSessionId).HasDatabaseName("UQ_UsageSession").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UsageSession)
                .WithMany(x=>x.InvoiceLines)
                .HasForeignKey(x => x.UsageSessionId);
        }
    }
}
