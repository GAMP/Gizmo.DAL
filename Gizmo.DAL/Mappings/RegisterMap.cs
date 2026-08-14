using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Register entity map.
    /// </summary>
    public class RegisterMap : IEntityTypeConfiguration<Register>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Register> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("RegisterId")
                .HasColumnOrder(0);

            builder.Property(x => x.Number)
                .HasColumnOrder(1);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.MacAddress)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.StartCash)
                .HasColumnOrder(4);

            builder.Property(x => x.IdleTimeout)
                .HasColumnOrder(5);

            builder.Property(x => x.Options)
                .HasColumnOrder(6);

            builder.Property(t => t.IsDeleted)
                .HasColumnOrder(7);

            builder.Property(t => t.PaymentTerminalNumber)
                .IsRequired(false)
                .HasColumnOrder(8);

            builder.Property(t => t.FiscalReceiptPrinterNumber)
                .IsRequired(false)
                .HasColumnOrder(9);

            // Indexes

            builder.HasIndex(t => new { t.Name, t.BranchId })
                .IsUnique();

            builder.HasIndex(t => t.MacAddress)
                .IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(Register));

            builder.HasMany(x => x.Shifts)
                .WithOne(x => x.Register)
                .HasForeignKey(x => x.RegisterId);

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Register)
                .HasForeignKey(x => x.RegisterId);

            builder.HasOne(register => register.Stock)
                .WithMany(stock => stock.Registers)
                .HasForeignKey(register => register.StockId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(register => register.Branch)
                .WithMany(branch => branch.Registers)
                .HasForeignKey(register => register.BranchId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<UserOperator>()
                .WithMany()
                .HasForeignKey(register => register.DefaultOperatorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
