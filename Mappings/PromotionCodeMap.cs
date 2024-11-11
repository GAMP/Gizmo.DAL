using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Promotion entity map.
    /// </summary>
    public sealed class PromotionCodeMap : IEntityTypeConfiguration<PromotionCode>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PromotionCode> builder)
        {
            builder.ToTable(nameof(PromotionCode));

            builder.HasKey(promotionCode => promotionCode.Id);
            builder.Property(promotionCode => promotionCode.Id)
                .HasColumnName("PromotionCodeId");

            builder.Property(promotionCode => promotionCode.Value)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY); // use 255 chars for now, should be enough to store barcodes etc

            builder.HasIndex(promotionCode => promotionCode.Value).IsUnique();

            builder.HasOne(promotionCode => promotionCode.Promotion)
                .WithMany(promotion => promotion.Codes)
                .HasForeignKey(promotionCode => promotionCode.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
