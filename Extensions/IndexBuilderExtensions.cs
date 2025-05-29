using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizmo.DAL.Extensions
{
    /// <summary>
    /// Database facade extensions.
    /// </summary>
    public static class IndexBuilderExtensions
    {
        /// <summary>
        /// Creates nullable filtered index.
        /// </summary>
        /// <typeparam name="T">Index entity type.</typeparam>
        /// <param name="builder">Index builder.</param>
        /// <param name="databaseFacade">Database facade.</param>
        /// <returns>Index builder.</returns>
        public static IndexBuilder HasNullableFilter<T>(this IndexBuilder<T> builder,
            DatabaseFacade databaseFacade) where T : class
        {
            var indexProperties = builder.Metadata.Properties;

            if (indexProperties.Count == 0)
                throw new InvalidOperationException("Index must have at least one property.");

            if (!databaseFacade.IsSqlServer() && !databaseFacade.IsNpgsql())
                throw new NotSupportedException("Database provider is not supported.");

            var filters = indexProperties.Select(p =>
            {
                var columnName = databaseFacade.IsSqlServer()
                    ? $"[{p.Name}]"
                    : $"\"{p.Name}\""; // For PostgreSQL

                if (!p.IsNullable)
                    throw new InvalidOperationException($"Property '{p.Name}' must be nullable to be used in a nullable filtered index.");

                return $"{columnName} IS NOT NULL";
            });

            var filter = string.Join(" AND ", filters);
            builder.HasFilter(filter);

            return builder;
        }
    }
}
