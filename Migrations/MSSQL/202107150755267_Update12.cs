namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceHost",
                c => new
                    {
                        DeviceHostId = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        HostId = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DeviceHostId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Host", t => t.HostId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => new { t.DeviceId, t.HostId }, unique: true, name: "UQ_HostDevice")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                        IsEnabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DeviceId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                //.Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.DeviceHdmi",
                c => new
                    {
                        DeviceId = c.Int(nullable: false),
                        UniqueId = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.DeviceId)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .Index(t => t.DeviceId)
                .Index(t => t.UniqueId, unique: true, name: "UQ_UniqueId");
            
            AddColumn("dbo.Host", "Guid", c => c.Guid(nullable: false));

            //update existing host and add newly generated guid
            Sql("UPDATE [dbo].[Host] SET Guid = NEWID();");

            CreateIndex("dbo.Host", "Guid", unique: true, name: "UQ_Guid");

            #region FILTERED INDEXES

            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_Name] ON [dbo].[Device](Name) WHERE Name IS NOT NULL");

            #endregion
        }

        public override void Down()
        {
            DropForeignKey("dbo.DeviceHdmi", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.DeviceHost", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.DeviceHost", "HostId", "dbo.Host");
            DropForeignKey("dbo.DeviceHost", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.Device", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Device", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.DeviceHost", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.DeviceHdmi", "UQ_UniqueId");
            DropIndex("dbo.DeviceHdmi", new[] { "DeviceId" });
            DropIndex("dbo.Device", new[] { "CreatedById" });
            DropIndex("dbo.Device", new[] { "ModifiedById" });
            DropIndex("dbo.Device", "UQ_Name");
            DropIndex("dbo.DeviceHost", new[] { "CreatedById" });
            DropIndex("dbo.DeviceHost", new[] { "ModifiedById" });
            DropIndex("dbo.DeviceHost", "UQ_HostDevice");
            DropIndex("dbo.Host", "UQ_Guid");
            DropColumn("dbo.Host", "Guid");
            DropTable("dbo.DeviceHdmi");
            DropTable("dbo.Device");
            DropTable("dbo.DeviceHost");
        }
    }
}
