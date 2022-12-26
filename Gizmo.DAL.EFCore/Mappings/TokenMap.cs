using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class TokenMap : IEntityTypeConfiguration<Token>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable(nameof(Token));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("TokenId");

            builder.Property(e => e.UserId)
               .IsRequired(false);

            builder.Property(e => e.Value)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.ConfirmationCode)
                .HasMaxLength(6)
                .IsRequired(false);

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired();

            builder.Property(e => e.Expires)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(t => t.Value).HasDatabaseName("UQ_Value").IsUnique();

            builder.HasOne(e => e.User)
                .WithMany(e => e.Tokens)
                .HasForeignKey(e => e.UserId);

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
