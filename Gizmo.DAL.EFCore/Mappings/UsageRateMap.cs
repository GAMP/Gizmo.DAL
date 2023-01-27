using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UsageRateMap : IEntityTypeConfiguration<UsageRate>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageRate> builder)
        {
            builder.ToTable(nameof(UsageRate));

            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.BillRateId)
                .HasColumnOrder(1);

            builder.Property(x => x.Total)
                .HasColumnOrder(2);

            builder.Property(x => x.Rate)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.BillRate)
                .WithMany(x=>x.Usage)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
