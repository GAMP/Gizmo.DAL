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

            // one row per user per ladder, referenced by nothing — the composite key IS
            // the identity, no surrogate (link-table convention)
            builder.HasKey(state => new { state.UserId, state.LadderId });

            builder.Property(state => state.UserId)
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(state => state.LadderId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(state => state.LastSettledPeriodStart)
                .HasColumnOrder(2)
                .IsRequired();

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
