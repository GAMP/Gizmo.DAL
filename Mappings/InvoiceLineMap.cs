using GizmoDALV2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class InvoiceLineMap : EntityTypeConfiguration<InvoiceLine>
    {
        public InvoiceLineMap()
        {
            HasKey(x => x.Id);

            ToTable(nameof(InvoiceLine));

            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasColumnName("InvoiceLineId");

            Property(x => x.InvoiceId)
                .HasColumnOrder(1);

            Property(x => x.UserId)
                .HasColumnOrder(2);

            Property(x => x.ProductName)
                .HasColumnOrder(3);

            Property(x => x.Quantity)
                .HasColumnOrder(4);

            Property(x => x.UnitPrice)
                .HasColumnOrder(5);

            Property(x => x.UnitListPrice)
                .HasColumnOrder(6);

            Property(x => x.UnitPointsPrice)
                .HasColumnOrder(7);

            Property(x => x.UnitPointsListPrice)
                .HasColumnOrder(8);

            Property(x => x.UnitCost)
                .HasColumnOrder(9);

            Property(x => x.Cost)
                .HasColumnOrder(10);

            Property(x => x.TaxRate)
                .HasColumnOrder(11);

            Property(x => x.PreTaxTotal)
                .HasColumnOrder(12);

            Property(x => x.Total)
                .HasColumnOrder(13);

            Property(x => x.PointsTotal)
                .HasColumnOrder(14);

            Property(x => x.Points)
                .HasColumnOrder(15);

            Property(x => x.PointsAward)
                .HasColumnOrder(16);

            Property(x => x.TaxTotal)
                .HasColumnOrder(17);

            Property(x => x.PayType)
                .HasColumnOrder(18);

            Property(x => x.PointsTransactionId)
                .HasColumnOrder(19)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_PointsTransaction")
                {
                    IsUnique = true
                }}));

            Property(x => x.IsDeleted)
                .HasColumnOrder(20);

            Property(x => x.IsVoided)
                .HasColumnOrder(21);

            HasRequired(x => x.Invoice)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.InvoiceId);

            HasRequired(x => x.User)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.PointsTransaction)
                .WithMany()
                .HasForeignKey(x => x.PointsTransactionId)
                .WillCascadeOnDelete(false);
        }
    }

    public class InvoiceLineExtendedMap : EntityTypeConfiguration<InvoiceLineExtended>
    {
        public InvoiceLineExtendedMap()
        {
            ToTable(nameof(InvoiceLineExtended));

            Property(x => x.Id)
                .HasColumnOrder(0);

            Property(x => x.BundleLineId)
                .HasColumnOrder(1);

            Property(x => x.StockTransactionId)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_StockTransaction")
                {
                    IsUnique = true
                }}));

            Property(x => x.StockReturnTransactionId)
                .HasColumnOrder(3)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_StockReturnTransaction")
                {
                    IsUnique = true
                }}));

            HasOptional(x => x.BundleLine)
                .WithMany()
                .HasForeignKey(x => x.BundleLineId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.StockTransaction)
                .WithMany()
                .HasForeignKey(x => x.StockTransactionId)
                .WillCascadeOnDelete(false);
        }
    }

    public class InvoiceLineProductMap : EntityTypeConfiguration<InvoiceLineProduct>
    {
        public InvoiceLineProductMap()
        {
            ToTable(nameof(InvoiceLineProduct));

            Property(x => x.OrderLineId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_OrderLine")
                {
                    IsUnique = true
                }}));

            HasRequired(x => x.Product)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ProductId);

            HasRequired(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId);
        }
    }

    public class InvoiceLineTimeMap : EntityTypeConfiguration<InvoiceLineTime>
    {
        public InvoiceLineTimeMap()
        {
            ToTable(nameof(InvoiceLineTime));

            Property(x => x.OrderLineId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_OrderLine")
                {
                    IsUnique = true
                }}));

            HasRequired(x => x.Product)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ProductTimeId);

            HasRequired(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId);
        }
    }

    public class InvoiceLineTimeFixedMap : EntityTypeConfiguration<InvoiceLineTimeFixed>
    {
        public InvoiceLineTimeFixedMap()
        {
            ToTable(nameof(InvoiceLineTimeFixed));

            Property(x => x.OrderLineId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_OrderLine")
                {
                    IsUnique = true
                }}));

            HasRequired(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId);
        }
    }

    public class InvoiceLineSessionMap : EntityTypeConfiguration<InvoiceLineSession>
    {
        public InvoiceLineSessionMap()
        {
            ToTable(nameof(InvoiceLineSession));

            Property(x => x.UsageSessionId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_UsageSession")
                {
                    IsUnique = true
                }}));

            HasRequired(x => x.OrderLine)
                .WithMany()
                .HasForeignKey(x => x.OrderLineId);

            HasRequired(x => x.UsageSession)
                .WithMany()
                .HasForeignKey(x => x.UsageSessionId);
        }
    }
}
