using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge requirement entity map.
    /// </summary>
    public sealed class AchievementChallengeRequirementMap : IEntityTypeConfiguration<AchievementChallengeRequirement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeRequirement> builder)
        {
            builder.ToTable(nameof(AchievementChallengeRequirement));

            builder.HasKey(requirement => requirement.Id);

            builder.Property(requirement => requirement.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementChallengeRequirementId");

            builder.Property(requirement => requirement.ChallengeId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(requirement => requirement.AchievementId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(requirement => requirement.RequiredCount)
                .HasColumnOrder(3)
                .IsRequired();

            // Indexes
            builder.HasIndex(requirement => new { requirement.ChallengeId, requirement.AchievementId })
                .IsUnique();

            builder.HasOne(requirement => requirement.Challenge)
                .WithMany(challenge => challenge.Requirements)
                .HasForeignKey(requirement => requirement.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(requirement => requirement.Achievement)
                .WithMany()
                .HasForeignKey(requirement => requirement.AchievementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
