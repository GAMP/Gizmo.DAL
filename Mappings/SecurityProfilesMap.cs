using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class SecurityProfileMap : EntityTypeConfiguration<SecurityProfile>
    {
        public SecurityProfileMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            Property(x => x.DisabledDrives)
                .HasColumnOrder(2);

            // Table & Column Mappings
            ToTable("SecurityProfile");

            Property(t => t.Id)
                .HasColumnName("SecurityProfileId");
        }
    }
}
