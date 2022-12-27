using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UserSessionMap : IEntityTypeConfiguration<UserSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.HostId)
                .HasColumnOrder(2);

            builder.Property(x => x.State)
                .HasColumnOrder(3);

            builder.Property(x => x.Slot)
                .HasColumnOrder(4);

            builder.Property(x => x.Span)
                .HasColumnOrder(5);

            builder.Property(x => x.BilledSpan)
                .HasColumnOrder(6);

            builder.Property(x => x.PendTime)
                .HasColumnOrder(7);

            builder.Property(x => x.PendSpan)
                .HasColumnOrder(8);

            builder.Property(x => x.EndTime)
                .HasColumnOrder(9);    

            // Table & Column Mappings
            builder.ToTable(nameof(UserSession));

            builder.Property(t => t.Id)
                .HasColumnName("UserSessionId");

            // Relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.CreatedBy)
                .WithMany(t => t.CreatedUserSessions)
                .HasForeignKey(d => d.CreatedById);

            builder.HasOne(t => t.Host)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.HostId);
        }
    }
}
