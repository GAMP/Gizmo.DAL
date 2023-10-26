using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class LicenseMap : EntityTypeConfiguration<License>
    {
        public LicenseMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("LicenseId")
                .HasColumnOrder(0);

            Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(t => t.Assembly)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Plugin)
                .IsRequired()
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            Property(t => t.Settings)
                .HasColumnOrder(4)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            Property(t => t.Guid)
                .HasColumnOrder(5)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true }
                }));

            // Table & Column Mappings
            ToTable("License");
        }
    }
}
