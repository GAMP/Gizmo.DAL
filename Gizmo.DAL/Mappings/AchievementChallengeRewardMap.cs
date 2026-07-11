using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge reward base entity map.
    /// </summary>
    public sealed class AchievementChallengeRewardMap : IEntityTypeConfiguration<AchievementChallengeReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeReward))
                .UseTptMappingStrategy();

            builder.HasKey(reward => reward.Id);

            builder.Property(reward => reward.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementChallengeRewardId");

            builder.Property(reward => reward.ChallengeId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(reward => reward.Options)
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasOne(reward => reward.Challenge)
                .WithMany(challenge => challenge.Rewards)
                .HasForeignKey(reward => reward.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
