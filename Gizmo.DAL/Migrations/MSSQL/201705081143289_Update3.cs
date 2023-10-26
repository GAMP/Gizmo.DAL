namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGuest", "IsJoined", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserGuest", "IsReserved", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserGuest", "ReservedHostId", c => c.Int());
            AddColumn("dbo.UserGuest", "ReservedSlot", c => c.Int());
            AddColumn("dbo.UserSessionChange", "Slot", c => c.Int(nullable: false));
            AddColumn("dbo.UserSession", "Slot", c => c.Int(nullable: false));
            AddColumn("dbo.MonetaryUnit", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Setting", "Value", c => c.String());

            //NORMAL INDEDX CREATION
            //CreateIndex("dbo.UserGuest", new[] { "ReservedHostId", "ReservedSlot" }, unique: true, name: "UQ_UserGuestHostSlot");

            //FILTERED INDEX CREATION
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_UserGuestHostSlot] ON [dbo].[UserGuest](ReservedHostId,ReservedSlot) WHERE ReservedHostId IS NOT NULL AND ReservedSlot IS NOT NULL");

            AddForeignKey("dbo.UserGuest", "ReservedHostId", "dbo.Host", "HostId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserGuest", "ReservedHostId", "dbo.Host");
            DropIndex("dbo.UserGuest", "UQ_UserGuestHostSlot");
            AlterColumn("dbo.Setting", "Value", c => c.String(maxLength: 255));
            DropColumn("dbo.MonetaryUnit", "IsDeleted");
            DropColumn("dbo.UserSession", "Slot");
            DropColumn("dbo.UserSessionChange", "Slot");
            DropColumn("dbo.UserGuest", "ReservedSlot");
            DropColumn("dbo.UserGuest", "ReservedHostId");
            DropColumn("dbo.UserGuest", "IsReserved");
            DropColumn("dbo.UserGuest", "IsJoined");
        }
    }
}
