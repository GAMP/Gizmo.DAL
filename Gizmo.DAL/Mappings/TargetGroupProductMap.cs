using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group product map.
    /// </summary>
    public sealed class TargetGroupProductMap : IEntityTypeConfiguration<TargetGroupProduct>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupProduct> builder)
        {
            builder.ToTable(nameof(TargetGroupProduct))
                .HasBaseType<TargetGroup>();
        }
    }
}
