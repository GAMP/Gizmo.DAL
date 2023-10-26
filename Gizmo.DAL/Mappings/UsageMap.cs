using Gizmo.DAL.Entities;

using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    public class UsageBaseMap : EntityTypeConfiguration<Usage>
    {
        public UsageBaseMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("UsageId");

            Property(x => x.UsageSessionId)
                .HasColumnOrder(1);

            Property(x => x.UserId)
                .HasColumnOrder(2);

            Property(x => x.Seconds)
                .HasColumnOrder(3);

            ToTable(nameof(Usage));

            HasRequired(x => x.UsageSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UsageSessionId);

            HasRequired(x => x.User)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }

    public class UsageUserSessionMap : EntityTypeConfiguration<UsageUserSession>
    {
        public UsageUserSessionMap()
        {
            ToTable(nameof(UsageUserSession));

            HasRequired(x => x.UserSession)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.UserSessionId)
                .WillCascadeOnDelete(false);
        }
    }

    public class UsageTimeMap : EntityTypeConfiguration<UsageTime>
    {
        public UsageTimeMap()
        {
            ToTable(nameof(UsageTime));

            HasRequired(x => x.InvoiceLine)
                .WithMany(x => x.Usages)
                .HasForeignKey(x => x.InvoiceLineId);
        }
    }

    public class UsageTimeFixedMap : EntityTypeConfiguration<UsageTimeFixed>
    {
        public UsageTimeFixedMap()
        {
            ToTable(nameof(UsageTimeFixed));

            HasRequired(x => x.InvoiceLine)
                .WithMany(x => x.Usages)
                .HasForeignKey(x => x.InvoiceLineId);
        }
    }

    public class UsageRateMap : EntityTypeConfiguration<UsageRate>
    {
        public UsageRateMap()
        {
            ToTable(nameof(UsageRate));

            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.BillRateId)
                .HasColumnOrder(1);

            Property(x => x.Total)
                .HasColumnOrder(2);

            Property(x => x.Rate)
                .HasColumnOrder(3);

            HasRequired(x => x.BillRate)
                .WithMany(x => x.Usage)
                .HasForeignKey(x => x.BillRateId);
        }
    }
}
