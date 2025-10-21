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
        /// <inheritdoc></inheritdoc>/>
        public void Configure(EntityTypeBuilder<PaymentIntent> builder)
        {
            builder.ToTable(nameof(PaymentIntent));

            builder.HasKey(paymentIntent => paymentIntent.Id);

            builder.Property(paymentIntent => paymentIntent.Id)
                .HasColumnOrder(0)
                .HasColumnName("PaymentIntentId");

            builder.Property(paymentIntent => paymentIntent.UserId)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(paymentIntent => paymentIntent.PaymentMethodId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(paymentIntent => paymentIntent.Amount)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(paymentIntent => paymentIntent.State)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(paymentIntent => paymentIntent.TransactionId)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(paymentIntent => paymentIntent.TransactionTime)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(paymentIntent => paymentIntent.Provider)
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(paymentIntent => paymentIntent.Guid)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(paymentIntent => paymentIntent.PaymentUrl)
                .HasColumnOrder(9)
                .IsRequired(false)
                .HasMaxLength(2048);

            builder.Property(paymentIntent => paymentIntent.Expiration)
                .HasColumnOrder(10)
                .IsRequired(false);

            builder.Property(paymentIntent => paymentIntent.ExpireAt)
                .HasColumnOrder(11)
                .IsRequired(false);

            builder.Property(paymentIntent => paymentIntent.PaymentId)
                .HasColumnOrder(12)
                .IsRequired(false);

            builder.Property(paymentIntent => paymentIntent.DisableReceiptPrinting)
                .HasColumnOrder(13)
                .IsRequired(true);

            builder.HasOne(paymentIntent => paymentIntent.User)
                .WithMany(userMember => userMember.PaymentIntents)
                .HasForeignKey(paymentIntent => paymentIntent.UserId)
                .OnDelete(DeleteBehavior.NoAction);          

            builder.HasOne(paymentIntent => paymentIntent.PaymentMethod)
                .WithMany(paymentMethod => paymentMethod.PaymentIntents)
                .HasForeignKey(paymentIntent => paymentIntent.PaymentMethodId);

            builder.HasOne(paymentIntent => paymentIntent.Payment)
                .WithOne()
                .HasForeignKey<PaymentIntent>(paymentIntent => paymentIntent.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(paymentIntent => paymentIntent.Guid).IsUnique();  
            builder.HasIndex(paymentIntent => paymentIntent.PaymentId).IsUnique();
        }
    }
}
