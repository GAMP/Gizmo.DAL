namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Pin = c.String(nullable: false, maxLength: 6),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Duration = c.Int(nullable: false),
                        ContactPhone = c.String(maxLength: 20),
                        ContactEmail = c.String(maxLength: 254),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Pin, unique: true, name: "UQ_Pin")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.ReservationHost",
                c => new
                    {
                        ReservationHostId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        HostId = c.Int(nullable: false),
                        PreferedUserId = c.Int(),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ReservationHostId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Host", t => t.HostId)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.PreferedUserId)
                .ForeignKey("dbo.Reservation", t => t.ReservationId, cascadeDelete: true)
                .Index(t => new { t.ReservationId, t.HostId }, unique: true, name: "UQ_Reservation_Host")
                .Index(t => t.PreferedUserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.ReservationUser",
                c => new
                    {
                        ReservationUserId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ReservationUserId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.Reservation", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => new { t.ReservationId, t.UserId }, unique: true, name: "UQ_Reservation_User")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        TokenId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Value = c.String(nullable: false, maxLength: 32),
                        ConfirmationCode = c.String(maxLength: 6),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Expires = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TokenId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Value, unique: true, name: "UQ_Value")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Verification",
                c => new
                    {
                        VerificationId = c.Int(nullable: false, identity: true),
                        TokenId = c.Int(nullable: false),
                        UserId = c.Int(),
                        Status = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VerificationId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.Token", t => t.TokenId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.TokenId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.VerificationEmail",
                c => new
                    {
                        VerificationId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.VerificationId)
                .ForeignKey("dbo.Verification", t => t.VerificationId)
                .Index(t => t.VerificationId);
            
            CreateTable(
                "dbo.VerificationMobilePhone",
                c => new
                    {
                        VerificationId = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.VerificationId)
                .ForeignKey("dbo.Verification", t => t.VerificationId)
                .Index(t => t.VerificationId);
            
            AddColumn("dbo.ProductOrder", "PreferedPaymentMethodId", c => c.Int());
            AddColumn("dbo.ProductOrder", "IsDelivered", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductOrder", "DeliveredTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductOrder", "Source", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOrder", "UserNote", c => c.String(maxLength: 255));
            AddColumn("dbo.PaymentMethod", "IsClient", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "IsManager", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "IsPortal", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethod", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductOL", "IsDelivered", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductOL", "DeliveredQuantity", c => c.Decimal(nullable: false, precision: 19, scale: 4));
            AddColumn("dbo.ProductOL", "DeliveredTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.ProductOrder", "PreferedPaymentMethodId");
            AddForeignKey("dbo.ProductOrder", "PreferedPaymentMethodId", "dbo.PaymentMethod", "PaymentMethodId");

            Sql("UPDATE[dbo].[ProductOrder] SET IsDelivered = 1, DeliveredTime = CreatedTime;");
            Sql("UPDATE[dbo].[ProductOL] SET IsDelivered = 1, DeliveredTime = CreatedTime;");          
            Sql("UPDATE[dbo].[PaymentMethod] SET IsEnabled = 1, IsClient = 1, IsManager = 1;");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VerificationMobilePhone", "VerificationId", "dbo.Verification");
            DropForeignKey("dbo.VerificationEmail", "VerificationId", "dbo.Verification");
            DropForeignKey("dbo.Verification", "UserId", "dbo.User");
            DropForeignKey("dbo.Verification", "TokenId", "dbo.Token");
            DropForeignKey("dbo.Verification", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.Verification", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Token", "UserId", "dbo.User");
            DropForeignKey("dbo.Token", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.Token", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ReservationUser", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.ReservationUser", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.ReservationUser", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.ReservationUser", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Reservation", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.Reservation", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.ReservationHost", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.ReservationHost", "PreferedUserId", "dbo.UserMember");
            DropForeignKey("dbo.ReservationHost", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.ReservationHost", "HostId", "dbo.Host");
            DropForeignKey("dbo.ReservationHost", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Reservation", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ProductOrder", "PreferedPaymentMethodId", "dbo.PaymentMethod");
            DropIndex("dbo.VerificationMobilePhone", new[] { "VerificationId" });
            DropIndex("dbo.VerificationEmail", new[] { "VerificationId" });
            DropIndex("dbo.Verification", new[] { "CreatedById" });
            DropIndex("dbo.Verification", new[] { "ModifiedById" });
            DropIndex("dbo.Verification", new[] { "UserId" });
            DropIndex("dbo.Verification", new[] { "TokenId" });
            DropIndex("dbo.Token", new[] { "CreatedById" });
            DropIndex("dbo.Token", new[] { "ModifiedById" });
            DropIndex("dbo.Token", "UQ_Value");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropIndex("dbo.ReservationUser", new[] { "CreatedById" });
            DropIndex("dbo.ReservationUser", new[] { "ModifiedById" });
            DropIndex("dbo.ReservationUser", "UQ_Reservation_User");
            DropIndex("dbo.ReservationHost", new[] { "CreatedById" });
            DropIndex("dbo.ReservationHost", new[] { "ModifiedById" });
            DropIndex("dbo.ReservationHost", new[] { "PreferedUserId" });
            DropIndex("dbo.ReservationHost", "UQ_Reservation_Host");
            DropIndex("dbo.Reservation", new[] { "CreatedById" });
            DropIndex("dbo.Reservation", new[] { "ModifiedById" });
            DropIndex("dbo.Reservation", "UQ_Pin");
            DropIndex("dbo.Reservation", new[] { "UserId" });
            DropIndex("dbo.ProductOrder", new[] { "PreferedPaymentMethodId" });
            DropColumn("dbo.ProductOL", "DeliveredTime");
            DropColumn("dbo.ProductOL", "DeliveredQuantity");
            DropColumn("dbo.ProductOL", "IsDelivered");
            DropColumn("dbo.PaymentMethod", "IsDeleted");
            DropColumn("dbo.PaymentMethod", "IsPortal");
            DropColumn("dbo.PaymentMethod", "IsManager");
            DropColumn("dbo.PaymentMethod", "IsClient");
            DropColumn("dbo.ProductOrder", "UserNote");
            DropColumn("dbo.ProductOrder", "Source");
            DropColumn("dbo.ProductOrder", "DeliveredTime");
            DropColumn("dbo.ProductOrder", "IsDelivered");
            DropColumn("dbo.ProductOrder", "PreferedPaymentMethodId");
            DropTable("dbo.VerificationMobilePhone");
            DropTable("dbo.VerificationEmail");
            DropTable("dbo.Verification");
            DropTable("dbo.Token");
            DropTable("dbo.ReservationUser");
            DropTable("dbo.ReservationHost");
            DropTable("dbo.Reservation");
        }
    }
}
