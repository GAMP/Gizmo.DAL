namespace GizmoDALV2.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Register",
                c => new
                {
                    RegisterId = c.Int(nullable: false, identity: true),
                    Number = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 45),
                    MacAddress = c.String(maxLength: 255),
                    StartCash = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IdleTimeout = c.Int(),
                    Options = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.RegisterId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.MacAddress, unique: true, name: "UQ_MACAddress")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Shift",
                c => new
                {
                    ShiftId = c.Int(nullable: false, identity: true),
                    IsActive = c.Boolean(nullable: false),
                    OperatorId = c.Int(nullable: false),
                    RegisterId = c.Int(nullable: false),
                    Start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    StartCash = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsEnding = c.Boolean(nullable: false),
                    EndedById = c.Int(),
                    EndTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ShiftId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.EndedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserOperator", t => t.OperatorId)
                .ForeignKey("dbo.Register", t => t.RegisterId, cascadeDelete: true)
                .Index(t => t.OperatorId)
                .Index(t => t.RegisterId)
                .Index(t => t.EndedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.RegisterTransaction",
                c => new
                {
                    RegisterTransactionId = c.Int(nullable: false, identity: true),
                    RegisterId = c.Int(nullable: false),
                    ShiftId = c.Int(),
                    Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Type = c.Int(nullable: false),
                    Note = c.String(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.RegisterTransactionId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.Shift", t => t.ShiftId)
                .ForeignKey("dbo.Register", t => t.RegisterId, cascadeDelete: true)
                .Index(t => t.RegisterId)
                .Index(t => t.ShiftId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ShiftCount",
                c => new
                {
                    ShiftCountId = c.Int(nullable: false, identity: true),
                    ShiftId = c.Int(nullable: false),
                    PaymentMethodId = c.Int(nullable: false),
                    StartCash = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Sales = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Deposits = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PayIns = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Withdrawals = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PayOuts = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Refunds = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Voids = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Expected = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Actual = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Difference = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Note = c.String(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ShiftCountId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId)
                .ForeignKey("dbo.Shift", t => t.ShiftId, cascadeDelete: true)
                .Index(t => new { t.ShiftId, t.PaymentMethodId }, unique: true, name: "UQ_ShiftCountPaymentMethod")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            AddColumn("dbo.UserOperator", "ShiftOptions", c => c.Int(nullable: false));
            AddColumn("dbo.ProductUserPrice", "IsEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.InvoiceLine", "UnitPointsPrice", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceLine", "UnitPointsListPrice", c => c.Int());
            AddColumn("dbo.InvoiceLine", "UnitCost", c => c.Decimal(precision: 19, scale: 4));
            AddColumn("dbo.InvoiceLine", "Cost", c => c.Decimal(precision: 19, scale: 4));
            AddColumn("dbo.InvoiceLine", "PayType", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceLine", "ShiftId", c => c.Int());
            AddColumn("dbo.InvoiceLine", "RegisterId", c => c.Int());
            AddColumn("dbo.Invoice", "ShiftId", c => c.Int());
            AddColumn("dbo.Invoice", "RegisterId", c => c.Int());
            AddColumn("dbo.PointTransaction", "ShiftId", c => c.Int());
            AddColumn("dbo.PointTransaction", "RegisterId", c => c.Int());
            AddColumn("dbo.ProductOL", "UnitPointsListPrice", c => c.Int());
            AddColumn("dbo.ProductOL", "Cost", c => c.Decimal(precision: 19, scale: 4));
            AddColumn("dbo.ProductOL", "PayType", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOL", "ShiftId", c => c.Int());
            AddColumn("dbo.ProductOL", "RegisterId", c => c.Int());
            AddColumn("dbo.ProductOrder", "ShiftId", c => c.Int());
            AddColumn("dbo.ProductOrder", "RegisterId", c => c.Int());
            AddColumn("dbo.InvoicePayment", "ShiftId", c => c.Int());
            AddColumn("dbo.InvoicePayment", "RegisterId", c => c.Int());
            AddColumn("dbo.Payment", "ShiftId", c => c.Int());
            AddColumn("dbo.Payment", "RegisterId", c => c.Int());
            AddColumn("dbo.DepositTransaction", "ShiftId", c => c.Int());
            AddColumn("dbo.DepositTransaction", "RegisterId", c => c.Int());
            AddColumn("dbo.DepositPayment", "ShiftId", c => c.Int());
            AddColumn("dbo.DepositPayment", "RegisterId", c => c.Int());

            Sql("UPDATE [dbo].[ProductOL] SET UnitPointsPrice=0 Where UnitPointsPrice IS NULL");
            AlterColumn("dbo.ProductOL", "UnitPointsPrice", c => c.Int(nullable: false));

            CreateIndex("dbo.ProductOrder", "ShiftId");
            CreateIndex("dbo.ProductOrder", "RegisterId");
            CreateIndex("dbo.Invoice", "ShiftId");
            CreateIndex("dbo.Invoice", "RegisterId");
            CreateIndex("dbo.InvoiceLine", "ShiftId");
            CreateIndex("dbo.InvoiceLine", "RegisterId");
            CreateIndex("dbo.PointTransaction", "ShiftId");
            CreateIndex("dbo.PointTransaction", "RegisterId");
            CreateIndex("dbo.DepositPayment", "ShiftId");
            CreateIndex("dbo.DepositPayment", "RegisterId");
            CreateIndex("dbo.DepositTransaction", "ShiftId");
            CreateIndex("dbo.DepositTransaction", "RegisterId");
            CreateIndex("dbo.Payment", "ShiftId");
            CreateIndex("dbo.Payment", "RegisterId");
            CreateIndex("dbo.InvoicePayment", "ShiftId");
            CreateIndex("dbo.InvoicePayment", "RegisterId");
            CreateIndex("dbo.ProductOL", "ShiftId");
            CreateIndex("dbo.ProductOL", "RegisterId");
            AddForeignKey("dbo.DepositTransaction", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.DepositTransaction", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.Payment", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.Payment", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.DepositPayment", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.DepositPayment", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.InvoicePayment", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.InvoicePayment", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.PointTransaction", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.PointTransaction", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.InvoiceLine", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.InvoiceLine", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.Invoice", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.Invoice", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.ProductOL", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.ProductOL", "ShiftId", "dbo.Shift", "ShiftId");
            AddForeignKey("dbo.ProductOrder", "RegisterId", "dbo.Register", "RegisterId");
            AddForeignKey("dbo.ProductOrder", "ShiftId", "dbo.Shift", "ShiftId");

            //DROP EXISTING INDEX
            DropIndex("[dbo].[Register]", "UQ_MACAddress");

            //FILTERED INDEX CREATION
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_MACAddress] ON [dbo].[Register](MacAddress) WHERE MacAddress IS NOT NULL");
        }

        public override void Down()
        {
            DropForeignKey("dbo.ProductOrder", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.ProductOrder", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.ProductOL", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.ProductOL", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Invoice", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.Invoice", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.InvoiceLine", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.InvoiceLine", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.PointTransaction", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.PointTransaction", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.RegisterTransaction", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Shift", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.ShiftCount", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.ShiftCount", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.ShiftCount", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ShiftCount", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.RegisterTransaction", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.RegisterTransaction", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.RegisterTransaction", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Shift", "OperatorId", "dbo.UserOperator");
            DropForeignKey("dbo.Shift", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoicePayment", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.InvoicePayment", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Shift", "EndedById", "dbo.UserOperator");
            DropForeignKey("dbo.DepositPayment", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.DepositPayment", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Payment", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.Payment", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.DepositTransaction", "ShiftId", "dbo.Shift");
            DropForeignKey("dbo.DepositTransaction", "RegisterId", "dbo.Register");
            DropForeignKey("dbo.Shift", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Register", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Register", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.ProductOL", new[] { "RegisterId" });
            DropIndex("dbo.ProductOL", new[] { "ShiftId" });
            DropIndex("dbo.ShiftCount", new[] { "CreatedById" });
            DropIndex("dbo.ShiftCount", new[] { "ModifiedById" });
            DropIndex("dbo.ShiftCount", "UQ_ShiftCountPaymentMethod");
            DropIndex("dbo.RegisterTransaction", new[] { "CreatedById" });
            DropIndex("dbo.RegisterTransaction", new[] { "ModifiedById" });
            DropIndex("dbo.RegisterTransaction", new[] { "ShiftId" });
            DropIndex("dbo.RegisterTransaction", new[] { "RegisterId" });
            DropIndex("dbo.InvoicePayment", new[] { "RegisterId" });
            DropIndex("dbo.InvoicePayment", new[] { "ShiftId" });
            DropIndex("dbo.Payment", new[] { "RegisterId" });
            DropIndex("dbo.Payment", new[] { "ShiftId" });
            DropIndex("dbo.DepositTransaction", new[] { "RegisterId" });
            DropIndex("dbo.DepositTransaction", new[] { "ShiftId" });
            DropIndex("dbo.DepositPayment", new[] { "RegisterId" });
            DropIndex("dbo.DepositPayment", new[] { "ShiftId" });
            DropIndex("dbo.Shift", new[] { "CreatedById" });
            DropIndex("dbo.Shift", new[] { "ModifiedById" });
            DropIndex("dbo.Shift", new[] { "EndedById" });
            DropIndex("dbo.Shift", new[] { "RegisterId" });
            DropIndex("dbo.Shift", new[] { "OperatorId" });
            DropIndex("dbo.Register", new[] { "CreatedById" });
            DropIndex("dbo.Register", new[] { "ModifiedById" });
            DropIndex("dbo.Register", "UQ_MACAddress");
            DropIndex("dbo.PointTransaction", new[] { "RegisterId" });
            DropIndex("dbo.PointTransaction", new[] { "ShiftId" });
            DropIndex("dbo.InvoiceLine", new[] { "RegisterId" });
            DropIndex("dbo.InvoiceLine", new[] { "ShiftId" });
            DropIndex("dbo.Invoice", new[] { "RegisterId" });
            DropIndex("dbo.Invoice", new[] { "ShiftId" });
            DropIndex("dbo.ProductOrder", new[] { "RegisterId" });
            DropIndex("dbo.ProductOrder", new[] { "ShiftId" });
            AlterColumn("dbo.ProductOL", "UnitPointsPrice", c => c.Int());
            DropColumn("dbo.DepositPayment", "RegisterId");
            DropColumn("dbo.DepositPayment", "ShiftId");
            DropColumn("dbo.DepositTransaction", "RegisterId");
            DropColumn("dbo.DepositTransaction", "ShiftId");
            DropColumn("dbo.Payment", "RegisterId");
            DropColumn("dbo.Payment", "ShiftId");
            DropColumn("dbo.InvoicePayment", "RegisterId");
            DropColumn("dbo.InvoicePayment", "ShiftId");
            DropColumn("dbo.ProductOrder", "RegisterId");
            DropColumn("dbo.ProductOrder", "ShiftId");
            DropColumn("dbo.ProductOL", "RegisterId");
            DropColumn("dbo.ProductOL", "ShiftId");
            DropColumn("dbo.ProductOL", "PayType");
            DropColumn("dbo.ProductOL", "Cost");
            DropColumn("dbo.ProductOL", "UnitPointsListPrice");
            DropColumn("dbo.PointTransaction", "RegisterId");
            DropColumn("dbo.PointTransaction", "ShiftId");
            DropColumn("dbo.Invoice", "RegisterId");
            DropColumn("dbo.Invoice", "ShiftId");
            DropColumn("dbo.InvoiceLine", "RegisterId");
            DropColumn("dbo.InvoiceLine", "ShiftId");
            DropColumn("dbo.InvoiceLine", "PayType");
            DropColumn("dbo.InvoiceLine", "Cost");
            DropColumn("dbo.InvoiceLine", "UnitCost");
            DropColumn("dbo.InvoiceLine", "UnitPointsListPrice");
            DropColumn("dbo.InvoiceLine", "UnitPointsPrice");
            DropColumn("dbo.ProductUserPrice", "IsEnabled");
            DropColumn("dbo.UserOperator", "ShiftOptions");
            DropTable("dbo.ShiftCount");
            DropTable("dbo.RegisterTransaction");
            DropTable("dbo.Shift");
            DropTable("dbo.Register");
        }
    }
}
