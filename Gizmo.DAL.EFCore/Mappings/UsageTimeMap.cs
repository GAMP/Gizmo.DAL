using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UsageTimeMap : IEntityTypeConfiguration<UsageTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageTime> builder)
        {
            builder.ToTable(nameof(UsageTime));

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.InvoiceLine)
                .WithMany(x=>x.Usages)
                .HasForeignKey(x => x.InvoiceLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
