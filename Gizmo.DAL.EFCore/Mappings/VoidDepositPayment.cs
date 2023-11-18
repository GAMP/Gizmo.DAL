using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class VoidDepositPaymentMap : IEntityTypeConfiguration<VoidDepositPayment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<VoidDepositPayment> builder)
        {
            //// Primary Key
            //builder.HasKey(t => t.Id);

            builder.Property(t => t.DepositPaymentId)
                .HasColumnOrder(1);

            // Indexes
            builder.HasIndex(t => t.DepositPaymentId).IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(t => t.DepositPayment)
                .WithMany(t=>t.Voids)
                .HasForeignKey(t => t.DepositPaymentId);

            // Table & Column Mappings
            builder.ToTable(nameof(VoidDepositPayment));
        }
    }
}
