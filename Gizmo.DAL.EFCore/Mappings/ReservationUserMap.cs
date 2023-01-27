using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
    public class ReservationUserMap : IEntityTypeConfiguration<ReservationUser>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ReservationUser> builder)
        {
            builder.ToTable(nameof(ReservationUser));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ReservationUserId");

            builder.Property(e => e.ReservationId)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.ReservationId, x.UserId }).HasDatabaseName("UQ_Reservation_User").IsUnique();

            builder.HasOne(e => e.Reservation)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.ReservationId);

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
