using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge points reward entity map.
    /// </summary>
    public sealed class AchievementChallengePointsRewardMap : IEntityTypeConfiguration<AchievementChallengePointsReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengePointsReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengePointsReward))
                .HasBaseType<AchievementChallengeReward>();

            builder.Property(reward => reward.Amount)
                .HasColumnOrder(1)
                .IsRequired();
        }
    }
}
