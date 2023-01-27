using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class UserGuestMap : IEntityTypeConfiguration<UserGuest>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UserGuest> builder)
        {
            // Table & Column Mappings
            builder.ToTable("UserGuest");

            builder.Property(x => x.IsJoined)
                .HasColumnOrder(1);

            builder.Property(x => x.IsReserved)
                .HasColumnOrder(2);

            builder.Property(x => x.ReservedHostId)
                .HasColumnOrder(3);

            builder.Property(x => x.ReservedSlot)
                .HasColumnOrder(4);

            // Indexes
            builder.HasIndex(x => new { x.ReservedHostId, x.ReservedSlot }).HasDatabaseName("UQ_UserGuestHostSlot").IsUnique();
            
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.ReservedHost)
                .WithMany(x => x.ReservedGuests)
                .HasForeignKey(x => x.ReservedHostId);
        }
    }
}
