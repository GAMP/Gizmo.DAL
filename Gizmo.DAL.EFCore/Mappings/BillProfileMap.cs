using GizmoDALV2;
using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
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

            builder.Property(x => x.Id)
                .HasColumnName("BillProfileId");

            // Indexes
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            builder.ToTable("BillProfile");

            builder.HasData(new BillProfile() { Id = 1, Name = "Member Prices" });
            builder.HasData(new BillProfile() { Id = 2, Name = "Guests Prices" });
        }
    }
}
