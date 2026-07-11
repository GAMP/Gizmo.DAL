using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge completion product reward entity map.
    /// </summary>
    public sealed class AchievementChallengeCompletionProductRewardMap : IEntityTypeConfiguration<AchievementChallengeCompletionProductReward>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeCompletionProductReward> builder)
        {
            builder.ToTable(nameof(AchievementChallengeCompletionProductReward))
                .HasBaseType<AchievementChallengeCompletionReward>();

            builder.Property(reward => reward.ProductId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(reward => reward.Quantity)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(reward => reward.InvoiceId)
                .HasColumnOrder(3)
                .IsRequired(false);

            // history is kept forever — granted products cannot cascade away
            builder.HasOne(reward => reward.Product)
                .WithMany()
                .HasForeignKey(reward => reward.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(reward => reward.Invoice)
                .WithMany()
                .HasForeignKey(reward => reward.InvoiceId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
