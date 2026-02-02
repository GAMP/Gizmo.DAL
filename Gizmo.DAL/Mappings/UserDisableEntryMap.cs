using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User disable entry entity map.
    /// </summary>
    public sealed class UserDisableEntryMap : IEntityTypeConfiguration<UserDisableEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserDisableEntry> builder)
        {
            builder.ToTable(nameof(UserDisableEntry));

            builder.HasKey(userDisableEntry => userDisableEntry.Id);

            builder.Property(userDisableEntry => userDisableEntry.Id)
                .HasColumnName("UserDisableEntryId")
                .HasColumnOrder(0);

            builder.Property(userDisableEntry => userDisableEntry.Type)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(userDisableEntry => userDisableEntry.DisableReasonId)
                .HasColumnOrder(2)
                .IsRequired(false);

            builder.Property(userDisableEntry => userDisableEntry.Note)
                .HasColumnOrder(3)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.HasOne(userDisableEntry => userDisableEntry.DisableReason)
                .WithMany(userDisableReason => userDisableReason.Entries)
                .HasForeignKey(userDisableEntry => userDisableEntry.DisableReasonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
