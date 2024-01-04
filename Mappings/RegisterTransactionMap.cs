using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class RegisterTransactionMap : IEntityTypeConfiguration<RegisterTransaction>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<RegisterTransaction> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("RegisterTransactionId")
                .HasColumnOrder(0);

            builder.Property(x => x.RegisterId)
                .HasColumnOrder(1);

            builder.Property(x => x.ShiftId)
                .HasColumnOrder(2) ;

            builder.Property(x => x.Amount)
                .HasColumnOrder(3);

            builder.Property(x => x.Type)
                .HasColumnOrder(4);

            builder.Property(x => x.Note)
                .HasColumnOrder(5);

            // Table & Column Mappings
            builder.ToTable(nameof(RegisterTransaction));

            // Relationships
            builder.HasOne(x => x.Register)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.RegisterId);

            builder.HasOne(x => x.Shift)
                .WithMany(x=>x.RegisterTransactions)
                .HasForeignKey(x => x.ShiftId);

            builder.HasOne(x => x.CreatedBy)
                .WithMany(x => x.RegisterTransactions)
                .HasForeignKey(x => x.CreatedById);
        }
    }
}
