using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Payment intent map.
    /// </summary>
    public class PaymentIntentMap : IEntityTypeConfiguration<PaymentIntent>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PaymentIntent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("PaymentIntentId");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.PaymentMethodId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(x => x.State)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(x => x.TransactionId)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.TransactionTime)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(x => x.Provider)
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(x => x.Guid)
                .HasColumnOrder(8)
                .IsRequired();;

            builder.HasOne(x => x.User)
                .WithMany(x => x.PaymentIntents)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.PaymentIntents)
                .HasForeignKey(x => x.PaymentMethodId);

            // Indexes
            builder.HasIndex(t => t.Guid).IsUnique();

            builder.ToTable(nameof(PaymentIntent));
        }
    }
}
