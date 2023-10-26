using Gizmo.DAL.Entities;

using GizmoDALV2;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class MonetaryUnitMap : EntityTypeConfiguration<MonetaryUnit>
    {
        public MonetaryUnitMap()
        {
            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Id)
                .HasColumnName("MonetaryUnitId")
                .HasColumnOrder(0);

            Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.Value)
                .HasColumnOrder(2)
                .IsRequired();

            Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Table & Coulmn
            ToTable(nameof(MonetaryUnit));
        }
    }
}
