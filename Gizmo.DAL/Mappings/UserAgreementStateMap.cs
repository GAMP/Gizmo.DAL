using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UserAgreementStateMap : EntityTypeConfiguration<Entities.UserAgreementState>
    {
        public UserAgreementStateMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(x => x.Id)
                    .HasColumnOrder(0)
                    .HasColumnName("UserAgreementStateId");

            Property(x => x.UserAgreementId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserAgreementState")
                {
                    IsUnique = true,
                    Order = 0
                }}));

            Property(x => x.UserId)
                 .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UserAgreementState")
                 {
                     IsUnique = true,
                     Order = 1
                 }}));

            Property(x => x.AcceptState)
                .HasColumnOrder(2);

            HasRequired(x => x.UserAgreement)
                .WithMany(x => x.UserAgreementStates)
                .HasForeignKey(x => x.UserAgreementId);

            HasRequired(x => x.User)
                .WithMany(x => x.UserAgreementStates)
                .HasForeignKey(x => x.UserId);

            ToTable(nameof(Entities.UserAgreementState));
        }
    }
}
