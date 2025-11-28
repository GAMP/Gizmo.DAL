namespace Gizmo.DAL.Migrations.MSSQL
{
    public static class Scripts
    {
        /// <summary>
        /// This script will create a branch and set it to dependent entities while migrating EF6 database.
        /// </summary>
        public static readonly string EF_6_BRANCH_SET = """
        DECLARE @DefaultBranchId INT;     
        DECLARE @DefaultStockId INT;  

        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '__MigrationHistory')
            
        BEGIN
        IF NOT EXISTS (SELECT BranchId FROM Branch)
        BEGIN
        INSERT INTO Branch (Name,Guid,IsDisabled,IsDeleted,CreatedTime,Latitude,Longitude,HasBusinessSchedule,IsFiscalizationEnabled,TreatDepositsAsService,DisableTime) VALUES ('Default',NEWID(),0,0,GETDATE(),0,0,1,NULL,NULL,NULL)
        END

        IF NOT EXISTS (SELECT StockId FROM Stock)
        BEGIN
        INSERT INTO Stock (Name,Type,IsDeleted,CreatedTime) VALUES ('Default',0,0,GETDATE())
        END

        SET @DefaultBranchId = (SELECT MIN(BranchId) FROM [dbo].[Branch]);
        SET @DefaultStockId = (SELECT MIN(StockId) FROM [dbo].[Stock]);

        -- Required branch

        UPDATE [dbo].[AppStat] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Asset] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[AssetTransaction] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[AssistanceRequest] Set BranchId=@DefaultBranchId;        
        UPDATE [dbo].[Device] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Register] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[RegisterTransaction] Set BranchId=@DefaultBranchId;    
        UPDATE [dbo].[HostGroup] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[HostLayoutGroup] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Reservation] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Shift] Set BranchId=@DefaultBranchId; 
        UPDATE [dbo].[StockTransaction] Set StockId=@DefaultStockId;
        UPDATE [dbo].[UsageSession] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[UserSession] Set BranchId=@DefaultBranchId; 

        -- Optional branch

        UPDATE [dbo].[DepositPayment] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Invoice] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[InvoicePayment] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Payment] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[PaymentIntent] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[ProductOrder] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Refund] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[User] Set BranchId=@DefaultBranchId;
        UPDATE [dbo].[Void] Set BranchId=@DefaultBranchId;
                 
        -- Multi branch

        INSERT INTO [dbo].[AppExeBranch] (AppExeId, BranchId, IsEnabled)
        SELECT
              AppExeId  = ae.AppExeId
            , BranchId  = @DefaultBranchId
            , IsEnabled = CAST(1 AS bit)
        FROM [dbo].[AppExe] AS ae
        WHERE NOT EXISTS (
            SELECT
                1
            FROM [dbo].[AppExeBranch] AS aeb
            WHERE aeb.AppExeId = ae.AppExeId
              AND aeb.BranchId = @DefaultBranchId
        );

        INSERT INTO [dbo].[FeedBranch] (FeedId, BranchId, IsEnabled)
        SELECT
              FeedId  = ae.FeedId
            , BranchId  = @DefaultBranchId
            , IsEnabled = CAST(1 AS bit)
        FROM [dbo].[Feed] AS ae
        WHERE NOT EXISTS (
            SELECT
                1
            FROM [dbo].[FeedBranch] AS aeb
            WHERE aeb.FeedId = ae.FeedId
              AND aeb.BranchId = @DefaultBranchId
        );

        INSERT INTO [dbo].[NewsBranch] (NewsId, BranchId, IsEnabled)
        SELECT
              NewsId  = ae.NewsId
            , BranchId  = @DefaultBranchId
            , IsEnabled = CAST(1 AS bit)
        FROM [dbo].[News] AS ae
        WHERE NOT EXISTS (
            SELECT
                1
            FROM [dbo].[NewsBranch] AS aeb
            WHERE aeb.NewsId = ae.NewsId
              AND aeb.BranchId = @DefaultBranchId
        );

        INSERT INTO [dbo].[ProductBranch] (ProductId, BranchId, IsEnabled)
        SELECT
              ProductId  = ae.ProductId
            , BranchId  = @DefaultBranchId
            , IsEnabled = CAST(1 AS bit)
        FROM [dbo].[ProductBase] AS ae
        WHERE NOT EXISTS (
            SELECT
                1
            FROM [dbo].[ProductBranch] AS aeb
            WHERE aeb.ProductId = ae.ProductId
              AND aeb.BranchId = @DefaultBranchId
        );

        -- Generic updates

        UPDATE dp
        SET dp.Amount = p.Amount
        FROM DepositPayment dp
        INNER JOIN Payment p ON dp.PaymentId = p.PaymentId;
        END

        """;
    }
}
