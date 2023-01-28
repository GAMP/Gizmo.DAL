using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class AppExeLicenseMap : IEntityTypeConfiguration<AppExeLicense>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExeLicense> builder)
        {
            // Primary Key
            builder.HasKey(t => new { AppExeId = t.AppExeId, LicenseId = t.LicenseId });

            // Properties
            builder.Property(t => t.AppExeId).ValueGeneratedNever();

            builder.Property(t => t.LicenseId).ValueGeneratedNever();

            builder.ToTable("AppExeLicense");

            // Indexes
            builder.HasIndex(x => x.AppExeId);

            // Ignores
            builder.Ignore(x => x.Id);

            // Relationships
            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.Licenses)
                .HasForeignKey(d => d.AppExeId);

            builder.HasOne(t => t.License)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.LicenseId);
        }
    }
}
