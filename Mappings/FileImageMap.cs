using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// File image entity map.
    /// </summary>
    public sealed class FileImageMap : IEntityTypeConfiguration<FileImage>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<FileImage> builder)
        {
            builder.ToTable(nameof(FileImage))
                .HasBaseType<File>();
        }
    }
}
