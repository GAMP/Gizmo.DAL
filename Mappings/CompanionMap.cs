using Gizmo.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Companion entity map.
    /// </summary>
    public class CompanionMap : IEntityTypeConfiguration<Companion>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Companion> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(x => x.Id)
                .HasColumnName("CompanionId")
                .HasColumnOrder(0);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(t => t.Guid)
                .HasColumnOrder(2);

            // Indexes
            builder.HasIndex(t => t.Guid).IsUnique();
            builder.HasIndex(t => t.Name).IsUnique();

            // Table & Column Mappings
            builder.ToTable(nameof(Companion));

            // Relationships
            builder.HasMany(t => t.Branches)
                .WithOne(t => t.Companion)
                .HasForeignKey(t => t.CompanionId);

            builder.HasMany(t => t.Registers)
                .WithOne(t => t.Companion)
                .HasForeignKey(t => t.CompanionId);
        }
    }
}
