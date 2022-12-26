using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
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

    public class BillRatePeriodTimeMap : IEntityTypeConfiguration<BillRatePeriodDayTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BillRatePeriodDayTime> builder)
        {
            // Key
            builder.HasKey(x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });

            // Table & Column Mappings
            builder.ToTable("BillRatePeriodDayTime");

            // Indexes
            builder.HasIndex(t => t.PeriodDayId);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Times)
                .HasForeignKey(x => x.PeriodDayId);
        }
    }
}
