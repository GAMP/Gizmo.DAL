using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group product group map.
    /// </summary>
    public sealed class TargetGroupProductGroupMap : IEntityTypeConfiguration<TargetGroupProductGroup>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupProductGroup> builder)
        {
            builder.ToTable(nameof(TargetGroupProductGroup))
                .HasBaseType<TargetGroup>();
        }
    }
}
