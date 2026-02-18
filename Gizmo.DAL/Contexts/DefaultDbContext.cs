using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Mappings;
using Gizmo.Server.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;

namespace Gizmo.DAL.Contexts
{
    #region DEFAULTDBCONTEXT

    /// <summary>
    /// Default db context.
    /// </summary>
    public class DefaultDbContext : DbContext, IGizmoDBContext
    {
        /// <summary>
        /// Default constructor for dependency injection
        /// </summary>
        /// <param name="options">Default database options</param>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Occurs on entity event.
        /// <remarks>
        /// A type must be registered or RaiseAllEntityEvents set to true in order to notification be raised.
        /// </remarks>
        /// </summary>
        public static event EventHandler<IEntityEventArgs> EntityEvent;
        private HashSet<IEntityEventArgs> _eventCache = new HashSet<IEntityEventArgs>();
        private bool _isEventsCached = false;
        private static bool raiseAllEntityEvents;
        private static HashSet<Type> notifyTypes;

        #region PROPERTIES

        private static HashSet<Type> NotifyTypes
        {
            get
            {
                if (notifyTypes == null)
                    notifyTypes = new HashSet<Type>();
                return notifyTypes;
            }
        }

        private HashSet<IEntityEventArgs> EventCache
        {
            get
            {
                if (_eventCache == null)
                    _eventCache = new HashSet<IEntityEventArgs>();
                return _eventCache;
            }
            set
            {
                _eventCache = value;
            }
        }

        /// <summary>
        /// Gets or sets if event caching enabled.
        /// </summary>
        public bool IsEventsCached
        {
            get { return _isEventsCached; }
            set
            {
                _isEventsCached = value;
            }
        }

        /// <summary>
        /// Gets if all events should raised ignoring the notification types.
        /// </summary>
        public static bool RaiseAllEntityEvents
        {
            get { return raiseAllEntityEvents; }
            set { raiseAllEntityEvents = value; }
        }

        #endregion

        #region FUNCTIONS

        /// <summary>
        /// Registers a type for notification.
        /// </summary>
        /// <param name="type">Type.</param>
        public static void RegisterNotification(Type type)
        {
            if (!NotifyTypes.Contains(type))
                NotifyTypes.Add(type);
        }

        /// <summary>
        /// Registers a type for notification.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        public static void RegisterNotification<T>()
        {
            RegisterNotification(typeof(T));
        }

        /// <summary>
        /// Check if specified type is registered for notification.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <returns>True or false.</returns>
        public static bool IsNotificationRegistered(Type type)
        {
            if (RaiseAllEntityEvents)
                return true;

            return NotifyTypes.Contains(type);
        }

