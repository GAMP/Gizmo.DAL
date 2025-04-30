using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Reservation entity map.
    /// </summary>
    public class ReservationMap : IEntityTypeConfiguration<Reservation>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable(nameof(Reservation));

            builder.HasKey(reservation => reservation.Id);

            builder.Property(reservation => reservation.Id)
                .HasColumnName("ReservationId");

            builder.Property(reservation => reservation.Pin)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(reservation => reservation.Date)
                .IsRequired();

            builder.Property(reservation => reservation.Duration)
                .IsRequired();

            builder.Property(reservation => reservation.ContactPhone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(reservation => reservation.ContactEmail)
                .HasMaxLength(254)
                .IsRequired(false);

            builder.Property(reservation => reservation.Note)
                .IsRequired(false);

            builder.Property(reservation => reservation.Status)
                .IsRequired();

            builder.Property(reservation => reservation.ExpireAfter)
                .IsRequired(false);

            builder.Property(reservation => reservation.CancellationGracePeriod)
                .IsRequired(false);

            builder.Property(reservation => reservation.CancellationRefundPercentage)
                .IsRequired();

            builder.Property(reservation => reservation.LoginBlockBeforeTime)
                .IsRequired(false);

            builder.Property(reservation => reservation.LoginBlockAfterTime)
                .IsRequired(false);

            builder.Property(reservation => reservation.FinalizedById)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(reservation => reservation.Pin)
                .IsUnique();

            builder.HasIndex(reservation => reservation.Status);

            builder.HasOne(reservation => reservation.User)
                .WithMany(reservation => reservation.Reservations)
                .HasForeignKey(reservation => reservation.UserId);

            builder.HasOne(reservation => reservation.CreatedBy)
                .WithMany()
                .HasForeignKey(reservation => reservation.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(reservation => reservation.ModifiedBy)
                .WithMany()
                .HasForeignKey(reservation => reservation.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(reservation => reservation.FinalizedBy)
                .WithMany()
                .HasForeignKey(reservation => reservation.FinalizedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
