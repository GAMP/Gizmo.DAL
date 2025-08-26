using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group payment method map.
    /// </summary>
    public sealed class TargetGroupPaymentMethodMap : IEntityTypeConfiguration<TargetGroupPaymentMethod>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetGroupPaymentMethod> builder)
        {
            builder.ToTable(nameof(TargetGroupPaymentMethod))
                .HasBaseType<TargetGroup>();
        }
    }
}
