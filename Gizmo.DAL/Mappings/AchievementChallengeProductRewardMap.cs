using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge product reward entity map.
    /// </summary>
    public sealed class AchievementChallengeProductRewardMap : IEntityTypeConfiguration<AchievementChallengeProductReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeProductReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeProductReward))
                .HasBaseType<AchievementChallengeReward>();

            builder.Property(reward => reward.ProductId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(reward => reward.Quantity)
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasOne(reward => reward.Product)
                .WithMany()
                .HasForeignKey(reward => reward.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
