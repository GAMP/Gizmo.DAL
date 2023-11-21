using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class MappingMap : EntityTypeConfiguration<Mapping>
    {
        public MappingMap()
        {
            //Primary key
            HasKey(x => x.Id);

            //Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.Label)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.Source)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            Property(x => x.MountPoint)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired()
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_MountPoint") { IsUnique = true } 
                })); ;

            Property(x => x.Type)
                .HasColumnOrder(4);

            Property(x => x.Size)
                .HasColumnOrder(5);

            Property(x => x.Username)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.Password)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45);

            Property(x => x.Options)
                .HasColumnOrder(8);

            //Table & Column mappings
            ToTable("Mapping");

            Property(x => x.Id)
                .HasColumnName("MappingId");
        }
    }
}
