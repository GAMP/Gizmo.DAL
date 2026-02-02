using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User disable entry entity map.
    /// </summary>
    public sealed class UserDisableEntryMap : IEntityTypeConfiguration<UserMemberDisableEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserMemberDisableEntry> builder)
        {
            builder.ToTable(nameof(UserMemberDisableEntry));

            builder.HasKey(userDisableEntry => userDisableEntry.Id);

            builder.Property(userDisableEntry => userDisableEntry.Id)
                .HasColumnName("UserDisableEntryId")
                .HasColumnOrder(0);

            builder.Property(userDisableEntry => userDisableEntry.UserId)
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(userDisableEntry => userDisableEntry.Type)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(userDisableEntry => userDisableEntry.DisableReasonId)
                .HasColumnOrder(3)
                .IsRequired(false);

            builder.Property(userDisableEntry => userDisableEntry.Note)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.HasOne(userDisableEntry => userDisableEntry.DisableReason)
                .WithMany(userDisableReason => userDisableReason.Entries)
                .HasForeignKey(userDisableEntry => userDisableEntry.DisableReasonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(userDisableEntry => userDisableEntry.User)
                .WithMany(userMember => userMember.DisableEntries)
                .HasForeignKey(userDisableEntry => userDisableEntry.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
