using Gizmo.DAL.Entities;

using Gizmo.DAL;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class VariableMap : EntityTypeConfiguration<Variable>
    {
        public VariableMap()
        {
            //Primary key
            HasKey(x => x.Id);

            //Properties
            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Name") { IsUnique = true }
                }));

            Property(x => x.Value)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired();

            Property(x => x.Scope)
                .HasColumnOrder(3)
                .IsRequired();

            Property(x => x.UseOrder)
                .HasColumnOrder(4)
                .IsRequired();

            //Table & Column mappings
            ToTable(nameof(Variable));

            Property(x => x.Id)
                .HasColumnName("VariableId");
        }
    }
}
