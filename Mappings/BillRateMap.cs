using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class BillRateMap : EntityTypeConfiguration<BillRate>
    {
        public BillRateMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.BillProfileId)
                .IsRequired()
                .HasColumnOrder(1);

            this.Property(x => x.StartFee)
                .HasColumnOrder(2)
                .IsRequired();

            this.Property(x => x.MinimumFee)
                .HasColumnOrder(3)
                .IsRequired();

            this.Property(x => x.Rate)
                .HasColumnOrder(4)
                .IsRequired();

            this.Property(x => x.ChargeEvery)
                .HasColumnOrder(5)
                .IsRequired();

            this.Property(x => x.ChargeAfter)
                .HasColumnOrder(6)
                .IsRequired();

            this.Property(x => x.Options)
                .HasColumnOrder(7)
                .IsRequired();

            // Relations
            this.ToTable("BillRate");

            this.Property(x => x.Id)
                .HasColumnName("BillRateId");
         
            this.HasRequired(x => x.BillProfile)
                .WithMany(x => x.BillRates)
                .HasForeignKey(x => x.BillProfileId);
        }
    }
}
