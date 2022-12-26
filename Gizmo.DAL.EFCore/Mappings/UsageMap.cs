using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GizmoDALV2.Mappings
{
    public class UsageBaseMap : IEntityTypeConfiguration<Usage>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<Usage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageId");

            builder.Property(x => x.UsageSessionId)
                .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                .HasColumnOrder(2);

            builder.Property(x => x.Seconds)
                .HasColumnOrder(3);

            builder.ToTable(nameof(Usage));

            builder.HasOne(x => x.UsageSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UsageSessionId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class UsageUserSessionMap : IEntityTypeConfiguration<UsageUserSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageUserSession> builder)
        {
            builder.ToTable(nameof(UsageUserSession));

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.UserSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserSessionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class UsageTimeMap : IEntityTypeConfiguration<UsageTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageTime> builder)
        {
            builder.ToTable(nameof(UsageTime));

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.InvoiceLine)
                .WithMany(x=>x.Usages)
                .HasForeignKey(x => x.InvoiceLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class UsageTimeFixedMap : IEntityTypeConfiguration<UsageTimeFixed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageTimeFixed> builder)
        {
            builder.ToTable(nameof(UsageTimeFixed));

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.InvoiceLine)
                .WithMany(x=>x.Usages)
                .HasForeignKey(x => x.InvoiceLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class UsageRateMap : IEntityTypeConfiguration<UsageRate>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<UsageRate> builder)
        {
            builder.ToTable(nameof(UsageRate));

            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.BillRateId)
                .HasColumnOrder(1);

            builder.Property(x => x.Total)
                .HasColumnOrder(2);

            builder.Property(x => x.Rate)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.BillRate)
                .WithMany(x=>x.Usage)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
