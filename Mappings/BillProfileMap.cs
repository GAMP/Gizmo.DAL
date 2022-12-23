using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class BillProfileMap : IEntityTypeConfiguration<BillProfile>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<BillProfile> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            builder.ToTable("BillProfile");

            builder.Property(x => x.Id)
                .HasColumnName("BillProfileId");
        }
    }
}
