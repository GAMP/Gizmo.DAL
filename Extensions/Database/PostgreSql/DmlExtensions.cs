using System.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class PostgreSql
{
    public static async Task CleanupUsers(DefaultDbContext cx, CancellationToken ct)
    {
        const string DeletedUsersSubquery =
            """
               SELECT A."UserId" 
               FROM "User" AS A 
               LEFT OUTER JOIN "UserGuest" AS B ON A."UserId" = B."UserId" 
               LEFT OUTER JOIN "UserOperator" AS C ON A."UserId" = C."UserId" 
               WHERE A."IsDeleted" = true
               AND B."UserId" IS NULL 
               AND C."UserId" IS NULL
            """;

        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            // Use a reasonable timeout value for PostgreSQL
            cx.Database.SetCommandTimeout(3600);

            // Basic DELETE operations (direct user ID reference)
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("AssetTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("AppStat"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("AppRating"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("AssistanceRequest"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("ReservationUser"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("Reservation"), ct);

            // Nested DELETE operations (joined tables)
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("UsageTime", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("UsageTimeFixed", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("UsageRate", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("UsageUserSession", "Usage", "UsageId"), ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UsageSession"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("Usage"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserSessionChange"), ct);

            // Delete based on CreatedById
            await cx.Database.ExecuteSqlRawAsync (
                $"""
                      DELETE FROM "UserSessionChange" 
                      WHERE "CreatedById" IN (
                          {DeletedUsersSubquery}
                      )
                 """, ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserSession"), ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("RefundInvoicePayment", "InvoicePayment", "InvoicePaymentId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("RefundDepositPayment", "DepositPayment", "DepositPaymentId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("Refund", "Payment", "PaymentId"), ct);

            // Complex join with multiple levels
            await cx.Database.ExecuteSqlRawAsync (
                $"""
                     DELETE FROM "RefundDepositPayment" 
                     WHERE "RefundId" IN (
                         SELECT "RefundId" 
                         FROM "Refund" 
                         WHERE "DepositTransactionId" IN (
                             SELECT "DepositTransactionId" 
                             FROM "DepositTransaction" 
                             WHERE "UserId" IN (
                                 {DeletedUsersSubquery}
                             )
                         )
                     )
                 """, ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("Refund", "DepositTransaction", "DepositTransactionId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("InvoicePayment"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("PaymentIntentDeposit", "PaymentIntent", "PaymentIntentId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("PaymentIntent"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("DepositPayment"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("Payment"), ct);

            // Update operations setting NULL values
            await cx.Database.ExecuteSqlRawAsync (
                UpdateTableSetNull(
                    "InvoiceLineExtended",
                    "BundleLineId",
                    "InvoiceLine",
                    "InvoiceLineId"),
                ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("InvoiceLineProduct", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("InvoiceLineSession", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("InvoiceLineTime", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("InvoiceLineTimeFixed", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("InvoiceLineExtended", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("InvoiceLine"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("Invoice"), ct);

            await cx.Database.ExecuteSqlRawAsync (
                UpdateTableSetNull(
                    "ProductOLExtended",
                    "BundleLineId",
                    "ProductOL",
                    "ProductOLId"),
                ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("ProductOLTimeFixed", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("ProductOLTime", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("ProductOLSession", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("ProductOLProduct", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("ProductOLExtended", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("ProductOL"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("ProductOrder"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("DepositTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("PointTransaction"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("HostGroupWaitingLineEntry"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserCreditLimit"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserAttribute"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlRawAsync (DeleteFromTableWithJoin("Note", "UserNote", "NoteId"), ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserNote"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("Verification"), ct);
            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("Token"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserGuest"), ct);

            await cx.Database.ExecuteSqlRawAsync (DeleteFromTable("UserMember"), ct);

            // Final user deletion
            await cx.Database.ExecuteSqlRawAsync (
                $"""
                    DELETE FROM "User" 
                    WHERE "IsDeleted" = true
                    AND "UserId" IN (
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
                UPDATE "{tableName}" 
                SET "{columnToSetNull}" = NULL 
                WHERE "{joinColumnName}" IN (
                    SELECT "{joinColumnName}" 
                    FROM "{joinTableName}" 
                    WHERE "{whereColumnName}" IN (
                        {DeletedUsersSubquery}
                    )
                )
            """;

        static string DeleteFromTableWithJoin(string tableName, string joinTableName, string joinColumnName, string whereColumnName = "UserId") =>
            $"""
                 DELETE FROM "{tableName}" 
                 WHERE "{joinColumnName}" IN (
                     SELECT "{joinColumnName}" 
                     FROM "{joinTableName}" 
                     WHERE "{whereColumnName}" IN (
                         {DeletedUsersSubquery}
                     )
                 )
             """;

        static string DeleteFromTable(string tableName, string columnName = "UserId") =>
            $"""
                DELETE FROM "{tableName}" 
                WHERE "{columnName}" IN (
                    {DeletedUsersSubquery}
                )
            """;
    }
}
