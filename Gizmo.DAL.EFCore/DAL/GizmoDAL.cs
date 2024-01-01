using CoreLib;

using Gizmo.DAL.Contexts;
using Gizmo.DAL.DTO;

using IntegrationLib;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using Npgsql;

using SharedLib;
using SharedLib.Configuration;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Gizmo.DAL.EFCore.Extensions;
using Gizmo.DAL.Scripts;

namespace Gizmo.DAL
{
    #region GIZMODATABASE
    /// <summary>
    /// Gizmo database class.
    /// </summary>
    public partial class GizmoDatabase
    {
        private readonly IGizmoDbContextProviderConcrete _dbContextProvider;
        
        #region CONSTRUCTOR

        /// <summary>
        /// New database instance.
        /// </summary>
        public GizmoDatabase(DatabaseType type, string cn, int? commandTimeout) : this(type, cn)
        {
            CommandTimeout = commandTimeout;

            var dbConfig = new ServiceDatabaseConfig
            {
                DbConnectionString = cn,
                DbType = type,
                CommandTimeout = commandTimeout
            };

            _dbContextProvider = new GizmoDbContextProviderConcrete(dbConfig);
        }

        /// <summary>
        /// New database instance.
        /// </summary>
        public GizmoDatabase(DatabaseType type, string cn)
        {
            if (string.IsNullOrWhiteSpace(cn))
                throw new ArgumentNullException(nameof(cn));

            DatabaseType = type;
            ConnectionString = cn;

            var dbConfig = new ServiceDatabaseConfig
            {
                DbConnectionString = cn,
                DbType = type
            };

            _dbContextProvider = new GizmoDbContextProviderConcrete(dbConfig);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets database type.
        /// </summary>
        public DatabaseType DatabaseType
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets connection string.
        /// </summary>
        public string ConnectionString
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets database command timeout.
        /// </summary>
        public int? CommandTimeout
        {
            get; set;
        }

        /// <summary>
        /// Gets current principal.
        /// </summary>
        public IDispatcherPrincipal CurrentPrincipal
        {
            get { return System.Threading.Thread.CurrentPrincipal as IDispatcherPrincipal; }
        }

        /// <summary>
        /// Gets current user id.
        /// </summary>
        public int? CurrentUserId
        {
            get { return CurrentPrincipal != null ? CurrentPrincipal.UserIdentity.UserId : (int?)null; }
        }

        /// <summary>
        /// Checks if no pending database migrations exist.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsLatestVersion
        {
            get
            {
                using var cx = GetDbNonProxyContext();
                var pendingChanges = cx.Database.GetPendingMigrations();
                return pendingChanges.Any() == false;
            }
        }

        #endregion

        #region CONTEXT

        /// <summary>
        /// Initiate Database Context
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DefaultDbContext GetDbContext(string connectionString)
        {
            var dbConfig = new ServiceDatabaseConfig
            {
                DbType = DatabaseType,
                DbConnectionString = connectionString,
                CommandTimeout = CommandTimeout
            };

            return _dbContextProvider.GetDbContext(dbConfig);
        }

        /// <summary>
        /// Gets new dbcontext instance based on current configuration.
        /// </summary>
        public DefaultDbContext GetDbContext()
        {
            return GetDbContext(ConnectionString);
        }

        /// <summary>
        /// Gets default non proxy db context.
        /// </summary>
        /// <returns>Default db context.</returns>
        public DefaultDbContext GetDbNonProxyContext(string connectionString)
        {
            var context = GetDbContext(connectionString);
            return context;
        }

        /// <summary>
        /// Gets default non proxy db context.
        /// </summary>
        /// <returns>Default db context.</returns>
        public DefaultDbContext GetDbNonProxyContext()
        {
            var context = GetDbContext();
            return context;
        }

        /// <summary>
        ///  Get SQL Connection Builder
        /// </summary>
        /// <returns></returns>
        private SqlConnectionStringBuilder GetSQLConnectionBuilder()
        {
            return new SqlConnectionStringBuilder(ConnectionString);
        }

        /// <summary>
        /// Get Postgre Connection Builder
        /// </summary>
        /// <returns></returns>
        private NpgsqlConnectionStringBuilder GetPostgreConnectionBuilder()
        {
            return new NpgsqlConnectionStringBuilder(ConnectionString);
        }

        /// <summary>
        /// Gets current database name based on connection string.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                switch (DatabaseType)
                {
                    case DatabaseType.MSSQL:
                    case DatabaseType.MSSQLEXPRESS:
                    case DatabaseType.LOCALDB:
                        return GetSQLConnectionBuilder().InitialCatalog;

                    case DatabaseType.POSTGRE:
                        return GetPostgreConnectionBuilder().Database;

                    default:
                        throw new NotSupportedException(nameof(DatabaseType));
                }
            }
        }

        #endregion

        #region DATABASE