        /// <summary>
        /// Raises cached events.
        /// </summary>
        public void RaiseEventCache()
        {
            try
            {
                var handler = EntityEvent;
                if (handler != null)
                {
                    //generate new event list
                    var events = EventCache.ToList();

                    //process events
                    foreach (var eventArgs in events)
                    {
                        //raise event
                        handler(this, eventArgs);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //clear all events
                EventCache.Clear();
            }
        }

        /// <summary>
        /// Clears all cached events.
        /// </summary>
        public void ClearEventCache()
        {
            EventCache.Clear();
        }

        /// <summary>
        /// Gets event cache.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEntityEventArgs> GetEventCache()
        {
            return EventCache.ToList();
        }

        /// <summary>
        /// Sets event cache from existing enumerable.
        /// </summary>
        /// <param name="cache">Cache source.</param>
        public void SetEventCache(IEnumerable<IEntityEventArgs> cache)
        {
            if (cache == null)
                throw new ArgumentNullException(nameof(cache));

            EventCache = new HashSet<IEntityEventArgs>(cache);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets icons.
        /// </summary>
        public DbSet<Icon> Icons { get; set; }

        /// <summary>
        /// Gets hosts.
        /// </summary>
        public DbSet<Host> Hosts { get; set; }

        /// <summary>
        /// Gets host computers.
        /// </summary>
        public DbSet<HostComputer> HostComputers { get; set; }

        /// <summary>
        /// Gets host group bill profiles.
        /// </summary>
        public DbSet<HostGroupUserBillProfile> HostGroupBillProfile { get; set; }

        /// <summary>
        /// Gets host endpoints.
        /// </summary>
        public DbSet<HostEndpoint> HostEndpoint { get; set; }

        /// <summary>
        /// Gets news.
        /// </summary>
        public DbSet<News> News { get; set; }

        /// <summary>
        /// Gets feeds.
        /// </summary>
        public DbSet<Feed> Feeds { get; set; }

        /// <summary>
        /// Gets enterprises.
        /// </summary>
        public DbSet<AppEnterprise> Enterprises { get; set; }

        /// <summary>
        /// Gets host groups.
        /// </summary>
        public DbSet<HostGroup> HostGroups { get; set; }

        /// <summary>
        /// Gets user groups.
        /// </summary>
        public DbSet<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Gets user group disallowed host groups.
        /// </summary>
        public DbSet<UserGroupHostDisallowed> UserGroupHostDisallowed { get; set; }

        /// <summary>
        /// Gets log.
        /// </summary>
        public DbSet<Log> Log { get; set; }

        /// <summary>
        /// Gets security profiles.
        /// </summary>
        public DbSet<SecurityProfile> SecurityProfiles { get; set; }

        /// <summary>
        /// Gets restrictions.
        /// </summary>
        public DbSet<SecurityProfileRestriction> Restrictions { get; set; }

        /// <summary>
        /// Gets security policies.
        /// </summary>
        public DbSet<SecurityProfilePolicy> Policies { get; set; }

        /// <summary>
        /// Gets categories.
        /// </summary>
        public DbSet<AppCategory> Categories { get; set; }

        /// <summary>
        /// Gets applications.
        /// </summary>
        public DbSet<App> Applications { get; set; }

        /// <summary>
        /// Gets application images.
        /// </summary>
        public DbSet<AppImage> AppImage { get; set; }

        /// <summary>
        /// Gets application executables.
        /// </summary>
        public DbSet<AppExe> AppExes { get; set; }

        /// <summary>
        /// Gets application executable images.
        /// </summary>
        public DbSet<AppExeImage> AppExeImage { get; set; }

        /// <summary>
        /// Gets application links.
        /// </summary>
        public DbSet<AppLink> AppLinks { get; set; }

        /// <summary>
        /// Gets personal files.
        /// </summary>
        public DbSet<PersonalFile> PersonalFiles { get; set; }

        /// <summary>
        /// Gets application executable personal files.
        /// </summary>
        public DbSet<AppExePersonalFile> AppExePersonalFiles { get; set; }

        /// <summary>
        /// Gets licenses.
        /// </summary>
        public DbSet<License> Licenses { get; set; }

        /// <summary>
        /// Gets license keys.
        /// </summary>
        public DbSet<LicenseKey> LicenseKeys { get; set; }

        /// <summary>
        /// Get app executable license.
        /// </summary>
        public DbSet<AppExeLicense> AppExeLicense { get; set; }

        /// <summary>
        /// Gets app executable cd image.
        /// </summary>
        public DbSet<AppExeCdImage> AppExeCdImages { get; set; }

        /// <summary>
        /// Gets deployments.
        /// </summary>
        public DbSet<Deployment> Deployments { get; set; }

        /// <summary>
        /// Gets app executable deployments.
        /// </summary>
        public DbSet<AppExeDeployment> AppExeDeployment { get; set; }

        /// <summary>
        /// Gets app stats.
        /// </summary>
        public DbSet<AppStat> AppStats { get; set; }

        /// <summary>
        /// Gets app ratings.
        /// </summary>
        public DbSet<AppRating> AppRatings { get; set; }

        /// <summary>
        /// Gets settings entities.
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Gets plugin library entities.
        /// </summary>
        public DbSet<PluginLibrary> PluginLibraries { get; set; }

        /// <summary>
        /// Gets users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets member users.
        /// </summary>
        public DbSet<UserMember> UsersMember { get; set; }

        /// <summary>
        /// Gets operator users.
        /// </summary>
        public DbSet<UserOperator> UsersOperator { get; set; }

        /// <summary>
        /// Gets guest users.
        /// </summary>
        public DbSet<UserGuest> UsersGuest { get; set; }

        /// <summary>
        /// Gets user permissions.
        /// </summary>
        public DbSet<UserPermission> UserPermissions { get; set; }

        /// <summary>
        /// Gets user pictures.
        /// </summary>
        public DbSet<UserPicture> UsersPictures { get; set; }

        /// <summary>
        /// Gets user credit limits.
        /// </summary>
        public DbSet<UserCreditLimit> UserCreditLimits { get; set; }

        /// <summary>
        /// Gets credentials.
        /// </summary>
        public DbSet<UserCredential> Credentials { get; set; }

        /// <summary>
        /// Gets user sessions.
        /// </summary>
        public DbSet<UserSession> Sessions { get; set; }

        /// <summary>
        /// Gets user session changes.
        /// </summary>
        public DbSet<UserSessionChange> SessionsChanges { get; set; }

        /// <summary>
        /// Gets variables.
        /// </summary>
        public DbSet<Variable> Variables { get; set; }

        /// <summary>
        /// Gets mappings.
        /// </summary>
        public DbSet<Mapping> Mappings { get; set; }

        /// <summary>
        /// Gets attributes.
        /// </summary>
        public DbSet<Entities.Attribute> Attributes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets user attribute.
        /// </summary>
        public DbSet<UserAttribute> UserAttribute
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets user notes.
        /// </summary>
        public DbSet<UserNote> UserNotes { get; set; }

        /// <summary>
        /// Gets or sets preset times.
        /// </summary>
        public DbSet<PresetTimeSale> PresetTimeSale { get; set; }

        /// <summary>
        /// Gets or sets preset time money.
        /// </summary>
        public DbSet<PresetTimeSaleMoney> PresetTimeSaleMoney { get; set; }

        /// <summary>
        /// Get or sets preset reservation time.
        /// </summary>
        public DbSet<PresetReservationTime> PresetReservationTime { get; set; }

        #region TASKS

        /// <summary>
        /// Gets system wide tasks.
        /// </summary>
        public DbSet<ClientTask> ClientTasks { get; set; }

        /// <summary>
        /// Gets app exe tasks.
        /// </summary>
        public DbSet<AppExeTask> AppExeTasks { get; set; }

        /// <summary>
        /// Gets tasks.
        /// </summary>
        public DbSet<TaskBase> Tasks { get; set; }

        /// <summary>
        /// Gets junction tasks.
        /// </summary>
        public DbSet<TaskJunction> TasksJunction { get; set; }

        /// <summary>
        /// Gets notification tasks.
        /// </summary>
        public DbSet<TaskNotification> TasksNotification { get; set; }

        /// <summary>
        /// Gets process tasks.
        /// </summary>
        public DbSet<TaskProcess> TasksProcess { get; set; }

        /// <summary>
        /// Gets script tasks.
        /// </summary>
        public DbSet<TaskScript> TasksScript { get; set; }

        #endregion

        /// <summary>
        /// Gets app groups.
        /// </summary>
        public DbSet<AppGroup> AppGroups { get; set; }

        /// <summary>
        /// Gets application app group relation.
        /// </summary>
        public DbSet<AppGroupApp> AppGroupApp { get; set; }

        /// <summary>
        /// Gets host layout groups.
        /// </summary>
        public DbSet<HostLayoutGroup> HostLayoutGroups { get; set; }

        /// <summary>
        /// Gets host layout group images.
        /// </summary>
        public DbSet<HostLayoutGroupImage> HostLayoutGroupImages { get; set; }

        /// <summary>
        /// Gets host layout group host relation.
        /// </summary>
        public DbSet<HostLayoutGroupLayout> HostLayoutGroupsLayouts { get; set; }

        /// <summary>
        /// Gets reservations.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }

        /// <summary>
        /// Reservation orders.
        /// </summary>
        public DbSet<ReservationProductOrder> ReservationOrders { get; set; }

        /// <summary>
        /// Gets reservation hosts.
        /// </summary>
        public DbSet<ReservationHost> ReservationHosts { get; set; }

        /// <summary>
        /// Gets reservation users.
        /// </summary>
        public DbSet<ReservationUser> ReservationUsers { get; set; }

        #region BILLING

        /// <summary>
        /// Gets payment methods.
        /// </summary>
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Gets stock transactions.
        /// </summary>
        public DbSet<StockTransaction> StockTransactions { get; set; }

        /// <summary>
        /// Gets points transactions.
        /// </summary>
        public DbSet<PointTransaction> PointsTransaction { get; set; }

        /// <summary>
        /// Gets deposit transactions.
        /// </summary>
        public DbSet<DepositTransaction> DepositTransactions { get; set; }

        /// <summary>
        /// Gets payments.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Gets deposit payments.
        /// </summary>
        public DbSet<DepositPayment> DepositPayments { get; set; }

        /// <summary>
        /// Gets invoice payments.
        /// </summary>
        public DbSet<InvoicePayment> InvoicePayments { get; set; }

        /// <summary>
        /// Gets taxes.
        /// </summary>
        public DbSet<Tax> Taxes { get; set; }

        /// <summary>
        /// Gets monetary units.
        /// </summary>
        public DbSet<MonetaryUnit> MonetaryUnits { get; set; }

        /// <summary>
        /// Gets bill profiles.
        /// </summary>
        public DbSet<BillProfile> BillProfiles { get; set; }

        /// <summary>
        /// Gets bill rates.
        /// </summary>
        public DbSet<BillRate> BillRates { get; set; }

        /// <summary>
        /// Gets bill rate steps.
        /// </summary>
        public DbSet<BillRateStep> BillRateSteps { get; set; }

        /// <summary>
        /// Gets bill rate period days.
        /// </summary>
        public DbSet<BillRatePeriodDay> BillRatePeriodDays { get; set; }

        /// <summary>
        /// Gets bill rates period times.
        /// </summary>
        public DbSet<BillRatePeriodDayTime> BillRatePeriodTimes { get; set; }

        /// <summary>
        /// Gets usage sessions.
        /// </summary>
        public DbSet<UsageSession> UsageSessions { get; set; }

        /// <summary>
        /// Gets usage.
        /// </summary>
        public DbSet<Usage> Usage { get; set; }

        /// <summary>
        /// Gets usage user sessions.
        /// </summary>
        public DbSet<UsageUserSession> UsageUserSession { get; set; }

        /// <summary>
        /// Gets usage rate.
        /// </summary>
        public DbSet<UsageRate> UsageRate { get; set; }

        /// <summary>
        /// Gets usage time.
        /// </summary>
        public DbSet<UsageTime> UsageTime { get; set; }

        /// <summary>
        /// Gets usage time fixed.
        /// </summary>
        public DbSet<UsageTimeFixed> UsageFixed { get; set; }

        /// <summary>
        /// Gets orders.
        /// </summary>
        public DbSet<ProductOrder> Orders { get; set; }

        /// <summary>
        /// Gets order lines.
        /// </summary>
        public DbSet<ProductOL> OrderLines { get; set; }

        /// <summary>
        /// Extended order lines.
        /// </summary>
        public DbSet<ProductOLExtended> OrderLinesExtended { get; set; }

        /// <summary>
        /// Gets product order lines.
        /// </summary>
        public DbSet<ProductOLProduct> OrderLinesProduct { get; set; }

        /// <summary>
        /// Gets time order lines.
        /// </summary>
        public DbSet<ProductOLTime> OrderLinesTime { get; set; }

        /// <summary>
        /// Gets time fixed order lines.
        /// </summary>
        public DbSet<ProductOLTimeFixed> OrderLinesTimeFixed { get; set; }

        /// <summary>
        /// Gets usage session order lines.
        /// </summary>
        public DbSet<ProductOLSession> OrderLineSession { get; set; }

        /// <summary>
        /// Order discounts.
        /// </summary>
        public DbSet<ProductOrderDiscount> OrderDiscounts { get; set; }

        /// <summary>
        /// Gets invoices.
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets invoice lines.
        /// </summary>
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        /// <summary>
        /// Gets extended invoice lines.
        /// </summary>
        public DbSet<InvoiceLineExtended> InvoiceLinesExtended { get; set; }

        /// <summary>
        /// Gets product invoice lines.
        /// </summary>
        public DbSet<InvoiceLineProduct> InvoiceLineProduct { get; set; }

        /// <summary>
        /// Gets usage session invoice lines.
        /// </summary>
        public DbSet<InvoiceLineSession> InvoiceLineSession { get; set; }

        /// <summary>
        /// Gets time product invoice lines.
        /// </summary>
        public DbSet<InvoiceLineTime> InvoiceLineTime { get; set; }

        /// <summary>
        /// Gets time fixed invoice lines.
        /// </summary>
        public DbSet<InvoiceLineTimeFixed> InvoiceLineTimeFixed { get; set; }

        /// <summary>
        /// Gets product groups.
        /// </summary>
        public DbSet<ProductGroup> ProductGroups { get; set; }

        /// <summary>
        /// Gets products.
        /// </summary>
        public DbSet<ProductBase> Products { get; set; }

        /// <summary>
        /// Gets product bundles.
        /// </summary>
        public DbSet<ProductBundle> ProductBundles { get; set; }

        /// <summary>
        /// Gets product times.
        /// </summary>
        public DbSet<ProductTime> ProductTimes { get; set; }

        /// <summary>
        /// Gets or sets product disallowed user groups.
        /// </summary>
        public DbSet<ProductUserDisallowed> ProductUserGroupDisallowed { get; set; }

        /// <summary>
        /// Get or sets product time disallowed host groups.
        /// </summary>
        public DbSet<ProductTimeHostDisallowed> ProductTimeHostDisallowed { get; set; }

        /// <summary>
        /// Gets or sets product taxes.
        /// </summary>
        public DbSet<ProductTax> ProductsTaxes { get; set; }

        /// <summary>
        /// Gets product images.
        /// </summary>
        public DbSet<ProductImage> ProductImages { get; set; }

        /// <summary>
        /// Gets product periods.
        /// </summary>
        public DbSet<ProductPeriod> ProductPeriods { get; set; }

        /// <summary>
        /// Gets product periods times.
        /// </summary>
        public DbSet<ProductPeriodDayTime> ProductPeriodsTimes { get; set; }

        /// <summary>
        /// Gets product time period days.
        /// </summary>
        public DbSet<ProductPeriodDay> ProductPeriodDays { get; set; }

        /// <summary>
        /// Gets product time periods.
        /// </summary>
        public DbSet<ProductTimePeriod> ProductTimePeriods { get; set; }

        /// <summary>
        /// Gets product time period days.
        /// </summary>
        public DbSet<ProductTimePeriodDay> ProductTimePeriodDays { get; set; }

        /// <summary>
        /// Gets product time period times.
        /// </summary>
        public DbSet<ProductTimePeriodDayTime> ProductTimePeriodsTimes { get; set; }

        /// <summary>
        /// Gets product user prices.
        /// </summary>
        public DbSet<ProductUserPrice> ProductUserPrices { get; set; }

        /// <summary>
        /// Gest bundled products.
        /// </summary>
        public DbSet<BundleProduct> BundleProducts { get; set; }

        /// <summary>
        /// Gets bundled product user prices.
        /// </summary>
        public DbSet<BundleProductUserPrice> BundledProductUserPrices { get; set; }

        /// <summary>
        /// Gets registers.
        /// </summary>
        public DbSet<Register> Registers { get; set; }

        /// <summary>
        /// Gets shifts.
        /// </summary>
        public DbSet<Shift> Shifts { get; set; }

        /// <summary>
        /// Gets shift counts.
        /// </summary>
        public DbSet<ShiftCount> ShiftCounts { get; set; }

        /// <summary>
        /// Gets asset types.
        /// </summary>
        public DbSet<AssetType> AssetTypes { get; set; }

        /// <summary>
        /// Gets assets.
        /// </summary>
        public DbSet<Asset> Assets { get; set; }

        /// <summary>
        /// Gets asset transactions.
        /// </summary>
        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        /// <summary>
        /// Gets invoice voids.
        /// </summary>
        public DbSet<VoidInvoice> InvoiceVoids { get; set; }

        /// <summary>
        /// Gets refunds.
        /// </summary>
        public DbSet<Refund> Refunds { get; set; }

        /// <summary>
        /// Gets invoice payment refunds.
        /// </summary>
        public DbSet<RefundInvoicePayment> InvoicePaymentRefund { get; set; }

        /// <summary>
        /// Gets invoice fiscal receipts.
        /// </summary>
        public DbSet<InvoiceFiscalReceipt> InvoiceFiscalReceipts { get; set; }

        /// <summary>
        /// Gets voids.
        /// </summary>
        public DbSet<Entities.Void> Voids { get; set; }

        /// <summary>
        /// Gets waiting line entries.
        /// </summary>
        public DbSet<HostGroupWaitingLineEntry> WaitingLineEntries { get; set; }

        /// <summary>
        /// Gets tokens.
        /// </summary>
        public DbSet<Token> Tokens { get; set; }

        /// <summary>
        /// Gets verifications.
        /// </summary>
        public DbSet<Verification> Verifications { get; set; }

        /// <summary>
        /// Gets email verifications.
        /// </summary>
        public DbSet<VerificationEmail> EmailVerifications { get; set; }

        /// <summary>
        /// Gets mobile phone verifications.
        /// </summary>
        public DbSet<VerificationMobilePhone> MobilePhoneVerifications { get; set; }

        /// <summary>
        /// Gets register transactions.
        /// </summary>
        public DbSet<RegisterTransaction> RegisterTransactions { get; set; }

        /// <summary>
        /// Gets or sets product hidden host groups.
        /// </summary>
        public DbSet<ProductHostHidden> ProductHostGroupHidden { get; set; }

        /// <summary>
        /// Gets deposit payment voids.
        /// </summary>
        public DbSet<VoidDepositPayment> DepositPaymentVoids { get; set; }

        /// <summary>
        /// Gets deposit payment refunds.
        /// </summary>
        public DbSet<RefundDepositPayment> DepositPaymentRefunds { get; set; }

        /// <summary>
        /// Gets fiscal receipts.
        /// </summary>
        public DbSet<FiscalReceipt> FiscalReceipts { get; set; }

        /// <summary>
        /// Gets user agreements.
        /// </summary>
        public DbSet<UserAgreement> UserAgreements { get; set; }

        /// <summary>
        /// Gets user agreement states.
        /// </summary>
        public DbSet<UserAgreementState> UserAgreementStates { get; set; }

        /// <summary>
        /// Gets payment intents.
        /// </summary>
        public DbSet<PaymentIntent> PaymentIntents { get; set; }

        /// <summary>
        /// AppExe branches.
        /// </summary>
        public DbSet<AppExeBranch> AppExeBranches { get; set; }

        /// <summary>
        /// Product branches.
        /// </summary>
        public DbSet<ProductBranch> ProductBranches { get; set; }

        /// <summary>
        /// Feed branches.
        /// </summary>
        public DbSet<FeedBranch> FeedBranches { get; set; }

        /// <summary>
        /// News branches.
        /// </summary>
        public DbSet<NewsBranch> NewsBranches { get; set; }

        /// <summary>
        /// Discount branches.
        /// </summary>
        public DbSet<DiscountBranch> DiscountBranches { get; set; }

        #region DEVICES

        /// <summary>
        /// Gets devices.
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets HDMI devices.
        /// </summary>
        public DbSet<DeviceHdmi> DevicesHdmi { get; set; }

        /// <summary>
        /// Gets host devices.
        /// </summary>
        public DbSet<DeviceHost> DevicesHosts { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// Gets or sets assistance requests.
        /// </summary>
        public DbSet<AssistanceRequest> AssistanceRequests { get; set; }

        /// <summary>
        /// Gets or sets assistance request types.
        /// </summary>
        public DbSet<AssistanceRequestType> AssistanceRequestTypes { get; set; }

        /// <summary>
        /// Gets report presets.
        /// </summary>
        public DbSet<ReportPreset> ReportPresets { get; set; }

        /// <summary>
        /// Gets branches.
        /// </summary>
        public DbSet<Branch> Branches { get; set; }

        /// <summary>
        /// Gets user operator branches.
        /// </summary>
        public DbSet<UserOperatorBranch> UserOperatorBranches { get; set; }

        /// <summary>
        /// Gets companions.
        /// </summary>
        public DbSet<Companion> Companions { get; set; }

        /// <summary>
        /// Gets document types.
        /// </summary>
        public DbSet<DocumentType> DocumentTypes { get; set; }

        /// <summary>
        /// Get documents.
        /// </summary>
        public DbSet<FileDocument> Documents { get; set; }

        /// <summary>
        /// File Images.
        /// </summary>
        public DbSet<FileImage> FileImages { get; set; }

        /// <summary>
        /// Gets files.
        /// </summary>
        public DbSet<File> Files { get; set; }

        /// <summary>
        /// Gets inventories.
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// Gets inventory documents.
        /// </summary>
        public DbSet<InventoryDocument> InventoryDocuments { get; set; }

        /// <summary>
        /// Gets inventory entries.
        /// </summary>
        public DbSet<InventoryEntry> InventoryEntries { get; set; }

        /// <summary>
        /// Gets stocks.
        /// </summary>
        public DbSet<Stock> Stocks { get; set; }

        /// <summary>
        /// Gets stock counts.
        /// </summary>
        public DbSet<StockCount> StockCounts { get; set; }

        /// <summary>
        /// Gets stock count entries.
        /// </summary>
        public DbSet<StockCountEntry> StockCountEntries { get; set; }

        /// <summary>
        /// Gets discounts.
        /// </summary>
        public DbSet<Discount> Discounts { get; set; }

        /// <summary>
        /// Gets discount groups.
        /// </summary>
        public DbSet<DiscountGroup> DiscountGroups { get; set; }

        /// <summary>
        /// Discount target groups.
        /// </summary>
        public DbSet<TargetGroup> TargetGroups { get; set; }

        /// <summary>
        /// Gets permission sets.
        /// </summary>
        public DbSet<UserPermissionSet> PermissionSets { get; set; }

        /// <summary>
        /// Gets permission set permissions.
        /// </summary>
        public DbSet<UserPermissionSetPermission> PermissionSetPermissions { get; set; }

        /// <summary>
        /// Gets api keys.
        /// </summary>
        public DbSet<UserApiKey> ApiKeys { get; set; }

        /// <summary>
        /// Gets user age restrictions.
        /// </summary>
        public DbSet<AgeRestriction> AgeRestrictions { get; set; }

        /// <summary>
        /// Gets user age login restrictions.
        /// </summary>
        public DbSet<AgeRestrictionLogin> AgeLoginRestrictions { get; set; }

        /// <summary>
        /// Gets top-up presets.
        /// </summary>
        public DbSet<PresetTopUp> PresetTopUps { get; set; }

        /// <summary>
        /// Gets notifications.
        /// </summary>
        public DbSet<Notification> Notifications { get; set; }

        /// <summary>
        /// Gets schedules.
        /// </summary>
        public DbSet<Schedule> Schedules { get; set; }

        /// <summary>
        /// Schedule report recipients.
        /// </summary>
        public DbSet<ScheduleReportRecipient> ScheduleReportRecipients { get; set; }

        /// <summary>
        /// Schedule report entries.
        /// </summary>
        public DbSet<ScheduleReportEntry> ScheduleReportEntries { get; set; }

        /// <summary>
        /// Payment receipts.
        /// </summary>
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }

        /// <summary>
        /// Client options.
        /// </summary>
        public DbSet<ClientOptions> ClientOptions { get; set; }

        /// <summary>
        /// Inventory adjustment reasons.
        /// </summary>
        public DbSet<InventoryAdjustmentReason> InventoryAdjustmentReasons { get; set; }

        /// <summary>
        /// Promotions.
        /// </summary>
        public DbSet<Promotion> Promotions { get; set; }

        /// <summary>
        /// Promotion codes.
        /// </summary>
        public DbSet<PromotionCode> PromotionCodes { get; set; }

        /// <summary>
        /// Gets integrations.
        /// </summary>
        public DbSet<Integration> Integrations { get; set; }

        /// <summary>
        /// User disable entries.
        /// </summary>
        public DbSet<UserMemberDisableEntry> UserDisableEntries { get; set; }

        /// <summary>
        /// User disable reasons.
        /// </summary>
        public DbSet<UserMemberDisableReason> UserDisableReasons { get; set; }

        #endregion

        #region OVERRIDES

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            optionsBuilder.EnableSensitiveDataLogging(false);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region COMMON
            modelBuilder.ApplyConfiguration(new SettingMap());
            modelBuilder.ApplyConfiguration(new NewsMap());
            modelBuilder.ApplyConfiguration(new FeedMap());
            modelBuilder.ApplyConfiguration(new VariableMap());
            modelBuilder.ApplyConfiguration(new MappingMap());
            modelBuilder.ApplyConfiguration(new IconMap());
            modelBuilder.ApplyConfiguration(new AttributeMap());
            #endregion

            #region TASK
            modelBuilder.ApplyConfiguration(new TaskBaseMap());
            modelBuilder.ApplyConfiguration(new TaskJunctionMap());
            modelBuilder.ApplyConfiguration(new TaskNotificationMap());
            modelBuilder.ApplyConfiguration(new TaskScriptMap());
            modelBuilder.ApplyConfiguration(new TaskProcessMap());
            modelBuilder.ApplyConfiguration(new ClientTaskMap());
            #endregion

            #region USER
            modelBuilder.ApplyConfiguration(new UserGroupMap());
            modelBuilder.ApplyConfiguration(new UserGroupHostDisallowedMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserMemberMap());
            modelBuilder.ApplyConfiguration(new UserGuestMap());
            modelBuilder.ApplyConfiguration(new UserOperatorMap());
            modelBuilder.ApplyConfiguration(new UserPermissionMap());
            modelBuilder.ApplyConfiguration(new UserCredentialMap());
            modelBuilder.ApplyConfiguration(new UserSessionMap());
            modelBuilder.ApplyConfiguration(new UserSessionChangeMap());
            modelBuilder.ApplyConfiguration(new UserPictureMap());
            modelBuilder.ApplyConfiguration(new UserCreditLimitMap());
            #endregion

            #region HOST
            modelBuilder.ApplyConfiguration(new HostMap());
            modelBuilder.ApplyConfiguration(new HostEndpointMap());
            modelBuilder.ApplyConfiguration(new HostComputerMap());
            modelBuilder.ApplyConfiguration(new HostGroupMap());
            modelBuilder.ApplyConfiguration(new HostLayoutGroupMap());
            modelBuilder.ApplyConfiguration(new HostLayoutGroupImageMap());
            modelBuilder.ApplyConfiguration(new HostLayoutGroupLayoutMap());
            #endregion

            #region LOG
            modelBuilder.ApplyConfiguration(new LogMap());
            modelBuilder.ApplyConfiguration(new LogExceptionMap());
            #endregion

            #region PLUGIN LIBRARY
            modelBuilder.ApplyConfiguration(new PluginLibraryMap());
            #endregion

            #region APP
            modelBuilder.ApplyConfiguration(new AppEnterpriseMap());
            modelBuilder.ApplyConfiguration(new AppCategoryMap());
            modelBuilder.ApplyConfiguration(new AppGroupMap());
            modelBuilder.ApplyConfiguration(new AppGroupAppMap());
            modelBuilder.ApplyConfiguration(new AppMap());
            modelBuilder.ApplyConfiguration(new AppRatingMap());
            modelBuilder.ApplyConfiguration(new AppLinkMap());
            modelBuilder.ApplyConfiguration(new AppImageMap());
            modelBuilder.ApplyConfiguration(new AppExeMap());
            modelBuilder.ApplyConfiguration(new AppExeMaxUserMap());
            modelBuilder.ApplyConfiguration(new AppExeImageMap());
            modelBuilder.ApplyConfiguration(new AppExeTaskMap());
            modelBuilder.ApplyConfiguration(new AppExeDeploymentMap());
            modelBuilder.ApplyConfiguration(new AppExeLicenseMap());
            modelBuilder.ApplyConfiguration(new AppExePersonalFileMap());
            modelBuilder.ApplyConfiguration(new AppStatMap());
            modelBuilder.ApplyConfiguration(new AppExeCdImageMap());

            modelBuilder.ApplyConfiguration(new DeploymentMap());
            modelBuilder.ApplyConfiguration(new DeploymentDeploymentMap());
            modelBuilder.ApplyConfiguration(new PersonalFileMap());
            modelBuilder.ApplyConfiguration(new LicenseMap());
            modelBuilder.ApplyConfiguration(new LicenseKeyMap());

            modelBuilder.ApplyConfiguration(new UserAttributeMap());
            modelBuilder.ApplyConfiguration(new NoteMap());
            modelBuilder.ApplyConfiguration(new UserNoteMap());
            #endregion

            #region SEC
            modelBuilder.ApplyConfiguration(new SecurityProfileMap());
            modelBuilder.ApplyConfiguration(new SecurityProfilePolicyMap());
            modelBuilder.ApplyConfiguration(new SecurityProfileRestrictionMap());

            modelBuilder.ApplyConfiguration(new MonetaryUnitMap());
            modelBuilder.ApplyConfiguration(new TaxMap());
            modelBuilder.ApplyConfiguration(new PaymentMethodMap());
            modelBuilder.ApplyConfiguration(new PaymentMap());

            modelBuilder.ApplyConfiguration(new BillProfileMap());
            modelBuilder.ApplyConfiguration(new BillRateMap());
            modelBuilder.ApplyConfiguration(new BillProfileRateStepMap());
            modelBuilder.ApplyConfiguration(new BillRatePeriodDayMap());
            modelBuilder.ApplyConfiguration(new BillRatePeriodTimeMap());
            modelBuilder.ApplyConfiguration(new UsageSessionMap());
            modelBuilder.ApplyConfiguration(new UsageBaseMap());
            modelBuilder.ApplyConfiguration(new UsageUserSessionMap());
            modelBuilder.ApplyConfiguration(new UsageTimeMap());
            modelBuilder.ApplyConfiguration(new UsageTimeFixedMap());
            modelBuilder.ApplyConfiguration(new UsageRateMap());

            modelBuilder.ApplyConfiguration(new ProductGroupMap());
            modelBuilder.ApplyConfiguration(new ProductBaseMap());
            modelBuilder.ApplyConfiguration(new ProductBaseExtendedMap());
            modelBuilder.ApplyConfiguration(new ProductPeriodMap());
            modelBuilder.ApplyConfiguration(new ProductPeriodDayMap());
            modelBuilder.ApplyConfiguration(new ProductPeriodDayTimeMap());
            modelBuilder.ApplyConfiguration(new ProductTimePeriodMap());
            modelBuilder.ApplyConfiguration(new ProductTimePeriodDayMap());
            modelBuilder.ApplyConfiguration(new ProductTimePeriodDayTimeMap());
            modelBuilder.ApplyConfiguration(new ProductTaxMap());
            modelBuilder.ApplyConfiguration(new ProductImageMap());
            modelBuilder.ApplyConfiguration(new ProductUserPriceMap());
            modelBuilder.ApplyConfiguration(new ProductUserDisallowedMap());
            modelBuilder.ApplyConfiguration(new ProductTimeHostDisallowedMap());
            modelBuilder.ApplyConfiguration(new ProductHostHiddenMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductTimeMap());

            modelBuilder.ApplyConfiguration(new ProductBundleMap());
            modelBuilder.ApplyConfiguration(new BundleProductMap());
            modelBuilder.ApplyConfiguration(new BundleProductUserPriceMap());

            modelBuilder.ApplyConfiguration(new ProductOrderMap());
            modelBuilder.ApplyConfiguration(new ProductOLExtendedMap());
            modelBuilder.ApplyConfiguration(new ProductOLBaseMap());
            modelBuilder.ApplyConfiguration(new ProductOLProductMap());
            modelBuilder.ApplyConfiguration(new ProductOLTimeMap());
            modelBuilder.ApplyConfiguration(new ProductOLTimeFixedMap());
            modelBuilder.ApplyConfiguration(new ProductOLSessionMap());

            modelBuilder.ApplyConfiguration(new PresetTimeSaleMap());
            modelBuilder.ApplyConfiguration(new PresetTimeSaleMoneyMap());
            #endregion

            #region TRANSACTIONS
            modelBuilder.ApplyConfiguration(new DepositTransactionMap());
            modelBuilder.ApplyConfiguration(new PointTransactionMap());
            modelBuilder.ApplyConfiguration(new StockTransactionMap());
            #endregion

            #region PAYMENT
            modelBuilder.ApplyConfiguration(new DepositPaymentMap());
            #endregion

            #region INVOICING
            modelBuilder.ApplyConfiguration(new InvoiceMap());
            modelBuilder.ApplyConfiguration(new InvoicePaymentMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineExtendedMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineProductMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineTimeMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineTimeFixedMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineSessionMap());

            modelBuilder.ApplyConfiguration(new ShiftMap());
            modelBuilder.ApplyConfiguration(new ShiftCountMap());
            modelBuilder.ApplyConfiguration(new RegisterMap());
            modelBuilder.ApplyConfiguration(new RegisterTransactionMap());

            modelBuilder.ApplyConfiguration(new AssetTypeMap());
            modelBuilder.ApplyConfiguration(new AssetMap());
            modelBuilder.ApplyConfiguration(new AssetTransactionMap());

            modelBuilder.ApplyConfiguration(new VoidMap());
            modelBuilder.ApplyConfiguration(new VoidInvoiceMap());
            modelBuilder.ApplyConfiguration(new RefundMap());
            modelBuilder.ApplyConfiguration(new RefundInvoicePaymentMap());
            modelBuilder.ApplyConfiguration(new ProductBundleUserPriceMap());
            modelBuilder.ApplyConfiguration(new HostGroupUserBillProfileMap());

            modelBuilder.ApplyConfiguration(new HostGroupWaitingLineMap());
            modelBuilder.ApplyConfiguration(new HostGroupWaitingLineEntryMap());

            modelBuilder.ApplyConfiguration(new TokenMap());
            modelBuilder.ApplyConfiguration(new VerificationMap());
            modelBuilder.ApplyConfiguration(new VerificationEmailMap());
            modelBuilder.ApplyConfiguration(new VerificationMobilePhoneMap());
            modelBuilder.ApplyConfiguration(new ReservationMap());
            modelBuilder.ApplyConfiguration(new ReservationUserMap());
            modelBuilder.ApplyConfiguration(new ReservationHostMap());
            #endregion

            #region DEVICES
            modelBuilder.ApplyConfiguration(new DeviceMap());
            modelBuilder.ApplyConfiguration(new DeviceHdmiMap());
            modelBuilder.ApplyConfiguration(new DeviceHostMap());

            modelBuilder.ApplyConfiguration(new VoidDepositPaymentMap());
            modelBuilder.ApplyConfiguration(new RefundDepositPaymentMap());
            modelBuilder.ApplyConfiguration(new FiscalReceiptMap());
            modelBuilder.ApplyConfiguration(new InvoiceFiscalReceiptMap());

            modelBuilder.ApplyConfiguration(new UserAgreementMap());
            modelBuilder.ApplyConfiguration(new UserAgreementStateMap());

            modelBuilder.ApplyConfiguration(new PaymentIntentMap());
            modelBuilder.ApplyConfiguration(new PaymentIntentDepositMap());
            modelBuilder.ApplyConfiguration(new PaymentIntentOrderMap());
            #endregion

            #region ASSISTANCE REQUEST
            modelBuilder.ApplyConfiguration(new AssistanceRequestMap());
            modelBuilder.ApplyConfiguration(new AssistanceRequestTypeMap());
            #endregion

            modelBuilder.ApplyConfiguration(new ReportPresetMap());
            modelBuilder.ApplyConfiguration(new BranchMap());
            modelBuilder.ApplyConfiguration(new UserOperatorBranchMap());

            modelBuilder.ApplyConfiguration(new AppExeBranchMap());
            modelBuilder.ApplyConfiguration(new ProductBranchMap());
            modelBuilder.ApplyConfiguration(new FeedBranchMap());
            modelBuilder.ApplyConfiguration(new NewsBranchMap());
            modelBuilder.ApplyConfiguration(new CompanionMap());

            modelBuilder.ApplyConfiguration(new DiscountMap());
            modelBuilder.ApplyConfiguration(new DiscountPeriodMap());
            modelBuilder.ApplyConfiguration(new DiscountPeriodDayMap());
            modelBuilder.ApplyConfiguration(new DiscountPeriodDayTimeMap());
            modelBuilder.ApplyConfiguration(new DiscountBranchMap());
            modelBuilder.ApplyConfiguration(new DiscountGroupMap());
            modelBuilder.ApplyConfiguration(new DiscountGroupDiscountMap());
            modelBuilder.ApplyConfiguration(new TargetGroupMap());
            modelBuilder.ApplyConfiguration(new TargetMap());
            modelBuilder.ApplyConfiguration(new TargetGroupProductMap());
            modelBuilder.ApplyConfiguration(new TargetGroupProductTimeMap());
            modelBuilder.ApplyConfiguration(new TargetGroupProductGroupMap());
            modelBuilder.ApplyConfiguration(new TargetGroupBillProfileMap());
            modelBuilder.ApplyConfiguration(new TargetGroupPaymentMethodMap());
            modelBuilder.ApplyConfiguration(new TargetProductMap());
            modelBuilder.ApplyConfiguration(new TargetProductTimeMap());
            modelBuilder.ApplyConfiguration(new TargetProductGroupMap());
            modelBuilder.ApplyConfiguration(new TargetBillProfileMap());
            modelBuilder.ApplyConfiguration(new TargetPaymentMethodMap());

            modelBuilder.ApplyConfiguration(new PromotionMap());
            modelBuilder.ApplyConfiguration(new PromotionLimitMap());
            modelBuilder.ApplyConfiguration(new PromotionPeriodMap());
            modelBuilder.ApplyConfiguration(new PromotionPeriodDayMap());
            modelBuilder.ApplyConfiguration(new PromotionPeriodDayTimeMap());
            modelBuilder.ApplyConfiguration(new PromotionCodeMap());
            modelBuilder.ApplyConfiguration(new PromotionDiscountMap());
            modelBuilder.ApplyConfiguration(new PromotionDiscountGroupMap());
            modelBuilder.ApplyConfiguration(new PromotionBranchMap());

            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new StockCountMap());
            modelBuilder.ApplyConfiguration(new StockCountEntryMap());
            modelBuilder.ApplyConfiguration(new StockCountInboundMap());
            modelBuilder.ApplyConfiguration(new StockCountAdjustmentMap());

            modelBuilder.ApplyConfiguration(new InventoryMap());

            modelBuilder.ApplyConfiguration(new InventoryInboundMap());
            modelBuilder.ApplyConfiguration(new InventoryAdjustmentMap());
            modelBuilder.ApplyConfiguration(new InventoryTransferMap());

            modelBuilder.ApplyConfiguration(new InventoryEntryMap());
            modelBuilder.ApplyConfiguration(new InventoryInboundEntryMap());
            modelBuilder.ApplyConfiguration(new InventoryAdjustmentEntryMap());
            modelBuilder.ApplyConfiguration(new InventoryTransferEntryMap());

            modelBuilder.ApplyConfiguration(new InventoryAdjustmentReasonMap());
            modelBuilder.ApplyConfiguration(new InventoryTransferReasonMap());

            modelBuilder.ApplyConfiguration(new FileMap());
            modelBuilder.ApplyConfiguration(new FileDocumentMap());
            modelBuilder.ApplyConfiguration(new FileImageMap());
            modelBuilder.ApplyConfiguration(new DocumentTypeMap());
            modelBuilder.ApplyConfiguration(new InventoryDocumentMap());

            modelBuilder.ApplyConfiguration(new UserPermissionSetMap());
            modelBuilder.ApplyConfiguration(new UserPermissionSetPermissionMap());
            modelBuilder.ApplyConfiguration(new UserApiKeyMap());

            modelBuilder.ApplyConfiguration(new AgeRestrictionMap());
            modelBuilder.ApplyConfiguration(new AgeRestrictionLoginMap());
            modelBuilder.ApplyConfiguration(new AgeRestrictionProductMap());
            modelBuilder.ApplyConfiguration(new PresetTopUpMap());

            modelBuilder.ApplyConfiguration(new ScheduleMap());
            modelBuilder.ApplyConfiguration(new ScheduleReportMap());
            modelBuilder.ApplyConfiguration(new ScheduleReportEntryMap());

            modelBuilder.ApplyConfiguration(new RecipientMap());
            modelBuilder.ApplyConfiguration(new RecipientChannelMap());
            modelBuilder.ApplyConfiguration(new ScheduleReportRecipientMap());

            modelBuilder.ApplyConfiguration(new UserChannelMap());

            modelBuilder.ApplyConfiguration(new NotificationMap());
            modelBuilder.ApplyConfiguration(new NotificationTimedMap());
            modelBuilder.ApplyConfiguration(new NotificationTimedRemainingMap());
            modelBuilder.ApplyConfiguration(new NotificationTimedReservationMap());
            modelBuilder.ApplyConfiguration(new PresetReservationTimeMap());

            modelBuilder.ApplyConfiguration(new ProductOLReservationFeeMap());
            modelBuilder.ApplyConfiguration(new InvoiceLineReservationFeeMap());
            modelBuilder.ApplyConfiguration(new ReservationProductOrderMap());

            modelBuilder.ApplyConfiguration(new PaymentReceiptMap());
            modelBuilder.ApplyConfiguration(new ClientOptionsMap());

            modelBuilder.ApplyConfiguration(new ProductOrderDiscountMap());

            modelBuilder.ApplyConfiguration(new IntentOrderMap());
            modelBuilder.ApplyConfiguration(new IntentOrderDepositMap());
            modelBuilder.ApplyConfiguration(new IntentInvoiceMap());
            modelBuilder.ApplyConfiguration(new RefundPaymentMap());

            modelBuilder.ApplyConfiguration(new IntegrationMap());

            modelBuilder.ApplyConfiguration(new UserMemberDisableReasonMap());
            modelBuilder.ApplyConfiguration(new UserMemberDisableEntryMap());

            #region GLOBAL CONFIGURATIONS
            ApplyGlobalMapConfigurations(modelBuilder);
            #endregion

            #region BASE MODEL CREATION
            base.OnModelCreating(modelBuilder);
            #endregion
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            SetBranchAsync().GetAwaiter().GetResult();

            #region OBJECT CONTEXT

            var objectStateEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            var addedEntries = objectStateEntries.Where(x => x.State == EntityState.Added).ToList();
            var modifiedEntries = objectStateEntries.Where(x => x.State == EntityState.Modified).ToList();
            var deletedEntries = objectStateEntries.Where(x => x.State == EntityState.Deleted).ToList();
            #endregion

            #region UPDATE REJECT
            foreach (var addedEntity in addedEntries)
            {
                #region ICreatable

                if (addedEntity.Entity is ICreatable iCreatable)
                {
                    if (!iCreatable.IgnoreCreatedUpdate)
                        iCreatable.SetCreatedTime();
                }

                #endregion

                #region ICreatedBy

                if (addedEntity.Entity is ICreatedBy iCreatedBy)
                {
                    if (!iCreatedBy.IgnoreCreatedUpdate)
                        iCreatedBy.SetCreatedBy();
                }

                #endregion
            }

            foreach (var modifiedEntity in modifiedEntries)
            {
                #region IModifiable

                if (modifiedEntity.Entity is IModifiable iModified)
                {
                    if (!iModified.IgnoreUpdatedUpdate)
                    {
                        iModified.SetModifiedTime();

                        if (iModified.IgnoreCreatedUpdate)
                        {
                            modifiedEntity.Property(nameof(ICreatable.CreatedTime)).IsModified = false;
                        }

                        if (!modifiedEntity.Property(nameof(IModifiedBy.ModifiedTime)).IsModified)
                        {
                            modifiedEntity.Property(nameof(IModifiedBy.ModifiedTime)).IsModified = true;
                        }
                    }
                }

                #endregion

                #region IModifiedBy

                var iModifiedBy = modifiedEntity.Entity as IModifiedBy;
                if (iModifiedBy != null)
                {
                    if (!iModifiedBy.IgnoreUpdatedUpdate)
                    {
                        iModifiedBy.SetModifiedBy();

                        if (!iModifiedBy.IgnoreCreatedUpdate)
                        {
                            modifiedEntity.Property(nameof(ICreatable.CreatedTime)).IsModified = false;
                            modifiedEntity.Property(nameof(ICreatedBy.CreatedById)).IsModified = false;
                        }

                        if (!modifiedEntity.Property(nameof(IModifiedBy.ModifiedById)).IsModified)
                        {
                            modifiedEntity.Property(nameof(IModifiedBy.ModifiedById)).IsModified = true;
                        }
                    }
                }

                #endregion

                #region IReplicatable

                if (iModifiedBy is IReplicatable iReplicate)
                {
                    modifiedEntity.Property(nameof(IReplicatable.Guid)).IsModified = false;
                }

                #endregion
            }
            #endregion

            #region EVENT GENERATION

            List<IEntityEventArgs> events = new List<IEntityEventArgs>();
            var handler = EntityEvent;
            if (handler != null)
            {
                var addedGroups = addedEntries.GroupBy(x => x.Entity.GetType());
                var modifiedGroups = modifiedEntries.GroupBy(x => x.Entity.GetType());
                var deletedGroups = deletedEntries.GroupBy(x => x.Entity.GetType());

                foreach (var entityGroup in addedGroups)
                {
                    if (!IsNotificationRegistered(entityGroup.Key))
                        continue;

                    var entityType = entityGroup.Key;
                    var argsType = typeof(EntityEventArgs<>);
                    var fullType = argsType.MakeGenericType(entityType);

                    IEnumerable<object> modifiedEntities = entityGroup.Select(x => x.Entity);
                    IEnumerable<object> temp = new List<object>();

                    var eventArgs = (IEntityEventArgs)Activator.CreateInstance(fullType, EntityEventType.Added, modifiedEntities, temp);
                    events.Add(eventArgs);
                }

                foreach (var entityGroup in modifiedGroups)
                {
                    if (!IsNotificationRegistered(entityGroup.Key))
                        continue;

                    var entityType = entityGroup.Key;
                    var argsType = typeof(EntityEventArgs<>);
                    var fullType = argsType.MakeGenericType(entityType);

                    IEnumerable<object> modifiedEntities = entityGroup.Select(x => x.Entity);
                    IEnumerable<object> temp = new List<object>();

                    var eventArgs = (IEntityEventArgs)Activator.CreateInstance(fullType, EntityEventType.Modified, modifiedEntities, temp);
                    events.Add(eventArgs);
                }

                foreach (var entityGroup in deletedGroups)
                {
                    if (!IsNotificationRegistered(entityGroup.Key))
                        continue;

                    var entityType = entityGroup.Key;
                    var argsType = typeof(EntityEventArgs<>);
                    var fullType = argsType.MakeGenericType(entityType);

                    IEnumerable<object> modifiedEntities = entityGroup.Select(x => x.Entity);
                    IEnumerable<object> temp = new List<object>();

                    var eventArgs = (IEntityEventArgs)Activator.CreateInstance(fullType, EntityEventType.Removed, temp, modifiedEntities);
                    events.Add(eventArgs);
                }
            }

            #endregion

            #region SAVE
            try
            {
                int result = base.SaveChanges();

                // if save was successful save events to cache
                if (IsEventsCached)
                {
                    foreach (var argument in events)
                        EventCache.Add(argument);
                }
                else
                {
                    //if events not cached raise them
                    foreach (var argument in events)
                        handler?.Invoke(this, argument);
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
            }
            #endregion
        }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            await SetBranchAsync(cancellationToken);

            #region OBJECT CONTEXT

            var objectStateEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            var addedEntries = objectStateEntries.Where(x => x.State == EntityState.Added).ToList();
            var modifiedEntries = objectStateEntries.Where(x => x.State == EntityState.Modified).ToList();
            var deletedEntries = objectStateEntries.Where(x => x.State == EntityState.Deleted).ToList();

            #endregion

            #region UPDATE REJECT
            foreach (var addedEntity in addedEntries)
            {
                #region ICreatable

                if (addedEntity.Entity is ICreatable iCreatable)
                {
                    if (!iCreatable.IgnoreCreatedUpdate)
                        iCreatable.SetCreatedTime();
                }

                #endregion

                #region ICreatedBy

                if (addedEntity.Entity is ICreatedBy iCreatedBy)
                {
                    if (!iCreatedBy.IgnoreCreatedUpdate)
                        iCreatedBy.SetCreatedBy();
                }

                #endregion
            }

            foreach (var modifiedEntity in modifiedEntries)
            {
                #region IModifiable

                if (modifiedEntity.Entity is IModifiable iModified)
                {
                    if (!iModified.IgnoreUpdatedUpdate)
                    {
                        iModified.SetModifiedTime();

                        if (iModified.IgnoreCreatedUpdate)
                        {
                            modifiedEntity.Property(nameof(ICreatable.CreatedTime)).IsModified = false;
                        }

                        if (!modifiedEntity.Property(nameof(IModifiedBy.ModifiedTime)).IsModified)
                        {
                            modifiedEntity.Property(nameof(IModifiedBy.ModifiedTime)).IsModified = true;
                        }
                    }
                }

                #endregion

                #region IModifiedBy

                var iModifiedBy = modifiedEntity.Entity as IModifiedBy;
                if (iModifiedBy != null)
                {
                    if (!iModifiedBy.IgnoreUpdatedUpdate)
                    {
                        iModifiedBy.SetModifiedBy();

                        if (!iModifiedBy.IgnoreCreatedUpdate)
                        {
                            modifiedEntity.Property(nameof(ICreatable.CreatedTime)).IsModified = false;
                            modifiedEntity.Property(nameof(ICreatedBy.CreatedById)).IsModified = false;
                        }

                        if (!modifiedEntity.Property(nameof(IModifiedBy.ModifiedById)).IsModified)
                        {
                            modifiedEntity.Property(nameof(IModifiedBy.ModifiedById)).IsModified = true;
                        }
                    }
                }

                #endregion

                #region IReplicatable

                if (iModifiedBy is IReplicatable iReplicate)
                {
                    modifiedEntity.Property(nameof(IReplicatable.Guid)).IsModified = false;
                }

                #endregion

                #region IDeleteable
                var iDeletable = modifiedEntity.Entity as IDeletable;
                #endregion
            }
            #endregion

            #region EVENT GENERATION

            List<IEntityEventArgs> events = new List<IEntityEventArgs>();
            var handler = EntityEvent;
            if (handler != null)
            {
                var addedGroups = addedEntries.GroupBy(x => x.Entity.GetType());
                var modifiedGroups = modifiedEntries.GroupBy(x => x.Entity.GetType());
                var deletedGroups = deletedEntries.GroupBy(x => x.Entity.GetType());

                foreach (var entityGroup in addedGroups)
                {
                    if (!IsNotificationRegistered(entityGroup.Key))
                        continue;

                    var entityType = entityGroup.Key;
                    var argsType = typeof(EntityEventArgs<>);
                    var fullType = argsType.MakeGenericType(entityType);

                    IEnumerable<object> modifiedEntities = entityGroup.Select(x => x.Entity);
                    IEnumerable<object> temp = new List<object>();

                    var eventArgs = (IEntityEventArgs)Activator.CreateInstance(fullType, EntityEventType.Added, modifiedEntities, temp);
                    events.Add(eventArgs);
                }

                foreach (var entityGroup in modifiedGroups)
                {
                    if (!IsNotificationRegistered(entityGroup.Key))
                        continue;

                    var entityType = entityGroup.Key;
                    var argsType = typeof(EntityEventArgs<>);
                    var fullType = argsType.MakeGenericType(entityType);

                    IEnumerable<object> modifiedEntities = entityGroup.Select(x => x.Entity);
                    IEnumerable<object> temp = new List<object>();

                    var eventArgs = (IEntityEventArgs)Activator.CreateInstance(fullType, EntityEventType.Modified, modifiedEntities, temp);
                    events.Add(eventArgs);
                }

                foreach (var entityGroup in deletedGroups)
                {
                    if (!IsNotificationRegistered(entityGroup.Key))
                        continue;

                    var entityType = entityGroup.Key;
                    var argsType = typeof(EntityEventArgs<>);
                    var fullType = argsType.MakeGenericType(entityType);

                    IEnumerable<object> modifiedEntities = entityGroup.Select(x => x.Entity);
                    IEnumerable<object> temp = new List<object>();

                    var eventArgs = (IEntityEventArgs)Activator.CreateInstance(fullType, EntityEventType.Removed, temp, modifiedEntities);
                    events.Add(eventArgs);
                }
            }

            #endregion

            #region SAVE
            try
            {
                int result = await base.SaveChangesAsync(cancellationToken);

                // if save was successful save events to cache
                if (IsEventsCached)
                {
                    foreach (var argument in events)
                        EventCache.Add(argument);
                }
                else
                {
                    //if events not cached raise them
                    foreach (var argument in events)
                        handler?.Invoke(this, argument);
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
            }
            #endregion
        }

        #endregion

        #region FUNCTIONS

        private async Task SetBranchAsync(CancellationToken cancellationToken = default)
        {
            var objectStateEntries = this.ChangeTracker.Entries()
             .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
             .ToList();

            var addedEntries = objectStateEntries.Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity)
                .OfType<IBranchedEntity>()
                .Where(entity => entity.BranchId == 0)
                .ToList();

            var modifiedEntries = objectStateEntries
                .Where(x => x.State == EntityState.Modified)
                .Where(x => x.Entity is IBranchedEntity branchedEntity && branchedEntity.BranchId == 0)
                .ToList();

            if (addedEntries.Count > 0)
            {
                int defaultBranchId = await Branches.Select(branch => branch.Id).FirstAsync(cancellationToken);
                foreach (var addedEntity in addedEntries)
                    addedEntity.BranchId = defaultBranchId;
            }

            if (modifiedEntries.Count > 0)
            {
                foreach (var modifiedEntity in modifiedEntries)
                    modifiedEntity.Property(nameof(IBranchedEntity.BranchId)).IsModified = false;
            }
        }

        /// <summary>
        /// Implements default method to be used by SaltGenerator delegate
        /// </summary>
        /// <returns>String value that can be used as salt.</returns>
        public byte[] GetNewSalt()
        {
            byte[] salt = new byte[100];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetNonZeroBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Gets hashed password.
        /// </summary>
        /// <param name="pwd">Password input string.</param>
        /// <param name="salt">Password salt.</param>
        /// <returns>Hashed password byte array.</returns>
        public byte[] GetHashedPassword(string pwd, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(pwd))
                throw new ArgumentNullException(nameof(pwd), "Password may not be null or empty");

            if (salt == null)
                throw new ArgumentException("Invalid salt specified", nameof(salt));

            List<byte> bytes = new List<byte>(Encoding.Default.GetBytes(pwd));
            bytes.AddRange(salt);
            using (SHA512 hasher = SHA512.Create())
            {
                return hasher.ComputeHash(bytes.ToArray());
            }
        }

        /// <summary>
        /// Validates credentials.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <param name="salt">Salt.</param>
        /// <param name="pwdHash">Password hash.</param>
        /// <returns>True or false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool CredentialsIsPasswordValid(string password, byte[] salt, byte[] pwdHash)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password may not be null or empty");

            if (salt == null)
                throw new ArgumentException("Invalid salt specified", nameof(salt));

            if (pwdHash == null)
                throw new ArgumentException("Invalid password hash specified", nameof(pwdHash));

            byte[] testHash = GetHashedPassword(password, salt);
            return testHash.SequenceEqual(pwdHash);
        }

