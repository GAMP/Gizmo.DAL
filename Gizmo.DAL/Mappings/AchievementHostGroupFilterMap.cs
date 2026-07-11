using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement host group filter entity map.
    /// </summary>
    public sealed class AchievementHostGroupFilterMap : IEntityTypeConfiguration<AchievementHostGroupFilter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementHostGroupFilter> builder)
        {
            builder.ToTable(nameof(AchievementHostGroupFilter))
                .HasBaseType<AchievementFilter>();

            builder.Property(filter => filter.HostGroupId)
                .HasColumnOrder(1)
                .IsRequired();

            // restrict — removing the last ANY-of value would invert the filter meaning
            builder.HasOne(filter => filter.HostGroup)
                .WithMany()
                .HasForeignKey(filter => filter.HostGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
