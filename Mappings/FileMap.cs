using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// File entity map.
    /// </summary>
    public sealed class FileMap : IEntityTypeConfiguration<File>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable(nameof(File))
                .UseTptMappingStrategy();

            builder.HasKey(file => file.Id);

            builder.Property(file => file.Id)
                .HasColumnName("FileId")
                .IsRequired()
                .HasColumnOrder(0);

            builder.Property(file => file.FileName)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(1);

            builder.Property(file => file.Size)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(file => file.MimeType)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3);

            builder.Property(file => file.Hash)
                .IsRequired(false)
                .HasMaxLength(32)
                .HasColumnOrder(4);

            builder.Property(file => file.Guid)
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(file => file.IsDeleted)
                .IsRequired()
                .HasColumnOrder(6);

            builder.HasIndex(file => file.Guid)
                .IsUnique();
        }
    }
}
