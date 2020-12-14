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
    public class MappingMap : EntityTypeConfiguration<Mapping>
    {
        public MappingMap()
        {
            //Primary key
            this.HasKey(x => x.Id);

            //Properties
            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.Label)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(x => x.Source)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            this.Property(x => x.MountPoint)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired()
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_MountPoint") { IsUnique = true } 
                })); ;

            this.Property(x => x.Type)
                .HasColumnOrder(4);

            this.Property(x => x.Size)
                .HasColumnOrder(5);

            this.Property(x => x.Username)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(x => x.Password)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45);

            this.Property(x => x.Options)
                .HasColumnOrder(8);

            //Table & Column mappings
            this.ToTable("Mapping");

            this.Property(x => x.Id)
                .HasColumnName("MappingId");
        }
    }
}
