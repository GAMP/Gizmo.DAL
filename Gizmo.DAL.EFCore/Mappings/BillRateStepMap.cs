using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class BillProfileRateStepMap : IEntityTypeConfiguration<BillRateStep>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BillRateStep> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.BillRateId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.Minute)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.Action)
                .HasColumnOrder(3)
                .IsRequired();     

            builder.Property(x => x.Charge)
                .HasColumnOrder(4);

            builder.Property(x => x.Rate)
                .HasColumnOrder(5);

            builder.Property(x => x.TargetMinute)
                .HasColumnOrder(6);

            // Indexes
            builder.HasIndex(t => new { t.BillRateId, t.Minute }).HasDatabaseName("UQ_BillRateMinute").IsUnique();

            builder.ToTable("BillRateStep");

            builder.Property(x => x.Id)
                .HasColumnName("BillRateStepId");

            // Relations
            builder.HasOne(x => x.BillRate)
                .WithMany(x => x.BillRateSteps)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
