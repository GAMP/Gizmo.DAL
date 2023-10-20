using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class UsageSessionMap : EntityTypeConfiguration<UsageSession>
    {
        public UsageSessionMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageSessionId");

            this.Property(x => x.UserId)
                .HasColumnOrder(1);

            this.Property(x => x.CurrentUsageId)
                .HasColumnOrder(2);

            this.Property(x => x.CurrentSecond)
                .HasColumnOrder(3);

            this.Property(x => x.IsActive)
                .HasColumnOrder(4);

            this.ToTable(nameof(UsageSession));

            this.HasRequired(x => x.User)
                .WithMany(x => x.UsageSessions)
                .HasForeignKey(x => x.UserId);

            this.HasOptional(x => x.CurrentUsage)
                .WithMany()
                .HasForeignKey(x => x.CurrentUsageId)
                .WillCascadeOnDelete(false);
        }
    }
}
