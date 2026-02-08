using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Microsoft.Extensions.Options;

namespace Gizmo.DAL
{
    /// <summary>
    /// An contract providing access to database options service implementation.
    /// </summary>
    /// <remarks>
    /// <b>This service is registered and used only by <see cref="DbInitializer"/>.</b>
    /// </remarks>
    public interface IDatabaseOptionsInitializeAccess
    {
        /// <summary>
        /// Writes options to database.
        /// </summary>
        /// <typeparam name="TOptions">Options type.</typeparam>
        /// <param name="dbContext">Database context.</param>
        /// <param name="options">Options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task WriteAsync<TOptions>(DefaultDbContext dbContext, TOptions options, CancellationToken cancellationToken = default) where TOptions : class, IStoreOptions;
    }
}
