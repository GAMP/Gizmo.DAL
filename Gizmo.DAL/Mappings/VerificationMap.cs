using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class VerificationMap : EntityTypeConfiguration<Verification>
    {
        public VerificationMap()
        {
            ToTable(nameof(Verification));

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("VerificationId");

            Property(e => e.TokenId)
                .IsRequired();

            Property(e => e.UserId)
                .IsOptional();

            Property(e => e.Status)
                .IsRequired();

            HasOptional(e => e.User)
                .WithMany(e => e.Verifications)
                .HasForeignKey(e => e.UserId);

            HasRequired(e => e.Token)
                .WithMany()
                .HasForeignKey(e => e.TokenId)
                .WillCascadeOnDelete(false);

            HasOptional(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .WillCascadeOnDelete(false);

            HasOptional(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .WillCascadeOnDelete(false);
        }
    }

    public class VerificationEmailMap : EntityTypeConfiguration<VerificationEmail>
    {
        public VerificationEmailMap()
        {
            ToTable(nameof(VerificationEmail));

            Property(e => e.Id)
                .IsRequired();

            Property(e => e.Email)
                .HasMaxLength(254)
                .IsRequired();
        }
    }

    public class VerificationMobilePhoneMap : EntityTypeConfiguration<VerificationMobilePhone>
    {
        public VerificationMobilePhoneMap()
        {
            ToTable(nameof(VerificationMobilePhone));

            Property(e => e.Id)
                .IsRequired();

            Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
