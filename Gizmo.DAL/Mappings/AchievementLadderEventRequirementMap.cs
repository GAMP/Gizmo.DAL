using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder event requirement entity map.
    /// </summary>
    public sealed class AchievementLadderEventRequirementMap : IEntityTypeConfiguration<AchievementLadderEventRequirement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadderEventRequirement> builder)
        {
            builder.ToTable(nameof(AchievementLadderEventRequirement))
                .HasBaseType<AchievementRequirementSnapshot>();

            builder.Property(requirement => requirement.EventId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(requirement => requirement.PointsAwarded)
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasOne(requirement => requirement.Event)
                .WithMany(ladderEvent => ladderEvent.Requirements)
                .HasForeignKey(requirement => requirement.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
