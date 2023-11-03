using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

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
        /// <param name="accessor">Property accessor.</param>
        /// <param name="databaseFacade">Database facade.</param>
        /// <returns>Index builder.</returns>
        public static IndexBuilder HasNullableFilter<T>(this IndexBuilder<T> builder, Expression<Func<T, object>> accessor, DatabaseFacade databaseFacade) where T : class
        {
            var accessorBody = accessor.Body;
            if (accessorBody is UnaryExpression unaryExpression
              && unaryExpression.NodeType == ExpressionType.Convert
              && unaryExpression.Type == typeof(object))
            {
                accessorBody = unaryExpression.Operand;
            }

            if (accessorBody is MemberExpression memberExpression)
            {
                var value = memberExpression.Member.Name;                
                if(databaseFacade.IsSqlServer())
                {
                    builder.HasFilter($"[{value}] IS NOT NULL");
                }
            }

            return builder;
        }
    }
}
