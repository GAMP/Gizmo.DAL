using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gizmo.DAL.Extensions;

/// <summary>
/// Provides extension methods for database operations using Entity Framework Core.
/// </summary>
/// <remarks>
/// These methods extend <see cref="DatabaseFacade"/> to provide additional database management capabilities such as backup and restore.
/// </remarks>
public static class DatabaseExtensions
{
    /// <summary>
    /// Restores a database from the specified backup file.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="path">The file path to the backup file to restore from.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous restore operation.</returns>
    /// <exception cref="NotImplementedException">This functionality is not implemented yet.</exception>
    /// <remarks>
    /// This method is intended to restore the database from a backup file. Implementation is pending.
    /// </remarks>
    public static Task Restore(this DatabaseFacade facade, string path, CancellationToken ct)
    {
        throw new NotImplementedException("Database restore functionality is not implemented yet.");
    }
    
    /// <summary>
    /// Creates a backup of the database to the specified location.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <param name="path">The file path where the backup will be stored.</param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous backup operation.</returns>
    /// <exception cref="NotImplementedException">This functionality is not implemented yet.</exception>
    /// <remarks>
    /// This method is intended to create a backup of the current database. Implementation is pending.
    /// </remarks>
    public static Task Backup(this DatabaseFacade facade, string path, CancellationToken ct)
    {
        throw new NotImplementedException("Database backup functionality is not implemented yet.");
    }
    
    /// <summary>
    /// Gets a Database Name.
    /// </summary>
    /// <param name="facade">The <see cref="DatabaseFacade"/> instance representing the database connection.</param>
    /// <returns>The name of the database.</returns>
    public static string GetName(this DatabaseFacade facade)
    {
        return facade.GetConnectionString();
    }
}
