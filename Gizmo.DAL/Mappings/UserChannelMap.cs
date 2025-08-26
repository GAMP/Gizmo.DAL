using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User channel entity configuration.
    /// </summary>
    public sealed class UserChannelMap : IEntityTypeConfiguration<UserChannel>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<UserChannel> builder)
        {
            builder.ToTable(nameof(UserChannel));

            builder.HasKey(userChannel => userChannel.Id);

            builder.Property(userChannel => userChannel.Id)
                .HasColumnName("UserChannelId")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(userChannel => userChannel.UserId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(userChannel => userChannel.Channel)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(userChannel => userChannel.Value)
                .HasMaxLength(SQLStringSize.TINY)
                .HasColumnOrder(3)
                .IsRequired();

            builder.HasIndex(userChannel => new { userChannel.UserId, userChannel.Channel }).HasFilter(null);

            builder.HasOne(userChannel => userChannel.User)
                .WithMany(user => user.Channels)
                .HasForeignKey(userChannel => userChannel.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(userChannel => userChannel.CreatedBy)
                .WithMany()
                .HasForeignKey(userChannel => userChannel.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(userChannel => userChannel.ModifiedBy)
               .WithMany()
               .HasForeignKey(userChannel => userChannel.ModifiedById)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
