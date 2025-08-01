using System;
using System.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;

namespace Gizmo.DAL.Extensions.DmlExtensions;

internal static class PostgreSql
{
    // We can use the same cleanup logic as SQL Server, because the logic is written as database-agnostic.
    public static Task Cleanup(DefaultDbContext cx, bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts, CancellationToken ct) =>
        SqlServer.Cleanup(cx, deleteUsers, deleteHosts, deleteOperators, deleteProducts, ct);

    public static async Task CleanupUsers(DefaultDbContext cx, CancellationToken ct)
    {
        const string DeletedUsersSubquery =
            """
               SELECT A."UserId" 
               FROM "User" AS A 
               LEFT OUTER JOIN "UserGuest" AS B ON A."UserId" = B."UserId" 
               LEFT OUTER JOIN "UserOperator" AS C ON A."UserId" = C."UserId" 
               WHERE A."IsDeleted" = 1 
               AND B."UserId" IS NULL 
               AND C."UserId" IS NULL
            """;

        await using (var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct))
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            cx.Database.SetCommandTimeout(int.MaxValue);

            // Basic DELETE operations (direct user ID reference)
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AssetTransaction"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AppStat"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AppRating"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("AssistanceRequest"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("ReservationUser"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Reservation"), ct);

            // Nested DELETE operations (joined tables)
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageTime", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageTimeFixed", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageRate", "Usage", "UsageId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("UsageUserSession", "Usage", "UsageId"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UsageSession"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Usage"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserSessionChange"), ct);

            // Delete based on CreatedById
            await cx.Database.ExecuteSqlAsync(
                $"""
                      DELETE FROM "UserSessionChange" 
                      WHERE "CreatedById" IN (
                          {DeletedUsersSubquery}
                      )
                 """, ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserSession"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("RefundInvoicePayment", "InvoicePayment", "InvoicePaymentId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("RefundDepositPayment", "DepositPayment", "DepositPaymentId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("Refund", "Payment", "PaymentId"), ct);

            // Complex join with multiple levels
            await cx.Database.ExecuteSqlAsync(
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

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("Refund", "DepositTransaction", "DepositTransactionId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("InvoicePayment"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("PaymentIntentDeposit", "PaymentIntent", "PaymentIntentId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("PaymentIntent"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("DepositPayment"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Payment"), ct);

            // Update operations setting NULL values
            await cx.Database.ExecuteSqlAsync(
                UpdateTableSetNull(
                    "InvoiceLineExtended",
                    "BundleLineId",
                    "InvoiceLine",
                    "InvoiceLineId"),
                ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineProduct", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineSession", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineTime", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineTimeFixed", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("InvoiceLineExtended", "InvoiceLine", "InvoiceLineId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("InvoiceLine"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Invoice"), ct);

            await cx.Database.ExecuteSqlAsync(
                UpdateTableSetNull(
                    "ProductOLExtended",
                    "BundleLineId",
                    "ProductOL",
                    "ProductOLId"),
                ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLTimeFixed", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLTime", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLSession", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLProduct", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("ProductOLExtended", "ProductOL", "ProductOLId"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("ProductOL"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("ProductOrder"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("DepositTransaction"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("PointTransaction"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("HostGroupWaitingLineEntry"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserCreditLimit"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserAttribute"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlAsync(DeleteFromTableWithJoin("Note", "UserNote", "NoteId"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserNote"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Verification"), ct);
            await cx.Database.ExecuteSqlAsync(DeleteFromTable("Token"), ct);

            // Commented in original code
            //await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserGuest"), ct);

            await cx.Database.ExecuteSqlAsync(DeleteFromTable("UserMember"), ct);

            // Final user deletion
            await cx.Database.ExecuteSqlAsync(
                $"""
                    DELETE FROM "User" 
                    WHERE "IsDeleted" = 1 
                    AND "UserId" IN (
                        {DeletedUsersSubquery}
                    )
                 """, ct);

            cx.ChangeTracker.DetectChanges();
            await cx.SaveChangesAsync(ct);
            await trx.CommitAsync(ct);
        }

        return;

        static FormattableString UpdateTableSetNull(
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

        static FormattableString DeleteFromTableWithJoin(string tableName, string joinTableName, string joinColumnName, string whereColumnName = "UserId") =>
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

        static FormattableString DeleteFromTable(string tableName, string columnName = "UserId") =>
            $"""
                DELETE FROM "{tableName}" 
                WHERE "{columnName}" IN (
                    {DeletedUsersSubquery}
                )
            """;
    }
}
