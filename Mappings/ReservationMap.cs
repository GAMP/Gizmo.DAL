using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
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

    public class ReservationHostMap : IEntityTypeConfiguration<ReservationHost>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<ReservationHost> builder)
        {
            builder.ToTable(nameof(ReservationHost));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ReservationHostId");

            builder.Property(e => e.ReservationId)
                .IsRequired();

            builder.Property(e => e.HostId)
                .IsRequired();

            builder.Property(e => e.PreferedUserId)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(x => new { x.ReservationId, x.HostId }).HasDatabaseName("UQ_Reservation_Host").IsUnique();

            builder.HasOne(e => e.Reservation)
                .WithMany(e => e.Hosts)
                .HasForeignKey(e => e.ReservationId);

            builder.HasOne(e => e.Host)
                .WithMany()
                .HasForeignKey(e => e.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.PreferedUser)
                .WithMany()
                .HasForeignKey(e => e.PreferedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
