using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Reservation host entity map.
    /// </summary>
    public class ReservationHostMap : IEntityTypeConfiguration<ReservationHost>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ReservationHost> builder)
        {
            builder.ToTable(nameof(ReservationHost));

            builder.HasKey(reservationHost => reservationHost.Id);

            builder.Property(reservationHost => reservationHost.Id)
                .HasColumnName("ReservationHostId");

            builder.Property(reservationHost => reservationHost.ReservationId)
                .IsRequired();

            builder.Property(reservationHost => reservationHost.HostId)
                .IsRequired();

            builder.Property(reservationHost => reservationHost.PreferredUserId)
                .IsRequired(false);

            builder.Property(reservationHost => reservationHost.Status)
                .IsRequired();
            
            builder.Property(reservationHost => reservationHost.ActivationTime)
                .IsRequired(false);

            builder.Property(reservationHost => reservationHost.FinalizedById)
                .IsRequired(false);

            builder.HasIndex(reservationHost => reservationHost.Status);
            builder.HasIndex(x => new { x.ReservationId, x.HostId }).IsUnique();

            builder.HasOne(reservationHost => reservationHost.Reservation)
                .WithMany(reservationHost => reservationHost.Hosts)
                .HasForeignKey(reservationHost => reservationHost.ReservationId);

            builder.HasOne(reservationHost => reservationHost.Host)
                .WithMany()
                .HasForeignKey(reservationHost => reservationHost.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(reservationHost => reservationHost.PreferredUser)
                .WithMany()
                .HasForeignKey(reservationHost => reservationHost.PreferredUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(reservationHost => reservationHost.FinalizedBy)
               .WithMany()
               .HasForeignKey(reservation => reservation.FinalizedById)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
