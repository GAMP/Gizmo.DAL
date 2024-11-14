using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Document entity map.
    /// </summary>
    public sealed class DocumentMap : IEntityTypeConfiguration<Document>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable(nameof(Document));

            builder.HasKey(document => document.Id);

            builder.Property(document => document.Id)
                .HasColumnName("DocumentId")
                .HasColumnOrder(0);

            builder.Property(document => document.DocumentTypeId)
                .HasColumnOrder(1);

            builder.Property(document => document.Description)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.Property(document => document.Guid)
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(document => document.IsDeleted)
                .HasColumnOrder(5)
                .IsRequired();

            builder.HasOne(document => document.DocumentType)
                .WithMany(documentType => documentType.Documents)
                .HasForeignKey(document => document.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
