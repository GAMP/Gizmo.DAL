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
            builder.Property(userOperator => userOperator.Id)
                .HasColumnOrder(0);

            builder.Property(userOperator => userOperator.Username)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(userOperator => userOperator.Email)
                .HasMaxLength(254);

            // Indexes
            builder.HasIndex(userOperator => userOperator.Username)
                .IsUnique()
                .HasFilter(null);

            builder.HasIndex(userOperator => userOperator.Email)
                .IsUnique();

            builder.HasIndex(userOperator => userOperator.Id);

            // Table & Column Mappings
            builder.ToTable("UserOperator");
        }
    }
}
