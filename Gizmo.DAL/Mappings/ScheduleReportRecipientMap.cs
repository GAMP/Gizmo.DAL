using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Schedule report recipient map.
    /// </summary>
    public sealed class ScheduleReportRecipientMap : IEntityTypeConfiguration<ScheduleReportRecipient>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<ScheduleReportRecipient> builder)
        {
            builder.ToTable(nameof(ScheduleReportRecipient))
                  .HasBaseType<Recipient>();

            builder.Property(scheduleReportRecipient => scheduleReportRecipient.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(scheduleReportRecipient => scheduleReportRecipient.ScheduleReportId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(scheduleReportRecipient => scheduleReportRecipient.UserId)      
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(scheduleReportRecipient => new { scheduleReportRecipient.ScheduleReportId, scheduleReportRecipient.UserId })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(scheduleReportRecipient => scheduleReportRecipient.ScheduleReport)
                .WithMany(scheduleReport => scheduleReport.Recipients)
                .HasForeignKey(scheduleReportRecipient => scheduleReportRecipient.ScheduleReportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(scheduleReportRecipient => scheduleReportRecipient.User)
                .WithMany()
                .HasForeignKey(scheduleReportRecipient => scheduleReportRecipient.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
