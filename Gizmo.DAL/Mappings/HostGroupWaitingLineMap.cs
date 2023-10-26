using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostGroupWaitingLineMap : EntityTypeConfiguration<HostGroupWaitingLine>
    {
        public HostGroupWaitingLineMap()
        {
            HasKey(entity => entity.Id);

            Property(entity => entity.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName("HosGroupId")
                .HasColumnOrder(0);

            Property(entity => entity.TimeOutOptions)
                .HasColumnOrder(1);

            Property(entity => entity.EnablePriorities)
                .HasColumnOrder(2);

            HasRequired(entity => entity.HostGroup)
                .WithRequiredDependent(hostGroup => hostGroup.WaitingLine)
                .WillCascadeOnDelete(true);

            ToTable(nameof(HostGroupWaitingLine));
        }
    }
}
