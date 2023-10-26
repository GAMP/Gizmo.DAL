using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using GizmoDALV2;

namespace Gizmo.DAL.Mappings
{
    public class AttributeMap : EntityTypeConfiguration<Gizmo.DAL.Entities.Attribute>
    {
        public AttributeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("AttributeId"); ;

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.FriendlyName)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            // Table & Column Mappings
            ToTable(nameof(Gizmo.DAL.Entities.Attribute));
        }
    }
}
