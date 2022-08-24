namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentIntent",
                c => new
                    {
                        PaymentIntentId = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                        State = c.Int(nullable: false),
                        Provider = c.Guid(nullable: false),
                        TransactionId = c.String(maxLength: 255),
                        TransactionTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserId = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PaymentIntentId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.PaymentIntentDeposit",
                c => new
                    {
                        PaymentIntentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentIntentId)
                .ForeignKey("dbo.PaymentIntent", t => t.PaymentIntentId)
                .Index(t => t.PaymentIntentId);
            
            CreateTable(
                "dbo.PaymentIntentOrder",
                c => new
                    {
                        PaymentIntentId = c.Int(nullable: false),
                        ProductOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentIntentId)
                .ForeignKey("dbo.PaymentIntent", t => t.PaymentIntentId)
                .ForeignKey("dbo.ProductOrder", t => t.ProductOrderId, cascadeDelete: true)
                .Index(t => t.PaymentIntentId)
                .Index(t => t.ProductOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentIntentOrder", "ProductOrderId", "dbo.ProductOrder");
            DropForeignKey("dbo.PaymentIntentOrder", "PaymentIntentId", "dbo.PaymentIntent");
            DropForeignKey("dbo.PaymentIntentDeposit", "PaymentIntentId", "dbo.PaymentIntent");
            DropForeignKey("dbo.PaymentIntent", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.PaymentIntent", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.PaymentIntent", "CreatedById", "dbo.User");
            DropIndex("dbo.PaymentIntentOrder", new[] { "ProductOrderId" });
            DropIndex("dbo.PaymentIntentOrder", new[] { "PaymentIntentId" });
            DropIndex("dbo.PaymentIntentDeposit", new[] { "PaymentIntentId" });
            DropIndex("dbo.PaymentIntent", new[] { "CreatedById" });
            DropIndex("dbo.PaymentIntent", new[] { "ModifiedById" });
            DropIndex("dbo.PaymentIntent", new[] { "UserId" });
            DropIndex("dbo.PaymentIntent", "UQ_Guid");
            DropTable("dbo.PaymentIntentOrder");
            DropTable("dbo.PaymentIntentDeposit");
            DropTable("dbo.PaymentIntent");
        }
    }
}
