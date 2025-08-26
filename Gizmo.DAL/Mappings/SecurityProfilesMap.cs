using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Security profile entity map.
    /// </summary>
    public class SecurityProfileMap : IEntityTypeConfiguration<SecurityProfile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<SecurityProfile> builder)
        {
            builder.ToTable(nameof(SecurityProfile));

            builder.HasKey(securityProfile => securityProfile.Id);

            builder.Property(securityProfile => securityProfile.Id)
                .HasColumnName("SecurityProfileId")
                .HasColumnOrder(0);

            builder.Property(securityProfile => securityProfile.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(securityProfile => securityProfile.DisabledDrives)
                .HasColumnOrder(2);

            builder.HasIndex(securityProfile => securityProfile.Name).IsUnique();           
        }
    }
}
