using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge completion requirement entity map.
    /// </summary>
    public sealed class AchievementChallengeCompletionRequirementMap : IEntityTypeConfiguration<AchievementChallengeCompletionRequirement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallengeCompletionRequirement> builder)
        {
            builder.ToTable(nameof(AchievementChallengeCompletionRequirement))
                .HasBaseType<AchievementRequirementSnapshot>();

            builder.Property(requirement => requirement.CompletionId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.HasOne(requirement => requirement.Completion)
                .WithMany(completion => completion.Requirements)
                .HasForeignKey(requirement => requirement.CompletionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
