using GizmoDALV2.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class UsageBaseMap : EntityTypeConfiguration<Usage>
    {
        public UsageBaseMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageId");

            this.Property(x => x.UsageSessionId)
                .HasColumnOrder(1);

            this.Property(x => x.UserId)
                .HasColumnOrder(2);

            this.Property(x => x.Seconds)
                .HasColumnOrder(3);

            this.ToTable(nameof(Usage));

            this.HasRequired(x => x.UsageSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UsageSessionId);

            this.HasRequired(x => x.User)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }

    public class UsageUserSessionMap : EntityTypeConfiguration<UsageUserSession>
    {
        public UsageUserSessionMap()
        {
            this.ToTable(nameof(UsageUserSession));

            this.HasRequired(x => x.UserSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserSessionId)
                .WillCascadeOnDelete(false);
        }
    }

    public class UsageTimeMap : EntityTypeConfiguration<UsageTime>
    {
        public UsageTimeMap()
        {
            this.ToTable(nameof(UsageTime));

            this.HasRequired(x => x.InvoiceLine)
                .WithMany(x=>x.Usages)
                .HasForeignKey(x => x.InvoiceLineId);
        }
    }

    public class UsageTimeFixedMap : EntityTypeConfiguration<UsageTimeFixed>
    {
        public UsageTimeFixedMap()
        {
            this.ToTable(nameof(UsageTimeFixed));

            this.HasRequired(x => x.InvoiceLine)
                .WithMany(x=>x.Usages)
                .HasForeignKey(x => x.InvoiceLineId);
        }
    }

    public class UsageRateMap : EntityTypeConfiguration<UsageRate>
    {
        public UsageRateMap()
        {
            this.ToTable(nameof(UsageRate));

            this.Property(x => x.Id)
                .HasColumnOrder(0);

            this.Property(x => x.BillRateId)
                .HasColumnOrder(1);

            this.Property(x => x.Total)
                .HasColumnOrder(2);

            this.Property(x => x.Rate)
                .HasColumnOrder(3);

            this.HasRequired(x => x.BillRate)
                .WithMany(x=>x.Usage)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
