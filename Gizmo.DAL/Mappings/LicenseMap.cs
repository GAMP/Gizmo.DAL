using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizmoDALV2.Mappings
{
    public class LicenseMap : EntityTypeConfiguration<License>
    {
        public LicenseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnName("LicenseId")
                .HasColumnOrder(0);

            this.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            this.Property(t => t.Assembly)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Plugin)
                .IsRequired()
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.Settings)
                .HasColumnOrder(4)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            this.Property(t => t.Guid)
                .HasColumnOrder(5)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            // Table & Column Mappings
            this.ToTable("License");
        }
    }
}
