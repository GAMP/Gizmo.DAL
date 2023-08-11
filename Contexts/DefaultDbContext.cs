using Gizmo.DAL.Entities;
using Gizmo.DAL.Mappings;
using GizmoDALV2.Entities;
using GizmoDALV2.Mappings;
using GizmoDALV2.Migrations;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GizmoDALV2
{
    #region DEFAULTDBCONTEXT

    /// <summary>
    /// Default db context.
    /// </summary>
    [DbConfigurationType(typeof(DefaultConfig))]
    public class DefaultDbContext : DbContext, IGizmoDBContext
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Creates new static instance.
        /// </summary>
        static DefaultDbContext()
        {
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public DefaultDbContext()
            : base()
        {
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string.</param>
        public DefaultDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="existingConnection">Existing database connection.</param>
        /// <param name="owned">Indicates if connection is owned by this context. The connection will not be disposed when context is disposed if <paramref name="owned"/> equals to false.</param>
        public DefaultDbContext(DbConnection existingConnection, bool owned)
            : base(existingConnection, owned)
        { }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets icons.
        /// </summary>
        public DbSet<Icon> Icons { get; set; }

        /// <summary>
        /// Gets hosts.
        /// </summary>
        public virtual DbSet<Host> Hosts { get; set; }

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
        public virtual DbSet<HostGroup> HostGroups { get; set; }

        /// <summary>
        /// Gets user groups.
        /// </summary>
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Gets user group dissalowed host groups.
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
        public virtual DbSet<UserMember> UsersMember { get; set; }

        /// <summary>
        /// Gets operator users.
        /// </summary>
        public DbSet<UserOperator> UsersOperator { get; set; }

        /// <summary>
        /// Gets guest users.
        /// </summary>
        public DbSet<UserGuest> UsersGuest { get; set; }

        /// <summary>
        /// Gets user perimssions.
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
        public virtual DbSet<UserSession> Sessions { get; set; }

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
        /// Gets hostlayout group images.
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
        public virtual DbSet<BillProfile> BillProfiles { get; set; }

        /// <summary>
        /// Gets bill rates.
        /// </summary>
        public virtual DbSet<BillRate> BillRates { get; set; }

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
        public virtual DbSet<UsageSession> UsageSessions { get; set; }

        /// <summary>
        /// Gets usage.
        /// </summary>
        public virtual DbSet<Usage> Usage { get; set; }

        /// <summary>
        /// Gets usage user sessions.
        /// </summary>
        public DbSet<UsageUserSession> UsageUserSession { get; set; }

        /// <summary>
        /// Gets usage rate.
        /// </summary>
        public virtual DbSet<UsageRate> UsageRate { get; set; }

        /// <summary>
        /// Gets usage time.
        /// </summary>
        public virtual DbSet<UsageTime> UsageTime { get; set; }

        /// <summary>
        /// Gets usage time fixed.
        /// </summary>
        public virtual DbSet<UsageTimeFixed> UsageFixed { get; set; }

        /// <summary>
        /// Gets orders.
        /// </summary>
        public DbSet<ProductOrder> Orders { get; set; }

        /// <summary>
        /// Gets order lines.
        /// </summary>
        public DbSet<ProductOL> OrderLines { get; set; }

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
        /// Gets invoices.
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets invoice lines.
        /// </summary>
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

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
        public virtual DbSet<InvoiceLineTime> InvoiceLineTime { get; set; }

        /// <summary>
        /// Gets time fixed invoice lines.
        /// </summary>
        public virtual DbSet<InvoiceLineTimeFixed> InvoiceLineTimeFixed { get; set; }

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
        public virtual DbSet<ProductTime> ProductTimes { get; set; }

        /// <summary>
        /// Gets or sets product disallowed user groups.
        /// </summary>
        public DbSet<ProductUserDisallowed> ProductUserGroupDisallowed { get; set; }

        /// <summary>
        /// Get or sets product time dissalowed host groups.
        /// </summary>
        public virtual DbSet<ProductTimeHostDisallowed> ProductTimeHostDisallowed { get; set; }

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
        public virtual DbSet<ProductTimePeriod> ProductTimePeriods { get; set; }

        /// <summary>
        /// Gets product time period days.
        /// </summary>
        public DbSet<ProductTimePeriodDay> ProductTimePeriodDays { get; set; }

        /// <summary>
        /// Gets product time priods times.
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
        public DbSet<BundleProductUserPrice> BunledProductUserPrices { get; set; }

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
        public IDbSet<VoidDepositPayment> DepositPaymentVoids { get; set; }

        /// <summary>
        /// Gets deposit payment refunds.
        /// </summary>
        public IDbSet<RefundDepositPayment> DepositPaymentRefunds { get; set; }

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

        #endregion

        #region OVERRIDES
        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //make all date time to be mapped as SQL server datetime2
            modelBuilder.Properties<DateTime>()
                .Configure(x => x.HasColumnType("datetime2"));

            //make all decimal to have 19,4 precision
            modelBuilder.Properties<decimal>()
                .Configure(x => x.HasPrecision(19, 4));

            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new FeedMap());
            modelBuilder.Configurations.Add(new VariableMap());
            modelBuilder.Configurations.Add(new MappingMap());
            modelBuilder.Configurations.Add(new IconMap());
            modelBuilder.Configurations.Add(new AttributeMap());

            //TASK
            modelBuilder.Configurations.Add(new TaskBaseMap());
            modelBuilder.Configurations.Add(new TaskJunctionMap());
            modelBuilder.Configurations.Add(new TaskNotificationMap());
            modelBuilder.Configurations.Add(new TaskScriptMap());
            modelBuilder.Configurations.Add(new TaskProcessMap());
            modelBuilder.Configurations.Add(new ClientTaskMap());

            //USER
            modelBuilder.Configurations.Add(new UserGroupMap());
            modelBuilder.Configurations.Add(new UserGroupHostDisallowedMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserMemberMap());
            modelBuilder.Configurations.Add(new UserGuestMap());
            modelBuilder.Configurations.Add(new UserOperatorMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
            modelBuilder.Configurations.Add(new UserCredentialMap());
            modelBuilder.Configurations.Add(new UserSessionMap());
            modelBuilder.Configurations.Add(new UserSessionChangeMap());
            modelBuilder.Configurations.Add(new UserPictureMap());
            modelBuilder.Configurations.Add(new UserCreditLimitMap());

            //HOST
            modelBuilder.Configurations.Add(new HostMap());
            modelBuilder.Configurations.Add(new HostEndpointMap());
            modelBuilder.Configurations.Add(new HostComputerMap());
            modelBuilder.Configurations.Add(new HostGroupMap());
            modelBuilder.Configurations.Add(new HostLayoutGroupMap());
            modelBuilder.Configurations.Add(new HostLayoutGroupImageMap());
            modelBuilder.Configurations.Add(new HostLayoutGroupLayoutMap());

            //LOG
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new LogExceptionMap());

            //PLUGIN LIBRARY
            modelBuilder.Configurations.Add(new PluginLibraryMap());

            //APP
            modelBuilder.Configurations.Add(new AppEnterpriseMap());
            modelBuilder.Configurations.Add(new AppCategoryMap());
            modelBuilder.Configurations.Add(new AppGroupMap());
            modelBuilder.Configurations.Add(new AppGroupAppMap());
            modelBuilder.Configurations.Add(new AppMap());
            modelBuilder.Configurations.Add(new AppRatingMap());
            modelBuilder.Configurations.Add(new AppLinkMap());
            modelBuilder.Configurations.Add(new AppImageMap());
            modelBuilder.Configurations.Add(new AppExeMap());
            modelBuilder.Configurations.Add(new AppExeMaxUserMap());
            modelBuilder.Configurations.Add(new AppExeImageMap());
            modelBuilder.Configurations.Add(new AppExeTaskMap());
            modelBuilder.Configurations.Add(new AppExeDeploymentMap());
            modelBuilder.Configurations.Add(new AppExeLicenseMap());
            modelBuilder.Configurations.Add(new AppExePersonalFileMap());
            modelBuilder.Configurations.Add(new AppStatMap());
            modelBuilder.Configurations.Add(new AppExeCdImageMap());

            modelBuilder.Configurations.Add(new DeploymentMap());
            modelBuilder.Configurations.Add(new DeploymentDeploymentMap());
            modelBuilder.Configurations.Add(new PersonalFileMap());
            modelBuilder.Configurations.Add(new LicenseMap());
            modelBuilder.Configurations.Add(new LicenseKeyMap());

            modelBuilder.Configurations.Add(new UserAttributeMap());
            modelBuilder.Configurations.Add(new NoteMap());
            modelBuilder.Configurations.Add(new UserNoteMap());

            //SEC
            modelBuilder.Configurations.Add(new SecurityProfileMap());
            modelBuilder.Configurations.Add(new SecurityProfilePolicyMap());
            modelBuilder.Configurations.Add(new SecurityProfileRestrictionMap());

            modelBuilder.Configurations.Add(new MonetaryUnitMap());
            modelBuilder.Configurations.Add(new TaxMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new PaymentMap());

            modelBuilder.Configurations.Add(new BillProfileMap());
            modelBuilder.Configurations.Add(new BillRateMap());
            modelBuilder.Configurations.Add(new BillProfileRateStepMap());
            modelBuilder.Configurations.Add(new BillRatePeriodDayMap());
            modelBuilder.Configurations.Add(new BillRatePeriodTimeMap());
            modelBuilder.Configurations.Add(new UsageSessionMap());
            modelBuilder.Configurations.Add(new UsageBaseMap());
            modelBuilder.Configurations.Add(new UsageUserSessionMap());
            modelBuilder.Configurations.Add(new UsageTimeMap());
            modelBuilder.Configurations.Add(new UsageTimeFixedMap());
            modelBuilder.Configurations.Add(new UsageRateMap());

            modelBuilder.Configurations.Add(new ProductGroupMap());
            modelBuilder.Configurations.Add(new ProductBaseMap());
            modelBuilder.Configurations.Add(new ProductBaseExtendedMap());
            modelBuilder.Configurations.Add(new ProductPeriodMap());
            modelBuilder.Configurations.Add(new ProductPeriodDayMap());
            modelBuilder.Configurations.Add(new ProductPeriodDayTimeMap());
            modelBuilder.Configurations.Add(new ProductTimePeriodMap());
            modelBuilder.Configurations.Add(new ProductTimePeriodDayMap());
            modelBuilder.Configurations.Add(new ProductTimePeriodDayTimeMap());
            modelBuilder.Configurations.Add(new ProductTaxMap());
            modelBuilder.Configurations.Add(new ProductImageMap());
            modelBuilder.Configurations.Add(new ProductUserPriceMap());
            modelBuilder.Configurations.Add(new ProductUserDisallowedMap());
            modelBuilder.Configurations.Add(new ProductTimeHostDisallowedMap());
            modelBuilder.Configurations.Add(new ProductHostHiddenMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductTimeMap());

            modelBuilder.Configurations.Add(new ProductBundleMap());
            modelBuilder.Configurations.Add(new BundleProductMap());
            modelBuilder.Configurations.Add(new BundleProductUserPriceMap());

            modelBuilder.Configurations.Add(new ProductOrderMap());
            modelBuilder.Configurations.Add(new ProductOLExtendedMap());
            modelBuilder.Configurations.Add(new ProductOLBaseMap());
            modelBuilder.Configurations.Add(new ProductOLProductMap());
            modelBuilder.Configurations.Add(new ProductOLTimeMap());
            modelBuilder.Configurations.Add(new ProductOLTimeFixedMap());
            modelBuilder.Configurations.Add(new ProductOLSessionMap());

            modelBuilder.Configurations.Add(new PresetTimeSaleMap());
            modelBuilder.Configurations.Add(new PresetTimeSaleMoneyMap());

            //TRANSACTIONS
            modelBuilder.Configurations.Add(new DepositTransactionMap());
            modelBuilder.Configurations.Add(new PointTransactionMap());
            modelBuilder.Configurations.Add(new StockTransactionMap());

            //PAYMENT
            modelBuilder.Configurations.Add(new DepositPaymentMap());

            //INVOICING
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new InvoicePaymentMap());
            modelBuilder.Configurations.Add(new InvoiceLineMap());
            modelBuilder.Configurations.Add(new InvoiceLineExtendedMap());
            modelBuilder.Configurations.Add(new InvoiceLineProductMap());
            modelBuilder.Configurations.Add(new InvoiceLineTimeMap());
            modelBuilder.Configurations.Add(new InvoiceLineTimeFixedMap());
            modelBuilder.Configurations.Add(new InvoiceLineSessionMap());

            modelBuilder.Configurations.Add(new ShiftMap());
            modelBuilder.Configurations.Add(new ShiftCountMap());
            modelBuilder.Configurations.Add(new RegisterMap());
            modelBuilder.Configurations.Add(new RegisterTransactionMap());

            modelBuilder.Configurations.Add(new AssetTypeMap());
            modelBuilder.Configurations.Add(new AssetMap());
            modelBuilder.Configurations.Add(new AssetTransactionMap());

            modelBuilder.Configurations.Add(new VoidMap());
            modelBuilder.Configurations.Add(new VoidInvoiceMap());
            modelBuilder.Configurations.Add(new RefundMap());
            modelBuilder.Configurations.Add(new RefundInvoicePaymentMap());
            modelBuilder.Configurations.Add(new ProductBundleUserPriceMap());
            modelBuilder.Configurations.Add(new HostGroupUserBillProfileMap());

            modelBuilder.Configurations.Add(new HostGroupWaitingLineMap());
            modelBuilder.Configurations.Add(new HostGroupWaitingLineEntryMap());

            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new VerificationMap());
            modelBuilder.Configurations.Add(new VerificationEmailMap());
            modelBuilder.Configurations.Add(new VerificationMobilePhoneMap());
            modelBuilder.Configurations.Add(new ReservationMap());
            modelBuilder.Configurations.Add(new ReservationUserMap());
            modelBuilder.Configurations.Add(new ReservationHostMap());

            //devices
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceHdmiMap());
            modelBuilder.Configurations.Add(new DeviceHostMap());

            modelBuilder.Configurations.Add(new VoidDepositPaymentMap());
            modelBuilder.Configurations.Add(new RefundDepositPaymentMap());
            modelBuilder.Configurations.Add(new FiscalReceiptMap());
            modelBuilder.Configurations.Add(new InvoiceFiscalReceiptMap());

            modelBuilder.Configurations.Add(new UserAgreementMap());
            modelBuilder.Configurations.Add(new UserAgreementStateMap());

            modelBuilder.Configurations.Add(new PaymentIntentMap());
            modelBuilder.Configurations.Add(new PaymentIntentDepositMap());
            modelBuilder.Configurations.Add(new PaymentIntentOrderMap());

            //IGNORES
            modelBuilder.Ignore<DiscountBase>();
            modelBuilder.Ignore<DiscountTimePeriod>();
            modelBuilder.Ignore<DiscountTimePeriodDayTime>();
            modelBuilder.Ignore<DiscountTimePeriodWeekDay>();
        }

        public override int SaveChanges()
        {
            #region OBJECT CONTEXT

            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;


            IEnumerable<ObjectStateEntry> objectStateEntries = from e in context
                                                               .ObjectStateManager
                                                               .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted)
                                                               where
                                                               e.IsRelationship == false &&
                                                               e.Entity != null
                                                               select e;

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
                            modifiedEntity.RejectPropertyChanges(nameof(ICreatable.CreatedTime));
                        }

                        if (!modifiedEntity.GetModifiedProperties().Any(PROPERTY => PROPERTY == nameof(IModifiedBy.ModifiedTime)))
                        {
                            modifiedEntity.SetModifiedProperty(nameof(IModifiedBy.ModifiedTime));
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
                            modifiedEntity.RejectPropertyChanges(nameof(ICreatable.CreatedTime));
                            modifiedEntity.RejectPropertyChanges(nameof(ICreatedBy.CreatedById));
                        }

                        if (!modifiedEntity.GetModifiedProperties().Any(PROPERTY => PROPERTY == nameof(IModifiedBy.ModifiedById)))
                        {
                            modifiedEntity.SetModifiedProperty(nameof(IModifiedBy.ModifiedById));
                        }
                    }
                }

                #endregion

                #region IReplicatable

                if (iModifiedBy is IReplicatable iReplicate)
                {
                    modifiedEntity.RejectPropertyChanges(nameof(IReplicatable.Guid));
                }

                #endregion

                #region IDeleteable
                var iDeletable = modifiedEntity.Entity as IDeletable;
                #endregion
            }
            #endregion

            #region EVENT GENERATION

            IList<IEntityEventArgs> events = new List<IEntityEventArgs>();
            var handler = DefaultDbContext.EntityEvent;
            if (handler != null)
            {
                var addedGroups = addedEntries.GroupBy(x => x.Entity.GetType());
                var modifiedGroups = modifiedEntries.GroupBy(x => x.Entity.GetType());
                var deltedGroups = deletedEntries.GroupBy(x => x.Entity.GetType());

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

                foreach (var entityGroup in deltedGroups)
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

                // if save was sucessfull save events to cache
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            #region OBJECT CONTEXT

            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;


            IEnumerable<ObjectStateEntry> objectStateEntries = from e in context
                                                               .ObjectStateManager
                                                               .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted)
                                                               where
                                                               e.IsRelationship == false &&
                                                               e.Entity != null
                                                               select e;

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
                            modifiedEntity.RejectPropertyChanges(nameof(ICreatable.CreatedTime));
                        }

                        if (!modifiedEntity.GetModifiedProperties().Any(PROPERTY => PROPERTY == nameof(IModifiedBy.ModifiedTime)))
                        {
                            modifiedEntity.SetModifiedProperty(nameof(IModifiedBy.ModifiedTime));
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
                            modifiedEntity.RejectPropertyChanges(nameof(ICreatable.CreatedTime));
                            modifiedEntity.RejectPropertyChanges(nameof(ICreatedBy.CreatedById));
                        }

                        if (!modifiedEntity.GetModifiedProperties().Any(PROPERTY => PROPERTY == nameof(IModifiedBy.ModifiedById)))
                        {
                            modifiedEntity.SetModifiedProperty(nameof(IModifiedBy.ModifiedById));
                        }
                    }
                }

                #endregion

                #region IReplicatable

                if (iModifiedBy is IReplicatable iReplicate)
                {
                    modifiedEntity.RejectPropertyChanges(nameof(IReplicatable.Guid));
                }

                #endregion

                #region IDeleteable
                var iDeletable = modifiedEntity.Entity as IDeletable;
                #endregion
            }
            #endregion

            #region EVENT GENERATION

            IList<IEntityEventArgs> events = new List<IEntityEventArgs>();
            var handler = DefaultDbContext.EntityEvent;
            if (handler != null)
            {
                var addedGroups = addedEntries.GroupBy(x => x.Entity.GetType());
                var modifiedGroups = modifiedEntries.GroupBy(x => x.Entity.GetType());
                var deltedGroups = deletedEntries.GroupBy(x => x.Entity.GetType());

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

                foreach (var entityGroup in deltedGroups)
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

                // if save was sucessfull save events to cache
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

        /// <summary>
        /// Implements default method to be used by SaltGenerator delegate
        /// </summary>
        /// <returns>String value that can be used as salt.</returns>
        public byte[] GetNewSalt()
        {
            byte[] salt = new byte[100];
            using (var generator = RNGCryptoServiceProvider.Create())
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
            using (SHA512 hasher = SHA512Managed.Create())
            {
                return hasher.ComputeHash(bytes.ToArray());
            }
        }

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
        /// Check if proxy is of specified type.
        /// </summary>
        /// <param name="type">Poco type.</param>
        /// <returns>True or false.</returns>
        public bool IsProxy(object type)
        {
            return type != null && ObjectContext.GetObjectType(type.GetType()) != type.GetType();
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
                entity = await Set<TEntity>().FindAsync(ct, entityKey);
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

        #endregion

        #region NOTIFICATIONS

        #region EVENTS
        /// <summary>
        /// Occurs on entity event.
        /// <remarks>
        /// A type must be registered or RaiseAllEntityEvents set to true in order to notification be raised.
        /// </remarks>
        /// </summary>
        public static event EventHandler<IEntityEventArgs> EntityEvent;
        #endregion

        #region FIELDS
        private static HashSet<Type> notifyTypes;
        private HashSet<IEntityEventArgs> eventCache = new HashSet<IEntityEventArgs>();
        private bool isEventsCached = false;
        private static bool raiseAllEntityEvents;
        #endregion

        #region PROPERTIES

        private static HashSet<Type> NotifyTypes
        {
            get
            {
                if (DefaultDbContext.notifyTypes == null)
                    DefaultDbContext.notifyTypes = new HashSet<Type>();
                return DefaultDbContext.notifyTypes;
            }
        }

        private HashSet<IEntityEventArgs> EventCache
        {
            get
            {
                if (eventCache == null)
                    eventCache = new HashSet<IEntityEventArgs>();
                return eventCache;
            }
            set
            {
                eventCache = value;
            }
        }

        /// <summary>
        /// Gets or sets if event caching enabled.
        /// </summary>
        public bool IsEventsCached
        {
            get { return isEventsCached; }
            set
            {
                isEventsCached = value;
            }
        }

        /// <summary>
        /// Gets if all events should raised ignoring the notification types.
        /// </summary>
        public static bool RaiseAllEntityEvents
        {
            get { return DefaultDbContext.raiseAllEntityEvents; }
            set { DefaultDbContext.raiseAllEntityEvents = value; }
        }

        #endregion

        #region FUNCTIONS

        /// <summary>
        /// Registers a type for notification.
        /// </summary>
        /// <param name="type">Type.</param>
        public static void RegisterNotification(Type type)
        {
            if (!DefaultDbContext.NotifyTypes.Contains(type))
                DefaultDbContext.NotifyTypes.Add(type);
        }

        /// <summary>
        /// Registers a type for notification.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        public static void RegisterNotification<T>()
        {
            DefaultDbContext.RegisterNotification(typeof(T));
        }

        /// <summary>
        /// Check if specified type is registered for notification.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <returns>True or false.</returns>
        public static bool IsNotificationRegistered(Type type)
        {
            if (DefaultDbContext.RaiseAllEntityEvents)
                return true;

            return DefaultDbContext.NotifyTypes.Contains(type);
        }

        /// <summary>
        /// Raises cached events.
        /// </summary>
        public void RaiseEventCache()
        {
            try
            {
                var handler = DefaultDbContext.EntityEvent;
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
        /// Sets event cache from exising enumerable.
        /// </summary>
        /// <param name="cache">Cache source.</param>
        public void SetEventCache(IEnumerable<IEntityEventArgs> cache)
        {
            if (cache == null)
                throw new ArgumentNullException(nameof(cache));

            EventCache = new HashSet<IEntityEventArgs>(cache);
        }

        #endregion

        #endregion

        #region SEEDING
        /// <summary>
        /// Gets or sets if only basic data seeding enabled.
        /// </summary>
        public bool IsSeedOnlyBasicEnabled
        {
            get; set;
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

        /// <summary>
        /// Checks if the exception is retriable.
        /// </summary>
        /// <param name="ex">Exception.</param>
        /// <returns>True or false.</returns>
        /// <exception cref="ArgumentNullException">thrown in case <paramref name="ex"/> parameter is equal to null.</exception>
        public static bool IsRetriableException(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            if (ex.GetBaseException() is SqlException sqlException)
                return Enum.IsDefined(typeof(MSSQLServerRetryableErrors), sqlException.Number);

            return false;
        }

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
            SaveChanges();
        }

        /// <summary>
        /// Restores permissions for specified users.
        /// </summary>
        /// <param name="userQuery">Users query.</param>
        /// <param name="cx">Database context.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="userQuery"/> is equal to null.</exception>
        public void RestorePermissions(IQueryable<User> userQuery,
            DefaultDbContext cx)
        {
            if (userQuery == null)
                throw new ArgumentNullException(nameof(userQuery));

            foreach (var @operator in userQuery.ToList())
            {
                cx.UserPermissions.RemoveRange(@operator.Permissions);
                var allPermissions = IntegrationLib.ClaimTypeBase.GetClaimTypes()
                    .Select(claim =>
                    {
                        return new UserPermission()
                        {
                            Type = claim.Resource,
                            Value = claim.Operation,
                        };
                    }).ToList();
                @operator.Permissions.UnionWith(allPermissions);
            }
        }
    }

    #endregion

    #region DEFAULTCONFIG
    /// <summary>
    /// Default configuration for use with base context.
    /// </summary>
    public class DefaultConfig : DbConfiguration
    {
        #region CONSTRUCTOR
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public DefaultConfig()
            : base()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory());
            SetDatabaseInitializer(new MSSQLInitializer());
        } 
        #endregion
    }
    #endregion

    #region DEFAULTINITIALIZER

    /// <summary>
    /// Database initializer.
    /// </summary>
    /// <typeparam name="TContextType">Context type.</typeparam>
    /// <typeparam name="TConfiguration">Context configuration.</typeparam>
    public class CreateAndMigrateDatabaseInitializer<TContextType, TConfiguration> :
        IDatabaseInitializer<TContextType>
        where TContextType : DefaultDbContext
        where TConfiguration : DbMigrationsConfiguration<TContextType>, new()
    {
        #region FIELDS
        private readonly DbMigrationsConfiguration _configuration;
        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public CreateAndMigrateDatabaseInitializer()
        {
            _configuration = new TConfiguration();
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="connection">Database connection string.</param>
        public CreateAndMigrateDatabaseInitializer(string connection)
        {
            Contract.Requires(!string.IsNullOrEmpty(connection), "connection");

            _configuration = new TConfiguration
            {
                TargetDatabase = new DbConnectionInfo(connection)
            };
        }

        #endregion        

        #region OVERRIDES

        /// <summary>
        /// Seeds data.
        /// </summary>
        /// <param name="context">Context type.</param>
        protected virtual void Seed(TContextType context)
        {
            AddDefaultOperator(context);
            AddPaymentMethods(context);
            AddMonetaryUnits(context);
            AddLayoutGroups(context);
            AddPresetTime(context);

            if (!context.IsSeedOnlyBasicEnabled)
            {
                AddTaxes(context);
                AddBillProfiles(context);
                AddUserGroups(context);
                AddUsers(context);
                AddHostGroups(context);
                AddProducts(context);
                AddHosts(context);
            }
        }

        #endregion

        #region SEED METHODS

        private void AddPaymentMethods(TContextType cx)
        {
            cx.PaymentMethods.AddOrUpdate(new PaymentMethod() { Id = (int)PaymentMethodType.Cash, Name = "Cash", DisplayOrder = 0, IsEnabled = true, IsClient = true, IsManager = true });
            cx.PaymentMethods.AddOrUpdate(new PaymentMethod() { Id = (int)PaymentMethodType.Points, Name = "Points", DisplayOrder = 2, IsEnabled = true, IsClient = true, IsManager = true });
            cx.PaymentMethods.AddOrUpdate(new PaymentMethod() { Id = (int)PaymentMethodType.Deposit, Name = "Deposit", DisplayOrder = 3, IsEnabled = true, IsClient = true, IsManager = true });
            cx.PaymentMethods.AddOrUpdate(new PaymentMethod() { Id = (int)PaymentMethodType.CreditCard, Name = "Credit Card", DisplayOrder = 4, IsEnabled = true, IsClient = true, IsManager = true });
        }

        private void AddMonetaryUnits(TContextType cx)
        {
            string isoCurrencySymbol = RegionInfo.CurrentRegion.ISOCurrencySymbol;

            if (isoCurrencySymbol == "EUR")
            {
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "1 Cent", Value = 0.01M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "5 Cent", Value = 0.05M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "10 Cent", Value = 0.10M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "20 Cent", Value = 0.20M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "50 Cent", Value = 0.50M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "1 Euro", Value = 1.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "2 Euro", Value = 2.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "5 Euro", Value = 5.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "10 Euro", Value = 10.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "20 Euro", Value = 20.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "50 Euro", Value = 50.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "100 Euro", Value = 100.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "200 Euro", Value = 200.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "500 Euro", Value = 500.00M });
            }
            else
            {
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "1 Cent", Value = 0.01M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "5 Cent", Value = 0.05M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "10 Cent", Value = 0.10M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "25 Cent", Value = 0.25M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "1 Dollar", Value = 1.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "2 Dollar", Value = 2.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "5 Dollar", Value = 5.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "10 Dollar", Value = 10.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "20 Dollar", Value = 20.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "50 Dollar", Value = 50.00M });
                cx.MonetaryUnits.Add(new MonetaryUnit() { Name = "100 Dollar", Value = 100.00M });
            }

            var newList = cx.MonetaryUnits.Local.ToList();
            newList.ForEach(unit =>
            {
                unit.DisplayOrder = newList.IndexOf(unit);
            });
        }

        private void AddDefaultOperator(TContextType cx)
        {
            var defaultOperator = new UserOperator();

            byte[] salt = cx.GetNewSalt();
            byte[] password = cx.GetHashedPassword("admin", salt);

            defaultOperator.UserCredential = new UserCredential();
            defaultOperator.Username = "Admin";

            defaultOperator.CreatedTime = DateTime.Now;
            defaultOperator.UserCredential.Salt = salt;
            defaultOperator.UserCredential.Password = password;

            var allPermissions = IntegrationLib.ClaimTypeBase.GetClaimTypes().Select(claim =>
            {
                return new UserPermission() { Type = claim.Resource, Value = claim.Operation };
            });

            defaultOperator.Permissions.UnionWith(allPermissions);

            cx.UsersOperator.AddOrUpdate(defaultOperator);
        }

        private void AddProducts(TContextType cx)
        {
            var tax = cx.Taxes.Local.First();

            var timeGroup = cx.ProductGroups.Add(new ProductGroup() { Name = "Time Offers", DisplayOrder = 0 });
            var foodGrooup = cx.ProductGroups.Add(new ProductGroup() { Name = "Food", DisplayOrder = 1 });
            var drinksGroup = cx.ProductGroups.Add(new ProductGroup() { Name = "Drinks", DisplayOrder = 2 });
            var sweetsGroup = cx.ProductGroups.Add(new ProductGroup() { Name = "Sweets", DisplayOrder = 3 });

            var product = new Product()
            {
                Name = "Mars Bar",
                Cost = 0.90m,
                Price = 1.10m,
                Points = 10,
                StockOptions = StockOptionType.EnableStock
            };

            product.Taxes.Add(new ProductTax() { Tax = tax });
            product.ProductGroup = sweetsGroup;
            sweetsGroup.Products.Add(product);
            cx.Products.Add(product);

            product = new Product()
            {
                Name = "Snickers Bar",
                Points = 15,
                StockOptions = StockOptionType.EnableStock,
                Cost = 1.20m,
                Price = 2.0m
            };
            product.Taxes.Add(new ProductTax() { Tax = tax });
            product.ProductGroup = sweetsGroup;
            sweetsGroup.Products.Add(product);
            cx.Products.Add(product);

            var bundle = new ProductBundle()
            {
                Name = "Pizza and Cola",
                StockOptions = StockOptionType.EnableStock,
                Points = 200,
                Price = 3.40m, //pizza plus cola
                ProductGroup = foodGrooup
            };
            bundle.Taxes.Add(new ProductTax() { Tax = tax });

            var pizza = new Product()
            {
                Name = "Pizza (Small)",
                ProductGroup = foodGrooup,
                Cost = 2.20m,
                Price = 6.0m
            };
            pizza.Taxes.Add(new ProductTax() { Tax = tax });
            cx.Products.Add(pizza);
            bundle.BundledProducts.Add(new BundleProduct() { Price = 1, Product = pizza, Quantity = 1 });

            var cola = new Product()
            {
                Name = "Coca Cola (Can)",
                ProductGroup = drinksGroup,
                Cost = 1.20m,
                Price = 2.0m
            };
            cola.Taxes.Add(new ProductTax() { Tax = tax });
            cx.Products.Add(cola);
            bundle.BundledProducts.Add(new BundleProduct() { Price = 2, Product = cola, Quantity = 1 });

            cx.Products.Add(bundle);

            var productTime = new ProductTime()
            {
                UsePeriod = new ProductTimePeriod()
            };
            productTime.Taxes.Add(new ProductTax() { Tax = tax });
            productTime.Minutes = 360;
            productTime.Price = 12;
            productTime.WeekEndMaxMinutes = null;
            productTime.Name = "Six Hours (6)";
            productTime.ProductGroup = timeGroup;
            timeGroup.Products.Add(productTime);
            cx.Products.Add(productTime);

            productTime = new ProductTime();
            productTime.Taxes.Add(new ProductTax() { Tax = tax });
            productTime.Minutes = 360;
            productTime.Price = 16;
            productTime.WeekEndMaxMinutes = null;
            productTime.Period = new ProductPeriod()
            {
                Options = PeriodOptionType.None
            };
            productTime.Period.Days.Add(new ProductPeriodDay() { Day = DayOfWeek.Saturday });
            productTime.Period.Days.Add(new ProductPeriodDay() { Day = DayOfWeek.Sunday });
            productTime.Name = "Six Hours (6 Weekends)";
            productTime.ProductGroup = timeGroup;
            timeGroup.Products.Add(productTime);
            cx.Products.Add(productTime);
        }

        private void AddTaxes(TContextType cx)
        {
            cx.Taxes.AddOrUpdate(new Tax() { Name = "24%", Value = 23, UseOrder = 0 });
            cx.Taxes.AddOrUpdate(new Tax() { Name = "16%", Value = 16, UseOrder = 1 });
            cx.Taxes.AddOrUpdate(new Tax() { Name = "None", Value = 0, UseOrder = 2 });
        }

        private void AddLayoutGroups(TContextType cx)
        {
            var hostLayoutGroup = new HostLayoutGroup()
            {
                Name = "Default",
                DisplayOrder = 0
            };
            cx.HostLayoutGroups.Add(hostLayoutGroup);
        }

        private void AddBillProfiles(TContextType cx)
        {
            var billProfile = cx.BillProfiles.Add(new BillProfile() { Name = "Member Prices" });
            var rate = new BillRate()
            {
                BillProfile = billProfile,
                IsDefault = true,
                MinimumFee = 2,
                ChargeAfter = 1,
                ChargeEvery = 5,
                Rate = 2,
                StartFee = 1
            };

            billProfile.BillRates.Add(rate);
            cx.BillRates.Add(rate);

            billProfile = cx.BillProfiles.Add(new BillProfile() { Name = "Guests Prices" });
            rate = new BillRate()
            {
                BillProfile = billProfile,
                IsDefault = true,
                MinimumFee = 2,
                ChargeAfter = 1,
                ChargeEvery = 5,
                Rate = 2,
                StartFee = 1
            };

            billProfile.BillRates.Add(rate);
            cx.BillRates.Add(rate);
        }

        private void AddUserGroups(TContextType cx)
        {
            var memberPrices = cx.BillProfiles.Local.Where(x => x.Name == "Member Prices").First();
            var guestPrices = cx.BillProfiles.Local.Where(x => x.Name == "Guests Prices").First();

            cx.UserGroups.Add(new UserGroup() { Name = "Members", BillProfile = memberPrices, IsDefault = true });
            cx.UserGroups.Add(new UserGroup() { Name = "Guests", Options = UserGroupOptionType.GuestUse, BillProfile = guestPrices });
        }

        private void AddUsers(TContextType cx)
        {
            var group = cx.UserGroups.Local.Where(x => x.Name == "Members").FirstOrDefault();
            cx.Users.Add(new UserMember() { Username = "User", UserGroup = group });
        }

        private void AddHostGroups(TContextType cx)
        {
            var defaultGuestGroup = cx.UserGroups.Local.Where(x => x.Name == "Guests").FirstOrDefault();

            cx.HostGroups.AddOrUpdate(new HostGroup() { Name = "Computers", DefaultGuestGroup = defaultGuestGroup });
            cx.HostGroups.AddOrUpdate(new HostGroup() { Name = "Endpoints", DefaultGuestGroup = defaultGuestGroup });
        }

        private void AddHosts(TContextType cx)
        {
            var hostGroup = cx.HostGroups.Local.LastOrDefault();

            if (hostGroup != null)
            {
                cx.HostEndpoint.Add(new HostEndpoint() { Name = "XBOX-ONE-1", Number = 1, MaximumUsers = 4, HostGroup = hostGroup });
                cx.HostEndpoint.Add(new HostEndpoint() { Name = "XBOX-ONE-2", Number = 2, MaximumUsers = 4, HostGroup = hostGroup });
                cx.HostEndpoint.Add(new HostEndpoint() { Name = "PS4-1", Number = 3, MaximumUsers = 4, HostGroup = hostGroup });
                cx.HostEndpoint.Add(new HostEndpoint() { Name = "WII-1", Number = 4, MaximumUsers = 4, HostGroup = hostGroup });
            }
        }

        private void AddPresetTime(TContextType cx)
        {
            cx.PresetTimeSale.Add(new PresetTimeSale() { Value = 1 });
            cx.PresetTimeSale.Add(new PresetTimeSale() { Value = 5 });
            cx.PresetTimeSale.Add(new PresetTimeSale() { Value = 15 });
            cx.PresetTimeSale.Add(new PresetTimeSale() { Value = 30 });
            cx.PresetTimeSale.Add(new PresetTimeSale() { Value = 60 });

            cx.PresetTimeSaleMoney.Add(new PresetTimeSaleMoney() { Value = 1 });
            cx.PresetTimeSaleMoney.Add(new PresetTimeSaleMoney() { Value = 2 });
            cx.PresetTimeSaleMoney.Add(new PresetTimeSaleMoney() { Value = 5 });
            cx.PresetTimeSaleMoney.Add(new PresetTimeSaleMoney() { Value = 10 });
            cx.PresetTimeSaleMoney.Add(new PresetTimeSaleMoney() { Value = 20 });
        }

        #endregion

        #region IDatabaseInitializer

        void IDatabaseInitializer<TContextType>.InitializeDatabase(TContextType context)
        {
            Contract.Requires(context != null, "context");

            if (context.Database.Exists())
            {
                var connection = context.Database.Connection;

                var internalContextProperty = context.Configuration.GetType().GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var internalContextValue = internalContextProperty.GetValue(context.Configuration);
                var providerNameProp = internalContextValue.GetType().GetProperty("ProviderName", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var providerNameValue = providerNameProp.GetValue(internalContextValue) as string;

                _configuration.TargetDatabase = new DbConnectionInfo(connection.ConnectionString, providerNameValue);

                var migrator = new DbMigrator(_configuration);
                var pendingMigrations = migrator.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    migrator.Update();
                }
            }
            else
            {
                context.Database.Create();
                Seed(context);
                context.SaveChanges();
            }
        }

        #endregion
    }

    #endregion

    #region MSSQLINITIALIZER
    /// <summary>
    /// Microsoft SQL Server initializer.
    /// </summary>
    public class MSSQLInitializer : CreateAndMigrateDatabaseInitializer<DefaultDbContext, MSSQLConfiguration>
    {
    }
    #endregion

    #region SQLSERVERCUSTOMMIGRATIONSQLGENERATOR
    /// <summary>
    /// Microsoft SQL Server migration generator.
    /// </summary>
    public class SqlServerCustomMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
    }
    #endregion

    #region MSSQLSERVERRETRYABLEERRORS
    /// <summary>
    /// Microsoft SQL Server retriable error codes.
    /// </summary>
    public enum MSSQLServerRetryableErrors
    {
        TimeoutExpired = -2,
        EncryptionNotSupported = 20,
        LoginError = 64,
        ConnectionInitialization = 233,
        Deadlock = 1205,
        TransportLevelReceiving = 10053,
        TransportLevelSending = 10054,
        EstablishingConnection = 10060,
        ProcessingRequest = 40143,
        ServiceBusy = 40501,
        DatabaseOrServerNotAvailable = 40613
    }
    #endregion
}
