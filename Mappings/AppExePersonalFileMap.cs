using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class AppExePersonalFileMap : EntityTypeConfiguration<AppExePersonalFile>
    {
        public AppExePersonalFileMap()
        {
            // Primary Key
            HasKey(t => new { AppExeId = t.AppExeId, PersonalFileId = t.PersonalFileId });

            // Properties
            Property(t => t.AppExeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.PersonalFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("AppExePersonalFile");

            // Ignores
            Ignore(x => x.Id);

            // Relationships
            HasRequired(t => t.AppExe)
                .WithMany(t => t.PersonalFiles)
                .HasForeignKey(d => d.AppExeId);

            HasRequired(t => t.PersonalFile)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.PersonalFileId);
        }
    }
}
