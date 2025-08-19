using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions.DmlExtensions;
using Gizmo.DAL.Scripts;
using IntegrationLib;
using Microsoft.EntityFrameworkCore;

namespace Gizmo.DAL.Extensions;

/// <summary>
/// Provides extension methods for Data Manipulation Language (DML) operations.
/// </summary>
/// <remarks>
/// DML commands are used for managing data within schema objects. Typical DML commands include:
/// <list type="bullet">
///   <item><description><c>SELECT</c> – Retrieves data from the database.</description></item>
///   <item><description><c>INSERT</c> – Inserts data into a table.</description></item>
///   <item><description><c>UPDATE</c> – Updates existing data within a table.</description></item>
///   <item><description><c>DELETE</c> – Deletes all records from a table, the space for the records remain.</description></item>
/// </list>
/// </remarks>
public static class DmlOperations
{
    /// <summary>
    /// Cleans up the database by removing all data from it based on specified criteria.
    /// </summary>
    /// <param name="cx">The <see cref="DefaultDbContext"/> instance representing the database context.</param>
    /// <param name="deleteUsers">If <c>true</c>, removes all user data from the database; otherwise, preserves user data.</param>
    /// <param name="deleteHosts">If <c>true</c>, removes all host data from the database; otherwise, preserves host data.</param>
    /// <param name="deleteOperators">If <c>true</c>, removes all operator data from the database; otherwise, preserves operator data.</param>
    /// <param name="deleteProducts">If <c>true</c>, removes all product data from the database; otherwise, preserves product data.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous cleanup operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="cx"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the cleanup operation fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for cleanup operations.</exception>
    /// <exception cref="NotImplementedException">Thrown when the cleanup operation is not implemented for the current provider (e.g., PostgreSQL).</exception>
    /// <remarks>
    /// This method performs a comprehensive cleanup of the database by removing specified data categories.
    /// <para><strong>Provider Support:</strong></para>
    /// <list type="bullet">
    /// <item><description><strong>SQL Server:</strong> Fully implemented with comprehensive cleanup including financial data, reservations, usage sessions, invoices, payments, and related entities.</description></item>
    /// <item><description><strong>PostgreSQL:</strong> Not implemented - throws <see cref="NotImplementedException"/>.</description></item>
    /// </list>
    /// <para><strong>Warning:</strong> Use with extreme caution as this operation will permanently delete data based on the specified flags. Always backup your database before running cleanup operations.</para>
    /// </remarks>
    public static async Task Cleanup(this DefaultDbContext cx, bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts, CancellationToken ct)
    {
        var cleanupScript = cx.Database.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.CleanupScript(deleteUsers, deleteHosts, deleteOperators, deleteProducts),
            Provider.Type.PostgreSql => PostgreSql.CleanupScript(deleteUsers, deleteHosts, deleteOperators, deleteProducts),
            _ => throw new NotSupportedException($"DML operation '{nameof(Cleanup)}' is not supported for the current database provider.")
        };

        await using var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);

        try
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            cx.Database.SetCommandTimeout(3600);

            // Only execute the script if it contains actual SQL commands
            if (!string.IsNullOrWhiteSpace(cleanupScript))
            {
                await cx.Database.ExecuteSqlRawAsync(cleanupScript, ct);
            }

            // Handle Entity Framework specific cleanup for users
            if (deleteUsers)
            {
                cx.UsersGuest.RemoveRange(cx.UsersGuest);
                cx.UsersMember.RemoveRange(cx.UsersMember);
            }

            // Handle Entity Framework specific cleanup for operators
            if (deleteOperators)
            {
                cx.UserPermissions.RemoveRange(cx.UserPermissions.Where(permission => permission.User is Entities.UserOperator));
                cx.UsersOperator.RemoveRange(cx.UsersOperator);

                // Create default admin operator
                var defaultOperator = new Entities.UserOperator
                {
                    UserCredential = new Entities.UserCredential(),
                    Username = "Admin",
                    CreatedTime = DateTime.UtcNow
                };

                byte[] salt = cx.GetNewSalt();
                byte[] password = cx.GetHashedPassword("admin", salt);

                defaultOperator.UserCredential.Salt = salt;
                defaultOperator.UserCredential.Password = password;

                var allPermissions = IntegrationLib.ClaimTypeBase
                    .GetClaimTypes()
                    .Select(claim => new Entities.UserPermission { Type = claim.Resource, Value = claim.Operation });

                defaultOperator.Permissions.UnionWith(allPermissions);

                cx.UsersOperator.Update(defaultOperator);
            }

            cx.ChangeTracker.DetectChanges();
            await cx.SaveChangesAsync(ct);
            await trx.CommitAsync(ct);
        }
        catch
        {
            await trx.RollbackAsync(ct);
            throw;
        }
    }

    /// <summary>
    /// Removes all users that are marked as deleted from the database asynchronously.
    /// </summary>
    /// <param name="cx">The <see cref="DefaultDbContext"/> instance representing the database context.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous user cleanup operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="cx"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the user cleanup operation fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported for user cleanup operations.</exception>
    /// <exception cref="NotImplementedException">Thrown when the user cleanup operation is not implemented for the current provider (e.g., PostgreSQL).</exception>
    /// <remarks>
    /// This method specifically targets users that have been marked for deletion (IsDeleted=1) and permanently removes them from the database
    /// along with all their associated data including financial records, sessions, and transactions.
    /// <para><strong>Provider Support:</strong></para>
    /// <list type="bullet">
    /// <item><description><strong>SQL Server:</strong> Fully implemented with comprehensive user data cleanup including related financial and session data.</description></item>
    /// <item><description><strong>PostgreSQL:</strong> Not implemented - throws <see cref="NotImplementedException"/>.</description></item>
    /// </list>
    /// <para><strong>Warning:</strong> This operation permanently deletes user data and cannot be undone. Ensure proper backups before execution.</para>
    /// </remarks>
    public static Task CleanupUsers(this DefaultDbContext cx, CancellationToken ct) =>
        cx.Database.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.CleanupUsers(cx, ct),
            Provider.Type.PostgreSql => PostgreSql.CleanupUsers(cx, ct),
            _ => throw new NotSupportedException($"DML operation '{nameof(CleanupUsers)}' is not supported for the current database provider.")
        };
}