        /// <summary>
        /// Demand find of specified entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity set type.</typeparam>
        /// <param name="entityKey">Entity key.</param>
        /// <returns>Found entity.</returns>
        /// <exception cref="EntityNotFoundException">
        /// Thrown if entity with specified key not found in the entity set.
        /// </exception>
        public TEntity DemandFind<TEntity>(int entityKey) where TEntity : class
        {
            TEntity entity = default;
            try
            {
                entity = Set<TEntity>().Find(entityKey);
            }
            catch (InvalidOperationException)
            {
                //this will occur if we fail to materialize
            }

            if (entity == null)
                throw new EntityNotFoundException(entityKey, typeof(TEntity));

            return entity;
        }

        /// <summary>
        /// Demand find of specified entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity set type.</typeparam>
        /// <param name="entityKey">Entity key.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Found entity.</returns>
        /// <exception cref="EntityNotFoundException">
        /// Thrown if entity with specified key not found in the entity set.
        /// </exception>
        public Task<TEntity> DemandFindEntityAsync<TEntity>(int entityKey, CancellationToken ct = default) where TEntity : class
        {
            return DemandFindEntityAsync<TEntity, TEntity>(entityKey, ct);
        }

        /// <summary>
        /// Demand find of specified entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity set type.</typeparam>
        /// <param name="entityKey">Entity key.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TNotFoundEntity">Type of not found exception entity.</typeparam>
        /// <returns>Found entity.</returns>
        /// <exception cref="EntityNotFoundException">
        /// Thrown if entity with specified key not found in the entity set.
        /// </exception>
        public async Task<TEntity> DemandFindEntityAsync<TEntity, TNotFoundEntity>(int entityKey, CancellationToken ct = default) where TEntity : class
        {
            TEntity entity = default;
            try
            {
                entity = await Set<TEntity>().FindAsync(new object[] { entityKey }, ct);
            }
            catch (InvalidOperationException)
            {
                //this will occur if we fail to materialize
            }

            if (entity == null)
                throw new EntityNotFoundException(entityKey, typeof(TNotFoundEntity));

            return entity;
        }

