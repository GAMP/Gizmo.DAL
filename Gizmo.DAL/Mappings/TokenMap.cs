using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            ToTable(nameof(Token));

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("TokenId");

            Property(e => e.UserId)
               .IsOptional();

            Property(e => e.Value)
                .HasMaxLength(32)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Value") { IsUnique = true }
                }));

            Property(e => e.ConfirmationCode)
                .HasMaxLength(6)
                .IsOptional();

            Property(e => e.Type)
                .IsRequired();

            Property(e => e.Status)
                .IsRequired();

            Property(e => e.Expires)
                .IsOptional();

            HasOptional(e => e.User)
                .WithMany(e => e.Tokens)
                .HasForeignKey(e => e.UserId);

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
}
