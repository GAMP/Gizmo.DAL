using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder requirement entity map.
    /// </summary>
    public sealed class AchievementLadderRequirementMap : IEntityTypeConfiguration<AchievementLadderRequirement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadderRequirement> builder)
        {
            builder.ToTable(nameof(AchievementLadderRequirement));

            builder.HasKey(requirement => requirement.Id);

            builder.Property(requirement => requirement.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementLadderRequirementId");

            builder.Property(requirement => requirement.LevelId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(requirement => requirement.AchievementId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(requirement => requirement.RequiredCount)
                .HasColumnOrder(3)
                .IsRequired();

            // Indexes
            builder.HasIndex(requirement => new { requirement.LevelId, requirement.AchievementId })
                .IsUnique();

            builder.HasOne(requirement => requirement.Level)
                .WithMany(level => level.Requirements)
                .HasForeignKey(requirement => requirement.LevelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(requirement => requirement.Achievement)
                .WithMany()
                .HasForeignKey(requirement => requirement.AchievementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
