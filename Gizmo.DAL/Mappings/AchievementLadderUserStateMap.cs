using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder user state entity map.
    /// </summary>
    public sealed class AchievementLadderUserStateMap : IEntityTypeConfiguration<AchievementLadderUserState>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadderUserState> builder)
        {
            builder.ToTable(nameof(AchievementLadderUserState));

            builder.HasKey(state => state.Id);

            builder.Property(state => state.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementLadderUserStateId");

            builder.Property(state => state.UserId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(state => state.LadderId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(state => state.LastSettledPeriodStart)
                .HasColumnOrder(3)
                .IsRequired();

            // Indexes
            builder.HasIndex(state => new { state.UserId, state.LadderId })
                .IsUnique();

            builder.HasOne(state => state.User)
                .WithMany()
                .HasForeignKey(state => state.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(state => state.Ladder)
                .WithMany()
                .HasForeignKey(state => state.LadderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
