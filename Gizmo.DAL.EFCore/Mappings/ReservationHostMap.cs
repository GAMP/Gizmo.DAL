using Gizmo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore.Mappings
{
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
