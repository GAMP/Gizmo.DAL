namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductHostHidden",
                c => new
                    {
                        ProductHostHiddenId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        HostGroupId = c.Int(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                        ModifiedById = c.Int(),
                        ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.Int(),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ProductHostHiddenId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostGroup", t => t.HostGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.HostGroupId }, unique: true, name: "UQ_ProductHostGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductHostHidden", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductHostHidden", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductHostHidden", "HostGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.ProductHostHidden", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.ProductHostHidden", new[] { "CreatedById" });
            DropIndex("dbo.ProductHostHidden", new[] { "ModifiedById" });
            DropIndex("dbo.ProductHostHidden", "UQ_ProductHostGroup");
            DropTable("dbo.ProductHostHidden");
        }
    }
}
