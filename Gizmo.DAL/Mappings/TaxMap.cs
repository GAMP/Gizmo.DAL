using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class TaxMap : EntityTypeConfiguration<Tax>
    {
        public TaxMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.Value)
                .IsRequired();

            ToTable("Tax");

            Property(x => x.Id)
                .HasColumnName("TaxId");
        }
    }
}
