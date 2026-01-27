using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions.DmlExtensions;
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
    /// Performs a full database cleanup according to the supplied flags.
    /// </summary>
    /// <param name="cx">The <see cref="DefaultDbContext"/> instance representing the database context.</param>
    /// <param name="deleteUsers">When <c>true</c>, user-related data will be removed.</param>
    /// <param name="deleteHosts">When <c>true</c>, host-related data will be removed.</param>
    /// <param name="deleteOperators">When <c>true</c>, operator-related data will be removed.</param>
    /// <param name="deleteProducts">When <c>true</c>, product-related data will be removed.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task that completes when cleanup finishes.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the provider-specific cleanup script or EF post-cleanup fails.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported.</exception>
    /// <remarks>
    /// <para>This method builds and executes a provider-specific SQL cleanup script (single script execution)
    /// inside a transaction using Serializable isolation. After the script executes, the method performs a
    /// small set of Entity Framework operations to bring the DbContext into a consistent state: it removes
    /// any guest/member EF entities when user cleanup is requested and, when operator cleanup is requested,
    /// removes operator entities and creates a default operator account (username: "Admin").</para>
    /// <para>Provider support: SQL Server and PostgreSQL implementations are available and are invoked via
    /// <c>SqlServer.CleanupScript</c> / <c>PostgreSql.CleanupScript</c>. The method temporarily disables
    /// change tracking and sets a long command timeout to improve performance for large deletes.</para>
    /// <para><strong>Safety:</strong> This operation permanently deletes data. Always backup the database
    /// before running cleanup operations in production or against important datasets. Prefer executing
    /// these scripts only in isolated test environments unless you are certain of the outcomes.</para>
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
                // Clear change tracker to avoid conflicts with raw SQL operations
                cx.ChangeTracker.Clear();
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

                var allPermissions = Gizmo.Server.Security.PolicesBuilder.Claims().Select(claim => new Entities.UserPermission { Type = claim.Resource, Value = claim.Operation });

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
    /// Permanently removes users that are marked as deleted and their dependent data.
    /// </summary>
    /// <param name="cx">The <see cref="DefaultDbContext"/> instance representing the database context.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task that completes when user cleanup finishes.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the provider-specific user cleanup script is empty or when EF state operations fail.</exception>
    /// <exception cref="NotSupportedException">Thrown when the database provider is not supported.</exception>
    /// <remarks>
    /// <para>This method builds and executes a provider-specific SQL script returned by
    /// <c>SqlServer.CleanupUsersScript()</c> or <c>PostgreSql.CleanupUsersScript()</c>. The script deletes
    /// users that have the <c>IsDeleted</c> flag set along with their dependent records in a single script
    /// executed inside a Serializable transaction. The method temporarily disables change tracking and sets
    /// a long command timeout to improve performance on large datasets.</para>
    /// <para>Both SQL Server and PostgreSQL providers are supported. The implementation validates that the
    /// provider returned a non-empty script and will throw <see cref="InvalidOperationException"/> if it did
    /// not.</para>
    /// <para><strong>Warning:</strong> This operation permanently deletes user data. Ensure backups exist
    /// and execute only against test or maintenance environments unless you have explicit authorization
    /// to perform data removal in production.</para>
    /// </remarks>
    public static async Task CleanupUsers(this DefaultDbContext cx, CancellationToken ct)
    {
        var cleanupScript = cx.Database.GetProviderType() switch
        {
            Provider.Type.SqlServer => SqlServer.CleanupUsersScript(),
            Provider.Type.PostgreSql => PostgreSql.CleanupUsersScript(),
            _ => throw new NotSupportedException($"DML operation '{nameof(CleanupUsers)}' is not supported for the current database provider.")
        };

        if (string.IsNullOrWhiteSpace(cleanupScript))
        {
            throw new InvalidOperationException("The cleanup script is empty. Ensure the provider supports cleanup operations.");
        }

        await using var trx = await cx.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);

        try
        {
            cx.ChangeTracker.AutoDetectChangesEnabled = false;
            cx.Database.SetCommandTimeout(3600);

            await cx.Database.ExecuteSqlRawAsync(cleanupScript, ct);

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
}
