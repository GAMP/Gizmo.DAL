namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        Options = c.Int(nullable: false),
                        Sevirity = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Text = c.String(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.PresetTimeSale",
                c => new
                    {
                        PresetTimeSaleId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PresetTimeSaleId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.PresetTimeSaleMoney",
                c => new
                    {
                        PresetTimeSaleMoneyId = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 19, scale: 4),
                        DisplayOrder = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PresetTimeSaleMoneyId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.UserNote",
                c => new
                    {
                        NoteId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserNoteOptions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Note", t => t.NoteId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.NoteId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.User", "Identification", c => c.String(maxLength: 255));
            AddColumn("dbo.InvoiceLine", "PointsTotal", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOL", "PointsTotal", c => c.Int(nullable: false));
            AddColumn("dbo.TaskNotification", "NotificationOptions", c => c.Int(nullable: false));

            //NORMAL INDEX CREATION
            //CreateIndex("dbo.User", "Identification", unique: true, name: "UQ_Identification");

            #region FILTERED INDEXES

            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_Identification] ON [dbo].[User](Identification) WHERE Identification IS NOT NULL");

            #endregion
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserNote", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.UserNote", "NoteId", "dbo.Note");
            DropForeignKey("dbo.Note", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Note", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.PresetTimeSaleMoney", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.PresetTimeSaleMoney", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.PresetTimeSale", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.PresetTimeSale", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.UserNote", new[] { "UserId" });
            DropIndex("dbo.UserNote", new[] { "NoteId" });
            DropIndex("dbo.PresetTimeSaleMoney", new[] { "CreatedById" });
            DropIndex("dbo.PresetTimeSaleMoney", new[] { "ModifiedById" });
            DropIndex("dbo.PresetTimeSale", new[] { "CreatedById" });
            DropIndex("dbo.PresetTimeSale", new[] { "ModifiedById" });
            DropIndex("dbo.Note", new[] { "CreatedById" });
            DropIndex("dbo.Note", new[] { "ModifiedById" });
            DropIndex("dbo.User", "UQ_Identification");
            DropColumn("dbo.TaskNotification", "NotificationOptions");
            DropColumn("dbo.ProductOL", "PointsTotal");
            DropColumn("dbo.InvoiceLine", "PointsTotal");
            DropColumn("dbo.User", "Identification");
            DropTable("dbo.UserNote");
            DropTable("dbo.PresetTimeSaleMoney");
            DropTable("dbo.PresetTimeSale");
            DropTable("dbo.Note");
        }
    }
}
