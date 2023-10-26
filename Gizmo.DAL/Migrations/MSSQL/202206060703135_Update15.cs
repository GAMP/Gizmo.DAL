namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseKey", "AssignedHostId", c => c.Int());
            CreateIndex("dbo.LicenseKey", "AssignedHostId");
            AddForeignKey("dbo.LicenseKey", "AssignedHostId", "dbo.HostComputer", "HostId");

            //--Add unlimited credit for sales anyway
            Sql("UPDATE UserGroup SET CreditLimitOptions = CreditLimitOptions | 8");

            //--Add limited credit for time
            Sql("UPDATE UserGroup SET CreditLimitOptions = CreditLimitOptions | 16 WHERE((CreditLimitOptions & 2) = 2 OR IsNegativeBalanceAllowed = 1) AND CreditLimit > 0");

            //--Add unlimited credit for time
            Sql("UPDATE UserGroup SET CreditLimitOptions = CreditLimitOptions | 32 WHERE((CreditLimitOptions & 2) = 2 OR IsNegativeBalanceAllowed = 1) AND CreditLimit = 0");
        }

        public override void Down()
        {
            DropForeignKey("dbo.LicenseKey", "AssignedHostId", "dbo.HostComputer");
            DropIndex("dbo.LicenseKey", new[] { "AssignedHostId" });
            DropColumn("dbo.LicenseKey", "AssignedHostId");
        }
    }
}
