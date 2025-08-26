using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group product time map.
    /// </summary>
    public sealed class TargetGroupProductTimeMap : IEntityTypeConfiguration<TargetGroupProductTime>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupProductTime> builder)
        {
            builder.ToTable(nameof(TargetGroupProductTime))
                .HasBaseType<TargetGroup>();
        }
    }
}
