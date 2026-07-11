using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder entry entity map.
    /// </summary>
    public sealed class AchievementLadderEntryMap : IEntityTypeConfiguration<AchievementLadderEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadderEntry> builder)
        {
            builder.ToTable(nameof(AchievementLadderEntry));

            builder.HasKey(entry => entry.Id);

            builder.Property(entry => entry.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementLadderEntryId");

            builder.Property(entry => entry.LadderId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(entry => entry.AchievementId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(entry => entry.Points)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(entry => entry.IsEnabled)
                .HasColumnOrder(4)
                .IsRequired();

            // Indexes
            builder.HasIndex(entry => new { entry.LadderId, entry.AchievementId })
                .IsUnique();

            builder.HasOne(entry => entry.Ladder)
                .WithMany(ladder => ladder.Achievements)
                .HasForeignKey(entry => entry.LadderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(entry => entry.Achievement)
                .WithMany()
                .HasForeignKey(entry => entry.AchievementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
