namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetTransaction",
                c => new
                    {
                        AssetTransactionId = c.Int(nullable: false, identity: true),
                        AssetTypeId = c.Int(nullable: false),
                        AssetTypeName = c.String(nullable: false, maxLength: 45),
                        AssetId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CheckedOutById = c.Int(),
                        CheckOutTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserId = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AssetTransactionId)
                .ForeignKey("dbo.Asset", t => t.AssetId, cascadeDelete: true)
                .ForeignKey("dbo.AssetType", t => t.AssetTypeId)
                .ForeignKey("dbo.UserOperator", t => t.CheckedOutById)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.AssetTypeId)
                .Index(t => t.AssetId)
                .Index(t => t.CheckedOutById)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        AssetId = c.Int(nullable: false, identity: true),
                        AssetTypeId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Tag = c.String(maxLength: 255),
                        SmartCardUID = c.String(maxLength: 255),
                        Barcode = c.String(maxLength: 255),
                        SerialNumber = c.String(maxLength: 255),
                        IsEnabled = c.Boolean(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AssetId)
                .ForeignKey("dbo.AssetType", t => t.AssetTypeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AssetTypeId)
                .Index(t => t.SmartCardUID, unique: true, name: "UQ_SmartCardUID")
                .Index(t => t.Barcode, unique: true, name: "UQ_Barcode")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.AssetType",
                c => new
                    {
                        AssetTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Description = c.String(),
                        PartNumber = c.String(maxLength: 255),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AssetTypeId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            AddColumn("dbo.UserMember", "IsPersonalInfoRequested", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserGroup", "IsAgeRatingEnabled", c => c.Boolean(nullable: false));

            //DROP EXISTING INDEX
            DropIndex("[dbo].[Asset]", "UQ_SmartCardUID");

            //FILTERED INDEX CREATION
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_SmartCardUID] ON [dbo].[Asset](SmartCardUID) WHERE SmartCardUID IS NOT NULL");

            //DROP EXISTING INDEX
            DropIndex("[dbo].[Asset]", "UQ_Barcode");

            //FILTERED INDEX CREATION
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_Barcode] ON [dbo].[Asset](Barcode) WHERE Barcode IS NOT NULL");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetTransaction", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.AssetTransaction", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AssetTransaction", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AssetTransaction", "CheckedOutById", "dbo.UserOperator");
            DropForeignKey("dbo.AssetTransaction", "AssetTypeId", "dbo.AssetType");
            DropForeignKey("dbo.AssetTransaction", "AssetId", "dbo.Asset");
            DropForeignKey("dbo.Asset", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Asset", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Asset", "AssetTypeId", "dbo.AssetType");
            DropForeignKey("dbo.AssetType", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AssetType", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.AssetType", new[] { "CreatedById" });
            DropIndex("dbo.AssetType", new[] { "ModifiedById" });
            DropIndex("dbo.AssetType", "UQ_Name");
            DropIndex("dbo.Asset", new[] { "CreatedById" });
            DropIndex("dbo.Asset", new[] { "ModifiedById" });
            DropIndex("dbo.Asset", "UQ_Barcode");
            DropIndex("dbo.Asset", "UQ_SmartCardUID");
            DropIndex("dbo.Asset", new[] { "AssetTypeId" });
            DropIndex("dbo.AssetTransaction", new[] { "CreatedById" });
            DropIndex("dbo.AssetTransaction", new[] { "ModifiedById" });
            DropIndex("dbo.AssetTransaction", new[] { "UserId" });
            DropIndex("dbo.AssetTransaction", new[] { "CheckedOutById" });
            DropIndex("dbo.AssetTransaction", new[] { "AssetId" });
            DropIndex("dbo.AssetTransaction", new[] { "AssetTypeId" });
            DropColumn("dbo.UserGroup", "IsAgeRatingEnabled");
            DropColumn("dbo.UserMember", "IsPersonalInfoRequested");
            DropTable("dbo.AssetType");
            DropTable("dbo.Asset");
            DropTable("dbo.AssetTransaction");
        }
    }
}
