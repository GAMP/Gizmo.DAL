using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Entities;
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
        public async Task ConvertToUtcAsync(CancellationToken cancellationToken = default)
        {
            // SQL Server's AT TIME ZONE only accepts Windows time zone ids (sys.time_zone_info).
            // TimeZoneInfo.Id is the Windows id on Windows, but an IANA id (e.g. "Europe/Istanbul")
            // on Linux, and TimeZoneInfo.StandardName on Linux/ICU is a display string such as
            // "GMT+03:00" that AT TIME ZONE rejects (error 9820), crashing the v2 upgrade.
            var sqlTimeZoneId = TimeZoneInfo.TryConvertIanaIdToWindowsId(_sourceTimeZone.Id, out var windowsTimeZoneId)
                ? windowsTimeZoneId
                : _sourceTimeZone.Id;

            var entityTypes = _context.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                var schemaName = entityType.GetSchema() ?? "dbo";
                var fullTableName = $"[{schemaName}].[{tableName}]";

                var dateTimeProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
                    .ToList();

                if (dateTimeProperties.Count == 0)
                    continue;

                foreach (var property in dateTimeProperties)
                {
                    if (IsUnspecifiedDateTimeProperty(property))
                        continue;

                    var columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName, null));

                    if (columnName == null)
                        continue;

                    var sql = $@"UPDATE {fullTableName}
                                     SET {columnName} = CAST({columnName} AS datetimeoffset)
                                     AT TIME ZONE '{sqlTimeZoneId}'
                                     AT TIME ZONE 'UTC'
                                     WHERE {columnName} IS NOT NULL";

                    await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
                }
            }
        }

        private static bool IsUnspecifiedDateTimeProperty(IProperty mutableProperty)
        {
            if (mutableProperty.DeclaringType.ClrType.IsSubclassOf(typeof(PeriodDate)))
                return true;

            if (mutableProperty.DeclaringType.ClrType == typeof(News))
            {
                if (mutableProperty.Name == nameof(News.StartDate) || mutableProperty.Name == nameof(News.EndDate))
                    return true;
            }

            if (mutableProperty.DeclaringType.ClrType == typeof(User))
            {
                if (mutableProperty.Name == nameof(User.BirthDate))
                    return true;
            }
                
            if (mutableProperty.DeclaringType.ClrType == typeof(App))
            {
                if (mutableProperty.Name == nameof(App.ReleaseDate))
                    return true;
            }

            return false;
        }
    }
}
