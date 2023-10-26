namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Refund", "PaymentId", "dbo.Payment");
            DropIndex("dbo.Refund", new[] { "PaymentId" });
            CreateTable(
                "dbo.InvoiceFiscalReceipt",
                c => new
                {
                    InvoiceId = c.Int(nullable: false),
                    FiscalReceiptId = c.Int(nullable: false),
                    InvoiceFiscalReceiptId = c.Int(nullable: false, identity: true),
                    ShiftId = c.Int(),
                    RegisterId = c.Int(),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.InvoiceFiscalReceiptId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.FiscalReceipt", t => t.FiscalReceiptId, cascadeDelete: true)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Register", t => t.RegisterId)
                .ForeignKey("dbo.Shift", t => t.ShiftId)
                .Index(t => t.InvoiceId)
                .Index(t => t.FiscalReceiptId, unique: true, name: "UQ_FiscalReceipt")
                .Index(t => t.ShiftId)
                .Index(t => t.RegisterId)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.FiscalReceipt",
                c => new
                {
                    FiscalReceiptId = c.Int(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    TaxSystem = c.Int(),
                    DocumentNumber = c.Int(),
                    Signature = c.String(),
                    ShiftId = c.Int(),
                    RegisterId = c.Int(),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.FiscalReceiptId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Register", t => t.RegisterId)
                .ForeignKey("dbo.Shift", t => t.ShiftId)
                .Index(t => t.ShiftId)
                .Index(t => t.RegisterId)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.RefundDepositPayment",
                c => new
                {
                    RefundId = c.Int(nullable: false),
                    DepositPaymentId = c.Int(),
                    FiscalReceiptStatus = c.Int(nullable: false),
                    FiscalReceiptId = c.Int(),
                })
                .PrimaryKey(t => t.RefundId)
                .ForeignKey("dbo.Refund", t => t.RefundId)
                .ForeignKey("dbo.DepositPayment", t => t.DepositPaymentId)
                .ForeignKey("dbo.FiscalReceipt", t => t.FiscalReceiptId)
                .Index(t => t.RefundId)
                .Index(t => t.FiscalReceiptId);

            Sql(Gizmo.DAL.Scripts.SQLScripts.CreateUniqueNullableIndex("UQ_DepositPayment", "RefundDepositPayment", "DepositPaymentId"));

            CreateTable(
                "dbo.VoidDepositPayment",
                c => new
                {
                    VoidId = c.Int(nullable: false),
                    DepositPaymentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.VoidId)
                .ForeignKey("dbo.Void", t => t.VoidId)
                .ForeignKey("dbo.DepositPayment", t => t.DepositPaymentId, cascadeDelete: true)
                .Index(t => t.VoidId)
                .Index(t => t.DepositPaymentId, unique: true, name: "UQ_DepositPayment");

            AddColumn("dbo.Invoice", "SaleFiscalReceiptStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "ReturnFiscalReceiptStatus", c => c.Int(nullable: false));
            AddColumn("dbo.DepositPayment", "RefundedAmount", c => c.Decimal(nullable: false, precision: 19, scale: 4));
            AddColumn("dbo.DepositPayment", "RefundStatus", c => c.Int(nullable: false));
            AddColumn("dbo.DepositPayment", "FiscalReceiptStatus", c => c.Int(nullable: false));
            AddColumn("dbo.DepositPayment", "FiscalReceiptId", c => c.Int());
            AddColumn("dbo.DepositPayment", "IsVoided", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Refund", "PaymentId", c => c.Int());
            CreateIndex("dbo.DepositPayment", "FiscalReceiptId");
            CreateIndex("dbo.Refund", "PaymentId");
            AddForeignKey("dbo.DepositPayment", "FiscalReceiptId", "dbo.FiscalReceipt", "FiscalReceiptId");
            AddForeignKey("dbo.Refund", "PaymentId", "dbo.Payment", "PaymentId");

            Sql(Gizmo.DAL.Scripts.SQLScripts.CREATE_DEPOSIT_PAYMENT_REFUNDS);
        }

        public override void Down()
        {
            Sql("DELETE FROM [dbo].[RefundDepositPayment];"); //this table did not exist before so all records should be deleted
            Sql("DELETE FROM [dbo].[Refund] WHERE PaymentId is NULL;");//this table did not support null values, all records with null values should be deleted

            DropForeignKey("dbo.Refund", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.VoidDepositPayment", "DepositPaymentId", "dbo.DepositPayment");
            DropForeignKey("dbo.VoidDepositPayment", "VoidId", "dbo.Void");
            DropForeignKey("dbo.RefundDepositPayment", "FiscalReceiptId", "dbo.FiscalReceipt");
            DropForeignKey("dbo.RefundDepositPayment", "DepositPaymentId", "dbo.DepositPayment");
            DropForeignKey("dbo.RefundDepositPayment", "RefundId", "dbo.Refund");
            DropForeignKey("dbo.InvoiceFiscalReceipt", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.InvoiceFiscalReceipt", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.InvoiceFiscalReceipt", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.InvoiceFiscalReceipt", "FiscalReceiptId", "dbo.FiscalReceipt");
            DropForeignKey("dbo.FiscalReceipt", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.FiscalReceipt", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.DepositPayment", "FiscalReceiptId", "dbo.FiscalReceipt");
            DropForeignKey("dbo.FiscalReceipt", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoiceFiscalReceipt", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.VoidDepositPayment", "UQ_DepositPayment");
            DropIndex("dbo.VoidDepositPayment", new[] { "VoidId" });
            DropIndex("dbo.RefundDepositPayment", new[] { "FiscalReceiptId" });
            DropIndex("dbo.RefundDepositPayment", "UQ_DepositPayment");
            DropIndex("dbo.RefundDepositPayment", new[] { "RefundId" });
            DropIndex("dbo.Refund", new[] { "PaymentId" });
            DropIndex("dbo.DepositPayment", new[] { "FiscalReceiptId" });
            DropIndex("dbo.FiscalReceipt", new[] { "CreatedById" });
            DropIndex("dbo.FiscalReceipt", new[] { "RegisterId" });
            DropIndex("dbo.FiscalReceipt", new[] { "ShiftId" });
            DropIndex("dbo.InvoiceFiscalReceipt", new[] { "CreatedById" });
            DropIndex("dbo.InvoiceFiscalReceipt", new[] { "RegisterId" });
            DropIndex("dbo.InvoiceFiscalReceipt", new[] { "ShiftId" });
            DropIndex("dbo.InvoiceFiscalReceipt", "UQ_FiscalReceipt");
            DropIndex("dbo.InvoiceFiscalReceipt", new[] { "InvoiceId" });
            AlterColumn("dbo.Refund", "PaymentId", c => c.Int(nullable: false));
            DropColumn("dbo.DepositPayment", "IsVoided");
            DropColumn("dbo.DepositPayment", "FiscalReceiptId");
            DropColumn("dbo.DepositPayment", "FiscalReceiptStatus");
            DropColumn("dbo.DepositPayment", "RefundStatus");
            DropColumn("dbo.DepositPayment", "RefundedAmount");
            DropColumn("dbo.Invoice", "ReturnFiscalReceiptStatus");
            DropColumn("dbo.Invoice", "SaleFiscalReceiptStatus");
            DropTable("dbo.VoidDepositPayment");
            DropTable("dbo.RefundDepositPayment");
            DropTable("dbo.FiscalReceipt");
            DropTable("dbo.InvoiceFiscalReceipt");
            CreateIndex("dbo.Refund", "PaymentId");
            AddForeignKey("dbo.Refund", "PaymentId", "dbo.Payment", "PaymentId", cascadeDelete: true);
        }
    }
}
