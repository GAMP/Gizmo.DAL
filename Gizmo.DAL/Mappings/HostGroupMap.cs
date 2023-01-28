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
    public class HostGroupMap : EntityTypeConfiguration<HostGroup>
    {
        public HostGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            this.Property(x => x.AppGroupId)
                .HasColumnOrder(2);

            this.Property(x => x.SecurityProfileId)
                .HasColumnOrder(3);

            this.Property(t => t.SkinName)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(x => x.Options)
                .HasColumnOrder(5);

            this.Property(x => x.DefaultGuestGroupId)
                .HasColumnOrder(6)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable(nameof(HostGroup));

            this.Property(t => t.Id)
                .HasColumnName("HostGroupId");

            // Relationships
            this.HasOptional(t => t.AppGroup)
                .WithMany(t => t.HostGroups)
                .HasForeignKey(d => d.AppGroupId);

            this.HasOptional(t => t.SecurityProfile)
                .WithMany(t => t.HostGroups)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
