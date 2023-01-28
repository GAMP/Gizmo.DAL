using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class UserSessionMap : EntityTypeConfiguration<UserSession>
    {
        public UserSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.UserId)
                .HasColumnOrder(1);

            this.Property(x => x.HostId)
                .HasColumnOrder(2);

            this.Property(x => x.State)
                .HasColumnOrder(3);

            this.Property(x => x.Slot)
                .HasColumnOrder(4);

            this.Property(x => x.Span)
                .HasColumnOrder(5);

            this.Property(x => x.BilledSpan)
                .HasColumnOrder(6);

            this.Property(x => x.PendTime)
                .HasColumnOrder(7);

            this.Property(x => x.PendSpan)
                .HasColumnOrder(8);

            this.Property(x => x.EndTime)
                .HasColumnOrder(9);    

            // Table & Column Mappings
            this.ToTable(nameof(UserSession));

            this.Property(t => t.Id)
                .HasColumnName("UserSessionId");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.UserId);

            this.HasOptional(t => t.CreatedBy)
                .WithMany(t => t.CreatedUserSessions)
                .HasForeignKey(d => d.CreatedById);

            this.HasRequired(t => t.Host)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.HostId);
        }
    }
}
