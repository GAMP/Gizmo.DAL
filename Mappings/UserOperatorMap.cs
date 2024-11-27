using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User operator mapping.
    /// </summary>
    public class UserOperatorMap : IEntityTypeConfiguration<UserOperator>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserOperator> builder)
        {
            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(t => t.Username).IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Email).IsUnique();
            builder.HasIndex(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("UserOperator");
        }
    }
}
