using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class SqlServer
{
    public static string CleanupScript(bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts)
    {
        var script = new StringBuilder();

        // Handle cross-dependencies first
        if (deleteUsers || deleteHosts)
        {
            script.AppendLine("""
                -- Clean up reservations and stats that depend on both users and hosts
                DELETE FROM [AppStat];
                DELETE FROM [ReservationUser];
                DELETE FROM [ReservationHost];  
                DELETE FROM [Reservation];
                """);
        }

        // Always clean up financial data when any category is being deleted
        if (deleteUsers || deleteHosts || deleteOperators || deleteProducts)
        {
            script.AppendLine("""
                -- Nullify foreign key references that need to be handled before deletion
                UPDATE [InvoiceLineExtended] SET BundleLineId = NULL;
                UPDATE [UsageSession] SET CurrentUsageId = NULL;
                UPDATE [ProductOLExtended] SET BundleLineId = NULL;

                -- Financial data cleanup in dependency order
                DELETE FROM [UsageRate];
                DELETE FROM [UsageTimeFixed];
                DELETE FROM [UsageTime];
                DELETE FROM [UsageUserSession];
                DELETE FROM [Usage];
                DBCC CHECKIDENT ('Usage', RESEED, 1);

                DELETE FROM [UserSessionChange];
                DELETE FROM [UserSession];
                DELETE FROM [UsageSession];
                DBCC CHECKIDENT ('UsageSession', RESEED, 1);

                DELETE FROM [RefundInvoicePayment];
                DELETE FROM [RefundDepositPayment];
                DELETE FROM [Refund];
                DBCC CHECKIDENT ('Refund', RESEED, 1);

                DELETE FROM [VoidInvoice];
                DELETE FROM [VoidDepositPayment];
                DELETE FROM [Void];
                DBCC CHECKIDENT ('Void', RESEED, 1);

                DELETE FROM [InvoicePayment];
                DBCC CHECKIDENT ('InvoicePayment', RESEED, 1);

                DELETE FROM [PaymentIntentDeposit];
                DELETE FROM [PaymentIntentOrder];
                DELETE FROM [PaymentIntent];
                DBCC CHECKIDENT ('PaymentIntent', RESEED, 1);

                DELETE FROM [DepositPayment];
                DBCC CHECKIDENT ('DepositPayment', RESEED, 1);

                DELETE FROM [Payment];
                DBCC CHECKIDENT ('Payment', RESEED, 1);

                DELETE FROM [InvoiceLineProduct];
                DELETE FROM [InvoiceLineSession];
                DELETE FROM [InvoiceLineTime];
                DELETE FROM [InvoiceLineTimeFixed];
                DELETE FROM [InvoiceLineExtended];
                DELETE FROM [InvoiceLine];
                DBCC CHECKIDENT ('InvoiceLine', RESEED, 1);

                DELETE FROM [InvoiceFiscalReceipt];
                DELETE FROM [Invoice];
                DBCC CHECKIDENT ('Invoice', RESEED, 1);

                DELETE FROM [ProductOLTimeFixed];
                DELETE FROM [ProductOLTime];
                DELETE FROM [ProductOLSession];
                DELETE FROM [ProductOLProduct];
                DELETE FROM [ProductOLExtended];
                DELETE FROM [ProductOL];
                DBCC CHECKIDENT ('ProductOL', RESEED, 1);

                DELETE FROM [ProductOrder];
                DBCC CHECKIDENT ('ProductOrder', RESEED, 1);

                DELETE FROM [DepositTransaction];
                DBCC CHECKIDENT ('DepositTransaction', RESEED, 1);

                DELETE FROM [PointTransaction];
                DBCC CHECKIDENT ('PointTransaction', RESEED, 1);

                DELETE FROM [StockTransaction];
                DELETE FROM [ShiftCount];
                DELETE FROM [RegisterTransaction];
                DELETE FROM [FiscalReceipt];
                DELETE FROM [Shift];
                DELETE FROM [Register];
                """);
        }

        // Products cleanup
        if (deleteProducts)
        {
            script.AppendLine("""
                -- Product cleanup in dependency order
                DELETE FROM [ProductImage];
                DELETE FROM [ProductTax];
                DELETE FROM [ProductPeriod];
                DELETE FROM [ProductPeriodDayTime];
                DELETE FROM [ProductPeriodDay];
                DELETE FROM [ProductTimeHostDisallowed];
                DELETE FROM [ProductUserDisallowed];
                DELETE FROM [ProductUserPrice];
                DELETE FROM [BundleProduct];
                DELETE FROM [ProductTimePeriod];
                DELETE FROM [ProductTimePeriodDayTime];
                DELETE FROM [ProductTimePeriodDay];
                DELETE FROM [ProductBundle];
                DELETE FROM [Product];
                DELETE FROM [ProductTime];
                DELETE FROM [ProductBaseExtended];
                DELETE FROM [ProductBase];
                DBCC CHECKIDENT ('ProductBase', RESEED, 1);
                """);
        }

        if (deleteProducts || deleteHosts)
        {
            script.AppendLine("DELETE FROM [ProductHostHidden];");
        }

        // Users cleanup
        if (deleteUsers)
        {
            script.AppendLine("""
                -- User-related data cleanup
                DELETE FROM [HostGroupWaitingLineEntry];
                DELETE FROM [AssetTransaction];
                DELETE FROM [AppRating];
                DELETE FROM [UserCreditLimit];
                DELETE FROM [UserAttribute];
                DELETE FROM [UserNote];
                DELETE FROM [Note];
                DELETE FROM [VerificationEmail];
                DELETE FROM [VerificationMobilePhone];
                DELETE FROM [Verification];
                DELETE FROM [Token];
                """);
        }

        // Hosts cleanup
        if (deleteHosts)
        {
            // Always reset user guests when deleting hosts to avoid foreign key constraint violations
            script.AppendLine("""
                -- Reset user guests when deleting hosts but keeping users
                UPDATE [UserGuest] SET ReservedHostId = NULL WHERE ReservedHostId IS NOT NULL;
                """);

            script.AppendLine("""
                -- Host cleanup
                DELETE FROM [HostComputer];
                DELETE FROM [HostEndpoint];
                DELETE FROM [Host];
                DBCC CHECKIDENT ('Host', RESEED, 1);
                """);
        }

        // Operators cleanup with proper foreign key handling
        if (deleteOperators)
        {
            script.AppendLine("""
                -- Clear ALL foreign key references to operators systematically
                
                -- Core entity updates (CreatedById/ModifiedById columns)
                UPDATE [App] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [AppCategory] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [AppExe] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [AppGroup] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [AssetTransaction] SET CreatedById = NULL, ModifiedById = NULL, CheckedInById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL OR CheckedInById IS NOT NULL;
                UPDATE [Attribute] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [BillProfile] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Device] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [DeviceHost] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Feed] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Host] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [HostGroup] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [MonetaryUnit] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [News] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [PaymentMethod] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [PluginLibrary] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ProductBase] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ProductGroup] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ProductHostHidden] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ProductImage] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ProductUserDisallowed] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Reservation] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ReservationHost] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [ReservationUser] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [SecurityProfile] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Setting] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Tax] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Token] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [User] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserAgreement] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserAttribute] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserCredential] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserCreditLimit] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserGroup] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserPermissionSet] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [UserPicture] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;
                UPDATE [Variable] SET CreatedById = NULL, ModifiedById = NULL WHERE CreatedById IS NOT NULL OR ModifiedById IS NOT NULL;

                -- Clear specific foreign key references before deleting related entities
                UPDATE [Host] SET HostGroupId = NULL WHERE HostGroupId IS NOT NULL;
                UPDATE [User] SET PermissionSetId = NULL WHERE PermissionSetId IS NOT NULL;

                -- Clean up dependent records that would cause foreign key constraint violations
                DELETE FROM [HostGroupWaitingLineEntry];

                -- Delete operator tokens (type 0)
                DELETE FROM [Token] WHERE Type = 0;

                -- Clean up operator-specific entities
                DELETE FROM [AgeRestriction];
                DELETE FROM [AssistanceRequestType];
                DELETE FROM [Stock];
                DELETE FROM [Branch];
                DELETE FROM [ClientOptions];
                DELETE FROM [Companion];
                DELETE FROM [Notification];
                DELETE FROM [UserPermissionSet];
                """);
        }

        return script.ToString();
    }

    public static async Task CleanupUsers(DefaultDbContext cx, CancellationToken ct)
    {
        const string DeletedUsersSubquery =
            """
               SELECT A.UserId 
               FROM [User] AS A 
               LEFT OUTER JOIN [UserGuest] AS B ON A.UserId = B.UserId 
               LEFT OUTER JOIN [UserOperator] AS C ON A.UserId = C.UserId 
               WHERE A.IsDeleted = 1 
               AND B.UserId IS NULL 
               AND C.UserId IS NULL
            """;

        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            // Use a reasonable timeout value for SQL Server
            cx.Database.SetCommandTimeout(3600);

            // Basic DELETE operations (direct user ID reference)
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AssetTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AppStat"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AppRating"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("AssistanceRequest"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("ReservationUser"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Reservation"), ct);

            // Nested DELETE operations (joined tables)
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageTime", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageTimeFixed", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageRate", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("UsageUserSession", "Usage", "UsageId"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UsageSession"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Usage"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserSessionChange"), ct);

            // Delete based on CreatedById
            await cx.Database.ExecuteSqlRawAsync(
                $"""
                      DELETE FROM [UserSessionChange] 
                      WHERE CreatedById IN (
                          {DeletedUsersSubquery}
                      )
                 """, ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserSession"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("RefundInvoicePayment", "InvoicePayment", "InvoicePaymentId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("RefundDepositPayment", "DepositPayment", "DepositPaymentId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("Refund", "Payment", "PaymentId"), ct);

            // Complex join with multiple levels
            await cx.Database.ExecuteSqlRawAsync(
                $"""
                     DELETE FROM [RefundDepositPayment] 
                     WHERE RefundId IN (
                         SELECT RefundId 
                         FROM [Refund] 
                         WHERE DepositTransactionId IN (
                             SELECT DepositTransactionId 
                             FROM [DepositTransaction] 
                             WHERE UserId IN (
                                 {DeletedUsersSubquery}
                             )
                         )
                     )
                 """, ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("Refund", "DepositTransaction", "DepositTransactionId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("InvoicePayment"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("PaymentIntentDeposit", "PaymentIntent", "PaymentIntentId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("PaymentIntent"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("DepositPayment"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Payment"), ct);

            // Update operations setting NULL values
            await cx.Database.ExecuteSqlRawAsync(
                UpdateTableSetNull(
                    "InvoiceLineExtended",
                    "BundleLineId",
                    "InvoiceLine",
                    "InvoiceLineId"),
                ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineProduct", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineSession", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineTime", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineTimeFixed", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("InvoiceLineExtended", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("InvoiceLine"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Invoice"), ct);

            await cx.Database.ExecuteSqlRawAsync(
                UpdateTableSetNull(
                    "ProductOLExtended",
                    "BundleLineId",
                    "ProductOL",
                    "ProductOLId"),
                ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLTimeFixed", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLTime", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLSession", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLProduct", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("ProductOLExtended", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("ProductOL"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("ProductOrder"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("DepositTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("PointTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("HostGroupWaitingLineEntry"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserCreditLimit"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserAttribute"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlRawAsync(DeleteFromTableWithJoin("Note", "UserNote", "NoteId"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserNote"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Verification"), ct);
            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("Token"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserGuest"), ct);

            await cx.Database.ExecuteSqlRawAsync(DeleteFromTable("UserMember"), ct);

            // Final user deletion
            await cx.Database.ExecuteSqlRawAsync(
                $"""
                    DELETE FROM [User] 
                    WHERE IsDeleted = 1 
                    AND UserId IN (
                        {DeletedUsersSubquery}
                    )
                 """, ct);

            cx.ChangeTracker.DetectChanges();
            await cx.SaveChangesAsync(ct);
            await trx.CommitAsync(ct);
        }

        return;

        static string UpdateTableSetNull(
            string tableName,
            string columnToSetNull,
            string joinTableName,
            string joinColumnName,
            string whereColumnName = "UserId") =>
            $"""
                UPDATE [{tableName}] 
                SET {columnToSetNull} = NULL 
                WHERE {joinColumnName} IN (
                    SELECT {joinColumnName} 
                    FROM [{joinTableName}] 
                    WHERE {whereColumnName} IN (
                        {DeletedUsersSubquery}
                    )
                )
            """;

        static string DeleteFromTableWithJoin(string tableName, string joinTableName, string joinColumnName, string whereColumnName = "UserId") =>
            $"""
                 DELETE FROM [{tableName}] 
                 WHERE {joinColumnName} IN (
                     SELECT {joinColumnName} 
                     FROM [{joinTableName}] 
                     WHERE {whereColumnName} IN (
                         {DeletedUsersSubquery}
                     )
                 )
             """;

        static string DeleteFromTable(string tableName, string columnName = "UserId") =>
            $"""
                DELETE FROM [{tableName}] 
                WHERE {columnName} IN (
                    {DeletedUsersSubquery}
                )
            """;
    }
}
