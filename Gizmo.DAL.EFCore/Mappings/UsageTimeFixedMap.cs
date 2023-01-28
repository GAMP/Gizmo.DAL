using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UsageTimeFixedMap : IEntityTypeConfiguration<UsageTimeFixed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageTimeFixed> builder)
        {
            builder.ToTable(nameof(UsageTimeFixed));

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.InvoiceLine)
                .WithMany(x=>x.Usages)
                .HasForeignKey(x => x.InvoiceLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
