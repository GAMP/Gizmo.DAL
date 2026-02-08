using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Scripts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Gizmo.DAL.Extensions
{
    /// <summary>
    /// Default database context extensions.
    /// </summary>
    public static class DefaultDbContextExtensions
    {
        /// <summary>
        /// Updates database to target migrations.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <param name="migrationName">
        /// Target migration name.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static async Task MigrateToAsync(this DefaultDbContext dbContext, string migrationName, CancellationToken cToken = default)
        {
            var migrator = dbContext.Database.GetInfrastructure().GetRequiredService<IMigrator>();

            await migrator.MigrateAsync(migrationName, cToken);
        }

        /// <summary>
        /// Upgrades the database to the last state on Entity Framework 6.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static async Task UpgradeDatabaseToLastEF6StateAsync(this DefaultDbContext dbContext, CancellationToken cToken = default)
        {
            await dbContext.MigrateToAsync("Initial", cToken);

            var migrationDbContext = dbContext.WithEF6Migrations();

            var migrator = migrationDbContext.Database.GetInfrastructure().GetRequiredService<IMigrator>();

            await migrator.MigrateAsync(Migration.InitialDatabase, cToken);
        }

        /// <summary>
        /// Returns new database context with EF6 migrations.
        /// </summary>
        /// <param name="dbContext">
        /// Database context.
        /// </param>
        /// <returns>
        /// New database context with EF6 migrations.
        /// </returns>
        public static DefaultDbContext WithEF6Migrations(this DefaultDbContext dbContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            var connectionString = dbContext.Database.GetConnectionString();
            var commandTimeout = dbContext.Database.GetCommandTimeout();

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout(commandTimeout);
                options.MigrationsAssembly("Gizmo.DAL.Migrations.EF6.MSSQL");
            });

            return new(optionsBuilder.Options);
        }

        /// <summary>
        /// Executes the SQL against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// </summary>
        /// <typeparam name="T">
        /// Type of DbSet.
        /// </typeparam>
        /// <param name="dbContext">
        /// Default database context.
        /// </param>
        /// <param name="scriptName">
        /// SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <returns>
        /// IQueryable of T.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        public static IQueryable<T> FromSqlScript<T>(this DefaultDbContext dbContext, string scriptName, Dictionary<string, object> parameters) where T : class
            => dbContext.Database.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbContext.Set<T>().FromSqlRaw(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbContext.Set<T>().FromSqlRaw(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()),
                _ => throw new NotSupportedException($"Database provider {dbContext.Database.ProviderName} is not supported for this sql command."),
            };

        /// <summary>
        /// Executes the SQL against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// </summary>
        /// <typeparam name="T"> Type of DbSet. </typeparam>
        /// <param name="dbContext"> Default database context. </param>
        /// <param name="scriptName"> SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs. </param>
        /// <param name="parameters"> Sql parameters for the script. Key is parameter name, value is parameter value. </param>
        /// <param name="cToken"> Cancellation token. </param>
        /// <returns> Identifiers array of script result. </returns>
        public static Task<int[]> FromSqlScriptToIdsAsync(this DefaultDbContext dbContext, string scriptName, Dictionary<string, object> parameters, CancellationToken cToken)
            => dbContext.Database.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => dbContext.Database.SqlQueryRaw<int>(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => dbContext.Database.SqlQueryRaw<int>(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                _ => throw new NotSupportedException($"Database provider {dbContext.Database.ProviderName} is not supported for this sql command."),
            };

        /// TODO: Check this method for correctness
        public static async Task<T[]> FromSqlScriptToModelsAsync<T>(this DefaultDbContext dbContext, string scriptName, Dictionary<string, object> parameters, CancellationToken cToken = default)
            where T : class
        {
            var result = dbContext.Database.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => await dbContext.Database.SqlQueryRaw<string>(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => await dbContext.Database.SqlQueryRaw<string>(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                _ => throw new NotSupportedException($"Database provider {dbContext.Database.ProviderName} is not supported for this sql command."),
            };

            if (result.Length == 0)
                return [];

            var data = result.Length > 1
                ? string.Join("", result)
                : result[0];

            return JsonSerializer.Deserialize<T[]>(data);
        }

        private sealed class PaginatedResult<T>
        {
            public int Total { get; set; }
            public T[] Items { get; set; }
        }

        /// <summary>
        /// Executes the SQL with paginated result against the database, choosing it from the file of the 'Gizmo file.DAL.Scripts' namespace depends on the database provider.
        /// Uses a classic pagination algorithm.
        /// </summary>
        /// <typeparam name="T">
        /// Any class to be returned.
        /// </typeparam>
        /// <param name="dbContext">
        /// Default database context.
        /// </param>
        /// <param name="scriptName">
        /// SQL script name from the Gizmo.DAL.Scripts.SQLScripts.cs.
        /// </param>
        /// <param name="pageNumber">
        /// Pagination page number.
        /// </param>
        /// <param name="pageSize">
        /// Pagination page size.
        /// </param>
        /// <param name="sortBy">
        /// Sort by column (REQUIRED).
        /// </param>
        /// <param name="isAsc">
        /// Sort direction.
        /// </param>
        /// <param name="parameters">
        /// Sql parameters for the script. Key is parameter name, value is parameter value.
        /// </param>
        /// <param name="cToken">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Paginated array of T and total items.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Database provider is not supported for this SQL script name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Order by column is required for pagination.
        /// </exception>
        /// <exception cref="JsonException">
        /// Invalid json data from the SQL script.
        /// </exception>
        /// <remarks>
        /// THIS FUNCTION DOESN'T SUPPORT SortableAttribute.
        /// </remarks>
        public static async Task<(int Total, T[] Items)> FromPaginatedSqlScript<T>(
            this DefaultDbContext dbContext,
            string scriptName,
            int pageNumber,
            int pageSize,
            string sortBy,
            bool isAsc,
            Dictionary<string, object> parameters,
            CancellationToken cToken = default)
        where T : class
        {
            var type = typeof(T);

            if (type == typeof(string) || type == typeof(char))
                throw new NotSupportedException("Type string and char are not supported for this function.");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException(nameof(sortBy), "Order by column is required for pagination.");

            var orderProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (!orderProperties.Any(x => x.Name == sortBy))
                throw new NotSupportedException($"Order by column '{sortBy}' is not supported for return type '{type.Name}'. Case sensitive.");

            if (pageNumber < 1)
                pageNumber = 1;
            else if (pageNumber == int.MaxValue)
                pageNumber = int.MaxValue - 1;

            if (pageSize < 1)
                pageSize = 10;
            else if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            var offset = pageSize * (pageNumber - 1);

            if (offset < 0)
                offset = 0;

            var sortOrder = isAsc ? "ASC" : "DESC";

            parameters.Add("Limit", pageSize);
            parameters.Add("Offset", offset);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortOrder", sortOrder);

            var result = dbContext.Database.ProviderName switch
            {
                "Microsoft.EntityFrameworkCore.SqlServer" => await dbContext.Database.SqlQueryRaw<string>(
                    MsSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new SqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                "Npgsql.EntityFrameworkCore.PostgreSQL" => await dbContext.Database.SqlQueryRaw<string>(
                    NpgSqlScripts.GetScript(scriptName),
                    parameters.Select(x => new Npgsql.NpgsqlParameter(x.Key, x.Value ?? DBNull.Value)).ToArray()).ToArrayAsync(cToken),
                _ => throw new NotSupportedException($"Database provider {dbContext.Database.ProviderName} is not supported for this sql command."),
            };

            if (result.Length == 0)
                return (0, []);

            var data = result.Length > 1
                ? string.Join("", result)
                : result[0];

            var paginatedResult = JsonSerializer.Deserialize<PaginatedResult<T>>(data);

            return (paginatedResult.Total, paginatedResult.Items);
        }
    }
}
