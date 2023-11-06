using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Gizmo.DAL.EFCore
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
        public static IndexBuilder HasNullableFilter<T>(this IndexBuilder<T> builder, DatabaseFacade databaseFacade) where T : class
        {
            var indexProperties = builder.Metadata.Properties;

            if (indexProperties.Count == 0)
                throw new InvalidOperationException("Index must have at least one property.");

            if (databaseFacade.IsSqlServer() || databaseFacade.IsNpgsql())
            {
                Span<char> indexFilterSymbols = stackalloc char[0];

                foreach (var indexProperty in indexProperties)
                {
                    if (!indexProperty.IsNullable)
                        throw new InvalidOperationException("Index property must be nullable for the nullable filter to work.");

                    var index = $"[{indexProperty.Name}] IS NOT NULL AND ".AsSpan();
                    
                    if(indexFilterSymbols.Length == 0)
                    {
                        indexFilterSymbols = index.ToArray();
                    }
                    else
                    {
                        Span<char> temp = stackalloc char[indexFilterSymbols.Length + index.Length];
                        indexFilterSymbols.CopyTo(temp);
                        index.CopyTo(temp[indexFilterSymbols.Length..]);
                        indexFilterSymbols = temp;
                    }
                }

                builder.HasFilter(indexFilterSymbols[..^5].ToString());
            }
            else
            {
                throw new NotSupportedException("Database provider is not supported.");
            }

            return builder;
        }
    }
}
