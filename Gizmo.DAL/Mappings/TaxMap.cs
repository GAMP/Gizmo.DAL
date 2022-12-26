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
    public class TaxMap : EntityTypeConfiguration<Tax>
    {
        public TaxMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[] 
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true } 
                }));

            this.Property(x => x.Value)
                .IsRequired();

            this.ToTable("Tax");

            this.Property(x => x.Id)
                .HasColumnName("TaxId");
        }
    }
}
