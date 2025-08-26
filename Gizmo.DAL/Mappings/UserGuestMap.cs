using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// User guest entity map.
    /// </summary>
    public class UserGuestMap : IEntityTypeConfiguration<UserGuest>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserGuest> builder)
        {
            // Table & Column Mappings
            builder.ToTable("UserGuest");

            builder.Property(userGuest => userGuest.Id)
                  .HasColumnOrder(0);

            builder.Property(userGuest => userGuest.IsJoined)
                .HasColumnOrder(1);

            builder.Property(userGuest => userGuest.IsReserved)
                .HasColumnOrder(2);

            builder.Property(userGuest => userGuest.ReservedHostId)
                .HasColumnOrder(3);

            builder.Property(userGuest => userGuest.ReservedSlot)
                .HasColumnOrder(4);

            // Indexes
            builder.HasIndex(userGuest => new { userGuest.ReservedHostId, userGuest.ReservedSlot }).IsUnique();

            builder.HasIndex(userGuest => userGuest.Id);

            builder.HasOne(userGuest => userGuest.ReservedHost)
                .WithMany(host => host.ReservedGuests)
                .HasForeignKey(userGuest => userGuest.ReservedHostId);
        }
    }
}
