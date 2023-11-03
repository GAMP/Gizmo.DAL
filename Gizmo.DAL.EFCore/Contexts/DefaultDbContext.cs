using Gizmo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CoreLib;
using Microsoft.EntityFrameworkCore.Metadata;
using GizmoDALV2;
using Gizmo.DAL.Mappings;
using SharedLib;
using System.Globalization;

namespace Gizmo.DAL.Contexts
{
    #region DEFAULTDBCONTEXT

    /// <summary>
    /// Default db context.
    /// </summary>
    public class DefaultDbContext : DbContext, IGizmoDBContext
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Default constructor for dependency injection
        /// </summary>
        /// <param name="options">Default database options</param>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
            Database.EnsureCreated();
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
        /// Get or sets product time dissalowed host groups.
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SettingMap());
            modelBuilder.ApplyConfiguration(new NewsMap());
            modelBuilder.ApplyConfiguration(new FeedMap());
            modelBuilder.ApplyConfiguration(new VariableMap());
            modelBuilder.ApplyConfiguration(new MappingMap());
            modelBuilder.ApplyConfiguration(new IconMap());
            modelBuilder.ApplyConfiguration(new AttributeMap());

            //TASK
            modelBuilder.ApplyConfiguration(new TaskBaseMap());
            modelBuilder.ApplyConfiguration(new TaskJunctionMap());
            modelBuilder.ApplyConfiguration(new TaskNotificationMap());
            modelBuilder.ApplyConfiguration(new TaskScriptMap());
            modelBuilder.ApplyConfiguration(new TaskProcessMap());
            modelBuilder.ApplyConfiguration(new ClientTaskMap());

            //USER
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

            //HOST
            modelBuilder.ApplyConfiguration(new HostMap());
            modelBuilder.ApplyConfiguration(new HostEndpointMap());
            modelBuilder.ApplyConfiguration(new HostComputerMap());
            modelBuilder.ApplyConfiguration(new HostGroupMap());
            modelBuilder.ApplyConfiguration(new HostLayoutGroupMap());
            modelBuilder.ApplyConfiguration(new HostLayoutGroupImageMap());
            modelBuilder.ApplyConfiguration(new HostLayoutGroupLayoutMap());

            //LOG
            modelBuilder.ApplyConfiguration(new LogMap());
            modelBuilder.ApplyConfiguration(new LogExceptionMap());

            //PLUGIN LIBRARY
            modelBuilder.ApplyConfiguration(new PluginLibraryMap());

            //APP
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

            //SEC
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

            //TRANSACTIONS
            modelBuilder.ApplyConfiguration(new DepositTransactionMap());
            modelBuilder.ApplyConfiguration(new PointTransactionMap());
            modelBuilder.ApplyConfiguration(new StockTransactionMap());

            //PAYMENT
            modelBuilder.ApplyConfiguration(new DepositPaymentMap());

            //INVOICING
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

            //devices
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

            //GLOBAL CONFIGURATIONS
            ApplyGlobalMapConfigurations(modelBuilder);

            //SEED
            Seed(modelBuilder);

            //BASE MODEL CREATION
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Override Save Changes
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
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

            IList<IEntityEventArgs> events = new List<IEntityEventArgs>();
            var handler = EntityEvent;
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

        /// <summary>
        /// Override Save Changes Async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
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

            IList<IEntityEventArgs> events = new List<IEntityEventArgs>();
            var handler = EntityEvent;
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
        public static byte[] GetNewSalt()
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
        public static byte[] GetHashedPassword(string pwd, byte[] salt)
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
            // TO DO
            //return type != null && ObjectContext.GetObjectType(type.GetType()) != type.GetType();

            return false;
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
        public void RestorePermissions(IQueryable<User> userQuery, DefaultDbContext cx)
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

        /// <summary>
        /// Apply gloable configurations on common properties types
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void ApplyGlobalMapConfigurations(ModelBuilder modelBuilder)
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

                //make all decimal properties to have 19,4 precision
                var decimalProperties = entity.GetProperties().Where(p => p.PropertyType == typeof(decimal) || p.PropertyType.GenericTypeArguments?.FirstOrDefault() == typeof(decimal)).ToList();
                foreach (var property in decimalProperties)
                    modelBuilder.Entity(entity).Property(property.Name).HasPrecision(19, 4);
            }


            if (Database.IsSqlServer())
            {
                // Change default generated index names of foreign keys to match the old database pattern 
                RenameIndexWithOldPattern(modelBuilder);
            }
            else if (Database.IsNpgsql())
            {
                // Remove duplicated index name to be unique on database level not the table level
                RenameDuplicatedIndexNames(modelBuilder);

                // Set all DateTime properties to be mapped as timestamp without time zone
                foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                                             .SelectMany(t => t.GetProperties())
                                                                             .Where(x => x.ClrType == typeof(DateTime) || x.ClrType == typeof(DateTime?)))
                {
                    property.SetColumnType("timestamp without time zone");
                }
            }
        }

        /// <summary>
        /// Change default generated index names of foreign keys to match the old database pattern 
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void RenameIndexWithOldPattern(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var index in entity.GetIndexes())
                {
                    var indexName = index.GetDatabaseName();
                    if (indexName.StartsWith("IX") == false)
                        continue;

                    var splitted = indexName.Split('_').ToList();
                    if (splitted.Count < 3)
                        continue;

                    splitted.RemoveAt(1);
                    splitted = splitted.Where(e => e.IsNullOrEmpty() == false).ToList();

                    index.SetDatabaseName($"{string.Join("_", splitted)}");
                }
            }
        }

        /// <summary>
        /// Change default generated index names of foreign keys to match the old database pattern 
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void RenameDuplicatedIndexNames(ModelBuilder modelBuilder)
        {
            var indexes = new Dictionary<string, List<KeyValuePair<IMutableEntityType, IMutableIndex>>>();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var index in entity.GetIndexes())
                {
                    var defaultIndexName = index.GetDefaultDatabaseName();
                    var indexName = index.GetDatabaseName();

                    // To check only on the index defined by the user not automated
                    if (defaultIndexName == indexName)
                        continue;

                    if (indexes.ContainsKey(indexName))
                        indexes[indexName].Add(new(entity, index));
                    else
                        indexes.Add(indexName, new List<KeyValuePair<IMutableEntityType, IMutableIndex>>() { new(entity, index) });
                }
            }

            var duplicatedIndexes = indexes.Where(e => e.Value.Count > 1).ToList();
            foreach (var duplicatedIndex in duplicatedIndexes)
            {
                foreach (var index in duplicatedIndex.Value)
                {
                    index.Value.SetDatabaseName($"{index.Value.GetDatabaseName()}_{index.Key.GetTableName()}");
                }
            }
        }

        #endregion

        #region SEED

        private static void Seed(ModelBuilder builder)
        {
            #region AddPaymentMethods

            builder.Entity<PaymentMethod>().HasData(new PaymentMethod[]
            {
                new() { Id = (int)PaymentMethodType.Cash, Name = "Cash", DisplayOrder = 0, IsEnabled = true, IsClient = true, IsManager = true },
                new() { Id = (int)PaymentMethodType.Points, Name = "Points", DisplayOrder = 2, IsEnabled = true, IsClient = true, IsManager = true },
                new() { Id = (int)PaymentMethodType.Deposit, Name = "Deposit", DisplayOrder = 3, IsEnabled = true, IsClient = true, IsManager = true },
                new() { Id = (int)PaymentMethodType.CreditCard, Name = "Credit Card", DisplayOrder = 4, IsEnabled = true, IsClient = true, IsManager = true },
            });

            #endregion

            #region AddMonetaryUnits

            if (RegionInfo.CurrentRegion.ISOCurrencySymbol == "EUR")
            {
                builder.Entity<MonetaryUnit>().HasData(new MonetaryUnit[]
                {
                    new() { Id = 1, DisplayOrder = 0, Name = "1 Cent", Value = 0.01M },
                    new() { Id = 2, DisplayOrder = 1, Name = "5 Cent", Value = 0.05M },
                    new() { Id = 3, DisplayOrder = 2, Name = "10 Cent", Value = 0.10M },
                    new() { Id = 4, DisplayOrder = 3, Name = "20 Cent", Value = 0.20M },
                    new() { Id = 5, DisplayOrder = 4, Name = "50 Cent", Value = 0.50M },
                    new() { Id = 6, DisplayOrder = 5, Name = "1 Euro", Value = 1.00M },
                    new() { Id = 7, DisplayOrder = 6, Name = "2 Euro", Value = 2.00M },
                    new() { Id = 8, DisplayOrder = 7, Name = "5 Euro", Value = 5.00M },
                    new() { Id = 9, DisplayOrder = 8, Name = "10 Euro", Value = 10.00M },
                    new() { Id = 10, DisplayOrder = 9, Name = "20 Euro", Value = 20.00M },
                    new() { Id = 11, DisplayOrder = 10, Name = "50 Euro", Value = 50.00M },
                    new() { Id = 12, DisplayOrder = 11, Name = "100 Euro", Value = 100.00M },
                    new() { Id = 13, DisplayOrder = 12, Name = "200 Euro", Value = 200.00M },
                    new() { Id = 14, DisplayOrder = 13, Name = "500 Euro", Value = 500.00M }
                });
            }
            else
            {
                builder.Entity<MonetaryUnit>().HasData(new MonetaryUnit[]
                {
                    new() { Id = 1, DisplayOrder = 0, Name = "1 Cent", Value = 0.01M },
                    new() { Id = 2, DisplayOrder = 1, Name = "5 Cent", Value = 0.05M },
                    new() { Id = 3, DisplayOrder = 2, Name = "10 Cent", Value = 0.10M },
                    new() { Id = 4, DisplayOrder = 3, Name = "25 Cent", Value = 0.25M },
                    new() { Id = 5, DisplayOrder = 4, Name = "1 Dollar", Value = 1.00M },
                    new() { Id = 6, DisplayOrder = 5, Name = "2 Dollar", Value = 2.00M },
                    new() { Id = 7, DisplayOrder = 6, Name = "5 Dollar", Value = 5.00M },
                    new() { Id = 8, DisplayOrder = 7, Name = "10 Dollar", Value = 10.00M },
                    new() { Id = 9, DisplayOrder = 8, Name = "20 Dollar", Value = 20.00M },
                    new() { Id = 10, DisplayOrder = 9, Name = "50 Dollar", Value = 50.00M },
                    new() { Id = 11, DisplayOrder = 10, Name = "100 Dollar", Value = 100.00M }
                });
            }

            #endregion

            #region AddDefaultOperator

            byte[] salt = GetNewSalt();
            byte[] password = GetHashedPassword("admin", salt);

            var admin = new UserOperator
            {
                Id = 1,
                Username = "Admin",
                CreatedTime = DateTimeOffset.UtcNow.DateTime,
                Guid = new("691ea8b4-d794-4096-84ae-bbdb7bcc0b02")
            };

            var adminCredential = new UserCredential() { Id = 1, Salt = salt, Password = password };

            var adminPermissions = IntegrationLib.ClaimTypeBase.GetClaimTypes()
                .Select((claim, id) => new UserPermission() { Id = id + 1, UserId = admin.Id, Type = claim.Resource, Value = claim.Operation });

            builder.Entity<UserCredential>().HasData(adminCredential);
            builder.Entity<UserPermission>().HasData(adminPermissions);
            builder.Entity<UserOperator>().HasData(admin);

            #endregion

            #region AddTaxes

            var taxes = new Tax[]
            {
                new() {Id = 1, Name = "24%", Value = 23, UseOrder = 0 },
                new() {Id = 2, Name = "16%", Value = 16, UseOrder = 1 },
                new() {Id = 3, Name = "None", Value = 0, UseOrder = 2 }
            };

            builder.Entity<Tax>().HasData(taxes);

            #endregion

            #region AddProducts

            var tax = taxes[0];

            var productGroupTime = new ProductGroup() { Id = 1, Name = "Time Offers", DisplayOrder = 0, Guid = new("e798a7fb-448b-4825-8b32-c5ea6db70271") };
            var productGroupFood = new ProductGroup() { Id = 2, Name = "Food", DisplayOrder = 1, Guid = new("e798a7fb-448b-4825-8b32-c5ea6db70272") };
            var productGroupDrinks = new ProductGroup() { Id = 3, Name = "Drinks", DisplayOrder = 2, Guid = new("e798a7fb-448b-4825-8b32-c5ea6db70273") };
            var productGroupSweets = new ProductGroup() { Id = 4, Name = "Sweets", DisplayOrder = 3, Guid = new("e798a7fb-448b-4825-8b32-c5ea6db70274") };

            var productGroups = new ProductGroup[] { productGroupTime, productGroupFood, productGroupDrinks, productGroupSweets };

            var productMars = new Product()
            {
                Id = 1,
                ProductGroupId = productGroupSweets.Id,
                Name = "Mars Bar",
                Cost = 0.90m,
                Price = 1.10m,
                Points = 10,
                StockOptions = StockOptionType.EnableStock,
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba1")
            };
            var productSnickers = new Product()
            {
                Id = 2,
                ProductGroupId = productGroupSweets.Id,
                Name = "Snickers Bar",
                Points = 15,
                StockOptions = StockOptionType.EnableStock,
                Cost = 1.20m,
                Price = 2.0m,
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba2")
            };
            var productPizza = new Product()
            {
                Id = 3,
                ProductGroupId = productGroupFood.Id,
                Name = "Pizza (Small)",
                Cost = 2.20m,
                Price = 6.0m,
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba3")
            };
            var productCocaCola = new Product()
            {
                Id = 4,
                ProductGroupId = productGroupDrinks.Id,
                Name = "Coca Cola (Can)",
                Points = 20,
                StockOptions = StockOptionType.EnableStock,
                Cost = 1.20m,
                Price = 2.0m,
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba4")
            };

            var products = new Product[] { productMars, productSnickers, productCocaCola, productPizza };
            
            var productBundlePizzaAndCola = new ProductBundle()
            {
                Id = 5,
                ProductGroupId = productGroupFood.Id,
                Name = "Pizza and Cola",
                StockOptions = StockOptionType.EnableStock,
                Points = 200,
                Price = 3.40m, //pizza plus cola
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba5")
            };

            var bundleProducts = new BundleProduct[]
            {
                new(){Id = 1, ProductId = productCocaCola.Id, ProductBundleId = productBundlePizzaAndCola.Id, Price = 1, Quantity = 1 },
                new(){Id = 2, ProductId = productPizza.Id, ProductBundleId = productBundlePizzaAndCola.Id, Price = 2, Quantity = 1 }
            };

            var productPeriod = new ProductPeriod()
            {
                Id = 1  ,
                Options = PeriodOptionType.None
            };
            var productPeriodDays = new ProductPeriodDay[]
            {
                new(){Id = 1, ProductPeriodId = productPeriod.Id, Day = DayOfWeek.Saturday },
                new(){Id = 2, ProductPeriodId = productPeriod.Id, Day = DayOfWeek.Sunday },
            };

            var productTimeSixHours = new ProductTime()
            {
                Id = 6,
                ProductGroupId = productGroupTime.Id,
                Name = "Six Hours (6)",
                Minutes = 360,
                Price = 12,
                WeekEndMaxMinutes = null,
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba6")
            };
            var productTimeSixHoursWeekends = new ProductTime()
            {
                Id = 7,
                ProductGroupId = productGroupTime.Id,
                Name = "Six Hours (6 Weekends)",
                Minutes = 360,
                Price = 16,
                WeekEndMaxMinutes = null,
                Guid = new("39a65689-65ae-49b4-80b9-ea0afb9daba7")
            };

            var productTimes = new ProductTime[] { productTimeSixHours, productTimeSixHoursWeekends };

            var productTaxes = new ProductTax[]
            {
                new() { Id = 1, ProductId = productMars.Id, TaxId = tax.Id },
                new() { Id = 2, ProductId = productSnickers.Id, TaxId = tax.Id },
                new() { Id = 3, ProductId = productPizza.Id, TaxId = tax.Id },
                new() { Id = 4, ProductId = productCocaCola.Id, TaxId = tax.Id },
                new() { Id = 5, ProductId = productBundlePizzaAndCola.Id, TaxId = tax.Id },
                new() { Id = 6, ProductId = productTimeSixHours.Id, TaxId = tax.Id },
                new() { Id = 7, ProductId = productTimeSixHoursWeekends.Id, TaxId = tax.Id },
            };

            builder.Entity<ProductGroup>().HasData(productGroups);
            builder.Entity<Product>().HasData(products);
            builder.Entity<ProductBundle>().HasData(productBundlePizzaAndCola);
            builder.Entity<BundleProduct>().HasData(bundleProducts);
            builder.Entity<ProductPeriod>().HasData(productPeriod);
            builder.Entity<ProductPeriodDay>().HasData(productPeriodDays);
            builder.Entity<ProductTime>().HasData(productTimes);
            builder.Entity<ProductTax>().HasData(productTaxes);

            #endregion

            #region AddLayoutGroups

            builder.Entity<HostLayoutGroup>().HasData(new HostLayoutGroup()
            {
                Id = 1,
                Name = "Default",
                DisplayOrder = 0
            });

            #endregion

            #region AddBillProfiles

            var billProfileMemberPrices = new BillProfile() { Id = 1, Name = "Member Prices" };
            var billProfileGuestsPrices = new BillProfile() { Id = 2, Name = "Guests Prices" };

            var billProfiles = new BillProfile[] { billProfileMemberPrices, billProfileGuestsPrices };
            var billRates = new BillRate[]
            {
                new()
                {
                    Id = 1,
                    BillProfileId = billProfileMemberPrices.Id,
                    IsDefault = true,
                    MinimumFee = 2,
                    ChargeAfter = 1,
                    ChargeEvery = 5,
                    Rate = 2,
                    StartFee = 1
                },
                new()
                {
                    Id = 2,
                    BillProfileId = billProfileGuestsPrices.Id,
                    IsDefault = true,
                    MinimumFee = 2,
                    ChargeAfter = 1,
                    ChargeEvery = 5,
                    Rate = 2,
                    StartFee = 1
                }
            };

            builder.Entity<BillRate>().HasData(billRates);
            builder.Entity<BillProfile>().HasData(billProfiles);

            #endregion

            #region AddUserGroups

            var userGroupMember = new UserGroup() { Id = 1, Name = "Members", BillProfileId = billProfileMemberPrices.Id, IsDefault = true };
            var userGroupGuest = new UserGroup() { Id = 2, Name = "Guests", BillProfileId = billProfileGuestsPrices.Id, Options = UserGroupOptionType.GuestUse };

            var userGroups = new UserGroup[] { userGroupMember, userGroupGuest };

            builder.Entity<UserGroup>().HasData(userGroups);

            #endregion

            #region AddUsers

            var userMember = new UserMember() { Id = 2, Username = "User", UserGroupId = userGroupMember.Id, Guid = new("38753737-24f1-40d7-8ac4-ba61660d666a") };

            builder.Entity<UserMember>().HasData(userMember);

            #endregion

            #region AddHostGroups

            var hostGroupComputers = new HostGroup() { Id = 1, Name = "Computers", DefaultGuestGroupId = userGroupGuest.Id };
            var hostGroupEndpoints = new HostGroup() { Id = 2, Name = "Endpoints", DefaultGuestGroupId = userGroupGuest.Id };

            var hostGroups = new HostGroup[] { hostGroupComputers, hostGroupEndpoints };

            builder.Entity<HostGroup>().HasData(hostGroups);

            #endregion

            #region AddHosts

            builder.Entity<HostEndpoint>().HasData(new HostEndpoint[]
            {
                new() { Id = 1, Name = "XBOX-ONE-1", Number = 1, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = new("cd41aa25-ac1f-4da9-8c8e-075032803871") },
                new() { Id = 2, Name = "XBOX-ONE-2", Number = 2, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = new("cd41aa25-ac1f-4da9-8c8e-075032803872") },
                new() { Id = 3, Name = "PS4-1", Number = 3, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = new("cd41aa25-ac1f-4da9-8c8e-075032803873") },
                new() { Id = 4, Name = "WII-1", Number = 4, MaximumUsers = 4, HostGroupId = hostGroupEndpoints.Id, Guid = new("cd41aa25-ac1f-4da9-8c8e-075032803874") }
            });

            #endregion

            #region AddPresetTime

            builder.Entity<PresetTimeSale>().HasData(new PresetTimeSale[]
           {
                new() {Id = 1, Value = 1 },
                new() {Id = 2, Value = 5 },
                new() {Id = 3, Value = 15 },
                new() {Id = 4, Value = 30 },
                new() {Id = 5, Value = 60 }
           });
            builder.Entity<PresetTimeSaleMoney>().HasData(new PresetTimeSaleMoney[]
            {
                new() {Id = 1, Value = 1 },
                new() {Id = 2, Value = 2 },
                new() {Id = 3, Value = 5 },
                new() {Id = 4, Value = 10 },
                new() {Id = 5, Value = 20 }
            });

            #endregion
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
                if (notifyTypes == null)
                    notifyTypes = new HashSet<Type>();
                return notifyTypes;
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
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            if (ex.GetBaseException() is SqlException sqlException)
                return Enum.IsDefined(typeof(MSSQLServerRetryableErrors), sqlException.Number);

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

    #endregion
}
