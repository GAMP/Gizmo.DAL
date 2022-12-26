using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class VerificationMap : IEntityTypeConfiguration<Verification>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Verification> builder)
        {
            builder.ToTable(nameof(Verification));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("VerificationId");

            builder.Property(e => e.TokenId)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired(false);

            builder.Property(e => e.Status)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(e => e.Verifications)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Token)
                .WithMany()
                .HasForeignKey(e => e.TokenId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class VerificationEmailMap : IEntityTypeConfiguration<VerificationEmail>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<VerificationEmail> builder)
        {
            builder.ToTable(nameof(VerificationEmail));

            builder.Property(e => e.Id)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.Property(e => e.Email)
                .HasMaxLength(254)
                .IsRequired();
        }
    }

    public class VerificationMobilePhoneMap : IEntityTypeConfiguration<VerificationMobilePhone>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<VerificationMobilePhone> builder)
        {
            builder.ToTable(nameof(VerificationMobilePhone));

            builder.Property(e => e.Id)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
