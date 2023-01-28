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
    public class AppExeCdImageMap : EntityTypeConfiguration<AppExeCdImage>
    {
        public AppExeCdImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.AppExeId)
                .HasColumnOrder(1);

            this.Property(t => t.Path)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.MountOptions)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.DeviceId)
                .HasColumnOrder(4);

            this.Property(x => x.CheckExitCode)
                .HasColumnOrder(5);

            this.Property(t => t.Guid)
                .HasColumnOrder(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Guid") { IsUnique = true } 
                }));

            // Table & Column Mappings
            this.ToTable("AppExeCdImage");

            this.Property(t => t.Id)
                .HasColumnName("AppExeCdImageId");

            // Relationships
            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.AppExeCdImages)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
