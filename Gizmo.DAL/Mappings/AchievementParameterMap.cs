using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement parameter entity map.
    /// </summary>
    public sealed class AchievementParameterMap : IEntityTypeConfiguration<AchievementParameter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementParameter> builder)
        {
            builder.ToTable(nameof(AchievementParameter));

            builder.HasKey(parameter => parameter.Id);

            builder.Property(parameter => parameter.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementParameterId");

            builder.Property(parameter => parameter.AchievementId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(parameter => parameter.Key)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.Property(parameter => parameter.Value)
                .HasColumnOrder(3)
                .HasMaxLength(SQLStringSize.TINY)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(parameter => new { parameter.AchievementId, parameter.Key })
                .IsUnique();

            builder.HasOne(parameter => parameter.Achievement)
                .WithMany(achievement => achievement.Parameters)
                .HasForeignKey(parameter => parameter.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
