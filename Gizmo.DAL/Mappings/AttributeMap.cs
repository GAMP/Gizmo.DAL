using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gizmo.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace GizmoDALV2.Mappings
{
    public class AttributeMap : EntityTypeConfiguration<Gizmo.DAL.Entities.Attribute>
    {
        public AttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AttributeId"); ;

            this.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            this.Property(x => x.FriendlyName)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY); 

            // Table & Column Mappings
            this.ToTable(nameof(Gizmo.DAL.Entities.Attribute));
        }
    }
}
