using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge completion entity map.
    /// </summary>
    public sealed class AchievementChallengeCompletionMap : IEntityTypeConfiguration<AchievementChallengeCompletion>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeCompletion> builder)
        {
            builder.ToTable(nameof(AchievementChallengeCompletion));

            builder.HasKey(completion => completion.Id);

            builder.Property(completion => completion.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementChallengeCompletionId");

            builder.Property(completion => completion.UserId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(completion => completion.ChallengeId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(completion => completion.Occurrence)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(completion => completion.CompletedTime)
                .HasColumnOrder(4)
                .IsRequired();

            // Indexes — the unique key is the evaluator's idempotency guard
            builder.HasIndex(completion => new { completion.UserId, completion.ChallengeId, completion.Occurrence })
                .IsUnique();

            builder.HasOne(completion => completion.User)
                .WithMany()
                .HasForeignKey(completion => completion.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // history is kept forever — challenges referenced by completions cannot cascade away
            builder.HasOne(completion => completion.Challenge)
                .WithMany()
                .HasForeignKey(completion => completion.ChallengeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
