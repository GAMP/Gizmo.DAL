using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class VariableMap : IEntityTypeConfiguration<Variable>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Variable> builder)
        {
            //Primary key
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Value)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired();

            builder.Property(x => x.Scope)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(x => x.UseOrder)
                .HasColumnOrder(4)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            //Table & Column mappings
            builder.ToTable(nameof(Variable));

            builder.Property(x => x.Id)
                .HasColumnName("VariableId");
        }
    }
}
