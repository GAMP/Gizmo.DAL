namespace GizmoDALV2.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HostGroupWaitingLine",
                c => new
                {
                    HosGroupId = c.Int(nullable: false),
                    TimeOutOptions = c.Int(nullable: false),
                    EnablePriorities = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.HosGroupId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostGroup", t => t.HosGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.HosGroupId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostGroupWaitingLineEntry",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HostGroupId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Position = c.Int(nullable: false),
                    IsManualPosition = c.Boolean(nullable: false),
                    TimeInLine = c.Double(nullable: false),
                    ReadyTime = c.Double(nullable: false),
                    IsReadyTimedOut = c.Boolean(nullable: false),
                    State = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostGroup", t => t.HostGroupId)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .ForeignKey("dbo.HostGroupWaitingLine", t => t.HostGroupId, cascadeDelete: true)
                .Index(t => t.HostGroupId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            AddColumn("dbo.UserMember", "EnableDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserMember", "DisabledDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserGroup", "WaitingLinePriority", c => c.Int(nullable: false));
            AddColumn("dbo.UserGroup", "IsWaitingLinePriorityEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserSession", "PendSpanTotal", c => c.Double(nullable: false));
            AddColumn("dbo.UserSession", "PauseSpan", c => c.Double(nullable: false));
            AddColumn("dbo.UserSession", "PauseSpanTotal", c => c.Double(nullable: false));
            AddColumn("dbo.News", "MediaUrl", c => c.String(maxLength: 255));

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
               "UserId, 'User', 'UserPasswordReset', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
               "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
               "AND upper([Type]) = 'USER' AND upper([Value]) = 'USERPASSWORDRESET');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'User', 'UserEnable', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'USER' AND upper([Value]) = 'USERENABLE');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'User', 'UserDisable', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'USER' AND upper([Value]) = 'USERDISABLE');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
              "UserId, 'Sale', 'Deposit', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
              "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
              "AND upper([Type]) = 'SALE' AND upper([Value]) = 'DEPOSIT');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
              "UserId, 'Sale', 'Withdraw', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
              "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
              "AND upper([Type]) = 'SALE' AND upper([Value]) = 'WITHDRAW');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
              "UserId, 'User', 'UserManualLogin', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
              "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
              "AND upper([Type]) = 'USER' AND upper([Value]) = 'USERMANUALLOGIN');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
            "UserId, 'Shift', 'ViewExpected', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
            "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
            "AND upper([Type]) = 'Shift' AND upper([Value]) = 'VIEWEXPECTED');");
        }

        public override void Down()
        {
            DropForeignKey("dbo.HostGroupWaitingLine", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostGroupWaitingLine", "HosGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.HostGroupWaitingLineEntry", "HostGroupId", "dbo.HostGroupWaitingLine");
            DropForeignKey("dbo.HostGroupWaitingLineEntry", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.HostGroupWaitingLineEntry", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.HostGroupWaitingLineEntry", "HostGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.HostGroupWaitingLineEntry", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostGroupWaitingLine", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.HostGroupWaitingLineEntry", new[] { "CreatedById" });
            DropIndex("dbo.HostGroupWaitingLineEntry", new[] { "ModifiedById" });
            DropIndex("dbo.HostGroupWaitingLineEntry", new[] { "UserId" });
            DropIndex("dbo.HostGroupWaitingLineEntry", new[] { "HostGroupId" });
            DropIndex("dbo.HostGroupWaitingLine", new[] { "CreatedById" });
            DropIndex("dbo.HostGroupWaitingLine", new[] { "ModifiedById" });
            DropIndex("dbo.HostGroupWaitingLine", new[] { "HosGroupId" });
            DropColumn("dbo.News", "MediaUrl");
            DropColumn("dbo.UserSession", "PauseSpanTotal");
            DropColumn("dbo.UserSession", "PauseSpan");
            DropColumn("dbo.UserSession", "PendSpanTotal");
            DropColumn("dbo.UserGroup", "IsWaitingLinePriorityEnabled");
            DropColumn("dbo.UserGroup", "WaitingLinePriority");
            DropColumn("dbo.UserMember", "DisabledDate");
            DropColumn("dbo.UserMember", "EnableDate");
            DropTable("dbo.HostGroupWaitingLineEntry");
            DropTable("dbo.HostGroupWaitingLine");
        }
    }
}
