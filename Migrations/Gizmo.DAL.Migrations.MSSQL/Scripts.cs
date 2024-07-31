namespace Gizmo.DAL.Migrations.MSSQL
{
    public static class Scripts
    {
        public static readonly string BRANCH_SET = """
            IF NOT EXISTS (SELECT BranchId FROM Branch)
            BEGIN
            INSERT INTO Branch (Name,Guid,IsEnabled,IsDeleted,CreatedTime) VALUES ('Default',NEWID(),1,0,GETDATE())
            END
            UPDATE [dbo].[Register] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Asset] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Device] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[HostGroup] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[HostLayoutGroup] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Shift] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[StockTransaction] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
            UPDATE [dbo].[Reservation] Set BranchId=(SELECT min(BranchId) FROM [dbo].[Branch]);
        """;
    }
}
