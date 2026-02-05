using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User disable entity map.
    /// </summary>
    public sealed class UserMemberDisableReasonMap : IEntityTypeConfiguration<UserMemberDisableReason>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserMemberDisableReason> builder)
        {
            builder.ToTable(nameof(UserMemberDisableReason));

            builder.HasKey(userDisableReason => userDisableReason.Id);

            builder.Property(userDisableReason => userDisableReason.Id)
                .HasColumnName("UserMemberDisableReasonId")
                .HasColumnOrder(0);

            builder.Property(userDisableReason => userDisableReason.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(SQLStringSize.TINY45);

            builder.Property(userDisableReason => userDisableReason.Description)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasMaxLength(SQLStringSize.TINY);

            builder.HasMany(userDisableReason => userDisableReason.Entries)
                .WithOne(userDisableEntry => userDisableEntry.DisableReason)
                .HasForeignKey(userDisableEntry => userDisableEntry.DisableReasonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
