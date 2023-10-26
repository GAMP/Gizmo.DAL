namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAgreementState",
                c => new
                {
                    UserAgreementStateId = c.Int(nullable: false, identity: true),
                    UserAgreementId = c.Int(nullable: false),
                    AcceptState = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserAgreementStateId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.UserAgreement", t => t.UserAgreementId, cascadeDelete: true)
                .Index(t => new { t.UserAgreementId, t.UserId }, unique: true, name: "UQ_UserAgreementState")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserAgreement",
                c => new
                {
                    UserAgreementId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Agreement = c.String(nullable: false),
                    Options = c.Int(nullable: false),
                    DisplayOptions = c.Int(nullable: false),
                    DisplayOrder = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserAgreementId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            AddColumn("dbo.UserSession", "GraceTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserSession", "GraceSpan", c => c.Double(nullable: false));
            AddColumn("dbo.UserSession", "GraceSpanTotal", c => c.Double(nullable: false));
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserAgreementState", "UserAgreementId", "dbo.UserAgreement");
            DropForeignKey("dbo.UserAgreement", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserAgreement", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserAgreementState", "UserId", "dbo.User");
            DropForeignKey("dbo.UserAgreementState", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.UserAgreementState", "CreatedById", "dbo.User");
            DropIndex("dbo.UserAgreement", new[] { "CreatedById" });
            DropIndex("dbo.UserAgreement", new[] { "ModifiedById" });
            DropIndex("dbo.UserAgreementState", new[] { "CreatedById" });
            DropIndex("dbo.UserAgreementState", new[] { "ModifiedById" });
            DropIndex("dbo.UserAgreementState", "UQ_UserAgreementState");
            DropColumn("dbo.UserSession", "GraceSpanTotal");
            DropColumn("dbo.UserSession", "GraceSpan");
            DropColumn("dbo.UserSession", "GraceTime");
            DropTable("dbo.UserAgreement");
            DropTable("dbo.UserAgreementState");
        }
    }
}
