using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class BillRateMap : EntityTypeConfiguration<BillRate>
    {
        public BillRateMap()
        {
            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.BillProfileId)
                .IsRequired()
                .HasColumnOrder(1);

            Property(x => x.StartFee)
                .HasColumnOrder(2)
                .IsRequired();

            Property(x => x.MinimumFee)
                .HasColumnOrder(3)
                .IsRequired();

            Property(x => x.Rate)
                .HasColumnOrder(4)
                .IsRequired();

            Property(x => x.ChargeEvery)
                .HasColumnOrder(5)
                .IsRequired();

            Property(x => x.ChargeAfter)
                .HasColumnOrder(6)
                .IsRequired();

            Property(x => x.Options)
                .HasColumnOrder(7)
                .IsRequired();

            // Relations
            ToTable("BillRate");

            Property(x => x.Id)
                .HasColumnName("BillRateId");
         
            HasRequired(x => x.BillProfile)
                .WithMany(x => x.BillRates)
                .HasForeignKey(x => x.BillProfileId);
        }
    }
}
