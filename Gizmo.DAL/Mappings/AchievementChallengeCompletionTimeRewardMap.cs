using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge completion time reward entity map.
    /// </summary>
    public sealed class AchievementChallengeCompletionTimeRewardMap : IEntityTypeConfiguration<AchievementChallengeCompletionTimeReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeCompletionTimeReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeCompletionTimeReward))
                .HasBaseType<AchievementChallengeCompletionReward>();

            builder.Property(reward => reward.Seconds)
                .HasColumnOrder(1)
                .IsRequired();
        }
    }
}
