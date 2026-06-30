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

            builder.Property(reservation => reservation.MinimumPaymentPercentage)
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

            // Gate for the ReservationProcessingService 1s timer ExpiringQuery (Status in (Active,Waiting) AND
            // ActivationTime IS NULL AND ExpireAfter IS NOT NULL) — a sargable filter, so this composite is
            // genuinely usable. The table is small today (~180 rows) but reservations accumulate into the
            // thousands+ per year, and this query runs every second — so it's indexed ahead of that growth to
            // avoid a recurring per-second scan as the table grows (cf. the UserSession timer-scan pathology).
            // (The admin-list (BranchId,Status,Date) composite was NOT restored: it's on-demand, not per-second,
            // and its Date.AddMinutes(Duration) upper bound is non-sargable anyway.)
            builder.HasIndex(reservation => new { reservation.Status, reservation.ActivationTime, reservation.ExpireAfter });

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
