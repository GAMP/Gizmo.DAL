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
            builder.ToTable(nameof(ScheduleReportRecipient));

            builder.HasKey(recipientScheduleReport =>  recipientScheduleReport.Id);

            builder.Property(recipientScheduleReport => recipientScheduleReport.Id)
                .HasColumnName("RecipientScheduleReportId")
                .HasColumnOrder(0);

            builder.Property(recipientScheduleReport => recipientScheduleReport.ScheduleReportId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(recipientScheduleReport => recipientScheduleReport.RecipientId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(recipientScheduleReport => recipientScheduleReport.IsDisabled)
                .IsRequired()
                .HasColumnOrder(3);
        }
    }
}
