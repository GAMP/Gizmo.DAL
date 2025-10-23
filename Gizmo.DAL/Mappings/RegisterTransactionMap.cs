using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Register transaction entity map.
    /// </summary>
    public class RegisterTransactionMap : IEntityTypeConfiguration<RegisterTransaction>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RegisterTransaction> builder)
        {
            // Table & Column Mappings
            builder.ToTable(nameof(RegisterTransaction));

            // Primary Key
            builder.HasKey(registerTransaction => registerTransaction.Id);

            // Properties
            builder.Property(registerTransaction => registerTransaction.Id)
                .HasColumnName("RegisterTransactionId")
                .HasColumnOrder(0);

            builder.Property(registerTransaction => registerTransaction.RegisterId)
                .HasColumnOrder(1);

            builder.Property(registerTransaction => registerTransaction.ShiftId)
                .HasColumnOrder(2);

            builder.Property(registerTransaction => registerTransaction.Amount)
                .HasColumnOrder(3);

            builder.Property(registerTransaction => registerTransaction.Type)
                .HasColumnOrder(4);

            builder.Property(registerTransaction => registerTransaction.Note)
                .HasColumnOrder(5);

            builder.Property(registerTransaction => registerTransaction.FiscalReceiptStatus)
                .HasColumnOrder(6);

            builder.Property(registerTransaction => registerTransaction.FiscalReceiptId)
                .IsRequired(false)
                .HasColumnOrder(7);

            builder.HasIndex(registerTransaction => registerTransaction.FiscalReceiptId).IsUnique();

            // Relationships
            builder.HasOne(registerTransaction => registerTransaction.Register)
                .WithMany(register => register.Transactions)
                .HasForeignKey(registerTransaction => registerTransaction.RegisterId);

            builder.HasOne(registerTransaction => registerTransaction.Shift)
                .WithMany(shift => shift.RegisterTransactions)
                .HasForeignKey(registerTransaction => registerTransaction.ShiftId);

            builder.HasOne(registerTransaction => registerTransaction.CreatedBy)
                .WithMany(userOperator => userOperator.RegisterTransactions)
                .HasForeignKey(registerTransaction => registerTransaction.CreatedById);

            builder.HasOne(registerTransaction => registerTransaction.FiscalReceipt)
                .WithMany()
                .HasForeignKey(registerTransaction => registerTransaction.FiscalReceiptId);
        }
    }
}
