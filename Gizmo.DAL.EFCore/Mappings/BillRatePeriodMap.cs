using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class BillRatePeriodDayMap : IEntityTypeConfiguration<BillRatePeriodDay>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BillRatePeriodDay> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Table & Column Mappings
            builder.ToTable("BillRatePeriodDay");

            builder.Property(x => x.Id)
                .HasColumnName("BillRatePeriodDayId");

            builder.Property(x => x.BillRateId);

            builder.Property(x => x.Day);

            // Indexes
            builder.HasIndex(t => new { t.BillRateId,t.Day }).HasDatabaseName("UQ_BillRatePeriodDay").IsUnique();

            builder.HasOne(x => x.BillRate)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
