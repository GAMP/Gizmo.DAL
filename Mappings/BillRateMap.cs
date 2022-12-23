using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class BillRateMap : IEntityTypeConfiguration<BillRate>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BillRate> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.BillProfileId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.StartFee)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.MinimumFee)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(x => x.Rate)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(x => x.ChargeEvery)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(x => x.ChargeAfter)
                .HasColumnOrder(6)
                .IsRequired();

            builder.Property(x => x.Options)
                .HasColumnOrder(7)
                .IsRequired();

            // Relations
            builder.ToTable("BillRate");

            builder.Property(x => x.Id)
                .HasColumnName("BillRateId");

            builder.HasOne(x => x.BillProfile)
                .WithMany(x => x.BillRates)
                .HasForeignKey(x => x.BillProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
