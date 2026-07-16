using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement challenge entity map.
    /// </summary>
    public sealed class AchievementChallengeMap : IEntityTypeConfiguration<AchievementChallenge>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementChallenge> builder)
        {
            builder.ToTable(nameof(AchievementChallenge));

            builder.HasKey(challenge => challenge.Id);

            builder.Property(challenge => challenge.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementChallengeId");

            builder.Property(challenge => challenge.Name)
                .HasColumnOrder(1)
                .HasMaxLength(SQLStringSize.TINY45)
                .IsRequired();

            builder.Property(challenge => challenge.Description)
                .HasColumnOrder(2)
                .HasMaxLength(SQLStringSize.NORMAL)
                .IsRequired(false);

            builder.Property(challenge => challenge.StartTime)
                .HasColumnOrder(3)
                .IsRequired(false);

            builder.Property(challenge => challenge.EndTime)
                .HasColumnOrder(4)
                .IsRequired(false);

            builder.Property(challenge => challenge.MaxCompletions)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(challenge => challenge.GlobalMaxCompletions)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(challenge => challenge.Options)
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(challenge => challenge.IsDisabled)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(challenge => challenge.ImageId)
                .HasColumnOrder(9)
                .IsRequired(false);

            builder.Property(challenge => challenge.IsDeleted)
                .HasColumnOrder(10)
                .IsRequired();

            builder.HasOne(challenge => challenge.Image)
                .WithMany()
                .HasForeignKey(challenge => challenge.ImageId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
