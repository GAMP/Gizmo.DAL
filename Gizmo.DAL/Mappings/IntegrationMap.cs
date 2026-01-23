using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Integration entity mapping.
    /// </summary>
    public sealed class IntegrationMap : IEntityTypeConfiguration<Integration>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Integration> builder)
        {
            builder.ToTable(nameof(Integration));

            builder.HasKey(integration => integration.Id);

            builder.Property(integration => integration.Id)
                .HasColumnName("IntegrationId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(integration => integration.Name)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(integration => integration.TypeGuid)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(integration => integration.PublicId)
                .IsRequired()                
                .HasColumnOrder(3);

            builder.Property(integration => integration.ConfigJson)
                .IsRequired(false)
                .HasColumnOrder(4);

            builder.Property(integration => integration.ConfigSchemaVersion)
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.Property(integration => integration.IsDisabled)
                .HasColumnOrder(6);

            builder.Property(integration => integration.IsDeleted)
                .HasColumnOrder(7);

            builder.HasIndex(integration => integration.TypeGuid).IsUnique(false);
            builder.HasIndex(integration => integration.PublicId).IsUnique();
        }
    }
}
