using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement host filter entity map.
    /// </summary>
    public sealed class AchievementHostFilterMap : IEntityTypeConfiguration<AchievementHostFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementHostFilter> builder)
        {
            builder.ToTable(nameof(AchievementHostFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.HostId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.Host)
                .WithMany()
                .HasForeignKey(filter => filter.HostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
