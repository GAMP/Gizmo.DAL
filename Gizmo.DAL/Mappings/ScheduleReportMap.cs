using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Schedule report entity configuration.
    /// </summary>
    public sealed class ScheduleReportMap : IEntityTypeConfiguration<ScheduleReport>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<ScheduleReport> builder)
        {
            builder.ToTable(nameof(ScheduleReport))
                .HasBaseType<Schedule>();

            builder.HasMany(scheduleReport => scheduleReport.Entries)
                .WithOne(scheduleReportEntry => scheduleReportEntry.ScheduleReport)
                .HasForeignKey(scheduleReportEntry => scheduleReportEntry.ScheduleReportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(schedulerReport=> schedulerReport.Recipients)
                .WithOne(recipientReport => recipientReport.ScheduleReport)
                .HasForeignKey(recipientReport => recipientReport.ScheduleReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
