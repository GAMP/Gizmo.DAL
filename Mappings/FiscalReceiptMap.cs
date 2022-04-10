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

            Property(x => x.DocumentNumber)
                .HasColumnOrder(3);

            Property(x => x.Signature)
                .HasColumnOrder(4)
                .IsOptional();            

            //Table name
            ToTable(nameof(Entities.FiscalReceipt));
        }
    }
}
