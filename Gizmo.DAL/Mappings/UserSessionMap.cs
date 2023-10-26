using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserSessionMap : EntityTypeConfiguration<UserSession>
    {
        public UserSessionMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.UserId)
                .HasColumnOrder(1);

            Property(x => x.HostId)
                .HasColumnOrder(2);

            Property(x => x.State)
                .HasColumnOrder(3);

            Property(x => x.Slot)
                .HasColumnOrder(4);

            Property(x => x.Span)
                .HasColumnOrder(5);

            Property(x => x.BilledSpan)
                .HasColumnOrder(6);

            Property(x => x.PendTime)
                .HasColumnOrder(7);

            Property(x => x.PendSpan)
                .HasColumnOrder(8);

            Property(x => x.EndTime)
                .HasColumnOrder(9);

            // Table & Column Mappings
            ToTable(nameof(UserSession));

            Property(t => t.Id)
                .HasColumnName("UserSessionId");

            // Relationships
            HasRequired(t => t.User)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.UserId);

            HasOptional(t => t.CreatedBy)
                .WithMany(t => t.CreatedUserSessions)
                .HasForeignKey(d => d.CreatedById);

            HasRequired(t => t.Host)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.HostId);
        }
    }
}
