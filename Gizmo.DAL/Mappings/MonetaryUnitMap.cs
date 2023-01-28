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
    public class MonetaryUnitMap : EntityTypeConfiguration<MonetaryUnit>
    {
        public MonetaryUnitMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            // Properties
            this.Property(x => x.Id)
                .HasColumnName("MonetaryUnitId")
                .HasColumnOrder(0);

            this.Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            this.Property(x => x.Value)
                .HasColumnOrder(2)
                .IsRequired();

            this.Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Table & Coulmn
            this.ToTable(nameof(MonetaryUnit));
        }
    }
}
