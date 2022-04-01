using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class FiscalReceiptMap : EntityTypeConfiguration<Entities.FiscalReceipt>
    {
        public FiscalReceiptMap()
        {
            //Primary key
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("FiscalReceiptId");

            Property(x => x.Type)
                .HasColumnOrder(1);

            Property(x => x.TaxSystem)
                .HasColumnOrder(2);

            Property(x => x.DocumentId)
                .HasColumnOrder(3)
                .IsOptional();

            Property(x => x.Signature)
                .HasColumnOrder(4)
                .IsRequired();            

            //Table name
            ToTable(nameof(Entities.FiscalReceipt));
        }
    }
}
