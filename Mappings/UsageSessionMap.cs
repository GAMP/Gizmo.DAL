using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Usage session entity map.
    /// </summary>
    public class UsageSessionMap : IEntityTypeConfiguration<UsageSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageSession> builder)
        {
            builder.HasKey(usageSession => usageSession.Id);

            builder.Property(usageSession => usageSession.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageSessionId");

            builder.Property(usageSession => usageSession.UserId)
                .HasColumnOrder(1);

            builder.Property(usageSession => usageSession.CurrentUsageId)
                .HasColumnOrder(2);

            builder.Property(usageSession => usageSession.CurrentSecond)
                .HasColumnOrder(3);

            builder.Property(usageSession => usageSession.NegativeSeconds)
                .HasColumnOrder(4);

            builder.Property(usageSession => usageSession.StartFee)
                .HasColumnOrder(5);

            builder.Property(usageSession => usageSession.MinimumFee)
                .HasColumnOrder(6);

            builder.Property(usageSession => usageSession.RatesTotal)
                .HasColumnOrder(7);

            builder.Property(usageSession => usageSession.DiscountAmount)
                .HasColumnOrder(8);

            builder.Property(usageSession => usageSession.IsActive)
                .HasColumnOrder(9);

            // Indexes
            builder.HasIndex(usageSession => usageSession.Id);

            builder.ToTable(nameof(UsageSession));

            builder.HasOne(usageSession => usageSession.User)
                .WithMany(usageSession => usageSession.UsageSessions)
                .HasForeignKey(usageSession => usageSession.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(usageSession => usageSession.CurrentUsage)
                .WithMany()
                .HasForeignKey(usageSession => usageSession.CurrentUsageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
