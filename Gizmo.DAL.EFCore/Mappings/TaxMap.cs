using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
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

            builder.Property(x => x.Id)
                .HasColumnName("TaxId");

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            builder.ToTable("Tax");

            // Seeds
            builder.HasData(new Tax() { Id = 1, Name = "24%", Value = 23, UseOrder = 0 });
            builder.HasData(new Tax() { Id = 2, Name = "16%", Value = 16, UseOrder = 1 });
            builder.HasData(new Tax() { Id = 3, Name = "None", Value = 0, UseOrder = 2 });
        }
    }
}
