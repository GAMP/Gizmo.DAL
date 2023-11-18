using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class HostGroupMap : IEntityTypeConfiguration<HostGroup>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<HostGroup> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.AppGroupId)
                .HasColumnOrder(2);

            builder.Property(x => x.SecurityProfileId)
                .HasColumnOrder(3);

            builder.Property(t => t.SkinName)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Options)
                .HasColumnOrder(5);

            builder.Property(x => x.DefaultGuestGroupId)
                .HasColumnOrder(6)
                .IsRequired(false);

            // Table & Column Mappings
            builder.ToTable(nameof(HostGroup));

            builder.Property(t => t.Id)
                .HasColumnName("HostGroupId");

            // Indexes
            builder.HasIndex(t => t.Name).IsUnique();

            // Relationships
            builder.HasOne(t => t.AppGroup)
                .WithMany(t => t.HostGroups)
                .HasForeignKey(d => d.AppGroupId);

            builder.HasOne(t => t.SecurityProfile)
                .WithMany(t => t.HostGroups)
                .HasForeignKey(d => d.SecurityProfileId);
        }
    }
}