        /// <summary>
        /// Demand find of specified entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity set type.</typeparam>
        /// <param name="entityKeys">Entity keys.</param>
        /// <returns>Found entity.</returns>
        /// <exception cref="EntityNotFoundException">
        /// Thrown if entity with specified keys not found in the entity set.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if entity keys are equal to null.
        /// </exception>
        public TEntity DemandFind<TEntity>(object[] entityKeys) where TEntity : class
        {
            if (entityKeys == null)
                throw new ArgumentNullException(nameof(entityKeys));

            var entity = Set<TEntity>().Find(entityKeys);
            if (entity == null)
                throw new EntityNotFoundException(entityKeys, typeof(TEntity));

            return entity;
        }

        /// <summary>
        /// Demands that an entity with specified key exists.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entityKey">Entity key.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task DemandFindAsync<TEntity>(int entityKey, CancellationToken ct = default) where TEntity : EntityBase
        {
            if (await Set<TEntity>().Where(entity => entity.Id == entityKey).AnyAsync(ct) == false)
                throw new EntityNotFoundException(entityKey, typeof(TEntity));
        }

        /// <summary>
        /// Demands that an entity with specified key exists.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TNotFoundEntity">Type of not found exception entity.</typeparam>
        /// <param name="entityKey">Entity key.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task DemandFindAsync<TEntity, TNotFoundEntity>(int entityKey, CancellationToken ct = default) where TEntity : EntityBase
        {
            if (await Set<TEntity>().Where(entity => entity.Id == entityKey).AnyAsync(ct) == false)
                throw new EntityNotFoundException(entityKey, typeof(TNotFoundEntity));
        }

