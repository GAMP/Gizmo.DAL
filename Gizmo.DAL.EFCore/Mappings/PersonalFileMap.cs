using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class PersonalFileMap : IEntityTypeConfiguration<PersonalFile>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<PersonalFile> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY);
            
            builder.Property(t => t.Caption)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Description)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(t => t.Source)
                .IsRequired()
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(x => x.Activation)
                .HasColumnOrder(5);

            builder.Property(x => x.Deactivation)
                .HasColumnOrder(6);

            builder.Property(x => x.MaxQuota)
                .HasColumnOrder(7);

            builder.Property(x => x.CompressionLevel)
                .HasColumnOrder(8);

            builder.Property(t => t.ExcludeDirectories)
                .HasColumnOrder(9)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.ExcludeFiles)
                .HasColumnOrder(10)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.IncludeDirectories)
                .HasColumnOrder(11)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(t => t.IncludeFiles)
                .HasColumnOrder(12)
                .HasMaxLength(SQLStringSize.NORMAL);

            builder.Property(x => x.Guid)
                .HasColumnOrder(13);

            builder.Property(x => x.Type)
                .HasColumnOrder(14);

            builder.Property(x => x.Options)
               .HasColumnOrder(15);

            builder.Property(x => x.Accessible)
                .HasColumnOrder(16);

            // Indexes
            builder.HasIndex(t => t.Guid).HasDatabaseName("UQ_Guid").IsUnique();
            builder.HasIndex(t => t.Name).HasDatabaseName("UQ_Name").IsUnique();

            // Table & Column Mappings
            builder.ToTable("PersonalFile");

            builder.Property(t => t.Id)
                .HasColumnName("PersonalFileId");
        }
    }
}
