using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gizmo.DAL
{
    /// <summary>
    /// Time-zone converter.
    /// </summary>
    /// <remarks>
    /// Used to convert all mapped DateTime properties in the database to UTC.
    /// </remarks>
    public class DateTimeTimeZoneConverter
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="context">DbContext.</param>
        /// <param name="sourceTimeZone">Source time zone.</param>
        public DateTimeTimeZoneConverter(DbContext context, TimeZoneInfo sourceTimeZone)
        {
            _context = context;
            _sourceTimeZone = sourceTimeZone;
        }

        private readonly DbContext _context;
        private readonly TimeZoneInfo _sourceTimeZone;

        /// <summary>
        /// Converts all DateTime properties in the database to UTC.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        public async Task ConvertToUtcAsync(CancellationToken cancellationToken = default)
        {
            var entityTypes = _context.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                var schemaName = entityType.GetSchema() ?? "dbo";
                var fullTableName = $"[{schemaName}].[{tableName}]";

                var dateTimeProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
                    .ToList();

                if (!dateTimeProperties.Any())
                    continue;

                foreach (var property in dateTimeProperties)
                {
                    var columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName, null));

                    if (columnName == null)
                        continue;

                    var sql = $@"UPDATE {fullTableName}
                                     SET {columnName} = CAST({columnName} AS datetimeoffset)
                                     AT TIME ZONE '{_sourceTimeZone.StandardName}'
                                     AT TIME ZONE 'UTC'
                                     WHERE {columnName} IS NOT NULL";

                    await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
                }
            }
        }
    }
}
