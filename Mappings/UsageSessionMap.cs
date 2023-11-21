using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UsageSessionMap : EntityTypeConfiguration<UsageSession>
    {
        public UsageSessionMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageSessionId");

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.CurrentUsageId)
                .HasColumnOrder(2);

            Property(x => x.CurrentSecond)
                .HasColumnOrder(3);

            Property(x => x.IsActive)
                .HasColumnOrder(4);

            ToTable(nameof(UsageSession));

            HasRequired(x => x.User)
                .WithMany(x => x.UsageSessions)
                .HasForeignKey(x => x.UserId);

            HasOptional(x => x.CurrentUsage)
                .WithMany()
                .HasForeignKey(x => x.CurrentUsageId)
                .WillCascadeOnDelete(false);
        }
    }
}
