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
            var sqlTimeZoneName = GetSqlServerTimeZoneName(_sourceTimeZone);

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

                    // The column must not be cast to datetimeoffset first: AT TIME ZONE assigns the zone offset only to a value
                    // that has none, an offset-carrying value is converted to the zone and back to UTC, leaving it unchanged.
                    // Values within a day of the datetime2 minimum or maximum are placeholders, not timestamps: shifting them
                    // overflows (error 9813), so they are left as they are. The bounds are typed because a plain string bound
                    // is converted to the column type and is out of range for a datetime column.
                    var sql = $@"UPDATE {fullTableName}
                                     SET {columnName} = {columnName}
                                     AT TIME ZONE '{sqlTimeZoneName}'
                                     AT TIME ZONE 'UTC'
                                     WHERE {columnName} IS NOT NULL
                                     AND {columnName} >= CAST('0001-01-02' AS datetime2)
                                     AND {columnName} < CAST('9999-12-31' AS datetime2)";

                    await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
                }
            }
        }

        /// <summary>
        /// Gets the time zone name SQL Server AT TIME ZONE accepts (a Windows time zone id, see sys.time_zone_info).
        /// </summary>
        /// <param name="timeZone">Time zone.</param>
        /// <returns>Windows time zone id.</returns>
        /// <exception cref="NotSupportedException">thrown if the time zone cannot be mapped to a Windows time zone id.</exception>
        private static string GetSqlServerTimeZoneName(TimeZoneInfo timeZone)
        {
            // On Windows the id already is a Windows id, on Linux it is an IANA id and has to be mapped.
            if (!timeZone.HasIanaId)
                return timeZone.Id;

            if (TimeZoneInfo.TryConvertIanaIdToWindowsId(timeZone.Id, out var windowsId))
                return windowsId;

            throw new NotSupportedException($"Time zone '{timeZone.Id}' cannot be mapped to the Windows time zone id required by SQL Server.");
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
