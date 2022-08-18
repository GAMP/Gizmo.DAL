namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepositIntent",
                c => new
                    {
                        DepositIntentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                        State = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DepositIntentId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepositIntent", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.DepositIntent", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.DepositIntent", "CreatedById", "dbo.User");
            DropIndex("dbo.DepositIntent", new[] { "CreatedById" });
            DropIndex("dbo.DepositIntent", new[] { "ModifiedById" });
            DropIndex("dbo.DepositIntent", new[] { "UserId" });
            DropTable("dbo.DepositIntent");
        }
    }
}
