using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class MappingMap : IEntityTypeConfiguration<Mapping>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Mapping> builder)
        {
            //Primary key
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Label)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Source)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            builder.Property(x => x.MountPoint)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnOrder(4);

            builder.Property(x => x.Size)
                .HasColumnOrder(5);

            builder.Property(x => x.Username)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Password)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(x => x.Options)
                .HasColumnOrder(8);

            // Indexes
            builder.HasIndex(t => t.MountPoint).HasDatabaseName("UQ_MountPoint").IsUnique();

            //Table & Column mappings
            builder.ToTable("Mapping");

            builder.Property(x => x.Id)
                .HasColumnName("MappingId");
        }
    }
}
