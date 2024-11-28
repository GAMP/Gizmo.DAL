using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Age restriction login map.
    /// </summary>
    public sealed class AgeRestrictionLoginMap : IEntityTypeConfiguration<AgeRestrictionLogin>
    {
        /// <inheritdoc/>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AgeRestrictionLogin> builder)
        {
            builder.ToTable(nameof(AgeRestrictionLogin))
                .HasBaseType<AgeRestriction>();
        }
    }
}
