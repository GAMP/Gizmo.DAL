using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class InvoiceLineTimeMap : IEntityTypeConfiguration<InvoiceLineTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineTime> builder)
        {
            builder.ToTable(nameof(InvoiceLineTime));

            // Indexes
            builder.HasIndex(t => t.OrderLineId).HasDatabaseName("UQ_OrderLine").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ProductTimeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
