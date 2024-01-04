using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Mappings
{
    public class ReservationMap : IEntityTypeConfiguration<Reservation>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable(nameof(Reservation));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ReservationId");

            builder.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Duration)
                .IsRequired();

            builder.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(e => e.ContactEmail)
                .HasMaxLength(254)
                .IsRequired(false);

            builder.Property(e => e.Note)
                .IsRequired(false);

            builder.Property(e => e.Status)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Pin, "UQ_Pin").IsUnique();

            builder.HasOne(e => e.User)
                .WithMany(e => e.Reservations)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
