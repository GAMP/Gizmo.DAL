using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Payment intent map.
    /// </summary>
    public class PaymentIntentMap : EntityTypeConfiguration<PaymentIntent>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public PaymentIntentMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("PaymentIntentId");

            Property(x => x.UserId)
                .IsRequired()
                .HasColumnOrder(1);

            Property(x => x.PaymentMethodId)
                .HasColumnOrder(2)
                .IsRequired();

            Property(x => x.Amount)
                .HasColumnOrder(3)
                .IsRequired();

            Property(x => x.State)
                .HasColumnOrder(4)
                .IsRequired();

            Property(x => x.TransactionId)
                .HasColumnOrder(5)
                .IsOptional()
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.TransactionTime)
                .HasColumnOrder(6)
                .IsOptional();

            Property(x => x.Provider)
                .HasColumnOrder(7)
                .IsRequired();

            Property(x => x.Guid)
                .HasColumnOrder(8)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            HasRequired(x => x.User)
                .WithMany(x => x.PaymentIntents)
                .HasForeignKey(x => x.UserId);

            HasRequired(x => x.PaymentMethod)
                .WithMany(x => x.PaymentIntents)
                .HasForeignKey(x => x.PaymentMethodId);

            ToTable(nameof(PaymentIntent));
        }
    }
}
