using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class InvoiceLineTimeFixedMap : IEntityTypeConfiguration<InvoiceLineTimeFixed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineTimeFixed> builder)
        {
            builder.ToTable(nameof(InvoiceLineTimeFixed));

            // Indexes
            builder.HasIndex(t => t.OrderLineId).HasDatabaseName("UQ_OrderLine").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
