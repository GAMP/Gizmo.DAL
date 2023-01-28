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
    public class AppExeMaxUserMap : EntityTypeConfiguration<AppExeMaxUser>
    {
        public AppExeMaxUserMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);          

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("AppExeMaxUserId")
                .HasColumnOrder(0);

            this.Property(x => x.AppExeId)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_AppExeAppExeMode") { IsUnique = true, Order = 0 } }));

            this.Property(x => x.Mode)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_AppExeAppExeMode") { IsUnique = true, Order = 1 } }));

            this.Property(x => x.MaxUsers)
                .HasColumnOrder(3);

            // Table & Column Mappings
            this.ToTable("AppExeMaxUser");

            // Relationships
            this.HasRequired(t => t.AppExe)
                .WithMany(t => t.MaxUsers)
                .HasForeignKey(d => d.AppExeId);
        }
    }
}
