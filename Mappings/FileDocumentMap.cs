using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// File document entity map.
    /// </summary>
    public sealed class FileDocumentMap : IEntityTypeConfiguration<FileDocument>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<FileDocument> builder)
        {
            builder.ToTable(nameof(FileDocument))
                .HasBaseType<File>();

            builder.Property(document => document.DocumentTypeId)
                .HasColumnOrder(0);

            builder.Property(fileDocument => fileDocument.Description)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(1);

            builder.HasOne(document => document.DocumentType)
                .WithMany(documentType => documentType.Documents)
                .HasForeignKey(document => document.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
