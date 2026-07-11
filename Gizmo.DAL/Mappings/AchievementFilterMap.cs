using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement filter base entity map.
    /// </summary>
    public sealed class AchievementFilterMap : IEntityTypeConfiguration<AchievementFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementFilter> builder)
        {
            builder.ToTable(nameof(AchievementFilter))
                .UseTptMappingStrategy();

            builder.HasKey(filter => filter.Id);

            builder.Property(filter => filter.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementFilterId");

            builder.Property(filter => filter.AchievementId)
                .HasColumnOrder(1)
                .IsRequired();

            // same-value duplicates within one achievement are validated at save time —
            // the achievement id and the filtered value live in different TPT tables,
            // so no composite database unique constraint can express it
            builder.HasOne(filter => filter.Achievement)
                .WithMany(achievement => achievement.Filters)
                .HasForeignKey(filter => filter.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
