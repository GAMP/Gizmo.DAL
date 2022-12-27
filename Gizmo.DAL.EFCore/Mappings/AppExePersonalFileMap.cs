using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class AppExePersonalFileMap : IEntityTypeConfiguration<AppExePersonalFile>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<AppExePersonalFile> builder)
        {
            // Primary Key
            builder.HasKey(t => new { AppExeId = t.AppExeId, PersonalFileId = t.PersonalFileId });

            // Properties
            builder.Property(t => t.AppExeId).ValueGeneratedNever();

            builder.Property(t => t.PersonalFileId).ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("AppExePersonalFile");

            // Ignores
            builder.Ignore(x => x.Id);

            // Indexes
            builder.HasIndex(x => x.AppExeId);

            // Relationships
            builder.HasOne(t => t.AppExe)
                .WithMany(t => t.PersonalFiles)
                .HasForeignKey(d => d.AppExeId);

            builder.HasOne(t => t.PersonalFile)
                .WithMany(t => t.AppExes)
                .HasForeignKey(d => d.PersonalFileId);
        }
    }
}
