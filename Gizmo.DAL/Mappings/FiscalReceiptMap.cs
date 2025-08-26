using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class FiscalReceiptMap : IEntityTypeConfiguration<FiscalReceipt>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<FiscalReceipt> builder)
        {
            //Primary key
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("FiscalReceiptId");

            builder.Property(x => x.Type)
                .HasColumnOrder(1);

            builder.Property(x => x.TaxSystem)
                .HasColumnOrder(2);

            builder.Property(x => x.DocumentNumber)
                .HasColumnOrder(3);

            builder.Property(x => x.Signature)
                .HasColumnOrder(4)
                .IsRequired(false);            

            //Table name
            builder.ToTable(nameof(FiscalReceipt));
        }
    }
}
