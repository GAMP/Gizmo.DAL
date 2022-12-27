using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UserSessionChangeMap : IEntityTypeConfiguration<UserSessionChange>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserSessionChange> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.UserSessionId)
                .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                .HasColumnOrder(2);

            builder.Property(x => x.HostId)
                .HasColumnOrder(3);

            builder.Property(x => x.State)
                .HasColumnOrder(4);

            builder.Property(x => x.Slot)
                .HasColumnOrder(5);

            builder.Property(x => x.Span)
                .HasColumnOrder(6);

            // Table & Column Mappings
            builder.ToTable(nameof(UserSessionChange));

            builder.Property(t => t.Id)
                .HasColumnName("UserSessionChangeId");

            // Relationships
            builder.HasOne(t => t.UserSession)
                .WithMany(t => t.UserSessionChanges)
                .HasForeignKey(d => d.UserSessionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.CreatedBy)
                .WithMany(t=>t.CreatedUserSessionChanges)
                .HasForeignKey(t => t.CreatedById);

            builder.HasOne(t => t.Host)
                .WithMany(t=>t.UserSessionsChanges)
                .HasForeignKey(d => d.HostId);

            builder.HasOne(t => t.User)
                .WithMany(t => t.UserSessionsChanges)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
