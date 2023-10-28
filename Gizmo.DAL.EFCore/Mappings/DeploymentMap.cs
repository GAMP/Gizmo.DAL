using GizmoDALV2;
using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class DeploymentMap : IEntityTypeConfiguration<Deployment>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Deployment> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).
                HasColumnOrder(0);

            builder.Property(t => t.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Source)
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Destination)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.ExcludeDirectories)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.ExcludeFiles)
                .HasColumnOrder(5)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.IncludeDirectories)
                .HasColumnOrder(6)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.IncludeFiles)
                .HasColumnOrder(7)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.RegistryString)
                .HasColumnOrder(8)
                .HasMaxLength(SQLStringSize.MEDIUM);

            builder.Property(t => t.Guid)
                .HasColumnOrder(9);

            builder.Property(t => t.ComparisonLevel)
                .HasColumnOrder(10);

            builder.Property(t => t.Options)
                .HasColumnOrder(11);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();

            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Column Mappings
            builder.ToTable("Deployment");

            builder.Property(t => t.Id)
                .HasColumnName("DeploymentId");
        }
    }
}
