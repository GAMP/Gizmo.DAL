using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Client options entity map.
    /// </summary>
    public sealed class ClientOptionsMap : IEntityTypeConfiguration<ClientOptions>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ClientOptions> builder)
        {
            builder.ToTable(nameof(ClientOptions));

            builder.HasKey(clientOptions => clientOptions.Id);

            builder.Property(clientOptions => clientOptions.Id)
                .HasColumnName("ClientOptionsId")
                .HasColumnOrder(0);

            builder.Property(clientOptions => clientOptions.Name)
                .HasMaxLength(SQLStringSize.TINY45)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(clientOptions => clientOptions.Description)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(2)
                .IsRequired(false);

            builder.Property(clientOptions => clientOptions.IsDefault)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(clientOptions => clientOptions.Data)
                .HasColumnOrder(4)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired();
        }
    }
}
