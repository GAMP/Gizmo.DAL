using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement entity map.
    /// </summary>
    public sealed class AchievementMap : IEntityTypeConfiguration<Achievement>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.ToTable(nameof(Achievement));

            builder.HasKey(achievement => achievement.Id);

            builder.Property(achievement => achievement.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementId");

            builder.Property(achievement => achievement.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.Property(achievement => achievement.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired(false);

            builder.Property(achievement => achievement.SignalGuid)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(achievement => achievement.Range)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(achievement => achievement.Value)
                .HasColumnOrder(5)
                .HasPrecision(19, 4);

            builder.Property(achievement => achievement.MaxCompletionsPerRange)
                .HasColumnOrder(6)
                .IsRequired();

            builder.Property(achievement => achievement.Options)
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(achievement => achievement.IsDisabled)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(achievement => achievement.IsDeleted)
                .HasColumnOrder(9)
                .IsRequired();
        }
    }
}
