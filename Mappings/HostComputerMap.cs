using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
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
            builder.HasIndex(t => t.MACAddress).IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id).IsUnique(false);

            // Table & Column Mappings
            builder.ToTable("HostComputer");
        }
    }
}
