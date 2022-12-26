using GizmoDALV2.Entities;
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
    public class HostLayoutGroupMap : EntityTypeConfiguration<HostLayoutGroup>
    {
        public HostLayoutGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("HostLayoutGroupId")
                .HasColumnOrder(0);

            this.Property(x => x.Name)
                .HasMaxLength(GizmoDALV2.SQLStringSize.TINY45)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            this.Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            // Table & Column Mappings
            this.ToTable("HostLayoutGroup");
        }
    }
}
