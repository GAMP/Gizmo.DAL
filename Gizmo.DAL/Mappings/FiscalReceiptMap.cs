using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Fiscal receipt entity map.
    /// </summary>
    public sealed class FiscalReceiptMap : IEntityTypeConfiguration<FiscalReceipt>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<FiscalReceipt> builder)
        {
            //Table name
            builder.ToTable(nameof(FiscalReceipt));

            //Primary key
            builder.HasIndex(fiscalReceipt => fiscalReceipt.Id);

            builder.Property(fiscalReceipt => fiscalReceipt.Id)
                .HasColumnOrder(0)
                .HasColumnName("FiscalReceiptId");

            builder.Property(fiscalReceipt => fiscalReceipt.Type)
                .HasColumnOrder(1);

            builder.Property(fiscalReceipt => fiscalReceipt.TaxSystem)
                .HasColumnOrder(2);

            builder.Property(fiscalReceipt => fiscalReceipt.DocumentNumber)
                .HasColumnOrder(3);

            builder.Property(fiscalReceipt => fiscalReceipt.Signature)
                .HasColumnOrder(4)
                .IsRequired(false);

            builder.Property(fiscalReceipt => fiscalReceipt.CompanionId)
                .HasColumnOrder(5)
                .IsRequired(false);

            builder.Property(fiscalReceipt => fiscalReceipt.PrinterNumber)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.HasOne(fiscalReceipt => fiscalReceipt.Companion)
                .WithMany(companion => companion.FiscalReceipts)
                .HasForeignKey(fiscalReceipt => fiscalReceipt.CompanionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
