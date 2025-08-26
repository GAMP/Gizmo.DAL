using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Void deposit payment map.
    /// </summary>
    public class VoidDepositPaymentMap : IEntityTypeConfiguration<VoidDepositPayment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<VoidDepositPayment> builder)
        {
            builder.ToTable(nameof(VoidDepositPayment));

            builder.HasIndex(voidDepositPayment => voidDepositPayment.Id);

            builder.Property(voidDepositPayment => voidDepositPayment.DepositPaymentId)
                .HasColumnOrder(1);

            builder.HasIndex(voidDepositPayment => voidDepositPayment.DepositPaymentId)
                .IsUnique()
                .HasFilter(null);

            builder.HasOne(voidDepositPayment => voidDepositPayment.DepositPayment)
                .WithMany(voidDepositPayment => voidDepositPayment.Voids)
                .HasForeignKey(voidDepositPayment => voidDepositPayment.DepositPaymentId);
        }
    }
}