        /// <summary>
        /// Gets Queryable set for specified entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Entity set.</returns>
        public IQueryable<TEntity> QueryableSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// Demands that specified property value is unique.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="propertyName">Entity property.</param>
        /// <param name="value">Desired unique value.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task DemandUniqueAsync<TEntity>(string propertyName, object value, CancellationToken ct = default) where TEntity : EntityBase
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var entitySet = Set<TEntity>();

            var entityExpression = Expression.Parameter(typeof(TEntity), "entity");
            var propertyExpression = Expression.Property(entityExpression, propertyName);
            var constant = Expression.Constant(value);
            var equalExpression = Expression.Equal(propertyExpression, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, entityExpression);

            if (await entitySet.Where(lambda).AnyAsync(ct) == true)
                throw new NonUniqueEntityValueException(propertyName, value, typeof(TEntity));
        }

        /// <summary>
        /// Demands that specified property value is unique.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TNonUniqueEntity">Non unique entity type.</typeparam>
        /// <param name="propertyName">Entity property.</param>
        /// <param name="value">Desired unique value.</param>
        /// <param name="existingId">Existing entity id for update operations.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Associated task.</returns>
        public async Task DemandUniqueAsync<TEntity, TNonUniqueEntity>(string propertyName, object value, int? existingId = null, CancellationToken ct = default) where TEntity : EntityBase
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var entitySet = Set<TEntity>();

