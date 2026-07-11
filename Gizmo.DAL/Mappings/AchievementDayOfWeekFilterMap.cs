using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement day of week filter entity map.
    /// </summary>
    public sealed class AchievementDayOfWeekFilterMap : IEntityTypeConfiguration<AchievementDayOfWeekFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementDayOfWeekFilter> builder)
        {
            builder.ToTable(nameof(AchievementDayOfWeekFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.Day)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(filter => filter.DayTimeFrom)
                .HasColumnOrder(2)
                .IsRequired(false);

            builder.Property(filter => filter.DayTimeTo)
                .HasColumnOrder(3)
                .IsRequired(false);
        }
    }
}
