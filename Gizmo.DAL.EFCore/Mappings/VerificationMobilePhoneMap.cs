using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
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
