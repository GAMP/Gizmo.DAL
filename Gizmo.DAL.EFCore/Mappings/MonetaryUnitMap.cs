using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class MonetaryUnitMap : IEntityTypeConfiguration<MonetaryUnit>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<MonetaryUnit> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("MonetaryUnitId")
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Value)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => t.Name).IsUnique();

            // Table & Coulmn
            builder.ToTable(nameof(MonetaryUnit));
        }
    }
}
