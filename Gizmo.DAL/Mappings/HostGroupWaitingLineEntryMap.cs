using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Host group waiting line entry map.
    /// </summary>
    public class HostGroupWaitingLineEntryMap : IEntityTypeConfiguration<HostGroupWaitingLineEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<HostGroupWaitingLineEntry> builder)
        {
            builder.ToTable(nameof(HostGroupWaitingLineEntry));

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .HasColumnName("HostGroupWaitingLineEntryId")
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(entity => entity.HostGroupId)
                .HasColumnOrder(1);

            builder.Property(entity => entity.UserId)
                .HasColumnOrder(2);

            builder.Property(entity => entity.Position)
                .HasColumnOrder(3);

            builder.Property(entity => entity.IsManualPosition)
                .HasColumnOrder(4);

            builder.Property(entity => entity.TimeInLine)
              .HasColumnOrder(5);

            builder.Property(entity => entity.ReadyTime)
                .HasColumnOrder(6);

            builder.Property(entity => entity.IsReadyTimedOut)
                .HasColumnOrder(7);

            builder.Property(entity => entity.State)
                .HasColumnOrder(8);

            builder.HasOne(e => e.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entity => entity.WaitingLine)
                .WithMany(waitingLine => waitingLine.Entries)
                .HasForeignKey(entity => entity.HostGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(entity => entity.HostGroup)
                .WithMany(hostGroup => hostGroup.WaitingLineEntries)
                .HasForeignKey(entity => entity.HostGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
