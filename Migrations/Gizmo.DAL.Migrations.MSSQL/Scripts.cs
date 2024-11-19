namespace Gizmo.DAL.Migrations.MSSQL
{
    public static class Scripts
    {
        /// <summary>
        /// This script will create a branch and set it to dependent entities while migrating EF6 database.
        /// </summary>
        public static readonly string EF_6_BRANCH_SET = """
            IF NOT EXISTS (SELECT BranchId FROM Branch)
            BEGIN
            INSERT INTO Branch (Name,Guid,IsEnabled,IsDeleted,CreatedTime,Latitude,Longitude) VALUES ('Default',NEWID(),1,0,GETDATE(),0,0)
            END
            UPDATE [dbo].[Register] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Asset] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Device] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[HostGroup] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[HostLayoutGroup] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Shift] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[StockTransaction] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Reservation] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[AppStat] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[UsageSession] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[UserSession] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[AssetTransaction] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[AssistanceRequest] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);

            IF NOT EXISTS (SELECT StockId FROM Stock)
            BEGIN
            INSERT INTO Stock (Name,Type,IsDeleted,CreatedTime) VALUES ('Default',0,0,GETDATE())
            END
        """;
    }
}
