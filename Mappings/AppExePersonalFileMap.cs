using GizmoDALV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class AppExePersonalFileMap : EntityTypeConfiguration<AppExePersonalFile>
    {
        public AppExePersonalFileMap()
        {
            // Primary Key
            this.HasKey(t => new { AppExeId = t.AppExeId, PersonalFileId = t.PersonalFileId });

            // Properties
            this.Property(t => t.AppExeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PersonalFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AppExePersonalFile");

            // Ignores
            this.Ignore(x => x.Id);

            // Relationships
            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.PersonalFiles)
                .HasForeignKey(d => d.AppExeId);

            this.HasRequired(t => t.PersonalFile)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.PersonalFileId);
        }
    }
}
