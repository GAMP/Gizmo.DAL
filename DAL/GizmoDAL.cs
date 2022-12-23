using CoreLib;
using GizmoDALV2.DTO;
using GizmoDALV2.Entities;
using IntegrationLib;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GizmoDALV2
{
    #region GIZMODATABASE
    /// <summary>
    /// Gizmo database class.
    /// </summary>
    [Export(typeof(IGizmoDbContextProvider))]
    public partial class GizmoDatabase : IGizmoDbContextProvider
    {
        #region CONSTRUCTOR

        public GizmoDatabase(DatabaseType type, string cn, int? commandTimeout) : this(type, cn)
        {
            CommandTimeout = commandTimeout;
        }

        public GizmoDatabase(DatabaseType type, string cn)
        {
            if (string.IsNullOrWhiteSpace(cn))
                throw new ArgumentNullException(nameof(cn));

            DatabaseType = type;
            ConnectionString = cn;
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

        #region IGizmoDbContextProvider

        IGizmoDBContext IDbContextProvider<IGizmoDBContext>.GetDbContext()
        {
            return GetDbContext();
        }

        IGizmoDBContext IDbContextProvider<IGizmoDBContext>.GetDbNonProxyContext()
        {
            return GetDbNonProxyContext();
        }

        #endregion

        /// <summary>
        /// Initiate Database Context
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DefaultDbContext GetDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            switch (DatabaseType)
            {
                case DatabaseType.MSSQL:
                case DatabaseType.LOCALDB:
                case DatabaseType.MSSQLEXPRESS:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;

                case DatabaseType.SQLITE:
                    optionsBuilder.UseSqlite(connectionString);
                    break;

                case DatabaseType.MYSQL:
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                    break;

                default:
                    throw new ArgumentException(nameof(DatabaseType));
            }

            var cx = new DefaultDbContext(optionsBuilder.Options);

            //set default command timeout
            cx.Database.SetCommandTimeout(CommandTimeout);

            return cx;
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
        ///  Get MyS SQL Connection Builder
        /// </summary>
        /// <returns></returns>
        private MySqlConnectionStringBuilder GetMySQLConnectionBuilder()
        {
            return new MySqlConnectionStringBuilder(ConnectionString);
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

                    case DatabaseType.MYSQL:
                        return GetMySQLConnectionBuilder().Database;

                    default:
                        throw new NotSupportedException();
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
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[AppStat];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[AppStat]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ReservationUser];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ReservationUser]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ReservationHost];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ReservationHost]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Reservation];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Reservation]', RESEED, 1);", ct);
                }

                if (deleteHosts && !deleteUsers)
                {
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserGuest] SET IsReserved=0,ReservedHostId=NULL,ReservedSlot=NULL WHERE (IsReserved=1 OR ReservedHostId IS NOT NULL OR ReservedSlot IS NOT NULL);", ct);
                }

                #region FINANCIAL

                await cx.Database.ExecuteSqlRawAsync("UPDATE InvoiceLineExtended SET BundleLineId=NULL;", ct);
                await cx.Database.ExecuteSqlRawAsync("UPDATE UsageSession SET CurrentUsageId=NULL;", ct);
                await cx.Database.ExecuteSqlRawAsync("UPDATE ProductOLExtended SET BundleLineId=NULL;", ct);

                //USAGE SESSION
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UsageRate];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UsageTimeFixed];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UsageTime];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UsageUserSession];", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Usage];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Usage]', RESEED, 1);", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UsageSession];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[UsageSession]', RESEED, 1);", ct);

                //USER SESSION
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UserSessionChange];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[UserSessionChange]', RESEED, 1);", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UserSession];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[UserSession]', RESEED, 1);", ct);

                //REFUNDS
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[RefundInvoicePayment];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[RefundDepositPayment];", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Refund];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Refund]', RESEED, 1);", ct);

                //VOIDS
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[VoidInvoice];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[VoidDepositPayment];", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Void];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Void]', RESEED, 1);", ct);

                //INVOICE PAYMENTS
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoicePayment];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[InvoicePayment]', RESEED, 1);", ct);

                //DEPOSIT PAYMENT
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[PaymentIntentDeposit];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[PaymentIntentOrder];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[PaymentIntent];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[PaymentIntent]', RESEED, 1);", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[DepositPayment];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[DepositPayment]', RESEED, 1);", ct);

                //PAYMENTS
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Payment];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Payment]', RESEED, 1);", ct);

                //INVOICE
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceLineProduct];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceLineSession];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceLineTime];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceLineTimeFixed];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceLineExtended];", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceLine];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[InvoiceLine]', RESEED, 1);", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[InvoiceFiscalReceipt];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[InvoiceFiscalReceipt]', RESEED, 1);", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Invoice];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Invoice]', RESEED, 1);", ct);

                //ORDER
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOLTimeFixed];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOLTime];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOLSession];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOLProduct];", ct);
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOLExtended];", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOL];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductOL]', RESEED, 1);", ct);

                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductOrder];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductOrder]', RESEED, 1);", ct);

                //DEPOSIT TRANSACTION
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[DepositTransaction];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[DepositTransaction]', RESEED, 1);", ct);

                //POINT TRANSACTION
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[PointTransaction];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[PointTransaction]', RESEED, 1);", ct);

                //STOCK TRANSACTION
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[StockTransaction];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[StockTransaction]', RESEED, 1);", ct);

                //SHIFT COUNT
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ShiftCount];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ShiftCount]', RESEED, 1);", ct);

                //REGISTER TRANSACTION
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[RegisterTransaction];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[RegisterTransaction]', RESEED, 1);", ct);

                //FISCAL RECEIPTS
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[FiscalReceipt];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[FiscalReceipt]', RESEED, 1);", ct);

                //SHIFT
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Shift];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Shift]', RESEED, 1);", ct);

                //REGISTER
                await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Register];", ct);
                await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Register]', RESEED, 1);", ct);

                #endregion

                #region PRODUCTS

                if (deleteProducts || deleteHosts)
                {
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductHostHidden];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductHostHidden]', RESEED, 1);", ct);
                }

                if (deleteProducts)
                {
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductImage];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductImage]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTax];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductTax]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductPeriod];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductPeriodDayTime];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductPeriodDay];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductPeriodDay]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTimeHostDisallowed];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductTimeHostDisallowed]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductUserDisallowed];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductUserDisallowed]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductUserPrice];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductUserPrice]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[BundleProduct];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[BundleProduct]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTimeHostDisallowed];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductTimeHostDisallowed]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTimePeriod];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTimePeriodDayTime];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTimePeriodDay];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductTimePeriodDay]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductBundle];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Product];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductTime];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductBaseExtended];", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[ProductBase];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[ProductBase]', RESEED, 1);", ct);
                }

                #endregion

                #region USERS

                if (deleteUsers)
                {
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[HostGroupWaitingLineEntry];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[HostGroupWaitingLineEntry]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[AssetTransaction];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[AssetTransaction]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[AppRating];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UserCreditLimit];", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UserAttribute];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[UserAttribute]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[UserNote];", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Note];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Note]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[VerificationEmail];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[VerificationMobilePhone];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Verification];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Verification]', RESEED, 1);", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Token];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Token]', RESEED, 1);", ct);

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
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[HostComputer];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[HostEndpoint];", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Host];", ct);
                    await cx.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[dbo].[Host]', RESEED, 1);", ct);
                }
                #endregion

                #region OPERATORS
                if (deleteOperators)
                {
                    if (!deleteUsers)
                    {
                        cx.ChangeTracker.DetectChanges();
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserCreditLimit] Set CreatedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserCreditLimit] Set ModifiedById=NULL", ct);

                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserPicture] Set CreatedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserPicture] Set ModifiedById=NULL", ct);

                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserAttribute] Set CreatedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserAttribute] Set ModifiedById=NULL", ct);

                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AssetTransaction] Set CreatedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AssetTransaction] Set ModifiedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AssetTransaction] Set CheckedInById=NULL", ct);
                    }

                    if (!deleteHosts)
                    {
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Host] Set CreatedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Host] Set ModifiedById=NULL", ct);
                    }

                    if (!deleteHosts && !deleteProducts)
                    {
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductHostHidden] Set CreatedById=NULL", ct);
                        await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductHostHidden] Set ModifiedById=NULL", ct);
                    }

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[DeviceHost] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[DeviceHost] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Device] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Device] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ReservationUser] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ReservationUser] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ReservationHost] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ReservationHost] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Reservation] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Reservation] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("DELETE FROM [dbo].[Token] WHERE Type=0;", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Token] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Token] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[App] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[App] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppCategory] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppCategory] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppEnterprise] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppEnterprise] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExe] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExe] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeCdImage] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeCdImage] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeDeployment] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeDeployment] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeImage] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeImage] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeLicense] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeLicense] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeMaxUser] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeMaxUser] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExePersonalFile] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExePersonalFile] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeTask] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppExeTask] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppGroup] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppGroup] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppImage] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppImage] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppLink] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AppLink] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Asset] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Asset] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AssetType] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[AssetType] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Attribute] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Attribute] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[BillProfile] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[BillProfile] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[BundleProduct] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[BundleProduct] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[BundleProductUserPrice] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[BundleProductUserPrice] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ClientTask] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ClientTask] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Deployment] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Deployment] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Feed] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Feed] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostGroup] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostGroup] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostGroupWaitingLine] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostGroupWaitingLine] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostGroupWaitingLineEntry] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostGroupWaitingLineEntry] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostLayoutGroup] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostLayoutGroup] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostLayoutGroupImage] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostLayoutGroupImage] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostLayoutGroupLayout] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[HostLayoutGroupLayout] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Icon] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Icon] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[License] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[License] Set ModifiedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[LicenseKey] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[LicenseKey] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Mapping] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Mapping] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[MonetaryUnit] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[MonetaryUnit] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[News] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[News] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Note] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Note] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PaymentMethod] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PaymentMethod] Set ModifiedById=NULL");

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PersonalFile] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PersonalFile] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PluginLibrary] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PluginLibrary] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PresetTimeSale] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PresetTimeSale] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PresetTimeSaleMoney] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[PresetTimeSaleMoney] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductBase] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductBase] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductBundleUserPrice] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductBundleUserPrice] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductGroup] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductGroup] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductImage] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductImage] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductTax] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductTax] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductTimeHostDisallowed] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductTimeHostDisallowed] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductUserDisallowed] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductUserDisallowed] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductUserPrice] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[ProductUserPrice] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[SecurityProfile] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[SecurityProfile] Set ModifiedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[SecurityProfilePolicy] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[SecurityProfilePolicy] Set ModifiedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[SecurityProfileRestriction] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[SecurityProfileRestriction] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Setting] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Setting] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[TaskBase] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[TaskBase] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Tax] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Tax] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserGroup] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserGroup] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserGroupHostDisallowed] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserGroupHostDisallowed] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Variable] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[Variable] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[User] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[User] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserCredential] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserCredential] Set ModifiedById=NULL", ct);

                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserAgreement] Set CreatedById=NULL", ct);
                    await cx.Database.ExecuteSqlRawAsync("UPDATE [dbo].[UserAgreement] Set ModifiedById=NULL", ct);

                    cx.UserPermissions.RemoveRange(cx.UserPermissions.Where(permission => permission.User is UserOperator));
                    cx.UsersOperator.RemoveRange(cx.UsersOperator);

                    var defaultOperator = new UserOperator();

                    byte[] salt = cx.GetNewSalt();
                    byte[] password = cx.GetHashedPassword("admin", salt);

                    defaultOperator.UserCredential = new UserCredential();
                    defaultOperator.Username = "Admin";

                    defaultOperator.CreatedTime = DateTime.Now;
                    defaultOperator.UserCredential.Salt = salt;
                    defaultOperator.UserCredential.Password = password;

                    var allPermissions = ClaimTypeBase.GetClaimTypes().Select(claim =>
                    {
                        return new UserPermission() { Type = claim.Resource, Value = claim.Operation };
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

            string SQL_COMMAND_STRING =
                $"TRUNCATE TABLE [LogException];" +
                "ALTER TABLE [LogException] DROP CONSTRAINT [FK_dbo.LogException_dbo.Log_LogId];" +
                "TRUNCATE TABLE [Log];" +
                "ALTER TABLE [LogException] ADD CONSTRAINT \"FK_dbo.LogException_dbo.Log_LogId\" FOREIGN KEY(LogId) REFERENCES [Log] (LogId) ON DELETE CASCADE;";

            // ************ NOT APPLICABLE FOR EF CORE MIGRATION ************ //
            //return cx.Database.ExecuteSqlRawAsync(TransactionalBehavior.EnsureTransaction, SQL_COMMAND_STRING, ct);

            return cx.Database.ExecuteSqlRawAsync(SQL_COMMAND_STRING, ct);
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
                        var userGroup = new UserGroup()
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
                        var userMember = new UserMember()
                        {
                            Username = user.Username,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Sex = user.Sex,
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
                            var order = new ProductOrder
                            {
                                User = userMember,
                                IsDelivered = true,
                                DeliveredTime = DateTime.Now,
                                Status = OrderStatus.Completed,
                            };

                            //create time order line
                            var orderLine = new ProductOLTimeFixed
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
                            var invoice = new Invoice
                            {
                                ProductOrder = order,
                                User = order.User,
                                Status = InvoiceStatus.Paid,
                            };

                            //create invoice line
                            var invoiceLine = new InvoiceLineTimeFixed
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
                            var depositTransaction = new DepositTransaction()
                            {
                                User = userMember,
                                Amount = deposits,
                                Type = DepositTransactionType.Credit,
                                Balance = deposits,
                            };

                            //add user deposits
                            userMember.Deposits.Add(depositTransaction);
                        }

                        if (user.Points > 0)
                        {
                            //create points transaction
                            var pointsTransaction = new PointTransaction()
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
        public void InsertOrUpdate<T>(T item) where T : EntityBase
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
        public void Remove<T>(T item) where T : EntityBase
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
        public IEnumerable<Setting> SettingGet()
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
        public Setting SettingGet(string name)
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
        public void SettingSet(Setting setting)
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
                    entity = new Setting() { Name = name, Value = value, GroupName = group };
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
