using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserSessionChangeMap : EntityTypeConfiguration<UserSessionChange>
    {
        public UserSessionChangeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.UserSessionId)
                .HasColumnOrder(1);

            Property(x => x.UserId)
                .HasColumnOrder(2);

            Property(x => x.HostId)
                .HasColumnOrder(3);

            Property(x => x.State)
                .HasColumnOrder(4);

            Property(x => x.Slot)
                .HasColumnOrder(5);

            Property(x => x.Span)
                .HasColumnOrder(6);

            // Table & Column Mappings
            ToTable(nameof(UserSessionChange));

            Property(t => t.Id)
                .HasColumnName("UserSessionChangeId");

            // Relationships
            HasRequired(t => t.UserSession)
                .WithMany(t => t.UserSessionChanges)
                .HasForeignKey(d => d.UserSessionId)
                .WillCascadeOnDelete(false);

            HasOptional(t => t.CreatedBy)
                .WithMany(t => t.CreatedUserSessionChanges)
                .HasForeignKey(t => t.CreatedById);

            HasRequired(t => t.Host)
                .WithMany(t => t.UserSessionsChanges)
                .HasForeignKey(d => d.HostId);

            HasRequired(t => t.User)
                .WithMany(t => t.UserSessionsChanges)
                .HasForeignKey(d => d.UserId);
        }
    }
}
