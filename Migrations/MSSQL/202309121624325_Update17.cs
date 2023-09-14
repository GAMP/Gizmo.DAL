namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssistanceRequest",
                c => new
                    {
                        AssistanceRequestId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        HostId = c.Int(nullable: false),
                        AssistanceRequestTypeId = c.Int(nullable: false),
                        Note = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AssistanceRequestId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Host", t => t.HostId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HostId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.AssistanceRequestType",
                c => new
                    {
                        AssistanceRequestTypeId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 45),
                        DisplayOrder = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AssistanceRequestTypeId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            AddColumn("dbo.ProductOLProduct", "Mark", c => c.String(maxLength: 126, unicode: false));
            AddColumn("dbo.News", "BackgroundUrl", c => c.String(maxLength: 255));
            AddColumn("dbo.News", "Options", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssistanceRequestType", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AssistanceRequestType", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AssistanceRequest", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.AssistanceRequest", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.AssistanceRequest", "HostId", "dbo.Host");
            DropForeignKey("dbo.AssistanceRequest", "CreatedById", "dbo.User");
            DropIndex("dbo.AssistanceRequestType", new[] { "CreatedById" });
            DropIndex("dbo.AssistanceRequestType", new[] { "ModifiedById" });
            DropIndex("dbo.AssistanceRequest", new[] { "CreatedById" });
            DropIndex("dbo.AssistanceRequest", new[] { "ModifiedById" });
            DropIndex("dbo.AssistanceRequest", new[] { "HostId" });
            DropIndex("dbo.AssistanceRequest", new[] { "UserId" });
            DropColumn("dbo.News", "Options");
            DropColumn("dbo.News", "BackgroundUrl");
            DropColumn("dbo.ProductOLProduct", "Mark");
            DropTable("dbo.AssistanceRequestType");
            DropTable("dbo.AssistanceRequest");
        }
    }
}
