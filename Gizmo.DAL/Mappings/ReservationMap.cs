using Gizmo.DAL.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class ReservationMap : EntityTypeConfiguration<Reservation>
    {
        public ReservationMap()
        {
            ToTable(nameof(Reservation));

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("ReservationId");

            Property(e => e.Pin)
                .HasMaxLength(6)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Pin") { IsUnique = true }
                })).IsRequired();

            Property(e => e.Date)
                .IsRequired();

            Property(e => e.Duration)
                .IsRequired();

            Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .IsOptional();

            Property(e => e.ContactEmail)
                .HasMaxLength(254)
                .IsOptional();

            Property(e => e.Note)
                .IsOptional();

            Property(e => e.Status)
                .IsRequired();

            HasOptional(e => e.User)
                .WithMany(e => e.Reservations)
                .HasForeignKey(e => e.UserId);

            HasOptional(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .WillCascadeOnDelete(false);

            HasOptional(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .WillCascadeOnDelete(false);

        }
    }

    public class ReservationUserMap : EntityTypeConfiguration<ReservationUser>
    {
        public ReservationUserMap()
        {
            ToTable(nameof(ReservationUser));

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("ReservationUserId");

            Property(e => e.ReservationId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                 {
                     new IndexAttribute("UQ_Reservation_User")
                     {
                         IsUnique = true,
                         Order = 0
                     }
                 }));

            Property(e => e.UserId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                     new IndexAttribute("UQ_Reservation_User")
                     {
                         IsUnique = true,
                         Order = 1
                     }
                 }));

            HasRequired(e => e.Reservation)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.ReservationId);

            HasRequired(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }

    public class ReservationHostMap : EntityTypeConfiguration<ReservationHost>
    {
        public ReservationHostMap()
        {
            ToTable(nameof(ReservationHost));

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("ReservationHostId");

            Property(e => e.ReservationId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Reservation_Host")
                    {
                        IsUnique = true,
                        Order = 0
                    }
                }));

            Property(e => e.HostId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[]
                {
                    new IndexAttribute("UQ_Reservation_Host")
                    {
                        IsUnique = true,
                        Order = 1
                    }
                }));

            Property(e => e.PreferedUserId)
                .IsOptional();

            HasRequired(e => e.Reservation)
                .WithMany(e => e.Hosts)
                .HasForeignKey(e => e.ReservationId);

            HasRequired(e => e.Host)
                .WithMany()
                .HasForeignKey(e => e.HostId)
                .WillCascadeOnDelete(false);

            HasOptional(e => e.PreferedUser)
                .WithMany()
                .HasForeignKey(e => e.PreferedUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
