using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    /// <summary>
    /// License key map.
    /// </summary>
    public class LicenseKeyMap : IEntityTypeConfiguration<LicenseKey>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<LicenseKey> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("LicenseKeyId");

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.LicenseId)
                .HasColumnOrder(1);

            builder.Property(x => x.Value)
                .HasColumnOrder(2)
                .HasMaxLength(SQLByteArraySize.NORMAL);

            builder.Property(t => t.Comment)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);            

            builder.Property(x => x.Guid)
                .HasColumnOrder(4);

            builder.Property(x => x.IsEnabled)
                .HasColumnOrder(5);

            builder.Property(x => x.AssignedHostId)
                .HasColumnOrder(6)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(LicenseKey));      

            // Relationships
            builder.HasOne(t => t.License)
                .WithMany(t => t.LicenseKeys)
                .HasForeignKey(d => d.LicenseId);

            builder.HasOne(x => x.AssignedHost)
                .WithMany()
                .HasForeignKey(x => x.AssignedHostId);
        }
    }
}
