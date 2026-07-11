using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement ladder entity map.
    /// </summary>
    public sealed class AchievementLadderMap : IEntityTypeConfiguration<AchievementLadder>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementLadder> builder)
        {
            builder.ToTable(nameof(AchievementLadder));

            builder.HasKey(ladder => ladder.Id);

            builder.Property(ladder => ladder.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementLadderId");

            builder.Property(ladder => ladder.Period)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(ladder => ladder.Mode)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(ladder => ladder.Options)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(ladder => ladder.IsEnabled)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(ladder => ladder.IsDeleted)
                .HasColumnOrder(5)
                .IsRequired();

            // single-enabled-ladder invariant: the filtered unique index on IsEnabled
            // (WHERE IsEnabled = 1) is engine-specific and added at context registration
        }
    }
}
