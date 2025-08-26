using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group bill profile map.
    /// </summary>
    public sealed class TargetGroupBillProfileMap : IEntityTypeConfiguration<TargetGroupBillProfile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupBillProfile> builder)
        {
            builder.ToTable(nameof(TargetGroupBillProfile))
                .HasBaseType<TargetGroup>();
        }
    }
}
