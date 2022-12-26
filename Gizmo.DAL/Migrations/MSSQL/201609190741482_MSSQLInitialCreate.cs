namespace GizmoDALV2.Migrations.MSSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MSSQLInitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppExeCdImage",
                c => new
                {
                    AppExeCdImageId = c.Int(nullable: false, identity: true),
                    AppExeId = c.Int(nullable: false),
                    Path = c.String(nullable: false, maxLength: 255),
                    MountOptions = c.String(maxLength: 255),
                    DeviceId = c.String(maxLength: 3),
                    CheckExitCode = c.Boolean(nullable: false),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppExeCdImageId)
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppExeId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExe",
                c => new
                {
                    AppExeId = c.Int(nullable: false, identity: true),
                    AppId = c.Int(nullable: false),
                    Caption = c.String(maxLength: 255),
                    Description = c.String(maxLength: 255),
                    ExecutablePath = c.String(nullable: false, maxLength: 255),
                    Arguments = c.String(maxLength: 255),
                    WorkingDirectory = c.String(maxLength: 255),
                    Modes = c.Int(nullable: false),
                    RunMode = c.Int(nullable: false),
                    DefaultDeploymentId = c.Int(),
                    ReservationType = c.Int(nullable: false),
                    DisplayOrder = c.Int(nullable: false),
                    Options = c.Int(nullable: false),
                    Guid = c.Guid(nullable: false),
                    Accessible = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppExeId)
                .ForeignKey("dbo.App", t => t.AppId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Deployment", t => t.DefaultDeploymentId)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppId)
                .Index(t => t.DefaultDeploymentId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.App",
                c => new
                {
                    AppId = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 255),
                    PublisherId = c.Int(),
                    DeveloperId = c.Int(),
                    AppCategoryId = c.Int(nullable: false),
                    Description = c.String(),
                    ReleaseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Version = c.String(maxLength: 45),
                    Options = c.Int(nullable: false),
                    AgeRating = c.Int(nullable: false),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppId)
                .ForeignKey("dbo.AppCategory", t => t.AppCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.AppEnterprise", t => t.DeveloperId)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.AppEnterprise", t => t.PublisherId)
                .Index(t => t.PublisherId)
                .Index(t => t.DeveloperId)
                .Index(t => t.AppCategoryId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppCategory",
                c => new
                {
                    AppCategoryId = c.Int(nullable: false, identity: true),
                    ParentId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 45),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppCategoryId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.AppCategory", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.User",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    FirstName = c.String(maxLength: 45),
                    LastName = c.String(maxLength: 45),
                    BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Address = c.String(maxLength: 255),
                    City = c.String(maxLength: 45),
                    Country = c.String(maxLength: 45),
                    PostCode = c.String(maxLength: 20),
                    Phone = c.String(maxLength: 20),
                    MobilePhone = c.String(maxLength: 20),
                    Sex = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    IsDisabled = c.Boolean(nullable: false),
                    Guid = c.Guid(nullable: false),
                    SmartCardUID = c.String(maxLength: 255),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.SmartCardUID, unique: true, name: "UQ_SmartCardUID")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserAttribute",
                c => new
                {
                    UserAttributeId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    AttributeId = c.Int(nullable: false),
                    Value = c.String(nullable: false, maxLength: 255),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserAttributeId)
                .ForeignKey("dbo.Attribute", t => t.AttributeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.AttributeId }, unique: true, name: "UQ_UserAttribute")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Attribute",
                c => new
                {
                    AttributeId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    FriendlyName = c.String(maxLength: 255),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AttributeId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserSessionChange",
                c => new
                {
                    UserSessionChangeId = c.Int(nullable: false, identity: true),
                    UserSessionId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    HostId = c.Int(nullable: false),
                    State = c.Int(nullable: false),
                    Span = c.Double(nullable: false),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserSessionChangeId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Host", t => t.HostId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .ForeignKey("dbo.UserSession", t => t.UserSessionId)
                .Index(t => t.UserSessionId)
                .Index(t => t.UserId)
                .Index(t => t.HostId)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Host",
                c => new
                {
                    HostId = c.Int(nullable: false, identity: true),
                    Number = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 45),
                    HostGroupId = c.Int(),
                    State = c.Int(nullable: false),
                    IconId = c.Int(),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.HostId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostGroup", t => t.HostGroupId)
                .ForeignKey("dbo.Icon", t => t.IconId)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.HostGroupId)
                .Index(t => t.IconId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostGroup",
                c => new
                {
                    HostGroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    AppGroupId = c.Int(),
                    SecurityProfileId = c.Int(),
                    SkinName = c.String(maxLength: 255),
                    Options = c.Int(nullable: false),
                    DefaultGuestGroupId = c.Int(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.HostGroupId)
                .ForeignKey("dbo.AppGroup", t => t.AppGroupId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserGroup", t => t.DefaultGuestGroupId)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.SecurityProfile", t => t.SecurityProfileId)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.AppGroupId)
                .Index(t => t.SecurityProfileId)
                .Index(t => t.DefaultGuestGroupId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppGroup",
                c => new
                {
                    AppGroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppGroupId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppGroupApp",
                c => new
                {
                    AppGroupId = c.Int(nullable: false),
                    AppId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.AppGroupId, t.AppId })
                .ForeignKey("dbo.App", t => t.AppId)
                .ForeignKey("dbo.AppGroup", t => t.AppGroupId, cascadeDelete: true)
                .Index(t => t.AppGroupId)
                .Index(t => t.AppId);

            CreateTable(
                "dbo.ProductBase",
                c => new
                {
                    ProductId = c.Int(nullable: false, identity: true),
                    ProductGroupId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 45),
                    Description = c.String(),
                    Price = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Cost = c.Decimal(precision: 19, scale: 4),
                    Points = c.Int(),
                    PointsPrice = c.Int(),
                    Barcode = c.String(maxLength: 255),
                    OrderOptions = c.Int(nullable: false),
                    PurchaseOptions = c.Int(nullable: false),
                    StockOptions = c.Int(nullable: false),
                    StockAlert = c.Decimal(nullable: false, precision: 19, scale: 4),
                    StockProductId = c.Int(),
                    StockProductAmount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsDeleted = c.Boolean(nullable: false),
                    DisplayOrder = c.Int(nullable: false),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductGroup", t => t.ProductGroupId, cascadeDelete: true)
                .ForeignKey("dbo.ProductBase", t => t.StockProductId)
                .Index(t => t.ProductGroupId)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Barcode, unique: true, name: "UQ_Barcode")
                .Index(t => t.StockProductId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductTimeHostDisallowed",
                c => new
                {
                    ProductTimeHostDisallowedId = c.Int(nullable: false, identity: true),
                    ProductTimeId = c.Int(nullable: false),
                    HostGroupId = c.Int(nullable: false),
                    IsDisallowed = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductTimeHostDisallowedId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostGroup", t => t.HostGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductTime", t => t.ProductTimeId)
                .Index(t => new { t.ProductTimeId, t.HostGroupId }, unique: true, name: "UQ_ProductTimeHostGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductUserDisallowed",
                c => new
                {
                    ProductUserDisallowedId = c.Int(nullable: false, identity: true),
                    ProductId = c.Int(nullable: false),
                    UserGroupId = c.Int(nullable: false),
                    IsDisallowed = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductUserDisallowedId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.UserGroupId }, unique: true, name: "UQ_ProductUserGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductImage",
                c => new
                {
                    ProductImageId = c.Int(nullable: false, identity: true),
                    Image = c.Binary(nullable: false),
                    ProductId = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductPeriod",
                c => new
                {
                    ProductId = c.Int(nullable: false),
                    StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Options = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.ProductPeriodDay",
                c => new
                {
                    ProductPeriodDayId = c.Int(nullable: false, identity: true),
                    ProductPeriodId = c.Int(nullable: false),
                    Day = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductPeriodDayId)
                .ForeignKey("dbo.ProductPeriod", t => t.ProductPeriodId, cascadeDelete: true)
                .Index(t => new { t.ProductPeriodId, t.Day }, unique: true, name: "UQ_ProductPeriodDay");

            CreateTable(
                "dbo.ProductPeriodDayTime",
                c => new
                {
                    PeriodDayId = c.Int(nullable: false),
                    StartSecond = c.Int(nullable: false),
                    EndSecond = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.PeriodDayId, t.StartSecond, t.EndSecond })
                .ForeignKey("dbo.ProductPeriodDay", t => t.PeriodDayId, cascadeDelete: true)
                .Index(t => t.PeriodDayId);

            CreateTable(
                "dbo.ProductGroup",
                c => new
                {
                    ProductGroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    ParentId = c.Int(),
                    DisplayOrder = c.Int(nullable: false),
                    SortOption = c.Int(nullable: false),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductGroupId)
                .ForeignKey("dbo.ProductGroup", t => t.ParentId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ParentId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.StockTransaction",
                c => new
                {
                    StockTransactionId = c.Int(nullable: false, identity: true),
                    ProductId = c.Int(nullable: false),
                    SourceProductId = c.Int(),
                    Type = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    OnHand = c.Decimal(nullable: false, precision: 19, scale: 4),
                    SourceProductAmount = c.Decimal(precision: 19, scale: 4),
                    SourceProductOnHand = c.Decimal(precision: 19, scale: 4),
                    IsVoided = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.StockTransactionId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductBase", t => t.SourceProductId)
                .Index(t => t.ProductId)
                .Index(t => t.SourceProductId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductTax",
                c => new
                {
                    ProductTaxId = c.Int(nullable: false, identity: true),
                    ProductId = c.Int(nullable: false),
                    TaxId = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductTaxId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Tax", t => t.TaxId)
                .Index(t => new { t.ProductId, t.TaxId }, unique: true, name: "UQ_TaxProduct")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Tax",
                c => new
                {
                    TaxId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Value = c.Decimal(nullable: false, precision: 19, scale: 4),
                    UseOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.TaxId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductUserPrice",
                c => new
                {
                    ProductUserPriceId = c.Int(nullable: false, identity: true),
                    ProductId = c.Int(nullable: false),
                    UserGroupId = c.Int(nullable: false),
                    Price = c.Decimal(precision: 19, scale: 4),
                    PointsPrice = c.Int(),
                    PurchaseOptions = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductUserPriceId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.UserGroupId }, unique: true, name: "UQ_ProductUserGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserGroup",
                c => new
                {
                    UserGroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Description = c.String(maxLength: 255),
                    AppGroupId = c.Int(),
                    SecurityProfileId = c.Int(),
                    BillProfileId = c.Int(),
                    RequiredUserInfo = c.Int(nullable: false),
                    Overrides = c.Int(nullable: false),
                    Options = c.Int(nullable: false),
                    CreditLimitOptions = c.Int(nullable: false),
                    IsDefault = c.Boolean(nullable: false),
                    CreditLimit = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsNegativeBalanceAllowed = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserGroupId)
                .ForeignKey("dbo.AppGroup", t => t.AppGroupId)
                .ForeignKey("dbo.BillProfile", t => t.BillProfileId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.SecurityProfile", t => t.SecurityProfileId)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.AppGroupId)
                .Index(t => t.SecurityProfileId)
                .Index(t => t.BillProfileId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.BillProfile",
                c => new
                {
                    BillProfileId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.BillProfileId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.BillRate",
                c => new
                {
                    BillRateId = c.Int(nullable: false, identity: true),
                    BillProfileId = c.Int(nullable: false),
                    StartFee = c.Decimal(nullable: false, precision: 19, scale: 4),
                    MinimumFee = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Rate = c.Decimal(nullable: false, precision: 19, scale: 4),
                    ChargeEvery = c.Int(nullable: false),
                    ChargeAfter = c.Int(nullable: false),
                    Options = c.Int(nullable: false),
                    IsDefault = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.BillRateId)
                .ForeignKey("dbo.BillProfile", t => t.BillProfileId, cascadeDelete: true)
                .Index(t => t.BillProfileId);

            CreateTable(
                "dbo.BillRateStep",
                c => new
                {
                    BillRateStepId = c.Int(nullable: false, identity: true),
                    BillRateId = c.Int(nullable: false),
                    Minute = c.Int(nullable: false),
                    Action = c.Int(nullable: false),
                    Charge = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Rate = c.Decimal(nullable: false, precision: 19, scale: 4),
                    TargetMinute = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.BillRateStepId)
                .ForeignKey("dbo.BillRate", t => t.BillRateId, cascadeDelete: true)
                .Index(t => new { t.BillRateId, t.Minute }, unique: true, name: "UQ_BillRateMinute");

            CreateTable(
                "dbo.BillRatePeriodDay",
                c => new
                {
                    BillRatePeriodDayId = c.Int(nullable: false, identity: true),
                    BillRateId = c.Int(nullable: false),
                    Day = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.BillRatePeriodDayId)
                .ForeignKey("dbo.BillRate", t => t.BillRateId, cascadeDelete: true)
                .Index(t => new { t.BillRateId, t.Day }, unique: true, name: "UQ_BillRatePeriodDay");

            CreateTable(
                "dbo.BillRatePeriodDayTime",
                c => new
                {
                    PeriodDayId = c.Int(nullable: false),
                    StartSecond = c.Int(nullable: false),
                    EndSecond = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.PeriodDayId, t.StartSecond, t.EndSecond })
                .ForeignKey("dbo.BillRatePeriodDay", t => t.PeriodDayId, cascadeDelete: true)
                .Index(t => t.PeriodDayId);

            CreateTable(
                "dbo.Usage",
                c => new
                {
                    UsageId = c.Int(nullable: false, identity: true),
                    UsageSessionId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Seconds = c.Double(nullable: false),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UsageId)
                .ForeignKey("dbo.UsageSession", t => t.UsageSessionId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UsageSessionId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UsageSession",
                c => new
                {
                    UsageSessionId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    CurrentUsageId = c.Int(),
                    CurrentSecond = c.Double(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    NegativeSeconds = c.Double(nullable: false),
                    StartFee = c.Decimal(nullable: false, precision: 19, scale: 4),
                    MinimumFee = c.Decimal(nullable: false, precision: 19, scale: 4),
                    RatesTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UsageSessionId)
                .ForeignKey("dbo.Usage", t => t.CurrentUsageId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CurrentUsageId);

            CreateTable(
                "dbo.AppRating",
                c => new
                {
                    AppId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Value = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => new { t.AppId, t.UserId })
                .ForeignKey("dbo.App", t => t.AppId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.AppId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AppStat",
                c => new
                {
                    AppStatId = c.Int(nullable: false, identity: true),
                    AppId = c.Int(nullable: false),
                    AppExeId = c.Int(nullable: false),
                    HostId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Span = c.Double(nullable: false),
                    StartTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppStatId)
                .ForeignKey("dbo.App", t => t.AppId, cascadeDelete: true)
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.HostComputer", t => t.HostId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.AppId)
                .Index(t => t.AppExeId)
                .Index(t => t.HostId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Icon",
                c => new
                {
                    IconId = c.Int(nullable: false, identity: true),
                    Image = c.Binary(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.IconId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostLayoutGroupLayout",
                c => new
                {
                    HostLayoutGroupLayoutId = c.Int(nullable: false, identity: true),
                    HostLayoutGroupId = c.Int(nullable: false),
                    HostId = c.Int(nullable: false),
                    X = c.Int(nullable: false),
                    Y = c.Int(nullable: false),
                    Height = c.Int(nullable: false),
                    Width = c.Int(nullable: false),
                    IsHidden = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.HostLayoutGroupLayoutId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Host", t => t.HostId, cascadeDelete: true)
                .ForeignKey("dbo.HostLayoutGroup", t => t.HostLayoutGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => new { t.HostLayoutGroupId, t.HostId }, unique: true, name: "UQ_HostLayoutGroupHost")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostLayoutGroup",
                c => new
                {
                    HostLayoutGroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    DisplayOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.HostLayoutGroupId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostLayoutGroupImage",
                c => new
                {
                    HostLayoutGroupId = c.Int(nullable: false),
                    Image = c.Binary(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.HostLayoutGroupId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostLayoutGroup", t => t.HostLayoutGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.HostLayoutGroupId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserSession",
                c => new
                {
                    UserSessionId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    HostId = c.Int(nullable: false),
                    State = c.Int(nullable: false),
                    Span = c.Double(nullable: false),
                    BilledSpan = c.Double(nullable: false),
                    PendTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    PendSpan = c.Double(nullable: false),
                    EndTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserSessionId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Host", t => t.HostId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HostId)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.InvoiceLine",
                c => new
                {
                    InvoiceLineId = c.Int(nullable: false, identity: true),
                    InvoiceId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    ProductName = c.String(nullable: false, maxLength: 45),
                    Quantity = c.Decimal(nullable: false, precision: 19, scale: 4),
                    UnitPrice = c.Decimal(nullable: false, precision: 19, scale: 4),
                    UnitListPrice = c.Decimal(nullable: false, precision: 19, scale: 4),
                    TaxRate = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PreTaxTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Total = c.Decimal(nullable: false, precision: 19, scale: 4),
                    TaxTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PointsTransactionId = c.Int(),
                    IsDeleted = c.Boolean(nullable: false),
                    IsVoided = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.InvoiceLineId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.PointTransaction", t => t.PointsTransactionId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.InvoiceId)
                .Index(t => t.UserId)
                .Index(t => t.PointsTransactionId, unique: true, name: "UQ_PointsTransaction")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Invoice",
                c => new
                {
                    InvoiceId = c.Int(nullable: false, identity: true),
                    ProductOrderId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                    SubTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PointsTotal = c.Int(nullable: false),
                    TaxTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Total = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Outstanding = c.Decimal(nullable: false, precision: 19, scale: 4),
                    OutstandngPoints = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductOrder", t => t.ProductOrderId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.ProductOrderId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.PointTransaction",
                c => new
                {
                    PointTransactionId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    Amount = c.Int(nullable: false),
                    Balance = c.Int(nullable: false),
                    IsVoided = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.PointTransactionId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductOL",
                c => new
                {
                    ProductOLId = c.Int(nullable: false, identity: true),
                    ProductOrderId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    ProductName = c.String(nullable: false, maxLength: 45),
                    Quantity = c.Decimal(nullable: false, precision: 19, scale: 4),
                    UnitPrice = c.Decimal(nullable: false, precision: 19, scale: 4),
                    UnitListPrice = c.Decimal(nullable: false, precision: 19, scale: 4),
                    UnitPointsPrice = c.Int(),
                    UnitCost = c.Decimal(precision: 19, scale: 4),
                    TaxRate = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PreTaxTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Total = c.Decimal(nullable: false, precision: 19, scale: 4),
                    TaxTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsDeleted = c.Boolean(nullable: false),
                    IsVoided = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductOLId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductOrder", t => t.ProductOrderId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.ProductOrderId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductOrder",
                c => new
                {
                    ProductOrderId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                    SubTotal = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Total = c.Decimal(nullable: false, precision: 19, scale: 4),
                    PointsTotal = c.Int(nullable: false),
                    Tax = c.Decimal(nullable: false, precision: 19, scale: 4),
                    HostId = c.Int(),
                    IsDeleted = c.Boolean(nullable: false),
                    IsVoided = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ProductOrderId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Host", t => t.HostId)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HostId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.BundleProduct",
                c => new
                {
                    BundleProductId = c.Int(nullable: false, identity: true),
                    ProductBundleId = c.Int(nullable: false),
                    ProductId = c.Int(nullable: false),
                    Quantity = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Price = c.Decimal(nullable: false, precision: 19, scale: 4),
                    DisplayOrder = c.Int(nullable: false),
                    Options = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.BundleProductId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.ProductBase", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductBundle", t => t.ProductBundleId)
                .Index(t => t.ProductBundleId)
                .Index(t => t.ProductId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.BundleProductUserPrice",
                c => new
                {
                    BundleProductUserPriceId = c.Int(nullable: false, identity: true),
                    BundleProductId = c.Int(nullable: false),
                    UserGroupId = c.Int(nullable: false),
                    Price = c.Decimal(precision: 19, scale: 4),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.BundleProductUserPriceId)
                .ForeignKey("dbo.BundleProduct", t => t.BundleProductId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId)
                .Index(t => new { t.BundleProductId, t.UserGroupId }, unique: true, name: "UQ_BundleProductUserGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.InvoicePayment",
                c => new
                {
                    InvoicePaymentId = c.Int(nullable: false, identity: true),
                    InvoiceId = c.Int(nullable: false),
                    PaymentId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.InvoicePaymentId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.Payment", t => t.PaymentId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.InvoiceId)
                .Index(t => t.PaymentId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Payment",
                c => new
                {
                    PaymentId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    PaymentMethodId = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    AmountReceived = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsDeleted = c.Boolean(nullable: false),
                    IsRefunded = c.Boolean(nullable: false),
                    IsVoided = c.Boolean(nullable: false),
                    DepositTransactionId = c.Int(),
                    PointTransactionId = c.Int(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.DepositTransaction", t => t.DepositTransactionId)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId, cascadeDelete: true)
                .ForeignKey("dbo.PointTransaction", t => t.PointTransactionId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.DepositTransactionId, unique: true, name: "UQ_DepositTransaction")
                .Index(t => t.PointTransactionId, unique: true, name: "UQ_PointsTransaction")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.DepositTransaction",
                c => new
                {
                    DepositTransactionId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Balance = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsVoided = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.DepositTransactionId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.PaymentMethod",
                c => new
                {
                    PaymentMethodId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 45),
                    Description = c.String(maxLength: 255),
                    Surcharge = c.Decimal(nullable: false, precision: 19, scale: 4),
                    DisplayOrder = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    Options = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.PaymentMethodId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserPermission",
                c => new
                {
                    UserPermissionId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    Type = c.String(nullable: false, maxLength: 255),
                    Value = c.String(nullable: false, maxLength: 255),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserPermissionId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.Type, t.Value }, unique: true, name: "UQ_UserPermission")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserCredential",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    Password = c.Binary(maxLength: 64, fixedLength: true),
                    Salt = c.Binary(maxLength: 100, fixedLength: true),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserCreditLimit",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    CreditLimit = c.Decimal(nullable: false, precision: 19, scale: 4),
                    IsEnabled = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserPicture",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    Picture = c.Binary(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ModifiedById)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.UserGroupHostDisallowed",
                c => new
                {
                    UserGroupHostDisallowedId = c.Int(nullable: false, identity: true),
                    UserGroupId = c.Int(nullable: false),
                    HostGroupId = c.Int(nullable: false),
                    IsDisallowed = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UserGroupHostDisallowedId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.HostGroup", t => t.HostGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.UserGroup", t => t.HostGroupId, cascadeDelete: true)
                .Index(t => new { t.UserGroupId, t.HostGroupId }, unique: true, name: "UQ_UserGroupHostGroup")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.SecurityProfile",
                c => new
                {
                    SecurityProfileId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    DisabledDrives = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.SecurityProfileId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.SecurityProfilePolicy",
                c => new
                {
                    SecurityProfilePolicyId = c.Int(nullable: false, identity: true),
                    SecurityProfileId = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.SecurityProfilePolicyId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.SecurityProfile", t => t.SecurityProfileId, cascadeDelete: true)
                .Index(t => new { t.SecurityProfileId, t.Type }, unique: true, name: "UQ_SecurityProfilePolicyType")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.SecurityProfileRestriction",
                c => new
                {
                    SecurityProfileRestrictionId = c.Int(nullable: false, identity: true),
                    SecurityProfileId = c.Int(nullable: false),
                    Parameter = c.String(nullable: false, maxLength: 255),
                    Type = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.SecurityProfileRestrictionId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.SecurityProfile", t => t.SecurityProfileId, cascadeDelete: true)
                .Index(t => t.SecurityProfileId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ProductTimePeriod",
                c => new
                {
                    ProductId = c.Int(nullable: false),
                    StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Options = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductTime", t => t.ProductId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.ProductTimePeriodDay",
                c => new
                {
                    ProductTimePeriodDayId = c.Int(nullable: false, identity: true),
                    ProductTimePeriodId = c.Int(nullable: false),
                    Day = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductTimePeriodDayId)
                .ForeignKey("dbo.ProductTimePeriod", t => t.ProductTimePeriodId, cascadeDelete: true)
                .Index(t => new { t.ProductTimePeriodId, t.Day }, unique: true, name: "UQ_ProductTimePeriodDay");

            CreateTable(
                "dbo.ProductTimePeriodDayTime",
                c => new
                {
                    PeriodDayId = c.Int(nullable: false),
                    StartSecond = c.Int(nullable: false),
                    EndSecond = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.PeriodDayId, t.StartSecond, t.EndSecond })
                .ForeignKey("dbo.ProductTimePeriodDay", t => t.PeriodDayId, cascadeDelete: true)
                .Index(t => t.PeriodDayId);

            CreateTable(
                "dbo.AppLink",
                c => new
                {
                    AppLinkId = c.Int(nullable: false, identity: true),
                    AppId = c.Int(nullable: false),
                    Caption = c.String(maxLength: 255),
                    Description = c.String(maxLength: 255),
                    Url = c.String(nullable: false, maxLength: 255),
                    DisplayOrder = c.Int(nullable: false),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppLinkId)
                .ForeignKey("dbo.App", t => t.AppId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppEnterprise",
                c => new
                {
                    AppEnterpriseId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppEnterpriseId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppImage",
                c => new
                {
                    AppId = c.Int(nullable: false),
                    Image = c.Binary(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppId)
                .ForeignKey("dbo.App", t => t.AppId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Deployment",
                c => new
                {
                    DeploymentId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Source = c.String(nullable: false, maxLength: 255),
                    Destination = c.String(nullable: false, maxLength: 255),
                    ExcludeDirectories = c.String(),
                    ExcludeFiles = c.String(),
                    IncludeDirectories = c.String(),
                    IncludeFiles = c.String(),
                    RegistryString = c.String(),
                    Guid = c.Guid(nullable: false),
                    ComparisonLevel = c.Int(nullable: false),
                    Options = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.DeploymentId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.DeploymentDeployment",
                c => new
                {
                    ParentId = c.Int(nullable: false),
                    ChildId = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => new { t.ParentId, t.ChildId })
                .ForeignKey("dbo.Deployment", t => t.ChildId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.Deployment", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExeDeployment",
                c => new
                {
                    AppExeId = c.Int(nullable: false),
                    DeploymentId = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => new { t.AppExeId, t.DeploymentId })
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.Deployment", t => t.DeploymentId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppExeId)
                .Index(t => t.DeploymentId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExeImage",
                c => new
                {
                    AppExeId = c.Int(nullable: false),
                    Image = c.Binary(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppExeId)
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppExeId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExeLicense",
                c => new
                {
                    AppExeId = c.Int(nullable: false),
                    LicenseId = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => new { t.AppExeId, t.LicenseId })
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.AppExeId)
                .Index(t => t.LicenseId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.License",
                c => new
                {
                    LicenseId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Assembly = c.String(nullable: false, maxLength: 255),
                    Plugin = c.String(nullable: false, maxLength: 255),
                    Settings = c.Binary(),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.LicenseId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.LicenseKey",
                c => new
                {
                    LicenseKeyId = c.Int(nullable: false, identity: true),
                    LicenseId = c.Int(nullable: false),
                    Value = c.Binary(nullable: false),
                    Comment = c.String(maxLength: 255),
                    Guid = c.Guid(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.LicenseKeyId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.License", t => t.LicenseId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.LicenseId)
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExeMaxUser",
                c => new
                {
                    AppExeMaxUserId = c.Int(nullable: false, identity: true),
                    AppExeId = c.Int(nullable: false),
                    Mode = c.Int(nullable: false),
                    MaxUsers = c.Int(),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppExeMaxUserId)
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => new { t.AppExeId, t.Mode }, unique: true, name: "UQ_AppExeAppExeMode")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExePersonalFile",
                c => new
                {
                    AppExeId = c.Int(nullable: false),
                    PersonalFileId = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => new { t.AppExeId, t.PersonalFileId })
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.PersonalFile", t => t.PersonalFileId, cascadeDelete: true)
                .Index(t => t.AppExeId)
                .Index(t => t.PersonalFileId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.PersonalFile",
                c => new
                {
                    PersonalFileId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Caption = c.String(maxLength: 255),
                    Description = c.String(maxLength: 255),
                    Source = c.String(nullable: false, maxLength: 255),
                    Activation = c.Int(nullable: false),
                    Deactivation = c.Int(nullable: false),
                    MaxQuota = c.Int(nullable: false),
                    CompressionLevel = c.Int(nullable: false),
                    ExcludeDirectories = c.String(),
                    ExcludeFiles = c.String(),
                    IncludeDirectories = c.String(),
                    IncludeFiles = c.String(),
                    Guid = c.Guid(nullable: false),
                    Type = c.Int(nullable: false),
                    Options = c.Int(nullable: false),
                    Accessible = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.PersonalFileId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.AppExeTask",
                c => new
                {
                    AppExeTaskId = c.Int(nullable: false, identity: true),
                    Activation = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    AppExeId = c.Int(nullable: false),
                    TaskBaseId = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.AppExeTaskId)
                .ForeignKey("dbo.AppExe", t => t.AppExeId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.TaskBase", t => t.TaskBaseId, cascadeDelete: true)
                .Index(t => t.AppExeId)
                .Index(t => t.TaskBaseId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.TaskBase",
                c => new
                {
                    TaskId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Guid = c.Guid(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.Guid, unique: true, name: "UQ_Guid")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.ClientTask",
                c => new
                {
                    ClientTaskId = c.Int(nullable: false, identity: true),
                    Activation = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    TaskBaseId = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ClientTaskId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.TaskBase", t => t.TaskBaseId, cascadeDelete: true)
                .Index(t => t.TaskBaseId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.DepositPayment",
                c => new
                {
                    DepositPaymentId = c.Int(nullable: false, identity: true),
                    DepositTransactionId = c.Int(nullable: false),
                    PaymentId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.DepositPaymentId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.DepositTransaction", t => t.DepositTransactionId, cascadeDelete: true)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .ForeignKey("dbo.Payment", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.DepositTransactionId)
                .Index(t => t.PaymentId)
                .Index(t => t.UserId)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Feed",
                c => new
                {
                    FeedId = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 255),
                    Url = c.String(nullable: false, maxLength: 255),
                    Maximum = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.FeedId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Log",
                c => new
                {
                    LogId = c.Int(nullable: false, identity: true),
                    Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    HostNumber = c.Int(),
                    Hostname = c.String(maxLength: 45),
                    ModuleType = c.Int(nullable: false),
                    ModuleVersion = c.String(maxLength: 45),
                    Category = c.Int(nullable: false),
                    MessageType = c.Int(nullable: false),
                    Message = c.String(nullable: false),
                })
                .PrimaryKey(t => t.LogId)
                .Index(t => t.Time)
                .Index(t => t.HostNumber)
                .Index(t => t.Category)
                .Index(t => t.MessageType);

            CreateTable(
                "dbo.LogException",
                c => new
                {
                    LogId = c.Int(nullable: false),
                    ExceptionData = c.Binary(nullable: false),
                })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.Log", t => t.LogId, cascadeDelete: true)
                .Index(t => t.LogId);

            CreateTable(
                "dbo.Mapping",
                c => new
                {
                    MappingId = c.Int(nullable: false, identity: true),
                    Label = c.String(maxLength: 45),
                    Source = c.String(nullable: false, maxLength: 255),
                    MountPoint = c.String(nullable: false, maxLength: 255),
                    Type = c.Int(nullable: false),
                    Size = c.Int(nullable: false),
                    Username = c.String(maxLength: 45),
                    Password = c.String(maxLength: 45),
                    Options = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.MappingId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.MountPoint, unique: true, name: "UQ_MountPoint")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.MonetaryUnit",
                c => new
                {
                    MonetaryUnitId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Value = c.Decimal(nullable: false, precision: 19, scale: 4),
                    DisplayOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.MonetaryUnitId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.News",
                c => new
                {
                    NewsId = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 255),
                    Data = c.String(),
                    Url = c.String(maxLength: 255),
                    StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.PluginLibrary",
                c => new
                {
                    PluginLibraryId = c.Int(nullable: false, identity: true),
                    FileName = c.String(nullable: false, maxLength: 255),
                    Scope = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.PluginLibraryId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.FileName, unique: true, name: "UQ_FileName")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Setting",
                c => new
                {
                    SettingId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    GroupName = c.String(maxLength: 45),
                    Value = c.String(maxLength: 255),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.SettingId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.Variable",
                c => new
                {
                    VariableId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Value = c.String(nullable: false),
                    Scope = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                    ModifiedById = c.Int(),
                    ModifiedTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    CreatedById = c.Int(),
                    CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.VariableId)
                .ForeignKey("dbo.UserOperator", t => t.CreatedById)
                .ForeignKey("dbo.UserOperator", t => t.ModifiedById)
                .Index(t => t.Name, unique: true, name: "UQ_Name")
                .Index(t => t.ModifiedById)
                .Index(t => t.CreatedById);

            CreateTable(
                "dbo.HostComputer",
                c => new
                {
                    HostId = c.Int(nullable: false),
                    Hostname = c.String(nullable: false, maxLength: 255),
                    MACAddress = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.HostId)
                .ForeignKey("dbo.Host", t => t.HostId)
                .Index(t => t.HostId)
                .Index(t => t.MACAddress, unique: true, name: "UQ_MACAddress");

            CreateTable(
                "dbo.HostEndpoint",
                c => new
                {
                    HostId = c.Int(nullable: false),
                    MaximumUsers = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.HostId)
                .ForeignKey("dbo.Host", t => t.HostId)
                .Index(t => t.HostId);

            CreateTable(
                "dbo.InvoiceLineSession",
                c => new
                {
                    InvoiceLineId = c.Int(nullable: false),
                    OrderLineId = c.Int(nullable: false),
                    UsageSessionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.InvoiceLineId)
                .ForeignKey("dbo.InvoiceLine", t => t.InvoiceLineId)
                .ForeignKey("dbo.ProductOLSession", t => t.OrderLineId)
                .ForeignKey("dbo.UsageSession", t => t.UsageSessionId, cascadeDelete: true)
                .Index(t => t.InvoiceLineId)
                .Index(t => t.OrderLineId)
                .Index(t => t.UsageSessionId, unique: true, name: "UQ_UsageSession");

            CreateTable(
                "dbo.InvoiceLineExtended",
                c => new
                {
                    InvoiceLineId = c.Int(nullable: false),
                    BundleLineId = c.Int(),
                    StockTransactionId = c.Int(),
                })
                .PrimaryKey(t => t.InvoiceLineId)
                .ForeignKey("dbo.InvoiceLine", t => t.InvoiceLineId)
                .ForeignKey("dbo.InvoiceLineProduct", t => t.BundleLineId)
                .ForeignKey("dbo.StockTransaction", t => t.StockTransactionId)
                .Index(t => t.InvoiceLineId)
                .Index(t => t.BundleLineId)
                .Index(t => t.StockTransactionId, unique: true, name: "UQ_StockTransaction");

            CreateTable(
                "dbo.InvoiceLineTime",
                c => new
                {
                    InvoiceLineId = c.Int(nullable: false),
                    OrderLineId = c.Int(nullable: false),
                    ProductTimeId = c.Int(nullable: false),
                    IsDepleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.InvoiceLineId)
                .ForeignKey("dbo.InvoiceLineExtended", t => t.InvoiceLineId)
                .ForeignKey("dbo.ProductOLTime", t => t.OrderLineId)
                .ForeignKey("dbo.ProductTime", t => t.ProductTimeId)
                .Index(t => t.InvoiceLineId)
                .Index(t => t.OrderLineId, unique: true, name: "UQ_OrderLine")
                .Index(t => t.ProductTimeId);

            CreateTable(
                "dbo.InvoiceLineTimeFixed",
                c => new
                {
                    InvoiceLineId = c.Int(nullable: false),
                    OrderLineId = c.Int(nullable: false),
                    IsDepleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.InvoiceLineId)
                .ForeignKey("dbo.InvoiceLine", t => t.InvoiceLineId)
                .ForeignKey("dbo.ProductOLTimeFixed", t => t.OrderLineId)
                .Index(t => t.InvoiceLineId)
                .Index(t => t.OrderLineId, unique: true, name: "UQ_OrderLine");

            CreateTable(
                "dbo.ProductOLSession",
                c => new
                {
                    ProductOLId = c.Int(nullable: false),
                    UsageSessionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductOLId)
                .ForeignKey("dbo.ProductOL", t => t.ProductOLId)
                .ForeignKey("dbo.UsageSession", t => t.UsageSessionId, cascadeDelete: true)
                .Index(t => t.ProductOLId)
                .Index(t => t.UsageSessionId);

            CreateTable(
                "dbo.ProductOLExtended",
                c => new
                {
                    ProductOLId = c.Int(nullable: false),
                    BundleLineId = c.Int(),
                })
                .PrimaryKey(t => t.ProductOLId)
                .ForeignKey("dbo.ProductOL", t => t.ProductOLId)
                .ForeignKey("dbo.ProductOLProduct", t => t.BundleLineId)
                .Index(t => t.ProductOLId)
                .Index(t => t.BundleLineId);

            CreateTable(
                "dbo.ProductOLProduct",
                c => new
                {
                    ProductOLId = c.Int(nullable: false),
                    ProductId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductOLId)
                .ForeignKey("dbo.ProductOLExtended", t => t.ProductOLId)
                .ForeignKey("dbo.ProductBaseExtended", t => t.ProductId)
                .Index(t => t.ProductOLId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.ProductOLTime",
                c => new
                {
                    ProductOLId = c.Int(nullable: false),
                    ProductTimeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductOLId)
                .ForeignKey("dbo.ProductOLExtended", t => t.ProductOLId)
                .ForeignKey("dbo.ProductTime", t => t.ProductTimeId)
                .Index(t => t.ProductOLId)
                .Index(t => t.ProductTimeId);

            CreateTable(
                "dbo.ProductOLTimeFixed",
                c => new
                {
                    ProductOLId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductOLId)
                .ForeignKey("dbo.ProductOL", t => t.ProductOLId)
                .Index(t => t.ProductOLId);

            CreateTable(
                "dbo.ProductBaseExtended",
                c => new
                {
                    ProductId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBase", t => t.ProductId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.ProductBundle",
                c => new
                {
                    ProductId = c.Int(nullable: false),
                    BundleStockOptions = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBaseExtended", t => t.ProductId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.ProductTime",
                c => new
                {
                    ProductId = c.Int(nullable: false),
                    Minutes = c.Int(nullable: false),
                    WeekDayMaxMinutes = c.Int(),
                    WeekEndMaxMinutes = c.Int(),
                    AppGroupId = c.Int(),
                    ExpiresAfterDays = c.Int(nullable: false),
                    ExpirationOptions = c.Int(nullable: false),
                    ExpireFromOptions = c.Int(nullable: false),
                    UsageOptions = c.Int(nullable: false),
                    UseOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBase", t => t.ProductId)
                .ForeignKey("dbo.AppGroup", t => t.AppGroupId)
                .Index(t => t.ProductId)
                .Index(t => t.AppGroupId);

            CreateTable(
                "dbo.TaskJunction",
                c => new
                {
                    TaskId = c.Int(nullable: false),
                    SourceDirectory = c.String(nullable: false, maxLength: 255),
                    DestinationDirectory = c.String(nullable: false, maxLength: 255),
                    Options = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.TaskBase", t => t.TaskId)
                .Index(t => t.TaskId);

            CreateTable(
                "dbo.TaskNotification",
                c => new
                {
                    TaskId = c.Int(nullable: false),
                    Title = c.String(nullable: false, maxLength: 255),
                    Message = c.String(nullable: false),
                })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.TaskBase", t => t.TaskId)
                .Index(t => t.TaskId);

            CreateTable(
                "dbo.TaskProcess",
                c => new
                {
                    TaskId = c.Int(nullable: false),
                    FileName = c.String(nullable: false, maxLength: 255),
                    Arguments = c.String(maxLength: 255),
                    WorkingDirectory = c.String(maxLength: 255),
                    Username = c.String(maxLength: 255),
                    Password = c.String(maxLength: 45),
                    ProcessOptions = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.TaskBase", t => t.TaskId)
                .Index(t => t.TaskId);

            CreateTable(
                "dbo.TaskScript",
                c => new
                {
                    TaskId = c.Int(nullable: false),
                    ScriptType = c.Int(nullable: false),
                    Data = c.String(nullable: false),
                    ProcessOptions = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.TaskBase", t => t.TaskId)
                .Index(t => t.TaskId);

            CreateTable(
                "dbo.UsageUserSession",
                c => new
                {
                    UsageId = c.Int(nullable: false),
                    UserSessionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UsageId)
                .ForeignKey("dbo.Usage", t => t.UsageId)
                .ForeignKey("dbo.UserSession", t => t.UserSessionId)
                .Index(t => t.UsageId)
                .Index(t => t.UserSessionId);

            CreateTable(
                "dbo.UsageTimeFixed",
                c => new
                {
                    UsageId = c.Int(nullable: false),
                    InvoiceLineId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UsageId)
                .ForeignKey("dbo.UsageUserSession", t => t.UsageId)
                .ForeignKey("dbo.InvoiceLineTimeFixed", t => t.InvoiceLineId)
                .Index(t => t.UsageId)
                .Index(t => t.InvoiceLineId);

            CreateTable(
                "dbo.UsageRate",
                c => new
                {
                    UsageId = c.Int(nullable: false),
                    BillRateId = c.Int(nullable: false),
                    Total = c.Decimal(nullable: false, precision: 19, scale: 4),
                    Rate = c.Decimal(nullable: false, precision: 19, scale: 4),
                    BillProfileStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.UsageId)
                .ForeignKey("dbo.UsageUserSession", t => t.UsageId)
                .ForeignKey("dbo.BillRate", t => t.BillRateId, cascadeDelete: true)
                .Index(t => t.UsageId)
                .Index(t => t.BillRateId);

            CreateTable(
                "dbo.UsageTime",
                c => new
                {
                    UsageId = c.Int(nullable: false),
                    InvoiceLineId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UsageId)
                .ForeignKey("dbo.UsageUserSession", t => t.UsageId)
                .ForeignKey("dbo.InvoiceLineTime", t => t.InvoiceLineId)
                .Index(t => t.UsageId)
                .Index(t => t.InvoiceLineId);

            CreateTable(
                "dbo.UserMember",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    Username = c.String(nullable: false, maxLength: 30),
                    Email = c.String(maxLength: 254),
                    UserGroupId = c.Int(nullable: false),
                    IsNegativeBalanceAllowed = c.Boolean(),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId)
                .Index(t => t.UserId)
                .Index(t => t.Username, unique: true, name: "UQ_Username")
                .Index(t => t.Email, unique: true, name: "UQ_Email")
                .Index(t => t.UserGroupId);

            CreateTable(
                "dbo.UserGuest",
                c => new
                {
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserMember", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UserOperator",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    Username = c.String(nullable: false, maxLength: 30),
                    Email = c.String(maxLength: 254),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Username, unique: true, name: "UQ_Username")
                .Index(t => t.Email, unique: true, name: "UQ_Email");

            CreateTable(
                "dbo.Product",
                c => new
                {
                    ProductId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBaseExtended", t => t.ProductId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.InvoiceLineProduct",
                c => new
                {
                    InvoiceLineId = c.Int(nullable: false),
                    OrderLineId = c.Int(nullable: false),
                    ProductId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.InvoiceLineId)
                .ForeignKey("dbo.InvoiceLineExtended", t => t.InvoiceLineId)
                .ForeignKey("dbo.ProductOLProduct", t => t.OrderLineId)
                .ForeignKey("dbo.ProductBaseExtended", t => t.ProductId)
                .Index(t => t.InvoiceLineId)
                .Index(t => t.OrderLineId, unique: true, name: "UQ_OrderLine")
                .Index(t => t.ProductId);

            #region CREATE FILTERED INDEXES

            Sql("DROP INDEX [UQ_Email] ON [dbo].[UserMember]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_Email] ON [dbo].[UserMember](Email) WHERE Email IS NOT NULL");

            Sql("DROP INDEX [UQ_Email] ON [dbo].[UserOperator]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_Email] ON [dbo].[UserOperator](Email) WHERE Email IS NOT NULL");

            Sql("DROP INDEX [UQ_Barcode] ON [dbo].[ProductBase]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_Barcode] ON [dbo].[ProductBase](Barcode) WHERE Barcode IS NOT NULL");

            Sql("DROP INDEX [UQ_PointsTransaction] ON [dbo].[InvoiceLine]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_PointsTransaction] ON [dbo].[InvoiceLine](PointsTransactionId) WHERE PointsTransactionId IS NOT NULL");

            Sql("DROP INDEX [UQ_StockTransaction] ON [dbo].[InvoiceLineExtended]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_StockTransaction] ON [dbo].[InvoiceLineExtended](StockTransactionId) WHERE StockTransactionId IS NOT NULL");

            Sql("DROP INDEX [UQ_DepositTransaction] ON [dbo].[Payment]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_DepositTransaction] ON [dbo].[Payment](DepositTransactionId) WHERE DepositTransactionId IS NOT NULL");

            Sql("DROP INDEX [UQ_PointsTransaction] ON [dbo].[Payment]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_PointsTransaction] ON [dbo].[Payment](PointTransactionId) WHERE PointTransactionId IS NOT NULL");

            Sql("DROP INDEX [UQ_SmartCardUID] ON [dbo].[User]");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX [UQ_SmartCardUID] ON [dbo].[User](SmartCardUID) WHERE SmartCardUID IS NOT NULL");
            
            #endregion
        }

        public override void Down()
        {
            DropForeignKey("dbo.InvoiceLineProduct", "ProductId", "dbo.ProductBaseExtended");
            DropForeignKey("dbo.InvoiceLineProduct", "OrderLineId", "dbo.ProductOLProduct");
            DropForeignKey("dbo.InvoiceLineProduct", "InvoiceLineId", "dbo.InvoiceLineExtended");
            DropForeignKey("dbo.Product", "ProductId", "dbo.ProductBaseExtended");
            DropForeignKey("dbo.UserOperator", "UserId", "dbo.User");
            DropForeignKey("dbo.UserGuest", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.UserMember", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.UserMember", "UserId", "dbo.User");
            DropForeignKey("dbo.UsageTime", "InvoiceLineId", "dbo.InvoiceLineTime");
            DropForeignKey("dbo.UsageTime", "UsageId", "dbo.UsageUserSession");
            DropForeignKey("dbo.UsageRate", "BillRateId", "dbo.BillRate");
            DropForeignKey("dbo.UsageRate", "UsageId", "dbo.UsageUserSession");
            DropForeignKey("dbo.UsageTimeFixed", "InvoiceLineId", "dbo.InvoiceLineTimeFixed");
            DropForeignKey("dbo.UsageTimeFixed", "UsageId", "dbo.UsageUserSession");
            DropForeignKey("dbo.UsageUserSession", "UserSessionId", "dbo.UserSession");
            DropForeignKey("dbo.UsageUserSession", "UsageId", "dbo.Usage");
            DropForeignKey("dbo.TaskScript", "TaskId", "dbo.TaskBase");
            DropForeignKey("dbo.TaskProcess", "TaskId", "dbo.TaskBase");
            DropForeignKey("dbo.TaskNotification", "TaskId", "dbo.TaskBase");
            DropForeignKey("dbo.TaskJunction", "TaskId", "dbo.TaskBase");
            DropForeignKey("dbo.ProductTime", "AppGroupId", "dbo.AppGroup");
            DropForeignKey("dbo.ProductTime", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductBundle", "ProductId", "dbo.ProductBaseExtended");
            DropForeignKey("dbo.ProductBaseExtended", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductOLTimeFixed", "ProductOLId", "dbo.ProductOL");
            DropForeignKey("dbo.ProductOLTime", "ProductTimeId", "dbo.ProductTime");
            DropForeignKey("dbo.ProductOLTime", "ProductOLId", "dbo.ProductOLExtended");
            DropForeignKey("dbo.ProductOLProduct", "ProductId", "dbo.ProductBaseExtended");
            DropForeignKey("dbo.ProductOLProduct", "ProductOLId", "dbo.ProductOLExtended");
            DropForeignKey("dbo.ProductOLExtended", "BundleLineId", "dbo.ProductOLProduct");
            DropForeignKey("dbo.ProductOLExtended", "ProductOLId", "dbo.ProductOL");
            DropForeignKey("dbo.ProductOLSession", "UsageSessionId", "dbo.UsageSession");
            DropForeignKey("dbo.ProductOLSession", "ProductOLId", "dbo.ProductOL");
            DropForeignKey("dbo.InvoiceLineTimeFixed", "OrderLineId", "dbo.ProductOLTimeFixed");
            DropForeignKey("dbo.InvoiceLineTimeFixed", "InvoiceLineId", "dbo.InvoiceLine");
            DropForeignKey("dbo.InvoiceLineTime", "ProductTimeId", "dbo.ProductTime");
            DropForeignKey("dbo.InvoiceLineTime", "OrderLineId", "dbo.ProductOLTime");
            DropForeignKey("dbo.InvoiceLineTime", "InvoiceLineId", "dbo.InvoiceLineExtended");
            DropForeignKey("dbo.InvoiceLineExtended", "StockTransactionId", "dbo.StockTransaction");
            DropForeignKey("dbo.InvoiceLineExtended", "BundleLineId", "dbo.InvoiceLineProduct");
            DropForeignKey("dbo.InvoiceLineExtended", "InvoiceLineId", "dbo.InvoiceLine");
            DropForeignKey("dbo.InvoiceLineSession", "UsageSessionId", "dbo.UsageSession");
            DropForeignKey("dbo.InvoiceLineSession", "OrderLineId", "dbo.ProductOLSession");
            DropForeignKey("dbo.InvoiceLineSession", "InvoiceLineId", "dbo.InvoiceLine");
            DropForeignKey("dbo.HostEndpoint", "HostId", "dbo.Host");
            DropForeignKey("dbo.HostComputer", "HostId", "dbo.Host");
            DropForeignKey("dbo.Variable", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Variable", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Setting", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Setting", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.PluginLibrary", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.PluginLibrary", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.News", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.News", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.MonetaryUnit", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.MonetaryUnit", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Mapping", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Mapping", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.LogException", "LogId", "dbo.Log");
            DropForeignKey("dbo.Feed", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Feed", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.DepositPayment", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.DepositPayment", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.DepositPayment", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.DepositPayment", "DepositTransactionId", "dbo.DepositTransaction");
            DropForeignKey("dbo.DepositPayment", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeCdImage", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeCdImage", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeCdImage", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppExeTask", "TaskBaseId", "dbo.TaskBase");
            DropForeignKey("dbo.ClientTask", "TaskBaseId", "dbo.TaskBase");
            DropForeignKey("dbo.ClientTask", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ClientTask", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.TaskBase", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.TaskBase", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeTask", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeTask", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeTask", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppExePersonalFile", "PersonalFileId", "dbo.PersonalFile");
            DropForeignKey("dbo.PersonalFile", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.PersonalFile", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExePersonalFile", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExePersonalFile", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExePersonalFile", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppExe", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeMaxUser", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeMaxUser", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeMaxUser", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppExeLicense", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeLicense", "LicenseId", "dbo.License");
            DropForeignKey("dbo.License", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.LicenseKey", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.LicenseKey", "LicenseId", "dbo.License");
            DropForeignKey("dbo.LicenseKey", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.License", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeLicense", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeLicense", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppExeImage", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeImage", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeImage", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppExe", "DefaultDeploymentId", "dbo.Deployment");
            DropForeignKey("dbo.Deployment", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeDeployment", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeDeployment", "DeploymentId", "dbo.Deployment");
            DropForeignKey("dbo.AppExeDeployment", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExeDeployment", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.DeploymentDeployment", "ParentId", "dbo.Deployment");
            DropForeignKey("dbo.DeploymentDeployment", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.DeploymentDeployment", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.DeploymentDeployment", "ChildId", "dbo.Deployment");
            DropForeignKey("dbo.Deployment", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExe", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppExe", "AppId", "dbo.App");
            DropForeignKey("dbo.App", "PublisherId", "dbo.AppEnterprise");
            DropForeignKey("dbo.App", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppImage", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppImage", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppImage", "AppId", "dbo.App");
            DropForeignKey("dbo.App", "DeveloperId", "dbo.AppEnterprise");
            DropForeignKey("dbo.AppEnterprise", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppEnterprise", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.App", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppLink", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppLink", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppLink", "AppId", "dbo.App");
            DropForeignKey("dbo.App", "AppCategoryId", "dbo.AppCategory");
            DropForeignKey("dbo.AppCategory", "ParentId", "dbo.AppCategory");
            DropForeignKey("dbo.AppCategory", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppCategory", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserAttribute", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.UserSessionChange", "UserSessionId", "dbo.UserSession");
            DropForeignKey("dbo.UserSessionChange", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.UserSessionChange", "HostId", "dbo.Host");
            DropForeignKey("dbo.Host", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Host", "IconId", "dbo.Icon");
            DropForeignKey("dbo.Host", "HostGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.HostGroup", "SecurityProfileId", "dbo.SecurityProfile");
            DropForeignKey("dbo.HostGroup", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostGroup", "DefaultGuestGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.HostGroup", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostGroup", "AppGroupId", "dbo.AppGroup");
            DropForeignKey("dbo.ProductTimePeriod", "ProductId", "dbo.ProductTime");
            DropForeignKey("dbo.ProductTimePeriodDayTime", "PeriodDayId", "dbo.ProductTimePeriodDay");
            DropForeignKey("dbo.ProductTimePeriodDay", "ProductTimePeriodId", "dbo.ProductTimePeriod");
            DropForeignKey("dbo.ProductUserDisallowed", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.ProductUserDisallowed", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductUserPrice", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.UserGroup", "SecurityProfileId", "dbo.SecurityProfile");
            DropForeignKey("dbo.SecurityProfileRestriction", "SecurityProfileId", "dbo.SecurityProfile");
            DropForeignKey("dbo.SecurityProfileRestriction", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.SecurityProfileRestriction", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.SecurityProfilePolicy", "SecurityProfileId", "dbo.SecurityProfile");
            DropForeignKey("dbo.SecurityProfilePolicy", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.SecurityProfilePolicy", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.SecurityProfile", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.SecurityProfile", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserGroup", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserGroupHostDisallowed", "HostGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.UserGroupHostDisallowed", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserGroupHostDisallowed", "HostGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.UserGroupHostDisallowed", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserGroup", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserGroup", "BillProfileId", "dbo.BillProfile");
            DropForeignKey("dbo.BillProfile", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.BillProfile", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UsageSession", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.UsageSession", "CurrentUsageId", "dbo.Usage");
            DropForeignKey("dbo.Usage", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.UserPicture", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPicture", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.UserPicture", "CreatedById", "dbo.User");
            DropForeignKey("dbo.UserCreditLimit", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.UserCreditLimit", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserCreditLimit", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserCredential", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCredential", "ModifiedById", "dbo.User");
            DropForeignKey("dbo.UserCredential", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserPermission", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPermission", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserPermission", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppStat", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.AppStat", "HostId", "dbo.HostComputer");
            DropForeignKey("dbo.UserSession", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.Invoice", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.Invoice", "ProductOrderId", "dbo.ProductOrder");
            DropForeignKey("dbo.Invoice", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoicePayment", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.InvoicePayment", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.Payment", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.Payment", "PointTransactionId", "dbo.PointTransaction");
            DropForeignKey("dbo.Payment", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.PaymentMethod", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.PaymentMethod", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Payment", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Payment", "DepositTransactionId", "dbo.DepositTransaction");
            DropForeignKey("dbo.DepositTransaction", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.DepositTransaction", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.DepositTransaction", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Payment", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoicePayment", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoicePayment", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.InvoicePayment", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductOrder", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.BundleProductUserPrice", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.BundleProductUserPrice", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.BundleProductUserPrice", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.BundleProductUserPrice", "BundleProductId", "dbo.BundleProduct");
            DropForeignKey("dbo.BundleProduct", "ProductBundleId", "dbo.ProductBundle");
            DropForeignKey("dbo.BundleProduct", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.BundleProduct", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.BundleProduct", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductOL", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.ProductOL", "ProductOrderId", "dbo.ProductOrder");
            DropForeignKey("dbo.ProductOL", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductOL", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductOrder", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductOrder", "HostId", "dbo.Host");
            DropForeignKey("dbo.ProductOrder", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoiceLine", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.InvoiceLine", "PointsTransactionId", "dbo.PointTransaction");
            DropForeignKey("dbo.PointTransaction", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.PointTransaction", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.PointTransaction", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoiceLine", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.InvoiceLine", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Invoice", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserSession", "HostId", "dbo.Host");
            DropForeignKey("dbo.UserSession", "CreatedById", "dbo.User");
            DropForeignKey("dbo.HostLayoutGroupLayout", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostLayoutGroupLayout", "HostLayoutGroupId", "dbo.HostLayoutGroup");
            DropForeignKey("dbo.HostLayoutGroup", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostLayoutGroupImage", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostLayoutGroupImage", "HostLayoutGroupId", "dbo.HostLayoutGroup");
            DropForeignKey("dbo.HostLayoutGroupImage", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostLayoutGroup", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.HostLayoutGroupLayout", "HostId", "dbo.Host");
            DropForeignKey("dbo.HostLayoutGroupLayout", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.Icon", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Icon", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppStat", "AppExeId", "dbo.AppExe");
            DropForeignKey("dbo.AppStat", "AppId", "dbo.App");
            DropForeignKey("dbo.AppRating", "UserId", "dbo.UserMember");
            DropForeignKey("dbo.AppRating", "AppId", "dbo.App");
            DropForeignKey("dbo.Usage", "UsageSessionId", "dbo.UsageSession");
            DropForeignKey("dbo.BillRatePeriodDayTime", "PeriodDayId", "dbo.BillRatePeriodDay");
            DropForeignKey("dbo.BillRatePeriodDay", "BillRateId", "dbo.BillRate");
            DropForeignKey("dbo.BillRateStep", "BillRateId", "dbo.BillRate");
            DropForeignKey("dbo.BillRate", "BillProfileId", "dbo.BillProfile");
            DropForeignKey("dbo.UserGroup", "AppGroupId", "dbo.AppGroup");
            DropForeignKey("dbo.ProductUserPrice", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductUserPrice", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductUserPrice", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductTax", "TaxId", "dbo.Tax");
            DropForeignKey("dbo.Tax", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Tax", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductTax", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductTax", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductTax", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.StockTransaction", "SourceProductId", "dbo.ProductBase");
            DropForeignKey("dbo.StockTransaction", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.StockTransaction", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.StockTransaction", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductBase", "StockProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductBase", "ProductGroupId", "dbo.ProductGroup");
            DropForeignKey("dbo.ProductGroup", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductGroup", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductGroup", "ParentId", "dbo.ProductGroup");
            DropForeignKey("dbo.ProductPeriod", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductPeriodDayTime", "PeriodDayId", "dbo.ProductPeriodDay");
            DropForeignKey("dbo.ProductPeriodDay", "ProductPeriodId", "dbo.ProductPeriod");
            DropForeignKey("dbo.ProductBase", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductImage", "ProductId", "dbo.ProductBase");
            DropForeignKey("dbo.ProductImage", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductImage", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductBase", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductUserDisallowed", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductUserDisallowed", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductTimeHostDisallowed", "ProductTimeId", "dbo.ProductTime");
            DropForeignKey("dbo.ProductTimeHostDisallowed", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.ProductTimeHostDisallowed", "HostGroupId", "dbo.HostGroup");
            DropForeignKey("dbo.ProductTimeHostDisallowed", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppGroup", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppGroup", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.AppGroupApp", "AppGroupId", "dbo.AppGroup");
            DropForeignKey("dbo.AppGroupApp", "AppId", "dbo.App");
            DropForeignKey("dbo.Host", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserSessionChange", "CreatedById", "dbo.User");
            DropForeignKey("dbo.User", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserAttribute", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserAttribute", "CreatedById", "dbo.UserOperator");
            DropForeignKey("dbo.UserAttribute", "AttributeId", "dbo.Attribute");
            DropForeignKey("dbo.Attribute", "ModifiedById", "dbo.UserOperator");
            DropForeignKey("dbo.Attribute", "CreatedById", "dbo.UserOperator");
            DropIndex("dbo.InvoiceLineProduct", new[] { "ProductId" });
            DropIndex("dbo.InvoiceLineProduct", "UQ_OrderLine");
            DropIndex("dbo.InvoiceLineProduct", new[] { "InvoiceLineId" });
            DropIndex("dbo.Product", new[] { "ProductId" });
            DropIndex("dbo.UserOperator", "UQ_Email");
            DropIndex("dbo.UserOperator", "UQ_Username");
            DropIndex("dbo.UserOperator", new[] { "UserId" });
            DropIndex("dbo.UserGuest", new[] { "UserId" });
            DropIndex("dbo.UserMember", new[] { "UserGroupId" });
            DropIndex("dbo.UserMember", "UQ_Email");
            DropIndex("dbo.UserMember", "UQ_Username");
            DropIndex("dbo.UserMember", new[] { "UserId" });
            DropIndex("dbo.UsageTime", new[] { "InvoiceLineId" });
            DropIndex("dbo.UsageTime", new[] { "UsageId" });
            DropIndex("dbo.UsageRate", new[] { "BillRateId" });
            DropIndex("dbo.UsageRate", new[] { "UsageId" });
            DropIndex("dbo.UsageTimeFixed", new[] { "InvoiceLineId" });
            DropIndex("dbo.UsageTimeFixed", new[] { "UsageId" });
            DropIndex("dbo.UsageUserSession", new[] { "UserSessionId" });
            DropIndex("dbo.UsageUserSession", new[] { "UsageId" });
            DropIndex("dbo.TaskScript", new[] { "TaskId" });
            DropIndex("dbo.TaskProcess", new[] { "TaskId" });
            DropIndex("dbo.TaskNotification", new[] { "TaskId" });
            DropIndex("dbo.TaskJunction", new[] { "TaskId" });
            DropIndex("dbo.ProductTime", new[] { "AppGroupId" });
            DropIndex("dbo.ProductTime", new[] { "ProductId" });
            DropIndex("dbo.ProductBundle", new[] { "ProductId" });
            DropIndex("dbo.ProductBaseExtended", new[] { "ProductId" });
            DropIndex("dbo.ProductOLTimeFixed", new[] { "ProductOLId" });
            DropIndex("dbo.ProductOLTime", new[] { "ProductTimeId" });
            DropIndex("dbo.ProductOLTime", new[] { "ProductOLId" });
            DropIndex("dbo.ProductOLProduct", new[] { "ProductId" });
            DropIndex("dbo.ProductOLProduct", new[] { "ProductOLId" });
            DropIndex("dbo.ProductOLExtended", new[] { "BundleLineId" });
            DropIndex("dbo.ProductOLExtended", new[] { "ProductOLId" });
            DropIndex("dbo.ProductOLSession", new[] { "UsageSessionId" });
            DropIndex("dbo.ProductOLSession", new[] { "ProductOLId" });
            DropIndex("dbo.InvoiceLineTimeFixed", "UQ_OrderLine");
            DropIndex("dbo.InvoiceLineTimeFixed", new[] { "InvoiceLineId" });
            DropIndex("dbo.InvoiceLineTime", new[] { "ProductTimeId" });
            DropIndex("dbo.InvoiceLineTime", "UQ_OrderLine");
            DropIndex("dbo.InvoiceLineTime", new[] { "InvoiceLineId" });
            DropIndex("dbo.InvoiceLineExtended", "UQ_StockTransaction");
            DropIndex("dbo.InvoiceLineExtended", new[] { "BundleLineId" });
            DropIndex("dbo.InvoiceLineExtended", new[] { "InvoiceLineId" });
            DropIndex("dbo.InvoiceLineSession", "UQ_UsageSession");
            DropIndex("dbo.InvoiceLineSession", new[] { "OrderLineId" });
            DropIndex("dbo.InvoiceLineSession", new[] { "InvoiceLineId" });
            DropIndex("dbo.HostEndpoint", new[] { "HostId" });
            DropIndex("dbo.HostComputer", "UQ_MACAddress");
            DropIndex("dbo.HostComputer", new[] { "HostId" });
            DropIndex("dbo.Variable", new[] { "CreatedById" });
            DropIndex("dbo.Variable", new[] { "ModifiedById" });
            DropIndex("dbo.Variable", "UQ_Name");
            DropIndex("dbo.Setting", new[] { "CreatedById" });
            DropIndex("dbo.Setting", new[] { "ModifiedById" });
            DropIndex("dbo.Setting", "UQ_Name");
            DropIndex("dbo.PluginLibrary", new[] { "CreatedById" });
            DropIndex("dbo.PluginLibrary", new[] { "ModifiedById" });
            DropIndex("dbo.PluginLibrary", "UQ_FileName");
            DropIndex("dbo.News", new[] { "CreatedById" });
            DropIndex("dbo.News", new[] { "ModifiedById" });
            DropIndex("dbo.MonetaryUnit", new[] { "CreatedById" });
            DropIndex("dbo.MonetaryUnit", new[] { "ModifiedById" });
            DropIndex("dbo.MonetaryUnit", "UQ_Name");
            DropIndex("dbo.Mapping", new[] { "CreatedById" });
            DropIndex("dbo.Mapping", new[] { "ModifiedById" });
            DropIndex("dbo.Mapping", "UQ_MountPoint");
            DropIndex("dbo.LogException", new[] { "LogId" });
            DropIndex("dbo.Log", new[] { "MessageType" });
            DropIndex("dbo.Log", new[] { "Category" });
            DropIndex("dbo.Log", new[] { "HostNumber" });
            DropIndex("dbo.Log", new[] { "Time" });
            DropIndex("dbo.Feed", new[] { "CreatedById" });
            DropIndex("dbo.Feed", new[] { "ModifiedById" });
            DropIndex("dbo.DepositPayment", new[] { "CreatedById" });
            DropIndex("dbo.DepositPayment", new[] { "ModifiedById" });
            DropIndex("dbo.DepositPayment", new[] { "UserId" });
            DropIndex("dbo.DepositPayment", new[] { "PaymentId" });
            DropIndex("dbo.DepositPayment", new[] { "DepositTransactionId" });
            DropIndex("dbo.ClientTask", new[] { "CreatedById" });
            DropIndex("dbo.ClientTask", new[] { "ModifiedById" });
            DropIndex("dbo.ClientTask", new[] { "TaskBaseId" });
            DropIndex("dbo.TaskBase", new[] { "CreatedById" });
            DropIndex("dbo.TaskBase", new[] { "ModifiedById" });
            DropIndex("dbo.TaskBase", "UQ_Guid");
            DropIndex("dbo.TaskBase", "UQ_Name");
            DropIndex("dbo.AppExeTask", new[] { "CreatedById" });
            DropIndex("dbo.AppExeTask", new[] { "ModifiedById" });
            DropIndex("dbo.AppExeTask", new[] { "TaskBaseId" });
            DropIndex("dbo.AppExeTask", new[] { "AppExeId" });
            DropIndex("dbo.PersonalFile", new[] { "CreatedById" });
            DropIndex("dbo.PersonalFile", new[] { "ModifiedById" });
            DropIndex("dbo.PersonalFile", "UQ_Guid");
            DropIndex("dbo.PersonalFile", "UQ_Name");
            DropIndex("dbo.AppExePersonalFile", new[] { "CreatedById" });
            DropIndex("dbo.AppExePersonalFile", new[] { "ModifiedById" });
            DropIndex("dbo.AppExePersonalFile", new[] { "PersonalFileId" });
            DropIndex("dbo.AppExePersonalFile", new[] { "AppExeId" });
            DropIndex("dbo.AppExeMaxUser", new[] { "CreatedById" });
            DropIndex("dbo.AppExeMaxUser", new[] { "ModifiedById" });
            DropIndex("dbo.AppExeMaxUser", "UQ_AppExeAppExeMode");
            DropIndex("dbo.LicenseKey", new[] { "CreatedById" });
            DropIndex("dbo.LicenseKey", new[] { "ModifiedById" });
            DropIndex("dbo.LicenseKey", "UQ_Guid");
            DropIndex("dbo.LicenseKey", new[] { "LicenseId" });
            DropIndex("dbo.License", new[] { "CreatedById" });
            DropIndex("dbo.License", new[] { "ModifiedById" });
            DropIndex("dbo.License", "UQ_Guid");
            DropIndex("dbo.License", "UQ_Name");
            DropIndex("dbo.AppExeLicense", new[] { "CreatedById" });
            DropIndex("dbo.AppExeLicense", new[] { "ModifiedById" });
            DropIndex("dbo.AppExeLicense", new[] { "LicenseId" });
            DropIndex("dbo.AppExeLicense", new[] { "AppExeId" });
            DropIndex("dbo.AppExeImage", new[] { "CreatedById" });
            DropIndex("dbo.AppExeImage", new[] { "ModifiedById" });
            DropIndex("dbo.AppExeImage", new[] { "AppExeId" });
            DropIndex("dbo.AppExeDeployment", new[] { "CreatedById" });
            DropIndex("dbo.AppExeDeployment", new[] { "ModifiedById" });
            DropIndex("dbo.AppExeDeployment", new[] { "DeploymentId" });
            DropIndex("dbo.AppExeDeployment", new[] { "AppExeId" });
            DropIndex("dbo.DeploymentDeployment", new[] { "CreatedById" });
            DropIndex("dbo.DeploymentDeployment", new[] { "ModifiedById" });
            DropIndex("dbo.DeploymentDeployment", new[] { "ChildId" });
            DropIndex("dbo.DeploymentDeployment", new[] { "ParentId" });
            DropIndex("dbo.Deployment", new[] { "CreatedById" });
            DropIndex("dbo.Deployment", new[] { "ModifiedById" });
            DropIndex("dbo.Deployment", "UQ_Guid");
            DropIndex("dbo.Deployment", "UQ_Name");
            DropIndex("dbo.AppImage", new[] { "CreatedById" });
            DropIndex("dbo.AppImage", new[] { "ModifiedById" });
            DropIndex("dbo.AppImage", new[] { "AppId" });
            DropIndex("dbo.AppEnterprise", new[] { "CreatedById" });
            DropIndex("dbo.AppEnterprise", new[] { "ModifiedById" });
            DropIndex("dbo.AppEnterprise", "UQ_Guid");
            DropIndex("dbo.AppEnterprise", "UQ_Name");
            DropIndex("dbo.AppLink", new[] { "CreatedById" });
            DropIndex("dbo.AppLink", new[] { "ModifiedById" });
            DropIndex("dbo.AppLink", "UQ_Guid");
            DropIndex("dbo.AppLink", new[] { "AppId" });
            DropIndex("dbo.ProductTimePeriodDayTime", new[] { "PeriodDayId" });
            DropIndex("dbo.ProductTimePeriodDay", "UQ_ProductTimePeriodDay");
            DropIndex("dbo.ProductTimePeriod", new[] { "ProductId" });
            DropIndex("dbo.SecurityProfileRestriction", new[] { "CreatedById" });
            DropIndex("dbo.SecurityProfileRestriction", new[] { "ModifiedById" });
            DropIndex("dbo.SecurityProfileRestriction", new[] { "SecurityProfileId" });
            DropIndex("dbo.SecurityProfilePolicy", new[] { "CreatedById" });
            DropIndex("dbo.SecurityProfilePolicy", new[] { "ModifiedById" });
            DropIndex("dbo.SecurityProfilePolicy", "UQ_SecurityProfilePolicyType");
            DropIndex("dbo.SecurityProfile", new[] { "CreatedById" });
            DropIndex("dbo.SecurityProfile", new[] { "ModifiedById" });
            DropIndex("dbo.SecurityProfile", "UQ_Name");
            DropIndex("dbo.UserGroupHostDisallowed", new[] { "CreatedById" });
            DropIndex("dbo.UserGroupHostDisallowed", new[] { "ModifiedById" });
            DropIndex("dbo.UserGroupHostDisallowed", "UQ_UserGroupHostGroup");
            DropIndex("dbo.UserPicture", new[] { "CreatedById" });
            DropIndex("dbo.UserPicture", new[] { "ModifiedById" });
            DropIndex("dbo.UserPicture", new[] { "UserId" });
            DropIndex("dbo.UserCreditLimit", new[] { "CreatedById" });
            DropIndex("dbo.UserCreditLimit", new[] { "ModifiedById" });
            DropIndex("dbo.UserCreditLimit", new[] { "UserId" });
            DropIndex("dbo.UserCredential", new[] { "CreatedById" });
            DropIndex("dbo.UserCredential", new[] { "ModifiedById" });
            DropIndex("dbo.UserCredential", new[] { "UserId" });
            DropIndex("dbo.UserPermission", new[] { "CreatedById" });
            DropIndex("dbo.UserPermission", new[] { "ModifiedById" });
            DropIndex("dbo.UserPermission", "UQ_UserPermission");
            DropIndex("dbo.PaymentMethod", new[] { "CreatedById" });
            DropIndex("dbo.PaymentMethod", new[] { "ModifiedById" });
            DropIndex("dbo.PaymentMethod", "UQ_Name");
            DropIndex("dbo.DepositTransaction", new[] { "CreatedById" });
            DropIndex("dbo.DepositTransaction", new[] { "ModifiedById" });
            DropIndex("dbo.DepositTransaction", new[] { "UserId" });
            DropIndex("dbo.Payment", new[] { "CreatedById" });
            DropIndex("dbo.Payment", new[] { "ModifiedById" });
            DropIndex("dbo.Payment", "UQ_PointsTransaction");
            DropIndex("dbo.Payment", "UQ_DepositTransaction");
            DropIndex("dbo.Payment", new[] { "PaymentMethodId" });
            DropIndex("dbo.Payment", new[] { "UserId" });
            DropIndex("dbo.InvoicePayment", new[] { "CreatedById" });
            DropIndex("dbo.InvoicePayment", new[] { "ModifiedById" });
            DropIndex("dbo.InvoicePayment", new[] { "UserId" });
            DropIndex("dbo.InvoicePayment", new[] { "PaymentId" });
            DropIndex("dbo.InvoicePayment", new[] { "InvoiceId" });
            DropIndex("dbo.BundleProductUserPrice", new[] { "CreatedById" });
            DropIndex("dbo.BundleProductUserPrice", new[] { "ModifiedById" });
            DropIndex("dbo.BundleProductUserPrice", "UQ_BundleProductUserGroup");
            DropIndex("dbo.BundleProduct", new[] { "CreatedById" });
            DropIndex("dbo.BundleProduct", new[] { "ModifiedById" });
            DropIndex("dbo.BundleProduct", new[] { "ProductId" });
            DropIndex("dbo.BundleProduct", new[] { "ProductBundleId" });
            DropIndex("dbo.ProductOrder", new[] { "CreatedById" });
            DropIndex("dbo.ProductOrder", new[] { "ModifiedById" });
            DropIndex("dbo.ProductOrder", new[] { "HostId" });
            DropIndex("dbo.ProductOrder", new[] { "UserId" });
            DropIndex("dbo.ProductOL", new[] { "CreatedById" });
            DropIndex("dbo.ProductOL", new[] { "ModifiedById" });
            DropIndex("dbo.ProductOL", new[] { "UserId" });
            DropIndex("dbo.ProductOL", new[] { "ProductOrderId" });
            DropIndex("dbo.PointTransaction", new[] { "CreatedById" });
            DropIndex("dbo.PointTransaction", new[] { "ModifiedById" });
            DropIndex("dbo.PointTransaction", new[] { "UserId" });
            DropIndex("dbo.Invoice", new[] { "CreatedById" });
            DropIndex("dbo.Invoice", new[] { "ModifiedById" });
            DropIndex("dbo.Invoice", new[] { "UserId" });
            DropIndex("dbo.Invoice", new[] { "ProductOrderId" });
            DropIndex("dbo.InvoiceLine", new[] { "CreatedById" });
            DropIndex("dbo.InvoiceLine", new[] { "ModifiedById" });
            DropIndex("dbo.InvoiceLine", "UQ_PointsTransaction");
            DropIndex("dbo.InvoiceLine", new[] { "UserId" });
            DropIndex("dbo.InvoiceLine", new[] { "InvoiceId" });
            DropIndex("dbo.UserSession", new[] { "CreatedById" });
            DropIndex("dbo.UserSession", new[] { "HostId" });
            DropIndex("dbo.UserSession", new[] { "UserId" });
            DropIndex("dbo.HostLayoutGroupImage", new[] { "CreatedById" });
            DropIndex("dbo.HostLayoutGroupImage", new[] { "ModifiedById" });
            DropIndex("dbo.HostLayoutGroupImage", new[] { "HostLayoutGroupId" });
            DropIndex("dbo.HostLayoutGroup", new[] { "CreatedById" });
            DropIndex("dbo.HostLayoutGroup", new[] { "ModifiedById" });
            DropIndex("dbo.HostLayoutGroup", "UQ_Name");
            DropIndex("dbo.HostLayoutGroupLayout", new[] { "CreatedById" });
            DropIndex("dbo.HostLayoutGroupLayout", new[] { "ModifiedById" });
            DropIndex("dbo.HostLayoutGroupLayout", "UQ_HostLayoutGroupHost");
            DropIndex("dbo.Icon", new[] { "CreatedById" });
            DropIndex("dbo.Icon", new[] { "ModifiedById" });
            DropIndex("dbo.AppStat", new[] { "UserId" });
            DropIndex("dbo.AppStat", new[] { "HostId" });
            DropIndex("dbo.AppStat", new[] { "AppExeId" });
            DropIndex("dbo.AppStat", new[] { "AppId" });
            DropIndex("dbo.AppRating", new[] { "UserId" });
            DropIndex("dbo.AppRating", new[] { "AppId" });
            DropIndex("dbo.UsageSession", new[] { "CurrentUsageId" });
            DropIndex("dbo.UsageSession", new[] { "UserId" });
            DropIndex("dbo.Usage", new[] { "UserId" });
            DropIndex("dbo.Usage", new[] { "UsageSessionId" });
            DropIndex("dbo.BillRatePeriodDayTime", new[] { "PeriodDayId" });
            DropIndex("dbo.BillRatePeriodDay", "UQ_BillRatePeriodDay");
            DropIndex("dbo.BillRateStep", "UQ_BillRateMinute");
            DropIndex("dbo.BillRate", new[] { "BillProfileId" });
            DropIndex("dbo.BillProfile", new[] { "CreatedById" });
            DropIndex("dbo.BillProfile", new[] { "ModifiedById" });
            DropIndex("dbo.BillProfile", "UQ_Name");
            DropIndex("dbo.UserGroup", new[] { "CreatedById" });
            DropIndex("dbo.UserGroup", new[] { "ModifiedById" });
            DropIndex("dbo.UserGroup", new[] { "BillProfileId" });
            DropIndex("dbo.UserGroup", new[] { "SecurityProfileId" });
            DropIndex("dbo.UserGroup", new[] { "AppGroupId" });
            DropIndex("dbo.UserGroup", "UQ_Name");
            DropIndex("dbo.ProductUserPrice", new[] { "CreatedById" });
            DropIndex("dbo.ProductUserPrice", new[] { "ModifiedById" });
            DropIndex("dbo.ProductUserPrice", "UQ_ProductUserGroup");
            DropIndex("dbo.Tax", new[] { "CreatedById" });
            DropIndex("dbo.Tax", new[] { "ModifiedById" });
            DropIndex("dbo.Tax", "UQ_Name");
            DropIndex("dbo.ProductTax", new[] { "CreatedById" });
            DropIndex("dbo.ProductTax", new[] { "ModifiedById" });
            DropIndex("dbo.ProductTax", "UQ_TaxProduct");
            DropIndex("dbo.StockTransaction", new[] { "CreatedById" });
            DropIndex("dbo.StockTransaction", new[] { "ModifiedById" });
            DropIndex("dbo.StockTransaction", new[] { "SourceProductId" });
            DropIndex("dbo.StockTransaction", new[] { "ProductId" });
            DropIndex("dbo.ProductGroup", new[] { "CreatedById" });
            DropIndex("dbo.ProductGroup", new[] { "ModifiedById" });
            DropIndex("dbo.ProductGroup", new[] { "ParentId" });
            DropIndex("dbo.ProductGroup", "UQ_Name");
            DropIndex("dbo.ProductPeriodDayTime", new[] { "PeriodDayId" });
            DropIndex("dbo.ProductPeriodDay", "UQ_ProductPeriodDay");
            DropIndex("dbo.ProductPeriod", new[] { "ProductId" });
            DropIndex("dbo.ProductImage", new[] { "CreatedById" });
            DropIndex("dbo.ProductImage", new[] { "ModifiedById" });
            DropIndex("dbo.ProductImage", new[] { "ProductId" });
            DropIndex("dbo.ProductUserDisallowed", new[] { "CreatedById" });
            DropIndex("dbo.ProductUserDisallowed", new[] { "ModifiedById" });
            DropIndex("dbo.ProductUserDisallowed", "UQ_ProductUserGroup");
            DropIndex("dbo.ProductTimeHostDisallowed", new[] { "CreatedById" });
            DropIndex("dbo.ProductTimeHostDisallowed", new[] { "ModifiedById" });
            DropIndex("dbo.ProductTimeHostDisallowed", "UQ_ProductTimeHostGroup");
            DropIndex("dbo.ProductBase", new[] { "CreatedById" });
            DropIndex("dbo.ProductBase", new[] { "ModifiedById" });
            DropIndex("dbo.ProductBase", new[] { "StockProductId" });
            DropIndex("dbo.ProductBase", "UQ_Barcode");
            DropIndex("dbo.ProductBase", "UQ_Name");
            DropIndex("dbo.ProductBase", new[] { "ProductGroupId" });
            DropIndex("dbo.AppGroupApp", new[] { "AppId" });
            DropIndex("dbo.AppGroupApp", new[] { "AppGroupId" });
            DropIndex("dbo.AppGroup", new[] { "CreatedById" });
            DropIndex("dbo.AppGroup", new[] { "ModifiedById" });
            DropIndex("dbo.AppGroup", "UQ_Guid");
            DropIndex("dbo.AppGroup", "UQ_Name");
            DropIndex("dbo.HostGroup", new[] { "CreatedById" });
            DropIndex("dbo.HostGroup", new[] { "ModifiedById" });
            DropIndex("dbo.HostGroup", new[] { "DefaultGuestGroupId" });
            DropIndex("dbo.HostGroup", new[] { "SecurityProfileId" });
            DropIndex("dbo.HostGroup", new[] { "AppGroupId" });
            DropIndex("dbo.HostGroup", "UQ_Name");
            DropIndex("dbo.Host", new[] { "CreatedById" });
            DropIndex("dbo.Host", new[] { "ModifiedById" });
            DropIndex("dbo.Host", new[] { "IconId" });
            DropIndex("dbo.Host", new[] { "HostGroupId" });
            DropIndex("dbo.UserSessionChange", new[] { "CreatedById" });
            DropIndex("dbo.UserSessionChange", new[] { "HostId" });
            DropIndex("dbo.UserSessionChange", new[] { "UserId" });
            DropIndex("dbo.UserSessionChange", new[] { "UserSessionId" });
            DropIndex("dbo.Attribute", new[] { "CreatedById" });
            DropIndex("dbo.Attribute", new[] { "ModifiedById" });
            DropIndex("dbo.Attribute", "UQ_Name");
            DropIndex("dbo.UserAttribute", new[] { "CreatedById" });
            DropIndex("dbo.UserAttribute", new[] { "ModifiedById" });
            DropIndex("dbo.UserAttribute", "UQ_UserAttribute");
            DropIndex("dbo.User", new[] { "CreatedById" });
            DropIndex("dbo.User", new[] { "ModifiedById" });
            DropIndex("dbo.User", "UQ_SmartCardUID");
            DropIndex("dbo.User", "UQ_Guid");
            DropIndex("dbo.AppCategory", new[] { "CreatedById" });
            DropIndex("dbo.AppCategory", new[] { "ModifiedById" });
            DropIndex("dbo.AppCategory", "UQ_Guid");
            DropIndex("dbo.AppCategory", new[] { "ParentId" });
            DropIndex("dbo.App", new[] { "CreatedById" });
            DropIndex("dbo.App", new[] { "ModifiedById" });
            DropIndex("dbo.App", "UQ_Guid");
            DropIndex("dbo.App", new[] { "AppCategoryId" });
            DropIndex("dbo.App", new[] { "DeveloperId" });
            DropIndex("dbo.App", new[] { "PublisherId" });
            DropIndex("dbo.AppExe", new[] { "CreatedById" });
            DropIndex("dbo.AppExe", new[] { "ModifiedById" });
            DropIndex("dbo.AppExe", new[] { "DefaultDeploymentId" });
            DropIndex("dbo.AppExe", new[] { "AppId" });
            DropIndex("dbo.AppExeCdImage", new[] { "CreatedById" });
            DropIndex("dbo.AppExeCdImage", new[] { "ModifiedById" });
            DropIndex("dbo.AppExeCdImage", "UQ_Guid");
            DropIndex("dbo.AppExeCdImage", new[] { "AppExeId" });
            DropTable("dbo.InvoiceLineProduct");
            DropTable("dbo.Product");
            DropTable("dbo.UserOperator");
            DropTable("dbo.UserGuest");
            DropTable("dbo.UserMember");
            DropTable("dbo.UsageTime");
            DropTable("dbo.UsageRate");
            DropTable("dbo.UsageTimeFixed");
            DropTable("dbo.UsageUserSession");
            DropTable("dbo.TaskScript");
            DropTable("dbo.TaskProcess");
            DropTable("dbo.TaskNotification");
            DropTable("dbo.TaskJunction");
            DropTable("dbo.ProductTime");
            DropTable("dbo.ProductBundle");
            DropTable("dbo.ProductBaseExtended");
            DropTable("dbo.ProductOLTimeFixed");
            DropTable("dbo.ProductOLTime");
            DropTable("dbo.ProductOLProduct");
            DropTable("dbo.ProductOLExtended");
            DropTable("dbo.ProductOLSession");
            DropTable("dbo.InvoiceLineTimeFixed");
            DropTable("dbo.InvoiceLineTime");
            DropTable("dbo.InvoiceLineExtended");
            DropTable("dbo.InvoiceLineSession");
            DropTable("dbo.HostEndpoint");
            DropTable("dbo.HostComputer");
            DropTable("dbo.Variable");
            DropTable("dbo.Setting");
            DropTable("dbo.PluginLibrary");
            DropTable("dbo.News");
            DropTable("dbo.MonetaryUnit");
            DropTable("dbo.Mapping");
            DropTable("dbo.LogException");
            DropTable("dbo.Log");
            DropTable("dbo.Feed");
            DropTable("dbo.DepositPayment");
            DropTable("dbo.ClientTask");
            DropTable("dbo.TaskBase");
            DropTable("dbo.AppExeTask");
            DropTable("dbo.PersonalFile");
            DropTable("dbo.AppExePersonalFile");
            DropTable("dbo.AppExeMaxUser");
            DropTable("dbo.LicenseKey");
            DropTable("dbo.License");
            DropTable("dbo.AppExeLicense");
            DropTable("dbo.AppExeImage");
            DropTable("dbo.AppExeDeployment");
            DropTable("dbo.DeploymentDeployment");
            DropTable("dbo.Deployment");
            DropTable("dbo.AppImage");
            DropTable("dbo.AppEnterprise");
            DropTable("dbo.AppLink");
            DropTable("dbo.ProductTimePeriodDayTime");
            DropTable("dbo.ProductTimePeriodDay");
            DropTable("dbo.ProductTimePeriod");
            DropTable("dbo.SecurityProfileRestriction");
            DropTable("dbo.SecurityProfilePolicy");
            DropTable("dbo.SecurityProfile");
            DropTable("dbo.UserGroupHostDisallowed");
            DropTable("dbo.UserPicture");
            DropTable("dbo.UserCreditLimit");
            DropTable("dbo.UserCredential");
            DropTable("dbo.UserPermission");
            DropTable("dbo.PaymentMethod");
            DropTable("dbo.DepositTransaction");
            DropTable("dbo.Payment");
            DropTable("dbo.InvoicePayment");
            DropTable("dbo.BundleProductUserPrice");
            DropTable("dbo.BundleProduct");
            DropTable("dbo.ProductOrder");
            DropTable("dbo.ProductOL");
            DropTable("dbo.PointTransaction");
            DropTable("dbo.Invoice");
            DropTable("dbo.InvoiceLine");
            DropTable("dbo.UserSession");
            DropTable("dbo.HostLayoutGroupImage");
            DropTable("dbo.HostLayoutGroup");
            DropTable("dbo.HostLayoutGroupLayout");
            DropTable("dbo.Icon");
            DropTable("dbo.AppStat");
            DropTable("dbo.AppRating");
            DropTable("dbo.UsageSession");
            DropTable("dbo.Usage");
            DropTable("dbo.BillRatePeriodDayTime");
            DropTable("dbo.BillRatePeriodDay");
            DropTable("dbo.BillRateStep");
            DropTable("dbo.BillRate");
            DropTable("dbo.BillProfile");
            DropTable("dbo.UserGroup");
            DropTable("dbo.ProductUserPrice");
            DropTable("dbo.Tax");
            DropTable("dbo.ProductTax");
            DropTable("dbo.StockTransaction");
            DropTable("dbo.ProductGroup");
            DropTable("dbo.ProductPeriodDayTime");
            DropTable("dbo.ProductPeriodDay");
            DropTable("dbo.ProductPeriod");
            DropTable("dbo.ProductImage");
            DropTable("dbo.ProductUserDisallowed");
            DropTable("dbo.ProductTimeHostDisallowed");
            DropTable("dbo.ProductBase");
            DropTable("dbo.AppGroupApp");
            DropTable("dbo.AppGroup");
            DropTable("dbo.HostGroup");
            DropTable("dbo.Host");
            DropTable("dbo.UserSessionChange");
            DropTable("dbo.Attribute");
            DropTable("dbo.UserAttribute");
            DropTable("dbo.User");
            DropTable("dbo.AppCategory");
            DropTable("dbo.App");
            DropTable("dbo.AppExe");
            DropTable("dbo.AppExeCdImage");
        }
    }
}
