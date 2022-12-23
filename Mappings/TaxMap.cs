using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class TaxMap : IEntityTypeConfiguration<Tax>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Value)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            builder.ToTable("Tax");

            builder.Property(x => x.Id)
                .HasColumnName("TaxId");
        }
    }
}
