using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Recipient schedule report map.
    /// </summary>
    public sealed class RecipientScheduleReportMap : IEntityTypeConfiguration<RecipientScheduleReport>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<RecipientScheduleReport> builder)
        {
            builder.ToTable(nameof(RecipientScheduleReport));

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