        /// <summary>
        /// Creates a new database on the database server for the model defined in the
        /// backing context, but only if a database with the same name does not already exist on the server.
        /// </summary>
        /// <returns>
        /// True if the database did not exist and was created; false otherwise.
        /// </returns>
        public bool CreateIfNotExists()
        {
            using var cx = GetDbContext();
            return cx.Database.EnsureCreated();
        }

        /// <summary>
        /// Drops existing database and creates a new one.
        /// </summary>
        public void DropCreate()
        {
            using var cx = GetDbContext();

            if (cx.Database.GetService<IRelationalDatabaseCreator>().Exists())
                cx.Database.EnsureDeleted();

            cx.Database.EnsureCreated();
        }

        /// <summary>
        /// Drops database if exists.
        /// </summary>
        public void DropIfExists()
        {
            using var cx = GetDbContext();
            cx.Database.EnsureDeleted();
        }

        /// <summary>
        /// Check if database exists.
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            using var cx = GetDbContext();
            return cx.Database.GetService<IRelationalDatabaseCreator>().Exists();
        }

        // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //
        ///// <summary>
        ///// Gets migrator for current database.
        ///// </summary>
        ///// <returns>New migrator instance.</returns>
        //public DbMigrator GetMigrator()
        //{
        //    switch (DatabaseType)
        //    {
        //        case DatabaseType.LOCALDB:
        //        case DatabaseType.MSSQL:
        //        case DatabaseType.MSSQLEXPRESS:
        //            return new DbMigrator(new Migrations.MSSQLConfiguration());
        //        default:
        //            throw new ArgumentException("Invalid database type", nameof(DatabaseType));
        //    }
        //}

        /// <summary>
        /// Backups the associated database.
        /// </summary>
        /// <param name="fileName">Backup file name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public Task BackupAsync(string fileName, CancellationToken ct)
        {
            return BackupAsync(DatabaseName, fileName, ct);
        }

        public void Backup(string fileName)
        {
            Backup(DatabaseName, fileName);
        }

        public void Backup(string databaseName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (!Path.IsPathRooted(fileName))
                throw new ArgumentException("Only full paths allowed.", nameof(fileName));

            switch (DatabaseType)
            {
                case DatabaseType.LOCALDB:
                case DatabaseType.MSSQL:
                case DatabaseType.MSSQLEXPRESS:
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!Path.IsPathRooted(fileName))
                throw new ArgumentException("Only full paths allowed.", nameof(fileName));

            string SQL_COMMAND_STRING = $"BACKUP DATABASE [{databaseName}] TO DISK = '{fileName}' WITH FORMAT,CHECKSUM,COPY_ONLY";

            var sb = new SqlConnectionStringBuilder(ConnectionString)
            {
                //InitialCatalog = string.Empty
            };
            using (var cx = GetDbContext(sb.ToString()))
            {
                // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //
                //cx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, SQL_COMMAND_STRING);

                cx.Database.ExecuteSqlRaw(SQL_COMMAND_STRING);
            }
        }

        /// <summary>
        /// Backups associated database.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        /// <param name="fileName">Backup file name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task BackupAsync(string databaseName, string fileName, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (!Path.IsPathRooted(fileName))
                throw new ArgumentException("Only full paths allowed.", nameof(fileName));

            switch (DatabaseType)
            {
                case DatabaseType.LOCALDB:
                case DatabaseType.MSSQL:
                case DatabaseType.MSSQLEXPRESS:
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!Path.IsPathRooted(fileName))
                throw new ArgumentException("Only full paths allowed.", nameof(fileName));

            string SQL_COMMAND_STRING = $"BACKUP DATABASE [{databaseName}] TO DISK = '{fileName}' WITH FORMAT,CHECKSUM,COPY_ONLY";

            var sb = new SqlConnectionStringBuilder(ConnectionString)
            {
                //InitialCatalog = string.Empty
            };
            using (var cx = GetDbContext(sb.ToString()))
            {
                //make the timeout infinite, 0 supposed to be the value to indicate that
                cx.Database.SetCommandTimeout(0);

                // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //
                //await cx.Database.ExecuteSqlRawAsync(TransactionalBehavior.DoNotEnsureTransaction, SQL_COMMAND_STRING, ct);

                await cx.Database.ExecuteSqlRawAsync(SQL_COMMAND_STRING, ct);
            }
        }

        /// <summary>
        /// Restores associated database.
        /// </summary>
        /// <param name="fileName">Backup file name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public Task RestoreAsync(string fileName, CancellationToken ct)
        {
            return RestoreAsync(DatabaseName, fileName, ct);
        }

        /// <summary>
        /// Restores database.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        /// <param name="fileName">Backup file name.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task RestoreAsync(string databaseName, string fileName, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (!Path.IsPathRooted(fileName))
                throw new ArgumentException("Only full paths allowed.", nameof(fileName));

            string INSTANCE_DATA_PATH = null;
            string INSTANCE_LOG_PATH = null;

            string FILE_LIST_STRING = "RESTORE FILELISTONLY FROM  DISK = '{0}'";
            string SQL_FILE_LIST_STRING = string.Format(FILE_LIST_STRING, fileName);

            string BACKUP_DATA_LOGICAL_NAME = null;
            string BACKUP_DATA_FILE_NAME = null;
            string BACKUP_LOG_FILE_NAME = null;
            string BACKUP_LOG_LOGICAL_NAME = null;

            var sb = new SqlConnectionStringBuilder(ConnectionString)
            {
                InitialCatalog = string.Empty
            };
            var CONNECTION_STRING = sb.ToString();

            using (var cx = GetDbContext(CONNECTION_STRING))
            {
                // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //
                //var PATHS = await cx.
                //    SqlQuery<SQLInstancePath>("SELECT SERVERPROPERTY('INSTANCEDEFAULTDATAPATH') as [DataPath], SERVERPROPERTY('INSTANCEDEFAULTLOGPATH') as [LogPath];").ToListAsync();

                var PATHS = new List<SQLInstancePath>();

                INSTANCE_DATA_PATH = PATHS[0].DataPath;
                INSTANCE_LOG_PATH = PATHS[0].LogPath;

                // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //
                //var FILES = await cx.Database.SqlQuery<SQLFileList>(SQL_FILE_LIST_STRING).ToListAsync();
                var FILES = new List<SQLFileList>();
                var DATA_ROW = FILES.FirstOrDefault(FILE => FILE.Type == "D");
                if (DATA_ROW == null)
                    throw new ArgumentException("Data file info not present in backup.");

                BACKUP_DATA_LOGICAL_NAME = DATA_ROW.LogicalName;
                BACKUP_DATA_FILE_NAME = DATA_ROW.PhysicalName;

                var LOG_ROW = FILES.FirstOrDefault(FILE => FILE.Type == "L");
                if (LOG_ROW == null)
                    throw new ArgumentException("Log file info not present in backup.");

                BACKUP_LOG_LOGICAL_NAME = LOG_ROW.LogicalName;
                BACKUP_LOG_FILE_NAME = LOG_ROW.PhysicalName;
            }

            string RESTORE_STRING = "RESTORE DATABASE {0} FROM DISK = '{1}' WITH CHECKSUM,REPLACE,";
            string SQL_COMMAND_STRING = string.Format(RESTORE_STRING, databaseName, fileName);

            string DATA_FILE_NAME = Path.Combine(INSTANCE_DATA_PATH, string.Format("{0}.mdf", databaseName));
            string LOG_FILE_NAME = Path.Combine(INSTANCE_LOG_PATH, string.Format("{0}_log.ldf", databaseName));
            string DATA_LOGICAL_NAME = string.Format("{0}_data", databaseName);
            string LOG_LOGICAL_NAME = string.Format("{0}_log", databaseName);

            string SQL_MOVE_STRING = string.Format("{0} MOVE '{1}' TO '{2}',MOVE '{3}' TO '{4}'",
                SQL_COMMAND_STRING,
                BACKUP_DATA_LOGICAL_NAME,
                DATA_FILE_NAME,
                BACKUP_LOG_LOGICAL_NAME,
                LOG_FILE_NAME);

            using (var cx = GetDbContext(CONNECTION_STRING))
            {
                //make the timeout infinite, 0 supposed to be the value to indicate that
                cx.Database.SetCommandTimeout(0);

                // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //

                //await cx.Database.ExecuteSqlRawAsync(TransactionalBehavior.DoNotEnsureTransaction, SQL_MOVE_STRING, ct);
                //await cx.Database.ExecuteSqlRawAsync(TransactionalBehavior.DoNotEnsureTransaction,
                //    string.Format("ALTER DATABASE [{0}] MODIFY FILE(NAME=N'{1}',NEWNAME=N'{2}')", databaseName, BACKUP_DATA_LOGICAL_NAME, DATA_LOGICAL_NAME),
                //    ct);
                //await cx.Database.ExecuteSqlRawAsync(TransactionalBehavior.DoNotEnsureTransaction,
                //    string.Format("ALTER DATABASE [{0}] MODIFY FILE(NAME=N'{1}',NEWNAME=N'{2}')", databaseName, BACKUP_LOG_LOGICAL_NAME, LOG_LOGICAL_NAME),
                //    ct);

                await cx.Database.ExecuteSqlRawAsync(SQL_MOVE_STRING, ct);
                await cx.Database.ExecuteSqlRawAsync(
                    string.Format("ALTER DATABASE [{0}] MODIFY FILE(NAME=N'{1}',NEWNAME=N'{2}')",
                    databaseName, BACKUP_DATA_LOGICAL_NAME, DATA_LOGICAL_NAME), ct);
                await cx.Database.ExecuteSqlRawAsync(string.Format("ALTER DATABASE [{0}] MODIFY FILE(NAME=N'{1}',NEWNAME=N'{2}')",
                    databaseName, BACKUP_LOG_LOGICAL_NAME, LOG_LOGICAL_NAME), ct);
            }
        }

        public Task CleanupAsync(bool deleteUsers,
            bool deleteHosts,
            bool deleteOperators,
            bool deleteProducts,
            CancellationToken ct)
        {
            using (var cx = GetDbNonProxyContext())
            {
                return CleanupAsync(cx, deleteUsers,
                    deleteHosts,
                    deleteOperators,
                    deleteProducts,
                    ct);
            }
        }

        public async Task CleanupAsync(DefaultDbContext cx,
            bool deleteUsers,
            bool deleteHosts,
            bool deleteOperators,
            bool deleteProducts,
            CancellationToken ct)
        {
            if (cx == null)
                throw new ArgumentNullException(nameof(cx));

            using (var trx = cx.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                cx.ChangeTracker.AutoDetectChangesEnabled = false;
                cx.Database.SetCommandTimeout(int.MaxValue);

                if (deleteHosts || deleteUsers)
                {
                    await cx.Database.DeleteFromAsync("AppStat", true, cToken: ct); 
                    await cx.Database.DeleteFromAsync("ReservationUser", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ReservationHost", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("Reservation", true, cToken: ct);
                }

                if (deleteHosts && !deleteUsers)
                {
                    await cx.Database.ExecuteSqlScriptAsync(SQLScripts.RESET_USERGUESTS, cToken: ct);
                }

                #region FINANCIAL

                await cx.Database.UpdateAsync("InvoiceLineExtended", new Dictionary<string, string> { { "BundleLineId", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("UsageSession", new Dictionary<string, string> { { "CurrentUsageId", "NULL" } }, cToken: ct);
                await cx.Database.UpdateAsync("ProductOLExtended", new Dictionary<string, string> { { "BundleLineId", "NULL" } }, cToken: ct);

                //USAGE SESSION
                await cx.Database.DeleteFromAsync("UsageRate", false, cToken: ct);
                await cx.Database.DeleteFromAsync("UsageTimeFixed", false, cToken: ct);
                await cx.Database.DeleteFromAsync("UsageTime", false, cToken: ct);
                await cx.Database.DeleteFromAsync("UsageUserSession", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Usage", true, cToken: ct);
                await cx.Database.DeleteFromAsync("UsageSession", true, cToken: ct);

                //USER SESSION
                await cx.Database.DeleteFromAsync("UserSessionChange", true, cToken: ct);
                await cx.Database.DeleteFromAsync("UserSession", true, cToken: ct);

                //REFUNDS
                await cx.Database.DeleteFromAsync("RefundInvoicePayment", false, cToken: ct);
                await cx.Database.DeleteFromAsync("RefundDepositPayment", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Refund", true, cToken: ct);

                //VOIDS
                await cx.Database.DeleteFromAsync("VoidInvoice", false, cToken: ct);
                await cx.Database.DeleteFromAsync("VoidDepositPayment", false, cToken: ct);
                await cx.Database.DeleteFromAsync("Void", true, cToken: ct);

                //INVOICE PAYMENTS
                await cx.Database.DeleteFromAsync("InvoicePayment", true, cToken: ct);

                //DEPOSIT PAYMENT
                await cx.Database.DeleteFromAsync("PaymentIntentDeposit", false, cToken: ct);
                await cx.Database.DeleteFromAsync("PaymentIntentOrder", false, cToken: ct);
                await cx.Database.DeleteFromAsync("PaymentIntent", true, cToken: ct);
                await cx.Database.DeleteFromAsync("DepositPayment", true, cToken: ct);

                //PAYMENTS
                await cx.Database.DeleteFromAsync("Payment", true, cToken: ct);

                //INVOICE
                await cx.Database.DeleteFromAsync("InvoiceLineProduct", false, cToken: ct);
                await cx.Database.DeleteFromAsync("InvoiceLineSession", false, cToken: ct);
                await cx.Database.DeleteFromAsync("InvoiceLineTime", false, cToken: ct);
                await cx.Database.DeleteFromAsync("InvoiceLineTimeFixed", false, cToken: ct);
                await cx.Database.DeleteFromAsync("InvoiceLineExtended", false, cToken: ct);
                await cx.Database.DeleteFromAsync("InvoiceLine", true, cToken: ct);
                await cx.Database.DeleteFromAsync("InvoiceFiscalReceipt", true, cToken: ct);
                await cx.Database.DeleteFromAsync("Invoice", true, cToken: ct);

                //ORDER
                await cx.Database.DeleteFromAsync("ProductOLTimeFixed", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductOLTime", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductOLSession", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductOLProduct", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductOLExtended", false, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductOL", true, cToken: ct);
                await cx.Database.DeleteFromAsync("ProductOrder", true, cToken: ct);

                //DEPOSIT TRANSACTION
                await cx.Database.DeleteFromAsync("DepositTransaction", true, cToken: ct);

                //POINT TRANSACTION
                await cx.Database.DeleteFromAsync("PointTransaction", true, cToken: ct);

                //STOCK TRANSACTION
                await cx.Database.DeleteFromAsync("StockTransaction", true, cToken: ct);

                //SHIFT COUNT
                await cx.Database.DeleteFromAsync("ShiftCount", true, cToken: ct);

                //REGISTER TRANSACTION
                await cx.Database.DeleteFromAsync("RegisterTransaction", true, cToken: ct);

                //FISCAL RECEIPTS
                await cx.Database.DeleteFromAsync("FiscalReceipt", true, cToken: ct);

                //SHIFT
                await cx.Database.DeleteFromAsync("Shift", true, cToken: ct);

                //REGISTER
                await cx.Database.DeleteFromAsync("Register", true, cToken: ct);

                #endregion

                #region PRODUCTS

                if (deleteProducts || deleteHosts)
                {
                    await cx.Database.DeleteFromAsync("ProductHostHidden", true, cToken: ct);
                }

                if (deleteProducts)
                {
                    await cx.Database.DeleteFromAsync("ProductImage", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTax", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductPeriod", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductPeriodDayTime", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductPeriodDay", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTimeHostDisallowed", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductUserDisallowed", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductUserPrice", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("BundleProduct", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTimeHostDisallowed", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTimePeriod", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTimePeriodDayTime", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTimePeriodDay", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductBundle", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("Product", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductTime", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductBaseExtended", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("ProductBase", true, cToken: ct);
                }

                #endregion

                #region USERS

                if (deleteUsers)
                {
                    await cx.Database.DeleteFromAsync("HostGroupWaitingLineEntry", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("AssetTransaction", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("AppRating", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("UserCreditLimit", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("UserAttribute", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("UserNote", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("Note", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("VerificationEmail", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("VerificationMobilePhone", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("Verification", true, cToken: ct);
                    await cx.Database.DeleteFromAsync("Token", true, cToken: ct);

                    cx.UsersGuest.RemoveRange(cx.UsersGuest);
                    cx.UsersMember.RemoveRange(cx.UsersMember);

                    //detect any changes made
                    cx.ChangeTracker.DetectChanges();

                    //save any changes made
                    await cx.SaveChangesAsync(ct);
                }

                #endregion

                #region HOSTS
                if (deleteHosts)
                {
                    await cx.Database.DeleteFromAsync("HostComputer", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("HostEndpoint", false, cToken: ct);
                    await cx.Database.DeleteFromAsync("Host", true, cToken: ct);
                }
                #endregion

                #region OPERATORS
                if (deleteOperators)
                {
                    if (!deleteUsers)
                    {
                        cx.ChangeTracker.DetectChanges();

                        await cx.Database.UpdateAsync("UserCreditLimit", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                        await cx.Database.UpdateAsync("UserPicture", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                        await cx.Database.UpdateAsync("UserAttribute", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                        await cx.Database.UpdateAsync("AssetTransaction", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" }, { "CheckedInById", "NULL" } }, cToken: ct);
                    }

                    if (!deleteHosts)
                    {
                        await cx.Database.UpdateAsync("Host", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    }

                    if (!deleteHosts && !deleteProducts)
                    {
                        await cx.Database.UpdateAsync("ProductHostHidden", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    }

                    await cx.Database.UpdateAsync("DeviceHost", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Device", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ReservationUser", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ReservationHost", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Reservation", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.DeleteFromAsync("Token", false, new Dictionary<string, string> { { "Type", "0" } }, ct);
                    await cx.Database.UpdateAsync("Token", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("App", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppCategory", new Dictionary<string, string>  { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppEnterprise", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExe", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExeCdImage", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExeDeployment", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExeImage", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExeLicense", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExeMaxUser", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExePersonalFile", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppExeTask", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppGroup", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppImage", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AppLink", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Asset", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("AssetType", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Attribute", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("BillProfile", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("BundleProduct", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("BundleProductUserPrice", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ClientTask", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Deployment", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Feed", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("HostGroup", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("HostGroupWaitingLine", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("HostGroupWaitingLineEntry", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("HostLayoutGroup", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("HostLayoutGroupImage", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("HostLayoutGroupLayout", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Icon", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("License", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("LicenseKey", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Mapping", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("MonetaryUnit", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("News", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Note", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("PaymentMethod", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("PersonalFile", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("PluginLibrary", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("PresetTimeSale", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("PresetTimeSaleMoney", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductBase", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductBundleUserPrice", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductGroup", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductImage", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductTax", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductTimeHostDisallowed", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductUserDisallowed", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("ProductUserPrice", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("SecurityProfile", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("SecurityProfilePolicy", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("SecurityProfileRestriction", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Setting", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("TaskBase", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Tax", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("UserGroup", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("UserGroupHostDisallowed", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("Variable", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("User", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("UserCredential", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);
                    await cx.Database.UpdateAsync("UserAgreement", new Dictionary<string, string> { { "CreatedById", "NULL" }, { "ModifiedById", "NULL" } }, cToken: ct);

                    cx.UserPermissions.RemoveRange(cx.UserPermissions.Where(permission => permission.User is Entities.UserOperator));
                    cx.UsersOperator.RemoveRange(cx.UsersOperator);

                    var defaultOperator = new Entities.UserOperator();

                    byte[] salt = cx.GetNewSalt();
                    byte[] password = cx.GetHashedPassword("admin", salt);

                    defaultOperator.UserCredential = new Entities.UserCredential();
                    defaultOperator.Username = "Admin";

                    defaultOperator.CreatedTime = DateTime.Now;
                    defaultOperator.UserCredential.Salt = salt;
                    defaultOperator.UserCredential.Password = password;

                    var allPermissions = ClaimTypeBase.GetClaimTypes().Select(claim =>
                    {
                        return new Entities.UserPermission() { Type = claim.Resource, Value = claim.Operation };
                    });

                    defaultOperator.Permissions.UnionWith(allPermissions);

                    cx.UsersOperator.Update(defaultOperator);
                    cx.SaveChanges();
                }
                #endregion

                //detect any changes made
                cx.ChangeTracker.DetectChanges();

                //save any changes made
                await cx.SaveChangesAsync(ct);

                //commit changes
                trx.Commit();
            }
        }

        //TODO Add invoice correcting functionality

        //using(var cx = GetDbNonProxyContext())
        //{
        //    var list = cx.Database.SqlQuery<int>("select InvoiceLineId FROM InvoiceLine").ToList();

        //    var extendedList = cx.Database.SqlQuery<int>("SELECT INVOICELINEID FROM INVOICELINEEXTENDED").ToList();

        //    var sessionList = cx.Database.SqlQuery<int>("SELECT INVOICELINEID FROM INVOICELINESESSION").ToList();
        //    var fixedList = cx.Database.SqlQuery<int>("SELECT INVOICELINEID FROM INVOICELINETIMEFIXED").ToList();

        //    var productList = cx.Database.SqlQuery<int>("SELECT INVOICELINEID FROM INVOICELINEPRODUCT").ToList();
        //    var timeList = cx.Database.SqlQuery<int>("SELECT INVOICELINEID FROM INVOICELINETIME").ToList();

        //    //list of extended records, entities using extended table are Prouduct and Product time.
        //    var orphanedExtended = extendedList.Where(id => !productList.Contains(id) && !timeList.Contains(id))
        //        .ToList();       

        //    foreach (var id in orphanedExtended)
        //    {
        //        cx.Database.ExecuteSqlCommand($"DELETE FROM INVOICELINEEXTENDED WHERE INVOICELINEID={id}");
        //        cx.Database.ExecuteSqlCommand($"DELETE FROM INVOICELINE WHERE INVOICELINEID={id}");
        //    }

        //    var l = cx.InvoiceLines.ToList();

        //}

        /// <summary>
        /// Truncates log and dependent tables.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result returned by the database after executing the command.</returns>
        public async Task<int> TruncateLogAsync(CancellationToken ct)
        {
            using (var cx = GetDbNonProxyContext())
            {
                return await TruncateLogAsync(cx, ct);
            }
        }

        /// <summary>
        /// Truncates log and dependent tables.
        /// </summary>
        /// <param name="cx">Database context.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result returned by the database after executing the command.</returns>
        public Task<int> TruncateLogAsync(DefaultDbContext cx, CancellationToken ct)
        {
            if (cx == null)
                throw new ArgumentNullException(nameof(cx));

            return cx.Database.ExecuteSqlScriptAsync(SQLScripts.TRUNCATE_LOGS, cToken: ct);
        }

        /// <summary>
        /// Imports data from external source.
        /// </summary>
        /// <param name="export">Data export.</param>
        /// <param name="options">Importing options.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task ImportAsync(ExternalExport export, ImportOptions options = default, CancellationToken ct = default)
        {
            //create new options value if one is not provided
            options ??= new ImportOptions();

            using (var cx = GetDbNonProxyContext())
            {
                var users = export.Users ?? Enumerable.Empty<UserImportInfo>();

                //normalize users
                //here we will need to check if all input parameters have valid lengths and convert them to maximum allowed lenghts
                var normalizedUsers = users.Select(user => new UserImportInfo()
                {
                    Username = user.Username.WithMaxLength(30, true),
                    Email = user.Email.WithMaxLength(254),
                    FirstName = user.FirstName.WithMaxLength(45),
                    LastName = user.LastName.WithMaxLength(45),
                    Address = user.Address.WithMaxLength(255),
                    Country = user.Country.WithMaxLength(45),
                    City = user.City.WithMaxLength(45),
                    PostCode = user.PostCode.WithMaxLength(25),
                    Phone = user.Phone.WithMaxLength(20),
                    MobilePhone = user.MobilePhone.WithMaxLength(20),
                    UserGroupName = user.UserGroupName.WithMaxLength(45),
                    Sex = user.Sex,
                    IsBanned = user.IsBanned,
                    RegistrationDate = user.RegistrationDate,
                    BirthDate = user.BirthDate,
                    Time = user.Time,
                    Deposits = user.Deposits,
                    Points = user.Points,
                    Password = user.Password,
                }).ToList();

                //check for unique usernames
                if (normalizedUsers.Where(user => !string.IsNullOrWhiteSpace(user.Username)).GroupBy(user => user.Username).Any(gropu => gropu.Count() > 1))
                    throw new ArgumentException("Non unique user name detected.", nameof(UserImportInfo.Username));

                //check for unique emails
                if (normalizedUsers.Where(user => !string.IsNullOrWhiteSpace(user.Email)).GroupBy(user => user.Email).Any(gropu => gropu.Count() > 1))
                    throw new ArgumentException("Non unique user email detected.", nameof(UserImportInfo.Email));

                //get distinct user groups from the import
                var importUserGroups = users.Where(userImport => !string.IsNullOrWhiteSpace(userImport.UserGroupName))
                    .Select(userImport => userImport.UserGroupName)
                    .Distinct(StringComparer.InvariantCultureIgnoreCase)
                    .Select(userGroupName => userGroupName.WithMaxLength(45));

                using (var trx = cx.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    //first we need to create any missing user groups
                    var existingUserGroups = await cx.UserGroups.Select(userGroup => userGroup.Name)
                        .ToListAsync(ct)
                        .ConfigureAwait(false);

                    //get missing user groups
                    var missingUserGroups = importUserGroups.Where(userGroup => !existingUserGroups.Contains(userGroup, StringComparer.InvariantCultureIgnoreCase))
                        .ToList()
                        .Distinct(StringComparer.InvariantCultureIgnoreCase);

                    //we will need to check if the user group names complies with out valid length limits
                    var validUserGroups = missingUserGroups.Select(userGroup => userGroup.WithMaxLength(45))
                        .ToList();

                    //add any missing user group
                    foreach (var userGroupName in validUserGroups)
                    {
                        var userGroup = new Entities.UserGroup()
                        {
                            Name = userGroupName,
                        };

                        cx.UserGroups.Add(userGroup);
                    }

                    //save changes
                    await cx.SaveChangesAsync(ct);

                    //create user group lookup table
                    var userGroupLookup = await cx.UserGroups
                        .ToDictionaryAsync(userGroup => userGroup.Name.ToLower(), userGroup => userGroup.Id, ct);

                    //disable changes tracking 
                    cx.ChangeTracker.AutoDetectChangesEnabled = false;

                    //add users
                    foreach (var user in normalizedUsers)
                    {
                        //get local user group id
                        var userGroupId = userGroupLookup[user.UserGroupName.ToLower()];

                        //create new user member
                        var userMember = new Entities.UserMember()
                        {
                            Username = user.Username,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Sex = (SharedLib.Sex)user.Sex,
                            Phone = user.Phone,
                            MobilePhone = user.MobilePhone,
                            BirthDate = user.BirthDate,
                            Country = user.Country,
                            Address = user.Address,
                            IsDisabled = user.IsBanned,
                            UserGroupId = userGroupId,

                            //if the registration date is not provided use current date time
                            CreatedTime = user.RegistrationDate ?? DateTime.Now,

                            //disable automatic create time update
                            IgnoreCreatedUpdate = true,

                            //disable automatic update time update
                            IgnoreUpdatedUpdate = true,
                        };

                        var userCredentials = new Entities.UserCredential();

                        if (string.IsNullOrWhiteSpace(user.Password))
                        {
                            userCredentials.Password = null;
                            userCredentials.Salt = null;
                        }
                        else
                        {
                            byte[] salt = cx.GetNewSalt();
                            byte[] password = cx.GetHashedPassword(user.Password, salt);
                            userCredentials.Salt = salt;
                            userCredentials.Password = password;
                        }

                        userMember.UserCredential = userCredentials;

                        //add user member
                        cx.UsersMember.Add(userMember);

                        //add any user time
                        if (user.Time > 0)
                        {
                            //based on options covert time values
                            var time = options.TreatTimeAsMinutes ? TimeSpan.FromMinutes(user.Time) : TimeSpan.FromSeconds(user.Time);

                            //create a product order
                            var order = new Entities.ProductOrder
                            {
                                User = userMember,
                                IsDelivered = true,
                                DeliveredTime = DateTime.Now,
                                Status = SharedLib.OrderStatus.Completed,
                            };

                            //create time order line
                            var orderLine = new Entities.ProductOLTimeFixed
                            {
                                ProductOrder = order,
                                ProductName = "Imported time",
                                Quantity = (decimal)Math.Round(time.TotalMinutes, 0, MidpointRounding.AwayFromZero),
                                User = userMember,
                                IsDelivered = true,
                                DeliveredTime = DateTime.Now,
                            };

                            //add order line to order
                            order.OrderLines.Add(orderLine);

                            //create invoice
                            var invoice = new Entities.Invoice
                            {
                                ProductOrder = order,
                                User = order.User,
                                Status = SharedLib.InvoiceStatus.Paid,
                            };

                            //create invoice line
                            var invoiceLine = new Entities.InvoiceLineTimeFixed
                            {
                                Invoice = invoice,
                                User = order.User,
                                OrderLine = orderLine,
                                ProductName = orderLine.ProductName,
                                Quantity = orderLine.Quantity,
                            };

                            //add invoice line to the invoice
                            invoice.InvoiceLines.Add(invoiceLine);

                            //add order and invoice to context
                            cx.Orders.Add(order);
                            cx.Invoices.Add(invoice);
                        }

                        //add any user deposits
                        if (user.Deposits > 0)
                        {
                            //get user deposits
                            var deposits = Math.Round(user.Deposits, 2);

                            //create deposit transaction
                            var depositTransaction = new Entities.DepositTransaction()
                            {
                                User = userMember,
                                Amount = deposits,
                                Type = SharedLib.DepositTransactionType.Credit,
                                Balance = deposits,
                            };

                            //add user deposits
                            userMember.Deposits.Add(depositTransaction);
                        }

                        if (user.Points > 0)
                        {
                            //create points transaction
                            var pointsTransaction = new Entities.PointTransaction()
                            {
                                User = userMember,
                                Amount = user.Points,
                                Balance = user.Points,
                                Type = LoyalityPointsTransactionType.Credit,
                            };

                            //add points
                            userMember.LoayalityPoints.Add(pointsTransaction);
                        }
                    }

                    //detect any changes made
                    cx.ChangeTracker.DetectChanges();

                    //save all changes
                    await cx.SaveChangesAsync(ct);

                    //commit any outstanding changes
                    trx.Commit();
                }
            }
        }

        #endregion

        #region GENERIC METHODS

        /// <summary>
        /// Generic insert or update method.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="item">Item.</param>
        public void InsertOrUpdate<T>(T item) where T : Entities.EntityBase
        {
            using (var cx = GetDbNonProxyContext())
            {
                cx.Entry(item).State = item.Id == 0 ?
                              EntityState.Added :
                              EntityState.Modified;

                cx.SaveChanges();
            }
        }

        /// <summary>
        /// Generic remove method.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="item">Item.</param>
        public void Remove<T>(T item) where T : Entities.EntityBase
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            using (var cx = GetDbNonProxyContext())
            {
                cx.Entry(item).State = EntityState.Deleted;
                cx.SaveChanges();
            }
        }

        #endregion

        #region SETTINGS

        /// <summary>
        /// Gets all current settings.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Entities.Setting> SettingGet()
        {
            using (var cx = GetDbContext())
            {
                return cx.Settings.ToList();
            }
        }

        /// <summary>
        /// Gets setting by name.
        /// </summary>
        /// <param name="name">Setting name.</param>
        /// <returns>Found setting, null in case no setting found.</returns>
        public Entities.Setting SettingGet(string name)
        {
            using (var cx = GetDbContext())
            {
                return cx.Settings.AsNoTracking()
                    .Where(x => string.Compare(x.Name, name, true) == 0)
                    .SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets settings value.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="name">Setting name.</param>
        /// <returns>Value.</returns>
        public T SettingGetValue<T>(string name)
        {
            var dbSetting = SettingGet(name);

            if (dbSetting != null)
            {
                var value = dbSetting.Value;
                if (value != null)
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    return converter.CanConvertFrom(value.GetType()) ?
                        (T)converter.ConvertFrom(value) :
                        default(T);
                }
            }

            return default(T);
        }

        /// <summary>
        /// Adds or updates specified setting.
        /// </summary>
        /// <param name="setting">Settng instance.</param>
        public void SettingSet(Entities.Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            InsertOrUpdate(setting);
        }

        /// <summary>
        /// Sets specified setting value.
        /// </summary>
        /// <param name="name">Setting name.</param>
        /// <param name="value">Setting value.</param>
        /// <param name="group">Setting group.</param>
        public void SettingSet(string name, string value, string group = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            using (var cx = GetDbNonProxyContext())
            {
                var entity = cx.Settings.Where(x => x.Name == name).FirstOrDefault();
                if (entity == null)
                {
                    entity = new Entities.Setting() { Name = name, Value = value, GroupName = group };
                    cx.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    entity.GroupName = group;
                    entity.Value = value;
                    entity.Name = name;
                    cx.Entry(entity).State = EntityState.Modified;
                }
                cx.SaveChanges();
            }
        }

        #endregion
    }
    #endregion
}
