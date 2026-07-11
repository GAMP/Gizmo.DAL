using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge time reward entity map.
    /// </summary>
    public sealed class AchievementChallengeTimeRewardMap : IEntityTypeConfiguration<AchievementChallengeTimeReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeTimeReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeTimeReward))
                .HasBaseType<AchievementChallengeReward>();

            builder.Property(reward => reward.Seconds)
                .HasColumnOrder(1)
                .IsRequired();
        }
    }
}
