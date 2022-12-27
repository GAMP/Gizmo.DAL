using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class UsageSessionMap : IEntityTypeConfiguration<UsageSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageSession> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageSessionId");

            builder.Property(x => x.UserId)
                .HasColumnOrder(1);

            builder.Property(x => x.CurrentUsageId)
                .HasColumnOrder(2);

            builder.Property(x => x.CurrentSecond)
                .HasColumnOrder(3);

            builder.Property(x => x.IsActive)
                .HasColumnOrder(4);

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.ToTable(nameof(UsageSession));

            builder.HasOne(x => x.User)
                .WithMany(x => x.UsageSessions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CurrentUsage)
                .WithMany()
                .HasForeignKey(x => x.CurrentUsageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
