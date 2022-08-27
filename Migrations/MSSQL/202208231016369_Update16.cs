namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update16 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Setting", "UQ_Name");
            CreateTable(
                "dbo.PaymentIntent",
                c => new
                    {
                        PaymentIntentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                        State = c.Int(nullable: false),
                        TransactionId = c.String(maxLength: 255),
                        TransactionTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Provider = c.Guid(nullable: false),
                        Guid = c.Guid(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PaymentIntentId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.PaymentIntentDeposit",
                c => new
                    {
                        PaymentIntentId = c.Int(nullable: false),
                        DepositPaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentIntentId)
                .ForeignKey("dbo.PaymentIntent", t => t.PaymentIntentId)
                .ForeignKey("dbo.DepositPayment", t => t.DepositPaymentId)
                .Index(t => t.PaymentIntentId);
            
            CreateTable(
                "dbo.PaymentIntentOrder",
                c => new
                    {
                        PaymentIntentId = c.Int(nullable: false),
                        ProductOrderId = c.Int(),
                        InvoicePaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentIntentId)
                .ForeignKey("dbo.PaymentIntent", t => t.PaymentIntentId)
                .ForeignKey("dbo.ProductOrder", t => t.ProductOrderId)
                .ForeignKey("dbo.InvoicePayment", t => t.InvoicePaymentId)
                .Index(t => t.PaymentIntentId)
                .Index(t => t.ProductOrderId);
            
            AddColumn("dbo.PaymentMethod", "PaymentProvider", c => c.Guid());
            CreateIndex("dbo.Setting", new[] { "Name", "GroupName" }, unique: true, name: "UQ_NameGroup");

            Sql(Gizmo.DAL.Scripts.SQLScripts.CreateUniqueNullableIndex("UQ_InvoicePayment", "PaymentIntentOrder", "InvoicePaymentId"));
            Sql(Gizmo.DAL.Scripts.SQLScripts.CreateUniqueNullableIndex("UQ_DepositPayment", "PaymentIntentDeposit", "DepositPaymentId"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentIntentOrder", "InvoicePaymentId", "dbo.InvoicePayment");
            DropForeignKey("dbo.PaymentIntentOrder", "ProductOrderId", "dbo.ProductOrder");
            DropForeignKey("dbo.PaymentIntentOrder", "PaymentIntentId", "dbo.PaymentIntent");
            DropForeignKey("dbo.PaymentIntentDeposit", "DepositPaymentId", "dbo.DepositPayment");
            DropForeignKey("dbo.PaymentIntentDeposit", "PaymentIntentId", "dbo.PaymentIntent");
            DropForeignKey("dbo.PaymentIntent", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.PaymentIntent", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.PaymentIntent", "CreatedById", "dbo.User");
            DropIndex("dbo.PaymentIntentOrder", "UQ_InvoicePayment");
            DropIndex("dbo.PaymentIntentOrder", new[] { "ProductOrderId" });
            DropIndex("dbo.PaymentIntentOrder", new[] { "PaymentIntentId" });
            DropIndex("dbo.PaymentIntentDeposit", "UQ_DepositPayment");
            DropIndex("dbo.PaymentIntentDeposit", new[] { "PaymentIntentId" });
            DropIndex("dbo.Setting", "UQ_NameGroup");
            DropIndex("dbo.PaymentIntent", new[] { "CreatedById" });
            DropIndex("dbo.PaymentIntent", new[] { "ModifiedById" });
            DropIndex("dbo.PaymentIntent", "UQ_Guid");
            DropIndex("dbo.PaymentIntent", new[] { "UserId" });
            DropColumn("dbo.PaymentMethod", "PaymentProvider");
            DropTable("dbo.PaymentIntentOrder");
            DropTable("dbo.PaymentIntentDeposit");
            DropTable("dbo.PaymentIntent");
            CreateIndex("dbo.Setting", "Name", unique: true, name: "UQ_Name");
        }
    }
}
