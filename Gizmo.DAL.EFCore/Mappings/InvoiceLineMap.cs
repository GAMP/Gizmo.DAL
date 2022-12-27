using GizmoDALV2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.EFCore
{
    public class InvoiceLineMap : IEntityTypeConfiguration<InvoiceLine>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(nameof(InvoiceLine));

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoiceLineId");

            builder.Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                .HasColumnOrder(2);

            builder.Property(x => x.ProductName)
                .HasColumnOrder(3);

            builder.Property(x => x.Quantity)
                .HasColumnOrder(4);

            builder.Property(x => x.UnitPrice)
                .HasColumnOrder(5);

            builder.Property(x => x.UnitListPrice)
                .HasColumnOrder(6);

            builder.Property(x => x.UnitPointsPrice)
                .HasColumnOrder(7);

            builder.Property(x => x.UnitPointsListPrice)
                .HasColumnOrder(8);

            builder.Property(x => x.UnitCost)
                .HasColumnOrder(9);

            builder.Property(x => x.Cost)
                .HasColumnOrder(10);

            builder.Property(x => x.TaxRate)
                .HasColumnOrder(11);

            builder.Property(x => x.PreTaxTotal)
                .HasColumnOrder(12);

            builder.Property(x => x.Total)
                .HasColumnOrder(13);

            builder.Property(x => x.PointsTotal)
                .HasColumnOrder(14);

            builder.Property(x => x.Points)
                .HasColumnOrder(15);

            builder.Property(x => x.PointsAward)
                .HasColumnOrder(16);

            builder.Property(x => x.TaxTotal)
                .HasColumnOrder(17);

            builder.Property(x => x.PayType)
                .HasColumnOrder(18);

            builder.Property(x => x.PointsTransactionId)
                .HasColumnOrder(19);

            builder.Property(x => x.IsDeleted)
                .HasColumnOrder(20);

            builder.Property(x => x.IsVoided)
                .HasColumnOrder(21);

            // Indexes
            builder.HasIndex(t => t.PointsTransactionId).HasDatabaseName("UQ_PointsTransaction").IsUnique().HasFilter(null);

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.InvoiceId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PointsTransaction)
                .WithMany()
                .HasForeignKey(x => x.PointsTransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class InvoiceLineExtendedMap : IEntityTypeConfiguration<InvoiceLineExtended>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineExtended> builder)
        {
            builder.ToTable(nameof(InvoiceLineExtended));

            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.BundleLineId)
                .HasColumnOrder(1);

            builder.Property(x => x.StockTransactionId)
                .HasColumnOrder(2);

            builder.Property(x => x.StockReturnTransactionId)
                .HasColumnOrder(3);

            // Indexes
            builder.HasIndex(x => x.StockTransactionId).HasDatabaseName("UQ_StockTransaction").IsUnique();
            builder.HasIndex(x => x.StockReturnTransactionId).HasDatabaseName("UQ_StockReturnTransaction").IsUnique();
            builder.HasIndex(x => x.Id);

            builder.HasOne(x => x.BundleLine)
                .WithMany()
                .HasForeignKey(x => x.BundleLineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.StockTransaction)
                .WithMany()
                .HasForeignKey(x => x.StockTransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class InvoiceLineProductMap : IEntityTypeConfiguration<InvoiceLineProduct>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineProduct> builder)
        {
            builder.ToTable(nameof(InvoiceLineProduct));

            // Indexes
            builder.HasIndex(t => t.OrderLineId).HasDatabaseName("UQ_OrderLine").IsUnique();
            builder.HasIndex(t => t.Id);
            
            builder.HasOne(x => x.Product)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class InvoiceLineTimeMap : IEntityTypeConfiguration<InvoiceLineTime>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineTime> builder)
        {
            builder.ToTable(nameof(InvoiceLineTime));

            // Indexes
            builder.HasIndex(t => t.OrderLineId).HasDatabaseName("UQ_OrderLine").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ProductTimeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class InvoiceLineTimeFixedMap : IEntityTypeConfiguration<InvoiceLineTimeFixed>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineTimeFixed> builder)
        {
            builder.ToTable(nameof(InvoiceLineTimeFixed));

            // Indexes
            builder.HasIndex(t => t.OrderLineId).HasDatabaseName("UQ_OrderLine").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class InvoiceLineSessionMap : IEntityTypeConfiguration<InvoiceLineSession>
    {
        /// <summary>
        /// Configure entity
        /// </summary>
        public void Configure(EntityTypeBuilder<InvoiceLineSession> builder)
        {
            builder.ToTable(nameof(InvoiceLineSession));

            // Indexes
            builder.HasIndex(t => t.UsageSessionId).HasDatabaseName("UQ_UsageSession").IsUnique().HasFilter(null);
            builder.HasIndex(t => t.Id);

            builder.HasOne(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UsageSession)
                .WithMany(x=>x.InvoiceLines)
                .HasForeignKey(x => x.UsageSessionId);
        }
    }
}
