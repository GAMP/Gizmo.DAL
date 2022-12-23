using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class HostComputerMap : IEntityTypeConfiguration<HostComputer>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostComputer> builder)
        {
            // Properties
            builder.Property(x => x.Id);

            builder.Property(t => t.Hostname)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.MACAddress)
                .IsRequired()
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY); 

            // Indexes
            builder.HasIndex(t => t.MACAddress).HasDatabaseName("UQ_MACAddress").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id).IsUnique(false);

            // Table & Column Mappings
            builder.ToTable("HostComputer");       
        }
    }
}
