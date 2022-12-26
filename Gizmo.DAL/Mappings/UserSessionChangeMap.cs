using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class UserSessionChangeMap : EntityTypeConfiguration<UserSessionChange>
    {
        public UserSessionChangeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.UserSessionId)
                .HasColumnOrder(1);

            this.Property(x => x.UserId)
                .HasColumnOrder(2);

            this.Property(x => x.HostId)
                .HasColumnOrder(3);

            this.Property(x => x.State)
                .HasColumnOrder(4);

            this.Property(x => x.Slot)
                .HasColumnOrder(5);

            this.Property(x => x.Span)
                .HasColumnOrder(6);

            // Table & Column Mappings
            this.ToTable(nameof(UserSessionChange));

            this.Property(t => t.Id)
                .HasColumnName("UserSessionChangeId");

            // Relationships
            this.HasRequired(t => t.UserSession)
                .WithMany(t => t.UserSessionChanges)
                .HasForeignKey(d => d.UserSessionId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.CreatedBy)
                .WithMany(t=>t.CreatedUserSessionChanges)
                .HasForeignKey(t => t.CreatedById);

            this.HasRequired(t => t.Host)
                .WithMany(t=>t.UserSessionsChanges)
                .HasForeignKey(d => d.HostId);

            this.HasRequired(t => t.User)
                .WithMany(t => t.UserSessionsChanges)
                .HasForeignKey(d => d.UserId);
        }
    }
}
