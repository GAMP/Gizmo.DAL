using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class HostLayoutGroupMap : EntityTypeConfiguration<HostLayoutGroup>
    {
        public HostLayoutGroupMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("HostLayoutGroupId")
                .HasColumnOrder(0);

            Property(x => x.Name)
                .HasMaxLength(GizmoDALV2.SQLStringSize.TINY45)
                .HasColumnOrder(1)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            Property(x => x.DisplayOrder)
                .HasColumnOrder(2);

            // Table & Column Mappings
            ToTable("HostLayoutGroup");
        }
    }
}
