using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge completion points reward entity map.
    /// </summary>
    public sealed class AchievementChallengeCompletionPointsRewardMap : IEntityTypeConfiguration<AchievementChallengeCompletionPointsReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeCompletionPointsReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeCompletionPointsReward))
                .HasBaseType<AchievementChallengeCompletionReward>();

            builder.Property(reward => reward.Amount)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(reward => reward.PointTransactionId)
                .HasColumnOrder(2)
                .IsRequired(false);

            // Indexes
            // Explicit short name keeps it under the 63-char DB identifier limit (PostgreSQL) —
            // the conventional IX_<table>_PointTransactionId is 64 chars and trips the guard.
            builder.HasIndex(reward => reward.PointTransactionId)
                .HasDatabaseName("IX_AchievementChallengeCompletionPointsReward_Transaction");

            builder.HasOne(reward => reward.PointTransaction)
                .WithMany()
                .HasForeignKey(reward => reward.PointTransactionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
