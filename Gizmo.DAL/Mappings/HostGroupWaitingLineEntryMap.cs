using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class HostGroupWaitingLineEntryMap : EntityTypeConfiguration<HostGroupWaitingLineEntry>
    {
        public HostGroupWaitingLineEntryMap()
        {
            HasKey(entity => entity.Id);

            Property(entity => entity.Id)
                .HasColumnOrder(0);

            Property(entity => entity.HostGroupId)
                .HasColumnOrder(1);

            Property(entity => entity.UserId)
                .HasColumnOrder(2);

            Property(entity => entity.Position)
                .HasColumnOrder(3);

            Property(entity => entity.IsManualPosition)
                .HasColumnOrder(4);

            Property(entity => entity.TimeInLine)
              .HasColumnOrder(5);

            Property(entity => entity.ReadyTime)
                .HasColumnOrder(6);

            Property(entity => entity.IsReadyTimedOut)
                .HasColumnOrder(7);

            Property(entity => entity.State)
                .HasColumnOrder(8);

            ToTable(nameof(HostGroupWaitingLineEntry));

            HasRequired(entity => entity.WatingLine)
                .WithMany(waitingLine => waitingLine.Entries)
                .HasForeignKey(entity => entity.HostGroupId)
                .WillCascadeOnDelete(true);

            HasRequired(entity => entity.HostGroup)
                .WithMany(hostGroup => hostGroup.WaitingLineEntries)
                .HasForeignKey(entity => entity.HostGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}
