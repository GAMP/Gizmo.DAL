using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
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
