using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement requirement snapshot base entity map.
    /// </summary>
    public sealed class AchievementRequirementSnapshotMap : IEntityTypeConfiguration<AchievementRequirementSnapshot>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementRequirementSnapshot> builder)
        {
            builder.ToTable(nameof(AchievementRequirementSnapshot))
                .UseTptMappingStrategy();

            builder.HasKey(snapshot => snapshot.Id);

            builder.Property(snapshot => snapshot.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementRequirementSnapshotId");

            builder.Property(snapshot => snapshot.AchievementId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(snapshot => snapshot.RequiredCount)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(snapshot => snapshot.CompletedCount)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(snapshot => snapshot.TargetValue)
                .HasColumnOrder(4)
                .HasPrecision(19, 4);

            builder.Property(snapshot => snapshot.ActualValue)
                .HasColumnOrder(5)
                .HasPrecision(19, 4);

            // history is kept forever — achievements referenced by snapshots cannot cascade away
            builder.HasOne(snapshot => snapshot.Achievement)
                .WithMany()
                .HasForeignKey(snapshot => snapshot.AchievementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
