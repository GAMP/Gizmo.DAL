using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Target group target entity map.
    /// </summary>
    public sealed class TargetMap : IEntityTypeConfiguration<Target>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Target> builder)
        {
            builder.ToTable(nameof(Target))
                .UseTptMappingStrategy();

            builder.HasKey(target => target.Id);

            builder.Property(target => target.Id)
                .HasColumnName("TargetId");

            builder.Property(target => target.Id)
                .IsRequired()
                .HasColumnOrder(0);
        }
    }
}
