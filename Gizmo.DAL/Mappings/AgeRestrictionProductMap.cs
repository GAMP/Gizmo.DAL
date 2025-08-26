using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Age restriction product entity map.
    /// </summary>
    public sealed class AgeRestrictionProductMap : IEntityTypeConfiguration<AgeRestrictionProduct>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AgeRestrictionProduct> builder)
        {
            builder.ToTable(nameof(AgeRestrictionProduct))
                .HasBaseType<AgeRestriction>();

            builder.Property(ageRestrictionProduct => ageRestrictionProduct.ProductId)
                .IsRequired()
                .HasColumnOrder(0);

            builder.HasOne(ageRestrictionProduct => ageRestrictionProduct.Product)
                .WithMany(product => product.AgeRestrictions)
                .HasForeignKey(ageRestrictionProduct => ageRestrictionProduct.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
