using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class VoidDepositPaymentMap : EntityTypeConfiguration<VoidDepositPayment>
    {
        public VoidDepositPaymentMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.DepositPaymentId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_DepositPayment") { IsUnique = true }
                }));

            HasRequired(t => t.DepositPayment)
                .WithMany(t => t.Void)
                .HasForeignKey(t => t.DepositPaymentId);

            // Table & Column Mappings
            ToTable(nameof(VoidDepositPayment));
        }
    }
}
