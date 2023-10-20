using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class PersonalFileMap : EntityTypeConfiguration<PersonalFile>
    {
        public PersonalFileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            this.Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Source)
                .IsRequired()
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.Activation)
                .HasColumnOrder(5);

            this.Property(x => x.Deactivation)
                .HasColumnOrder(6);

            this.Property(x => x.MaxQuota)
                .HasColumnOrder(7);

            this.Property(x => x.CompressionLevel)
                .HasColumnOrder(8);

            this.Property(t => t.ExcludeDirectories)
                .HasColumnOrder(9)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.ExcludeFiles)
                .HasColumnOrder(10)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.IncludeDirectories)
                .HasColumnOrder(11)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(t => t.IncludeFiles)
                .HasColumnOrder(12)
                .HasMaxLength(SQLStringSize.NORMAL);

            this.Property(x => x.Guid)
                .HasColumnOrder(13)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            this.Property(x => x.Type)
                .HasColumnOrder(14);

            this.Property(x => x.Options)
               .HasColumnOrder(15);

            this.Property(x => x.Accessible)
                .HasColumnOrder(16);

            // Table & Column Mappings
            this.ToTable("PersonalFile");

            this.Property(t => t.Id)
                .HasColumnName("PersonalFileId");
        }
    }
}
