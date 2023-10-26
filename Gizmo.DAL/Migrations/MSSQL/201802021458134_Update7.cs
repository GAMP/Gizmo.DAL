namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBundleUserPrice",
                c => new
                {
                    ProductBundleUserPriceId = c.Int(nullable: false, identity: true),
                    ProductBundleId = c.Int(nullable: false),
                    UserGroupId = c.Int(nullable: false),
                    Price = c.Decimal(precision: 19, scale: 4),
                    PointsPrice = c.Int(),
                    PurchaseOptions = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductBundleUserPriceId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBundle", t => t.ProductBundleId)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId)
                .Index(t => new { t.ProductBundleId, t.UserGroupId }, unique: true, name: "UQ_ProductBundlePriceUserGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Void",
                c => new
                {
                    VoidId = c.Int(nullable: false, identity: true),
                    ShiftId = c.Int(),
                    RegisterId = c.Int(),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.VoidId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Register", t => t.RegisterId)
                .ForeignKey("dbo.Shift", t => t.ShiftId)
                .Index(t => t.ShiftId)
                .Index(t => t.RegisterId)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostGroupUserBillProfile",
                c => new
                {
                    HostGroupUserBillProfileId = c.Int(nullable: false, identity: true),
                    HostGroupId = c.Int(nullable: false),
                    UserGroupId = c.Int(nullable: false),
                    BillProfileId = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.HostGroupUserBillProfileId)
                .ForeignKey("dbo.BillProfile", t => t.BillProfileId)
                .ForeignKey("dbo.HostGroup", t => t.HostGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId)
                .Index(t => new { t.HostGroupId, t.UserGroupId }, unique: true, name: "UQ_HostGroupUserBillProfile")
                .Index(t => t.BillProfileId);

            CreateTable(
                "dbo.Refund",
                c => new
                {
                    RefundId = c.Int(nullable: false, identity: true),
                    PaymentId = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    DepositTransactionId = c.Int(),
                    RefundMethodId = c.Int(nullable: false),
                    PointTransactionId = c.Int(),
                    ShiftId = c.Int(),
                    RegisterId = c.Int(),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.RefundId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.DepositTransaction", t => t.DepositTransactionId)
                .ForeignKey("dbo.Payment", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.PointTransaction", t => t.PointTransactionId)
                .ForeignKey("dbo.PaymentMethod", t => t.RefundMethodId)
                .ForeignKey("dbo.Register", t => t.RegisterId)
                .ForeignKey("dbo.Shift", t => t.ShiftId)
                .Index(t => t.PaymentId)
                .Index(t => t.DepositTransactionId)
                .Index(t => t.RefundMethodId)
                .Index(t => t.PointTransactionId)
                .Index(t => t.ShiftId)
                .Index(t => t.RegisterId)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.RefundInvoicePayment",
                c => new
                {
                    RefundId = c.Int(nullable: false),
                    InvoicePaymentId = c.Int(nullable: false),
                    InvoiceId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.RefundId)
                .ForeignKey("dbo.Refund", t => t.RefundId)
                .ForeignKey("dbo.InvoicePayment", t => t.InvoicePaymentId)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId)
                .Index(t => t.RefundId)
                .Index(t => t.InvoicePaymentId, unique: true, name: "UQ_InvoicePayment")
                .Index(t => t.InvoiceId);

            CreateTable(
                "dbo.VoidInvoice",
                c => new
                {
                    VoidId = c.Int(nullable: false),
                    InvoiceId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.VoidId)
                .ForeignKey("dbo.Void", t => t.VoidId)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.VoidId)
                .Index(t => t.InvoiceId, unique: true, name: "UQ_Invoice");

            //DropIndex("dbo.UserGroupHostDisallowed", "UQ_UserGroupHostGroup");
            //DropColumn("dbo.UserGroupHostDisallowed", "UserGroupId");
            //RenameColumn(table: "dbo.UserGroupHostDisallowed", name: "HostGroupId", newName: "UserGroupId");

            DropIndex("dbo.UserGroupHostDisallowed", "UQ_UserGroupHostGroup");
            DropForeignKey("dbo.UserGroupHostDisallowed", "FK_dbo.UserGroupHostDisallowed_dbo.HostGroup_HostGroupId");
            DropForeignKey("dbo.UserGroupHostDisallowed", "FK_dbo.UserGroupHostDisallowed_dbo.UserGroup_HostGroupId");

            AddForeignKey("dbo.UserGroupHostDisallowed", "UserGroupId", "dbo.UserGroup", "UserGroupId");
            AddForeignKey("dbo.UserGroupHostDisallowed", "HostGroupId", "dbo.HostGroup", "HostGroupId");

            CreateIndex("dbo.UserGroupHostDisallowed", new[] { "UserGroupId", "HostGroupId" }, unique: true, name: "UQ_UserGroupHostGroup");

            AddColumn("dbo.App", "DefaultExecutableId", c => c.Int());
            AddColumn("dbo.UserMember", "BillingOptions", c => c.Int());
            //AddColumn("dbo.ProductTime", "ExpiresAfter", c => c.Int(nullable: false));
            AddColumn("dbo.ProductTime", "ExpireAfterType", c => c.Int(nullable: false));
            AddColumn("dbo.ProductTime", "ExpireAtDayTimeMinute", c => c.Int(nullable: false));
            AddColumn("dbo.StockTransaction", "ShiftId", c => c.Int());
            AddColumn("dbo.StockTransaction", "RegisterId", c => c.Int());
            AddColumn("dbo.UserGroup", "BillingOptions", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "IsVoided", c => c.Boolean(nullable: false));
            AddColumn("dbo.InvoiceLineExtended", "StockReturnTransactionId", c => c.Int());
            AddColumn("dbo.Payment", "RefundedAmount", c => c.Decimal(nullable: false, precision: 19, scale: 4));
            AddColumn("dbo.Payment", "RefundStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InvoicePayment", "RefundedAmount", c => c.Decimal(nullable: false, precision: 19, scale: 4));
            AddColumn("dbo.InvoicePayment", "RefundStatus", c => c.Int(nullable: false));
            AddColumn("dbo.AssetTransaction", "ShiftId", c => c.Int());
            AddColumn("dbo.AssetTransaction", "RegisterId", c => c.Int());
            //CreateIndex("dbo.UserGroupHostDisallowed", new[] { "UserGroupId", "HostGroupId" }, unique: true, name: "UQ_UserGroupHostGroup");
            //CreateIndex("dbo.InvoiceLineExtended", "StockReturnTransactionId", unique: true, name: "UQ_StockReturnTransaction");
            AddForeignKey("dbo.InvoiceLineExtended", "StockReturnTransactionId", "dbo.StockTransaction", "StockTransactionId");
            //DropColumn("dbo.ProductTime", "ExpiresAfterDays");
            DropColumn("dbo.Payment", "IsRefunded");

            RenameColumn("ProductTime", "ExpiresAfterDays", "ExpiresAfter");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_StockReturnTransaction] ON [dbo].[InvoiceLineExtended](StockReturnTransactionId) WHERE StockReturnTransactionId IS NOT NULL");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT UserId, 'Sale', '*', CURRENT_TIMESTAMP FROM UserOperator; ");
            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT UserId, 'Sale', 'VoidInvoices', CURRENT_TIMESTAMP FROM UserOperator; ");
            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT UserId, 'Sale', 'DeleteTimePurchases', CURRENT_TIMESTAMP FROM UserOperator; ");
        }

        public override void Down()
        {
            AddColumn("dbo.Payment", "IsRefunded", c => c.Boolean(nullable: false));
            //AddColumn("dbo.ProductTime", "ExpiresAfterDays", c => c.Int(nullable: false));
            DropForeignKey("dbo.VoidInvoice", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.VoidInvoice", "VoidId", "dbo.Void");
            DropForeignKey("dbo.RefundInvoicePayment", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.RefundInvoicePayment", "InvoicePaymentId", "dbo.InvoicePayment");
            DropForeignKey("dbo.RefundInvoicePayment", "RefundId", "dbo.Refund");
            DropForeignKey("dbo.InvoiceLineExtended", "StockReturnTransactionId", "dbo.StockTransaction");
            DropForeignKey("dbo.Refund", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.Refund", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Refund", "RefundMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.Refund", "PointTransactionId", "dbo.PointTransaction");
            DropForeignKey("dbo.Refund", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.Refund", "DepositTransactionId", "dbo.DepositTransaction");
            DropForeignKey("dbo.Refund", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Void", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.Void", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Void", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostGroupUserBillProfile", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.HostGroupUserBillProfile", "HostGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.HostGroupUserBillProfile", "BillProfileId", "dbo.BillProfile");
            DropForeignKey("dbo.ProductBundleUserPrice", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.ProductBundleUserPrice", "ProductBundleId", "dbo.ProductBundle");
            DropForeignKey("dbo.ProductBundleUserPrice", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductBundleUserPrice", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.VoidInvoice", "UQ_Invoice");
            DropIndex("dbo.VoidInvoice", new[] { "VoidId" });
            DropIndex("dbo.RefundInvoicePayment", new[] { "InvoiceId" });
            DropIndex("dbo.RefundInvoicePayment", "UQ_InvoicePayment");
            DropIndex("dbo.RefundInvoicePayment", new[] { "RefundId" });
            DropIndex("dbo.InvoiceLineExtended", "UQ_StockReturnTransaction");
            DropIndex("dbo.Refund", new[] { "CreatedById" });
            DropIndex("dbo.Refund", new[] { "RegisterId" });
            DropIndex("dbo.Refund", new[] { "ShiftId" });
            DropIndex("dbo.Refund", new[] { "PointTransactionId" });
            DropIndex("dbo.Refund", new[] { "RefundMethodId" });
            DropIndex("dbo.Refund", new[] { "DepositTransactionId" });
            DropIndex("dbo.Refund", new[] { "PaymentId" });
            DropIndex("dbo.HostGroupUserBillProfile", new[] { "BillProfileId" });
            DropIndex("dbo.HostGroupUserBillProfile", "UQ_HostGroupUserBillProfile");
            //DropIndex("dbo.UserGroupHostDisallowed", "UQ_UserGroupHostGroup");
            DropIndex("dbo.Void", new[] { "CreatedById" });
            DropIndex("dbo.Void", new[] { "RegisterId" });
            DropIndex("dbo.Void", new[] { "ShiftId" });
            DropIndex("dbo.ProductBundleUserPrice", new[] { "CreatedById" });
            DropIndex("dbo.ProductBundleUserPrice", new[] { "ModifiedById" });
            DropIndex("dbo.ProductBundleUserPrice", "UQ_ProductBundlePriceUserGroup");
            DropColumn("dbo.AssetTransaction", "RegisterId");
            DropColumn("dbo.AssetTransaction", "ShiftId");
            DropColumn("dbo.InvoicePayment", "RefundStatus");
            DropColumn("dbo.InvoicePayment", "RefundedAmount");
            DropColumn("dbo.Payment", "RefundStatus");
            DropColumn("dbo.Payment", "RefundedAmount");
            DropColumn("dbo.InvoiceLineExtended", "StockReturnTransactionId");
            DropColumn("dbo.Invoice", "IsVoided");
            DropColumn("dbo.UserGroup", "BillingOptions");
            DropColumn("dbo.StockTransaction", "RegisterId");
            DropColumn("dbo.StockTransaction", "ShiftId");
            DropColumn("dbo.ProductTime", "ExpireAtDayTimeMinute");
            DropColumn("dbo.ProductTime", "ExpireAfterType");
            //DropColumn("dbo.ProductTime", "ExpiresAfter");
            DropColumn("dbo.UserMember", "BillingOptions");
            DropColumn("dbo.App", "DefaultExecutableId");
            DropTable("dbo.VoidInvoice");
            DropTable("dbo.RefundInvoicePayment");
            DropTable("dbo.Refund");
            DropTable("dbo.HostGroupUserBillProfile");
            DropTable("dbo.Void");
            DropTable("dbo.ProductBundleUserPrice");

            RenameColumn("ProductTime", "ExpiresAfter", "ExpiresAfterDays");

            //RenameColumn(table: "dbo.UserGroupHostDisallowed", name: "UserGroupId", newName: "HostGroupId");
            //AddColumn("dbo.UserGroupHostDisallowed", "UserGroupId", c => c.Int(nullable: false));
            //CreateIndex("dbo.UserGroupHostDisallowed", new[] { "UserGroupId", "HostGroupId" }, unique: true, name: "UQ_UserGroupHostGroup");

            DropIndex("dbo.UserGroupHostDisallowed", "UQ_UserGroupHostGroup");

            DropForeignKey("dbo.UserGroupHostDisallowed", "FK_dbo.UserGroupHostDisallowed_dbo.HostGroup_HostGroupId");
            DropForeignKey("dbo.UserGroupHostDisallowed", "FK_dbo.UserGroupHostDisallowed_dbo.UserGroup_UserGroupId");

            AddForeignKey("dbo.UserGroupHostDisallowed", "HostGroupId", "dbo.UserGroup", "UserGroupId");
            AddForeignKey("dbo.UserGroupHostDisallowed", "HostGroupId", "dbo.HostGroup", "HostGroupId");

            CreateIndex("dbo.UserGroupHostDisallowed", new[] { "UserGroupId", "HostGroupId" }, unique: true, name: "UQ_UserGroupHostGroup");
        }
    }
}
