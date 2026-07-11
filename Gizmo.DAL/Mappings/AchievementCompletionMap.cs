using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Achievement completion entity map.
    /// </summary>
    public sealed class AchievementCompletionMap : IEntityTypeConfiguration<AchievementCompletion>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AchievementCompletion> builder)
        {
            builder.ToTable(nameof(AchievementCompletion));

            builder.HasKey(completion => completion.Id);

            builder.Property(completion => completion.Id)
                .HasColumnOrder(0)
                .HasColumnName("AchievementCompletionId");

            builder.Property(completion => completion.UserId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(completion => completion.AchievementId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(completion => completion.RangeStart)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(completion => completion.CompletedTime)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(completion => completion.Quantity)
                .HasColumnOrder(5)
                .IsRequired();

            // Indexes — the unique key also makes the live upsert race-safe
            builder.HasIndex(completion => new { completion.UserId, completion.AchievementId, completion.RangeStart })
                .IsUnique();

            builder.HasOne(completion => completion.User)
                .WithMany()
                .HasForeignKey(completion => completion.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(completion => completion.Achievement)
                .WithMany()
                .HasForeignKey(completion => completion.AchievementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
