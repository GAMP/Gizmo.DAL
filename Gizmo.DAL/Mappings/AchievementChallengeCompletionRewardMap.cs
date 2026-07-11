using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge completion reward base entity map.
    /// </summary>
    public sealed class AchievementChallengeCompletionRewardMap : IEntityTypeConfiguration<AchievementChallengeCompletionReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeCompletionReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeCompletionReward))
                .UseTptMappingStrategy();

            builder.HasKey(reward => reward.Id);

            builder.Property(reward => reward.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementChallengeCompletionRewardId");

            builder.Property(reward => reward.CompletionId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(reward => reward.Status)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(reward => reward.ProcessedTime)
                .HasColumnOrder(3)
                .IsRequired(false);

            builder.Property(reward => reward.ProcessedById)
                .HasColumnOrder(4)
                .IsRequired(false);

            builder.HasOne(reward => reward.Completion)
                .WithMany(completion => completion.Rewards)
                .HasForeignKey(reward => reward.CompletionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(reward => reward.ProcessedBy)
                .WithMany()
                .HasForeignKey(reward => reward.ProcessedById)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
