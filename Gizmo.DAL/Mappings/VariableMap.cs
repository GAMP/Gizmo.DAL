using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Variable entity map.
    /// </summary>
    public class VariableMap : IEntityTypeConfiguration<Variable>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Variable> builder)
        {
            builder.ToTable(nameof(Variable));

            builder.HasKey(variable => variable.Id);

            builder.Property(variable => variable.Id)
                .HasColumnName("VariableId")
                .HasColumnOrder(0);

            builder.Property(variable => variable.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(variable => variable.Value)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired();

            builder.Property(variable => variable.Scope)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(variable => variable.UseOrder)
                .HasColumnOrder(4)
                .IsRequired();

            builder.HasIndex(variable => variable.Name)
                .IsUnique();
        }
    }
}
