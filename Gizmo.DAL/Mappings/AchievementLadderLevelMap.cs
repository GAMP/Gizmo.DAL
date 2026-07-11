using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder level entity map.
    /// </summary>
    public sealed class AchievementLadderLevelMap : IEntityTypeConfiguration<AchievementLadderLevel>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadderLevel> builder)
        {
            builder.ToTable(nameof(AchievementLadderLevel));

            builder.HasKey(level => level.Id);

            builder.Property(level => level.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementLadderLevelId");

            builder.Property(level => level.LadderId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(level => level.Rank)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(level => level.Threshold)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(level => level.UserGroupId)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(level => level.Description)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired(false);

            builder.Property(level => level.ImageId)
                .HasColumnOrder(6)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(level => new { level.LadderId, level.Rank })
                .IsUnique();

            builder.HasIndex(level => new { level.LadderId, level.UserGroupId })
                .IsUnique();

            builder.HasOne(level => level.Ladder)
                .WithMany(ladder => ladder.Levels)
                .HasForeignKey(level => level.LadderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(level => level.UserGroup)
                .WithMany()
                .HasForeignKey(level => level.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(level => level.Image)
                .WithMany()
                .HasForeignKey(level => level.ImageId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
