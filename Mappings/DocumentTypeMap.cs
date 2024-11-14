using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Document type entity map.
    /// </summary>
    public sealed class DocumentTypeMap : IEntityTypeConfiguration<DocumentType>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.ToTable(nameof(DocumentType));
            builder.HasKey(documentType => documentType.Id);

            builder.Property(documentType => documentType.Id)
                .HasColumnName("DocumentTypeId")
                .HasColumnOrder(0);

            builder.Property(documentType => documentType.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(documentType => documentType.IsDeleted)
                .HasColumnOrder(3)
                .IsRequired();

            builder.HasIndex(builder => builder.Name)
                .IsUnique();
        }
    }
}
