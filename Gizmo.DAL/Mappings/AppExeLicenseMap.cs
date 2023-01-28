using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppExeLicenseMap : EntityTypeConfiguration<AppExeLicense>
    {
        public AppExeLicenseMap()
        {
            // Primary Key
            this.HasKey(t => new { AppExeId = t.AppExeId, LicenseId = t.LicenseId });

            // Properties
            this.Property(t => t.AppExeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LicenseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.ToTable("AppExeLicense");

            // Ignores
            this.Ignore(x => x.Id);

            // Relationships
            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.Licenses)
                .HasForeignKey(d => d.AppExeId);

            this.HasRequired(t => t.License)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.LicenseId);
        }
    }
}
