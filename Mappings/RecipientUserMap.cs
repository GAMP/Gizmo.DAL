using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Recipient user map.
    /// </summary>
    public sealed class RecipientUserMap : IEntityTypeConfiguration<RecipientUser>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<RecipientUser> builder)
        {
            builder.ToTable(nameof(RecipientUser))
                .HasBaseType<RecipientChanneled>();

            builder.Property(RecipientUser => RecipientUser.UserId)
                .IsRequired()
                .HasColumnOrder(0);

            builder.HasOne(RecipientUser => RecipientUser.User)
                .WithMany()
                .HasForeignKey(RecipientUser => RecipientUser.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
