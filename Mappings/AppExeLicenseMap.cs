using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExeLicenseMap : EntityTypeConfiguration<AppExeLicense>
    {
        public AppExeLicenseMap()
        {
            // Primary Key
            HasKey(t => new { AppExeId = t.AppExeId, LicenseId = t.LicenseId });

            // Properties
            Property(t => t.AppExeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.LicenseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            ToTable("AppExeLicense");

            // Ignores
            Ignore(x => x.Id);

            // Relationships
            HasRequired(t => t.AppExe)
                .WithMany(t => t.Licenses)
                .HasForeignKey(d => d.AppExeId);

            HasRequired(t => t.License)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.LicenseId);
        }
    }
}
