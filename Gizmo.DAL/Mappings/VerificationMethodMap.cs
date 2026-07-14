using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Verification method entity mapping.
    /// </summary>
    public sealed class VerificationMethodMap : IEntityTypeConfiguration<VerificationMethod>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<VerificationMethod> builder)
        {
            builder.ToTable(nameof(VerificationMethod));

            builder.HasKey(method => method.Id);

            builder.Property(method => method.Id)
                .HasColumnName("VerificationMethodId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(method => method.Context)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(method => method.IntegrationId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(method => method.CapabilityGuid)
                .IsRequired()
                .HasColumnOrder(3);

            builder.Property(method => method.CustomName)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired(false)
                .HasColumnOrder(4);

            builder.Property(method => method.IsPrimary)
                .HasColumnOrder(5);

            builder.Property(method => method.IsDisabled)
                .HasColumnOrder(6);

            builder.Property(method => method.DisplayOrder)
                .HasColumnOrder(7);

            // one entry per mechanism of an integration within a chain — same instance may appear
            // with different capabilities (e.g. sms + flash call) or in different chain contexts
            builder.HasIndex(method => new { method.Context, method.IntegrationId, method.CapabilityGuid }).IsUnique();

            // restrict — deleting an integration must not silently rewrite operator method chains;
            // entries referencing removed integrations are skipped at read time and surfaced in the management ui
            builder.HasOne(method => method.Integration)
                .WithMany()
                .HasForeignKey(method => method.IntegrationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
