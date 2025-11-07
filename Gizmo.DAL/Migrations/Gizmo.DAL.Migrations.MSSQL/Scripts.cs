namespace Gizmo.DAL.Migrations.MSSQL
{
    public static class Scripts
    {
        /// <summary>
        /// This script will create a branch and set it to dependent entities while migrating EF6 database.
        /// </summary>
        public static readonly string EF_6_BRANCH_SET = """
        DECLARE @DefaultBranchId INT;     

        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '__MigrationHistory')
            
        BEGIN
        IF NOT EXISTS (SELECT BranchId FROM Branch)
        BEGIN
        INSERT INTO Branch (Name,Guid,IsDisabled,IsDeleted,CreatedTime,Latitude,Longitude,HasBusinessSchedule,IsFiscalizationEnabled,TreatDepositsAsService,DisableTime) VALUES ('Default',NEWID(),0,0,GETDATE(),0,0,1,NULL,NULL,NULL)
        END

        SET @DefaultBranchId = (SELECT MIN(BranchId) FROM [dbo].[Branch]);
        
        UPDATE [dbo].[Register] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[RegisterTransaction] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Asset] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Device] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[HostGroup] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[HostLayoutGroup] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Shift] Set BranchId=@DefaultBranchId;
 
        UPDATE [dbo].[Reservation] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[AppStat] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[UsageSession] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[UserSession] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[AssetTransaction] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[AssistanceRequest] Set BranchId=@DefaultBranchId;
        
        IF NOT EXISTS (SELECT StockId FROM Stock)
        BEGIN
        INSERT INTO Stock (Name,Type,IsDeleted,CreatedTime) VALUES ('Default',0,0,GETDATE())
        END

        UPDATE [dbo].[StockTransaction] Set StockId=(SELECT min(StockId) FROM [dbo].[Stock]);

        UPDATE dp
        SET dp.Amount = p.Amount
        FROM DepositPayment dp
        INNER JOIN Payment p ON dp.PaymentId = p.PaymentId;

        END
        """;
    }
}
