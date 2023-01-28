using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class HostMap : IEntityTypeConfiguration<Host>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Host> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("HostId")
                .HasColumnOrder(0);

            builder.Property(x => x.Number)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(x => x.HostGroupId)
                .HasColumnOrder(3);

            builder.Property(x => x.State)
                .HasColumnOrder(4);

            builder.Property(x => x.IconId)
                .HasColumnOrder(5);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(6);

            builder.Property(x => x.Guid)
                .HasColumnOrder(7);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(Host));

            // Relationships
            builder.HasOne(t => t.HostGroup)
                .WithMany(t => t.Hosts)
                .HasForeignKey(d => d.HostGroupId);

            builder.HasOne(x => x.Icon)
                .WithMany()
                .HasForeignKey(x => x.IconId);
        }
    }
}
