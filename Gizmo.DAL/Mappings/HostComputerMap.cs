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
    public class HostComputerMap : EntityTypeConfiguration<HostComputer>
    {
        public HostComputerMap()
        {
            // Properties
            this.Property(x => x.Id);

            this.Property(t => t.Hostname)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            this.Property(t => t.MACAddress)
                .IsRequired()
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_MACAddress") { IsUnique = true } 
                })); 

            // Table & Column Mappings
            this.ToTable("HostComputer");       
        }
    }
}
