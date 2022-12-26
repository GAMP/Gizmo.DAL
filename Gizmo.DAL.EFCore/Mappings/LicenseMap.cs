using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class LicenseMap : IEntityTypeConfiguration<License>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<License> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("LicenseId")
                .HasColumnOrder(0);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Assembly)
                .IsRequired()
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Plugin)
                .IsRequired()
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Settings)
                .HasColumnOrder(4)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            builder.Property(t => t.Guid)
                .HasColumnOrder(5);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Column Mappings
            builder.ToTable("License");
        }
    }
}
