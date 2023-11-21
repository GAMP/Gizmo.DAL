namespace Gizmo.DAL.Migrations.MSSQL
{
    using System.Data.Entity.Migrations;

    public partial class Update9 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'User', 'Delete', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'USER' AND upper([Value]) = 'DELETE');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'User', 'ChangeUserName', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'USER' AND upper([Value]) = 'CHANGEUSERNAME');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'User', 'ChangeUserGroup', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'USER' AND upper([Value]) = 'CHANGEUSERGROUP');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'User', 'Edit', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'USER' AND upper([Value]) = 'EDIT');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
                "UserId, 'Log', 'Clear', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
                "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
                "AND upper([Type]) = 'LOG' AND upper([Value]) = 'CLEAR');");

            Sql("INSERT INTO [dbo].[UserPermission] ([UserId],[Type],[Value],[CreatedTime]) SELECT " +
               "UserId, 'Management', 'Maintenance', CURRENT_TIMESTAMP FROM UserOperator as tUser " +
               "WHERE NOT EXISTS(SELECT UserId,[Type],[Value] FROM[dbo].[UserPermission] WHERE[UserId] = tUser.UserId " +
               "AND upper([Type]) = 'MANAGEMENT' AND upper([Value]) = 'MAINTENANCE');");
        }

        public override void Down()
        {
        }
    }
}
