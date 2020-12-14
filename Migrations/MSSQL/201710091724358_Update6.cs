namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AssetTransaction", name: "CheckOutTime", newName: "CheckInTime");
            RenameColumn(table: "dbo.AssetTransaction", name: "CheckedOutById", newName: "CheckedInById");
            RenameIndex(table: "dbo.AssetTransaction", name: "IX_CheckedOutById", newName: "IX_CheckedInById");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AssetTransaction", name: "CheckInTime", newName: "CheckOutTime");
            RenameIndex(table: "dbo.AssetTransaction", name: "IX_CheckedInById", newName: "IX_CheckedOutById");
            RenameColumn(table: "dbo.AssetTransaction", name: "CheckedInById", newName: "CheckedOutById");
        }
    }
}
