using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder event entity map.
    /// </summary>
    public sealed class AchievementLadderEventMap : IEntityTypeConfiguration<AchievementLadderEvent>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadderEvent> builder)
        {
            builder.ToTable(nameof(AchievementLadderEvent));

            builder.HasKey(ladderEvent => ladderEvent.Id);

            builder.Property(ladderEvent => ladderEvent.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementLadderEventId");

            builder.Property(ladderEvent => ladderEvent.UserId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.LadderId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.FromUserGroupId)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.ToUserGroupId)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.FromRank)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.ToRank)
                .HasColumnOrder(6)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.Trigger)
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.PeriodStart)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(ladderEvent => ladderEvent.Score)
                .HasColumnOrder(9)
                .IsRequired(false);

            builder.Property(ladderEvent => ladderEvent.CreatedTime)
                .HasColumnOrder(10)
                .IsRequired();

            // Indexes
            builder.HasIndex(ladderEvent => new { ladderEvent.UserId, ladderEvent.CreatedTime });

            builder.HasOne(ladderEvent => ladderEvent.User)
                .WithMany()
                .HasForeignKey(ladderEvent => ladderEvent.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // history is kept forever — config referenced by events cannot cascade away
            builder.HasOne(ladderEvent => ladderEvent.Ladder)
                .WithMany()
                .HasForeignKey(ladderEvent => ladderEvent.LadderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ladderEvent => ladderEvent.FromUserGroup)
                .WithMany()
                .HasForeignKey(ladderEvent => ladderEvent.FromUserGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ladderEvent => ladderEvent.ToUserGroup)
                .WithMany()
                .HasForeignKey(ladderEvent => ladderEvent.ToUserGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
