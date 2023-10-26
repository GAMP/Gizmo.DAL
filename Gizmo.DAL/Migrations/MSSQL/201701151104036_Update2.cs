namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGroup", "PointsAwardOptions", c => c.Int(nullable: false));
            AddColumn("dbo.UserGroup", "PointsMoneyRatio", c => c.Decimal(nullable: false, precision: 19, scale: 4));
            AddColumn("dbo.UserGroup", "PointsTimeRatio", c => c.Int(nullable: false));
            AddColumn("dbo.UserGroup", "Points", c => c.Int());
            AddColumn("dbo.InvoiceLine", "Points", c => c.Int());
            AddColumn("dbo.InvoiceLine", "PointsAward", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOL", "Points", c => c.Int());
            AddColumn("dbo.ProductOL", "PointsAward", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.ProductOL", "PointsAward");
            DropColumn("dbo.ProductOL", "Points");
            DropColumn("dbo.InvoiceLine", "PointsAward");
            DropColumn("dbo.InvoiceLine", "Points");
            DropColumn("dbo.UserGroup", "Points");
            DropColumn("dbo.UserGroup", "PointsTimeRatio");
            DropColumn("dbo.UserGroup", "PointsMoneyRatio");
            DropColumn("dbo.UserGroup", "PointsAwardOptions");
        }
    }
}
