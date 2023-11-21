using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class PersonalFileMap : EntityTypeConfiguration<PersonalFile>
    {
        public PersonalFileMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));
            
            Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Source)
                .IsRequired()
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            Property(x => x.Activation)
                .HasColumnOrder(5);

            Property(x => x.Deactivation)
                .HasColumnOrder(6);

            Property(x => x.MaxQuota)
                .HasColumnOrder(7);

            Property(x => x.CompressionLevel)
                .HasColumnOrder(8);

            Property(t => t.ExcludeDirectories)
                .HasColumnOrder(9)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.ExcludeFiles)
                .HasColumnOrder(10)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.IncludeDirectories)
                .HasColumnOrder(11)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(t => t.IncludeFiles)
                .HasColumnOrder(12)
                .HasMaxLength(SQLStringSize.NORMAL);

            Property(x => x.Guid)
                .HasColumnOrder(13)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            Property(x => x.Type)
                .HasColumnOrder(14);

            Property(x => x.Options)
               .HasColumnOrder(15);

            Property(x => x.Accessible)
                .HasColumnOrder(16);

            // Table & Column Mappings
            ToTable("PersonalFile");

            Property(t => t.Id)
                .HasColumnName("PersonalFileId");
        }
    }
}