            var entityExpression = Expression.Parameter(typeof(TEntity), "entity");
            var propertyExpression = Expression.Property(entityExpression, propertyName);
            var constant = Expression.Constant(value);
            var equalExpression = Expression.Equal(propertyExpression, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, entityExpression);

            if (existingId != null)
            {
                var existingEntityExpression = Expression.Parameter(typeof(TEntity), "entity");
                var existingPropertyExpression = Expression.Property(existingEntityExpression, nameof(EntityBase.Id));
                var existingConstant = Expression.Constant(existingId);
                var notEqualExpression = Expression.NotEqual(existingPropertyExpression, existingConstant);
                var notEqualExpressionLambda = Expression.Lambda<Func<TEntity, bool>>(notEqualExpression, existingEntityExpression);

                lambda = lambda.And(notEqualExpressionLambda);
            }

            if (await entitySet.Where(lambda).AnyAsync(ct) == true)
                throw new NonUniqueEntityValueException(propertyName, value, typeof(TNonUniqueEntity));
        }

        /// <summary>
        /// Restores permissions for specified users.
        /// </summary>
        /// <param name="userQuery">Users query.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="userQuery"/> is equal to null.</exception>
        public void RestorePermissions(IQueryable<User> userQuery)
        {
            if (userQuery == null)
                throw new ArgumentNullException(nameof(userQuery));
            RestorePermissions(userQuery, this);
        }

        /// <summary>
        /// Restores permissions for specified users.
        /// </summary>
        /// <param name="userQuery">Users query.</param>
        /// <param name="cx">Database context.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="userQuery"/> is equal to null.</exception>
        public void RestorePermissions(IQueryable<User> userQuery, DefaultDbContext cx)
        {
            if (userQuery == null)
                throw new ArgumentNullException(nameof(userQuery));

            using (var dbTransaction = Database.BeginTransaction())
            {
                // reset all user permission sets, this will allow the normal policies attached to the user to be used
                userQuery.ExecuteUpdate(userOperator => userOperator.SetProperty(entity => entity.PermissionSetId, entity => null));

                foreach (int userId in userQuery.Select(user => user.Id).ToList())
                {
                    cx.UserPermissions.Where(user => user.UserId == userId).ExecuteDelete();

                    var policyAttributes = Enum.GetValues<GizmoPolicies>()
                        .Select(policy => new
                        {
                            Policy = policy,
                            Description = policy.GetAttribute<PolicyDescriptionAttribute>()
                        })
                        .Where(policy => policy.Description != null)
                        .ToList();

                    var allPermissions = policyAttributes
                        .Select(claim =>
                        {
                            return new UserPermission()
                            {
                                UserId = userId,
                                Type = claim.Description.Resource,
                                Value = claim.Description.Operation,
                            };
                        });
                    cx.UserPermissions.AddRange(allPermissions);
                }
                SaveChanges();
                dbTransaction.Commit();
            }
        }

        /// <summary>
        /// Apply global configurations on common properties types
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void ApplyGlobalMapConfigurations(ModelBuilder modelBuilder)
        {
            ApplyDefaultTypesConfigurations(modelBuilder);

            GuardDatabaseNameExceedLimits(modelBuilder);

            // In our implementation the date time is stored without an time-zone in both MSSQL and Postgres databases
            // the following conversion applies an conversion rule that will store the date time as unspecified and read (treat) as UTC

            // two converters are needed since we have nullable and non-nullable date-times

            var utcNullableConverter = new ValueConverter<DateTime?, DateTime?>(storeDate => storeDate.HasValue ? DateTime.SpecifyKind(storeDate.Value, DateTimeKind.Unspecified) : storeDate,
                readDate => readDate.HasValue ? DateTime.SpecifyKind(readDate.Value, DateTimeKind.Utc) : readDate);

            var utcConverter = new ValueConverter<DateTime, DateTime>(storeDate => DateTime.SpecifyKind(storeDate, DateTimeKind.Unspecified),
                readDate => DateTime.SpecifyKind(readDate, DateTimeKind.Utc));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(utcNullableConverter);

                    if (property.ClrType == typeof(DateTime))
                        property.SetValueConverter(utcConverter);
                }
            }
        }

        /// <summary>
        /// Apply default types configurations
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        private void ApplyDefaultTypesConfigurations(ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model.GetEntityTypes().Select(e => e.ClrType).ToList();
            foreach (var entity in entities)
            {
                if (Database.IsSqlServer())
                {
                    //make all datetime properties to be mapped as SQL server datetime2
                    var dateTimeProperties = entity.GetProperties().Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType.GenericTypeArguments?.FirstOrDefault() == typeof(DateTime)).ToList();
                    foreach (var property in dateTimeProperties)
                        modelBuilder.Entity(entity).Property(property.Name).HasColumnType("datetime2");
                }

                //TODO: Here we change the decimals precision, in Branch entity we have latitude and longitude properties that have different precision BUT we do need to use the precision on some other
                //properties , an better way to do this must be found otherwise we could end up with not desired precision on some properties

                //make all nullable decimal properties to have 19,4 precision
                var nullableDecimalProperties = entity.GetProperties().Where(p => p.PropertyType == typeof(decimal?) || p.PropertyType.GenericTypeArguments?.FirstOrDefault() == typeof(decimal?)).ToList();
                foreach (var property in nullableDecimalProperties)
                    modelBuilder.Entity(entity).Property(property.Name).HasPrecision(19, 4);

                //branch has custom latitude/longitude decimal configuration, we should not alter the model
                if (entity == typeof(Branch))
                    continue;

                //make all decimal properties to have 19,4 precision
                var decimalProperties = entity.GetProperties().Where(p => p.PropertyType == typeof(decimal) || p.PropertyType.GenericTypeArguments?.FirstOrDefault() == typeof(decimal)).ToList();
                foreach (var property in decimalProperties)
                    modelBuilder.Entity(entity).Property(property.Name).HasPrecision(19, 4);
            }

            if (Database.IsNpgsql())
            {
                // Set all DateTime properties to be mapped as timestamp without time zone
                var datetimeProperties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                    .Where(x => x.ClrType == typeof(DateTime) || x.ClrType == typeof(DateTime?));

                foreach (var property in datetimeProperties)
                {
                    property.SetColumnType("timestamp without time zone");
                }
            }
        }

        /// <summary>
        /// Guard the tables/columns/indexes against exceed the max limit of chars in naming convention 
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        private static void GuardDatabaseNameExceedLimits(ModelBuilder modelBuilder)
        {
            const int MaxLengthLimit = 63;

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (tableName.Length > MaxLengthLimit)
                    throw new Exception($"Table {tableName} exceed the limit {MaxLengthLimit}");

                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName();
                    if (string.IsNullOrEmpty(columnName))
                        continue;

                    if (columnName.Length <= MaxLengthLimit)
                        continue;

                    throw new Exception($"Table {tableName} - Column {columnName} exceed the limit {MaxLengthLimit}");
                }

                foreach (var index in entity.GetIndexes())
                {
                    var indexName = index.GetDatabaseName();
                    if (string.IsNullOrEmpty(indexName))
                        continue;

                    if (indexName.Length <= MaxLengthLimit)
                        continue;

                    throw new Exception($"Table {tableName} - Index {indexName} exceed the limit {MaxLengthLimit}");
                }
            }
        }

        #endregion

        #region TRANSACTIONS

        /// <inheritdoc/>
        public IDatabaseTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <inheritdoc/>
        public IDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new DatabaseTransaction(Database.BeginTransaction(isolationLevel));
        }

        #endregion

        #region EXCEPTION HANDLERS

        /// <summary>
        /// Checks if the exception is retriable.
        /// </summary>
        /// <param name="ex">Exception.</param>
        /// <returns>True or false.</returns>
        /// <exception cref="ArgumentNullException">thrown in case <paramref name="ex"/> parameter is equal to null.</exception>
        public static bool IsRetriableException(Exception ex)
        {
            ArgumentNullException.ThrowIfNull(ex);

            if (ex.GetBaseException() is SqlException sqlException)
                return Enum.IsDefined(typeof(MSSQLServerRetriableErrors), sqlException.Number);

            else if (ex.GetBaseException() is NpgsqlException npgsqlException)
                return Enum.IsDefined(typeof(NPGSQLRetriableErrors), npgsqlException.ErrorCode);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="retries"></param>
        /// <param name="minWaitTime"></param>
        /// <param name="maxWaitTime"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void RetryBeforeThrow(Action action, int retries = 10, int minWaitTime = 100, int maxWaitTime = 1000)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            for (int tries = 1; tries <= retries; tries++)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    if (IsRetriableException(ex))
                    {
                        if (tries >= retries)
                        {
                            PreserveStackTrace(ex);
                            throw;
                        }

                        Thread.Sleep(new Random().Next(minWaitTime, maxWaitTime));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <param name="retries"></param>
        /// <param name="minWaitTime"></param>
        /// <param name="maxWaitTime"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static TResult RetryBeforeThrow<TResult>(Func<TResult> action, int retries = 10, int minWaitTime = 100, int maxWaitTime = 1000)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            for (int tries = 1; tries <= retries; tries++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    if (IsRetriableException(ex))
                    {
                        if (tries >= retries)
                        {
                            PreserveStackTrace(ex);
                            throw;
                        }

                        Thread.Sleep(new Random().Next(minWaitTime, maxWaitTime));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //this should not happen but just in case
            throw new ArgumentException("Maximum retries reached.", nameof(retries));
        }

        /// <summary>
        /// Retries an operation.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <param name="retries"></param>
        /// <param name="minWaitTime"></param>
        /// <param name="maxWaitTime"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<TResult> RetryBeforeThrowAsync<TResult>(Func<Task<TResult>> action, int retries = 10, int minWaitTime = 100, int maxWaitTime = 1000)
        {
            ArgumentNullException.ThrowIfNull(action);

            for (int tries = 1; tries <= retries; tries++)
            {
                try
                {
                    return await action();
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (IsRetriableException(ex))
                    {
                        if (tries >= retries)
                        {
                            PreserveStackTrace(ex);
                            throw;
                        }

                        await Task.Delay(new Random().Next(minWaitTime, maxWaitTime));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //this should not happen but just in case
            throw new ArgumentException("Maximum retries reached.", nameof(retries));
        }

        /// <summary>
        /// Retries an operation.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="retries"></param>
        /// <param name="minWaitTime"></param>
        /// <param name="maxWaitTime"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task RetryBeforeThrowAsync(Func<Task> action, int retries = 10, int minWaitTime = 100, int maxWaitTime = 1000)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            for (int tries = 1; tries <= retries; tries++)
            {
                try
                {
                    await action();
                    return;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (IsRetriableException(ex))
                    {
                        if (tries >= retries)
                        {
                            PreserveStackTrace(ex);
                            throw;
                        }

                        Thread.Sleep(new Random().Next(minWaitTime, maxWaitTime));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //this should not happen but just in case
            throw new ArgumentException("Maximum retries reached.", nameof(retries));
        }

        /// <summary>
        /// Sets a flag on an <see cref="T:System.Exception"/> so that all the stack trace information is preserved 
        /// when the exception is re-thrown.
        /// </summary>
        /// <remarks>This is useful because "throw" removes information, such as the original stack frame.</remarks>
        /// <see href="http://weblogs.asp.net/fmarguerie/archive/2008/01/02/rethrowing-exceptions-and-preserving-the-full-call-stack-trace.aspx"/>
        public static void PreserveStackTrace(Exception ex)
        {
            MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(ex, null);
        }

        #endregion

        #region MSSQLSERVERRETRYABLEERRORS

        /// <summary>
        /// Microsoft SQL Server retriable error codes.
        /// </summary>
        public enum MSSQLServerRetriableErrors
        {
            /// <summary>
            /// Timeout expired. The timeout period elapsed before completion of the operation.
            /// Error code: -2
            /// </summary>
            TimeoutExpired = -2,

            /// <summary>
            /// Encryption is not supported on the SQL Server instance.
            /// Error code: 20
            /// </summary>
            EncryptionNotSupported = 20,

            /// <summary>
            /// A login error occurred, often due to network issues.
            /// Error code: 64
            /// </summary>
            LoginError = 64,

            /// <summary>
            /// Connection initialization error. SQL Server is unable to initialize a connection.
            /// Error code: 233
            /// </summary>
            ConnectionInitialization = 233,

            /// <summary>
            /// Deadlock detected. One or more processes were chosen as the deadlock victim.
            /// Error code: 1205
            /// </summary>
            Deadlock = 1205,

            /// <summary>
            /// Transport-level error while receiving results from the server, typically caused by network issues.
            /// Error code: 10053
            /// </summary>
            TransportLevelReceiving = 10053,

            /// <summary>
            /// Transport-level error while sending results to the server, typically caused by network issues.
            /// Error code: 10054
            /// </summary>
            TransportLevelSending = 10054,

            /// <summary>
            /// Error while establishing a connection, often due to network timeouts or server unavailability.
            /// Error code: 10060
            /// </summary>
            EstablishingConnection = 10060,

            /// <summary>
            /// Error encountered while processing the request. Temporary server issue, retry might succeed.
            /// Error code: 40143
            /// </summary>
            ProcessingRequest = 40143,

            /// <summary>
            /// The service is currently too busy to process the request.
            /// Error code: 40501
            /// </summary>
            ServiceBusy = 40501,

            /// <summary>
            /// Database or server is temporarily unavailable. Retry after some time may succeed.
            /// Error code: 40613
            /// </summary>
            DatabaseOrServerNotAvailable = 40613
        }

        #endregion

        #region NPGSQLRETRIABLEERRORS

        /// <summary>
        /// Npgsql PostgreSQL retriable error codes.
        /// </summary>
        public enum NPGSQLRetriableErrors
        {
            /// <summary>
            /// A deadlock has been detected, and the transaction can be retried.
            /// Error code: 40P01
            /// </summary>
            DeadlockDetected = 40001,

            /// <summary>
            /// A transaction serialization failure occurred.
            /// Error code: 40001
            /// </summary>
            SerializationFailure = 40001,

            /// <summary>
            /// Connection exception due to a timeout, often retriable.
            /// Error code: 08006
            /// </summary>
            ConnectionExceptionTimeout = 8006,

            /// <summary>
            /// Could not obtain a lock on the resource, often retriable.
            /// Error code: 55P03
            /// </summary>
            LockNotAvailable = 55003,

            /// <summary>
            /// Too many connections, retrying later might succeed.
            /// Error code: 53300
            /// </summary>
            TooManyConnections = 53300,

            /// <summary>
            /// Server is too busy to handle the request, retry might succeed.
            /// Error code: 57P03
            /// </summary>
            CannotConnectNow = 57003
        }

        #endregion

        /// <summary>
        /// Checks if the exception is a unique constraint violation.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public bool IsUniqueViolation(DbUpdateException ex)
        {
            if (Database.IsSqlServer())
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    // SQL Server unique constraint violation error code
                    return sqlException.Number == 2627 || sqlException.Number == 2601;
                }
            }
            else if (Database.IsNpgsql())
            {
                if (ex.InnerException is PostgresException postgresException)
                {
                    return postgresException.SqlState == PostgresErrorCodes.UniqueViolation;
                }
            }

            return false;
        }
    }

    #endregion
}
