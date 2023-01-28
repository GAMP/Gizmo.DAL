using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
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
}
