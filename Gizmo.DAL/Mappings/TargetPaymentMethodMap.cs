using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target payment method map.
    /// </summary>
    public sealed class TargetPaymentMethodMap : IEntityTypeConfiguration<TargetPaymentMethod>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TargetPaymentMethod> builder)
        {
            builder.ToTable(nameof(TargetPaymentMethod))
                .HasBaseType<Target>();

            builder.Property(targetPaymentMethod => targetPaymentMethod.Id)
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(targetPaymentMethod => targetPaymentMethod.TargetGroupPaymentMethodId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(targetPaymentMethod => targetPaymentMethod.MethodId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasIndex(target => new { target.TargetGroupPaymentMethodId, target.MethodId })
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(target => target.TargetGroupPaymentMethod)
                .WithMany(targetGroup => targetGroup.PaymentMethods)
                .HasForeignKey(target => target.TargetGroupPaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(target => target.PaymentMethod)
                .WithMany()
                .HasForeignKey(target => target.MethodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
