using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Schedule report entry map.
    /// </summary>
    public sealed class ScheduleReportEntryMap : IEntityTypeConfiguration<ScheduleReportEntry>
    {
        ///<inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ScheduleReportEntry> builder)
        {
            builder.ToTable(nameof(ScheduleReportEntry));
            builder.HasKey(scheduleReportEntry => scheduleReportEntry.Id);

            builder.Property(scheduleReportEntry => scheduleReportEntry.Id)
                .HasColumnName("ScheduleReportEntryId")
                .HasColumnOrder(0);

            builder.Property(scheduleReportEntry => scheduleReportEntry.ScheduleReportId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(scheduleReportEntry => scheduleReportEntry.ReportType)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property (scheduleReportEntry => scheduleReportEntry.ReportRange)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(scheduleReportEntry => scheduleReportEntry.ReportParameters)
                .IsRequired(false)
                .HasColumnOrder(4);

            builder.Property(scheduleReportEntry => scheduleReportEntry.ReportPresetId)
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.HasOne(scheduleReportEntry => scheduleReportEntry.ScheduleReport)
                .WithMany(scheduleReport => scheduleReport.Entries)
                .HasForeignKey(scheduleReportEntry => scheduleReportEntry.ScheduleReportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(scheduleReportEntry => scheduleReportEntry.ReportPreset)
                .WithMany()
                .HasForeignKey(scheduleReportEntry => scheduleReportEntry.ReportPresetId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
