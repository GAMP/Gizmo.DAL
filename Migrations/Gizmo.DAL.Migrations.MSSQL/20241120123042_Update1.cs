using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_PaymentMethod_PreferedPaymentMethodId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_UserMember_PreferedUserId",
                table: "ReservationHost");

            migrationBuilder.DropIndex(
                name: "IX_HostLayoutGroup_Name",
                table: "HostLayoutGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_Name",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_Device_Name",
                table: "Device");

            migrationBuilder.RenameColumn(
                name: "PreferedUserId",
                table: "ReservationHost",
                newName: "PreferredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationHost_PreferedUserId",
                table: "ReservationHost",
                newName: "IX_ReservationHost_PreferredUserId");

            migrationBuilder.RenameColumn(
                name: "PreferedPaymentMethodId",
                table: "ProductOrder",
                newName: "PreferredPaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_PreferedPaymentMethodId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_PreferredPaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "OutstandngPoints",
                table: "Invoice",
                newName: "OutstandingPoints");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Void",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "UserSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountGroupId",
                table: "UserGroup",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "UsageSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "StockTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Shift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Register",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "Register",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FiscalReceiptPrinterNumber",
                table: "Register",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTerminalNumber",
                table: "Register",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Register",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Refund",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "PaymentIntent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Payment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "InvoicePayment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "HostLayoutGroupLayout",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "HostLayoutGroupLayout",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "HostLayoutGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "HostGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "DepositPayment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BillRate",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "AssistanceRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "AssetTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Asset",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "AppStat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companion",
                columns: table => new
                {
                    CompanionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companion", x => x.CompanionId);
                    table.ForeignKey(
                        name: "FK_Companion_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Companion_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discount_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Discount_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DiscountGroup",
                columns: table => new
                {
                    DiscountGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountGroup", x => x.DiscountGroupId);
                    table.ForeignKey(
                        name: "FK_DiscountGroup_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DiscountGroup_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentTypeId);
                    table.ForeignKey(
                        name: "FK_DocumentType_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DocumentType_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustmentReason",
                columns: table => new
                {
                    InventoryAdjustmentReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustmentReason", x => x.InventoryAdjustmentReasonId);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustmentReason_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_InventoryAdjustmentReason_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_Promotion_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Promotion_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ReportPreset",
                columns: table => new
                {
                    ReportPresetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Report = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Filters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPreset", x => x.ReportPresetId);
                    table.ForeignKey(
                        name: "FK_ReportPreset_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ReportPreset_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    City = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    HasWorkingSchedule = table.Column<bool>(type: "bit", nullable: false),
                    BusinessDayStart = table.Column<TimeOnly>(type: "time", nullable: true),
                    BusinessDayEnd = table.Column<TimeOnly>(type: "time", nullable: true),
                    BusinessStartWeekDay = table.Column<int>(type: "int", nullable: true),
                    BusinessEndWeekDay = table.Column<int>(type: "int", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanionId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_Companion_CompanionId",
                        column: x => x.CompanionId,
                        principalTable: "Companion",
                        principalColumn: "CompanionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Branch_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Branch_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DiscountBonusFlat",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountBonusFlat", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountBonusFlat_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriodic",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriodic", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountPeriodic_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountGroupDiscount",
                columns: table => new
                {
                    DiscountGroupDiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountGroupId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountGroupDiscount", x => x.DiscountGroupDiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountGroupDiscount_DiscountGroup_DiscountGroupId",
                        column: x => x.DiscountGroupId,
                        principalTable: "DiscountGroup",
                        principalColumn: "DiscountGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountGroupDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountGroupDiscount_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Document_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Document_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PromotionCode",
                columns: table => new
                {
                    PromotionCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCode", x => x.PromotionCodeId);
                    table.ForeignKey(
                        name: "FK_PromotionCode_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionCode_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_PromotionCode_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PromotionDiscount",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDiscount", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDiscount_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDiscountGroup",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    DiscountGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDiscountGroup", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountGroup_DiscountGroup_DiscountGroupId",
                        column: x => x.DiscountGroupId,
                        principalTable: "DiscountGroup",
                        principalColumn: "DiscountGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountGroup_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionLimit",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionLimit", x => new { x.PromotionId, x.Type });
                    table.ForeignKey(
                        name: "FK_PromotionLimit_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPeriod",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPeriod", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionPeriod_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeBranch",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeBranch", x => new { x.AppExeId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_AppExeBranch_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppExeBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountBranch",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountBranch", x => new { x.DiscountId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_DiscountBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountBranch_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBranch",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBranch", x => new { x.FeedId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_FeedBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedBranch_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "FeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsBranch",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsBranch", x => new { x.NewsId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_NewsBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsBranch_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBranch",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBranch", x => new { x.ProductId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_ProductBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBranch_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionBranch",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionBranch", x => new { x.PromotionId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_PromotionBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionBranch_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stock_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Stock_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserOperatorBranch",
                columns: table => new
                {
                    OperatorBranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperatorBranch", x => x.OperatorBranchId);
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriod",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriod", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountPeriod_DiscountPeriodic_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "DiscountPeriodic",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTargeted",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTargeted", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountTargeted_DiscountPeriodic_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "DiscountPeriodic",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPeriodDay",
                columns: table => new
                {
                    PromotionPeriodDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionPeriodId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPeriodDay", x => x.PromotionPeriodDayId);
                    table.ForeignKey(
                        name: "FK_PromotionPeriodDay_PromotionPeriod_PromotionPeriodId",
                        column: x => x.PromotionPeriodId,
                        principalTable: "PromotionPeriod",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventory_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_Inventory_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventory_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StockCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UnexpectedEntries = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCount_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_StockCount_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_StockCount_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCount_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriodDay",
                columns: table => new
                {
                    DiscountPeriodDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPeriodId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriodDay", x => x.DiscountPeriodDayId);
                    table.ForeignKey(
                        name: "FK_DiscountPeriodDay_DiscountPeriod_DiscountPeriodId",
                        column: x => x.DiscountPeriodId,
                        principalTable: "DiscountPeriod",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountBasic",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    ApplyType = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountBasic", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountBasic_DiscountTargeted_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "DiscountTargeted",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountBonus",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountBonus", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountBonus_DiscountTargeted_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "DiscountTargeted",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroup",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Requirement = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    IncludeAll = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroup", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroup_DiscountTargeted_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "DiscountTargeted",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetGroup_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_TargetGroup_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PromotionPeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "int", nullable: false),
                    EndSecond = table.Column<int>(type: "int", nullable: false),
                    PromotionPeriodDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPeriodDayTime", x => new { x.PromotionPeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_PromotionPeriodDayTime_PromotionPeriodDay_PromotionPeriodDayId",
                        column: x => x.PromotionPeriodDayId,
                        principalTable: "PromotionPeriodDay",
                        principalColumn: "PromotionPeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustment",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    AdjustmentType = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustment", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustment_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocument",
                columns: table => new
                {
                    InventoryDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocument", x => x.InventoryDocumentId);
                    table.ForeignKey(
                        name: "FK_InventoryDocument_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_InventoryDocument_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryDocument_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StockTransactionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryEntry_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryEntry_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_InventoryEntry_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_InventoryEntry_StockTransaction_StockTransactionId",
                        column: x => x.StockTransactionId,
                        principalTable: "StockTransaction",
                        principalColumn: "StockTransactionId");
                    table.ForeignKey(
                        name: "FK_InventoryEntry_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId");
                    table.ForeignKey(
                        name: "FK_InventoryEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryInbound",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryInbound", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryInbound_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransfer",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    TransferStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransfer", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Stock_TransferStockId",
                        column: x => x.TransferStockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockCountEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Expected = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Actual = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StockCountId = table.Column<int>(type: "int", nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCountEntry_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCountEntry_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCountEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriodDayTime",
                columns: table => new
                {
                    DiscountPeriodDayId = table.Column<int>(type: "int", nullable: false),
                    StartSecond = table.Column<int>(type: "int", nullable: false),
                    EndSecond = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriodDayTime", x => new { x.DiscountPeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_DiscountPeriodDayTime_DiscountPeriodDay_DiscountPeriodDayId",
                        column: x => x.DiscountPeriodDayId,
                        principalTable: "DiscountPeriodDay",
                        principalColumn: "DiscountPeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Target",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetGroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Target", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_Target_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Target_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupBillProfile",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupBillProfile", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupBillProfile_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupProduct",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupProduct", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupProduct_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupProductGroup",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupProductGroup", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupProductGroup_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupProductTime",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupProductTime", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupProductTime_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustmentEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    AdjustmentReasonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustmentEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustmentEntry_InventoryAdjustmentReason_AdjustmentReasonId",
                        column: x => x.AdjustmentReasonId,
                        principalTable: "InventoryAdjustmentReason",
                        principalColumn: "InventoryAdjustmentReasonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustmentEntry_InventoryEntry_InventoryEntryId",
                        column: x => x.InventoryEntryId,
                        principalTable: "InventoryEntry",
                        principalColumn: "InventoryEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryInboundEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryInboundEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryInboundEntry_InventoryEntry_InventoryEntryId",
                        column: x => x.InventoryEntryId,
                        principalTable: "InventoryEntry",
                        principalColumn: "InventoryEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransferEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "int", nullable: false),
                    TransferStockId = table.Column<int>(type: "int", nullable: false),
                    TransferStockTransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransferEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryTransferEntry_InventoryEntry_InventoryEntryId",
                        column: x => x.InventoryEntryId,
                        principalTable: "InventoryEntry",
                        principalColumn: "InventoryEntryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTransferEntry_StockTransaction_TransferStockTransactionId",
                        column: x => x.TransferStockTransactionId,
                        principalTable: "StockTransaction",
                        principalColumn: "StockTransactionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTransferEntry_Stock_TransferStockId",
                        column: x => x.TransferStockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TargetBillProfile",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TargetGroupBillProfileId = table.Column<int>(type: "int", nullable: false),
                    BillProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetBillProfile", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetBillProfile_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetBillProfile_TargetGroupBillProfile_TargetGroupBillProfileId",
                        column: x => x.TargetGroupBillProfileId,
                        principalTable: "TargetGroupBillProfile",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetBillProfile_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetProduct",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TargetGroupProductId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetProduct", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetProduct_ProductBaseExtended_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBaseExtended",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetProduct_TargetGroupProduct_TargetGroupProductId",
                        column: x => x.TargetGroupProductId,
                        principalTable: "TargetGroupProduct",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetProduct_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetProductGroup",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TargetGroupProductGroupId = table.Column<int>(type: "int", nullable: false),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetProductGroup", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetProductGroup_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "ProductGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetProductGroup_TargetGroupProductGroup_TargetGroupProductGroupId",
                        column: x => x.TargetGroupProductGroupId,
                        principalTable: "TargetGroupProductGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetProductGroup_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetProductTime",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TargetGroupProductTimeId = table.Column<int>(type: "int", nullable: false),
                    ProductTimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetProductTime", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetProductTime_ProductTime_ProductTimeId",
                        column: x => x.ProductTimeId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetProductTime_TargetGroupProductTime_TargetGroupProductTimeId",
                        column: x => x.TargetGroupProductTimeId,
                        principalTable: "TargetGroupProductTime",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetProductTime_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(Scripts.EF_6_BRANCH_SET);

            migrationBuilder.CreateIndex(
                name: "IX_Void_BranchId",
                table: "Void",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_BranchId",
                table: "UserSession",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_DiscountGroupId",
                table: "UserGroup",
                column: "DiscountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BranchId",
                table: "User",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_BranchId",
                table: "UsageSession",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_BranchId",
                table: "StockTransaction",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BranchId",
                table: "Shift",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_BranchId",
                table: "Register",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_CompanionId",
                table: "Register",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register",
                columns: new[] { "Name", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Register_StockId",
                table: "Register",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_BranchId",
                table: "Refund",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_BranchId",
                table: "PaymentIntent",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BranchId",
                table: "Payment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_BranchId",
                table: "InvoicePayment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BranchId",
                table: "Invoice",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_BranchId",
                table: "HostLayoutGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_Name_BranchId",
                table: "HostLayoutGroup",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_Name_BranchId",
                table: "HostGroup",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_BranchId",
                table: "Device",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Name_BranchId",
                table: "Device",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_BranchId",
                table: "DepositPayment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_BranchId",
                table: "AssistanceRequest",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_BranchId",
                table: "AssetTransaction",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_BranchId",
                table: "Asset",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_BranchId",
                table: "AppStat",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeBranch_AppExeId_BranchId",
                table: "AppExeBranch",
                columns: new[] { "AppExeId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExeBranch_BranchId",
                table: "AppExeBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchId",
                table: "Branch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanionId",
                table: "Branch",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CreatedById",
                table: "Branch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Guid",
                table: "Branch",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_ModifiedById",
                table: "Branch",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Name",
                table: "Branch",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companion_CreatedById",
                table: "Companion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companion_Guid",
                table: "Companion",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companion_ModifiedById",
                table: "Companion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companion_Name",
                table: "Companion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_CreatedById",
                table: "Discount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ModifiedById",
                table: "Discount",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_Name",
                table: "Discount",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountBranch_BranchId",
                table: "DiscountBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountBranch_DiscountId_BranchId",
                table: "DiscountBranch",
                columns: new[] { "DiscountId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroup_CreatedById",
                table: "DiscountGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroup_ModifiedById",
                table: "DiscountGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroup_Name",
                table: "DiscountGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroupDiscount_CreatedById",
                table: "DiscountGroupDiscount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroupDiscount_DiscountGroupId_DiscountId",
                table: "DiscountGroupDiscount",
                columns: new[] { "DiscountGroupId", "DiscountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroupDiscount_DiscountId",
                table: "DiscountGroupDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountPeriod_DiscountId",
                table: "DiscountPeriod",
                column: "DiscountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountPeriodDay_DiscountPeriodId_Day",
                table: "DiscountPeriodDay",
                columns: new[] { "DiscountPeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountPeriodDayTime_DiscountPeriodDayId",
                table: "DiscountPeriodDayTime",
                column: "DiscountPeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_CreatedById",
                table: "Document",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentTypeId",
                table: "Document",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_FileName",
                table: "Document",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_Guid",
                table: "Document",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_ModifiedById",
                table: "Document",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_CreatedById",
                table: "DocumentType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_ModifiedById",
                table: "DocumentType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_Name",
                table: "DocumentType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedBranch_BranchId",
                table: "FeedBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBranch_FeedId_BranchId",
                table: "FeedBranch",
                columns: new[] { "FeedId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CreatedById",
                table: "Inventory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ShiftId",
                table: "Inventory",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_StockId",
                table: "Inventory",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentEntry_AdjustmentReasonId",
                table: "InventoryAdjustmentEntry",
                column: "AdjustmentReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentReason_CreatedById",
                table: "InventoryAdjustmentReason",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentReason_ModifiedById",
                table: "InventoryAdjustmentReason",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_CreatedById",
                table: "InventoryDocument",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_DocumentId",
                table: "InventoryDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_InventoryId_DocumentId",
                table: "InventoryDocument",
                columns: new[] { "InventoryId", "DocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_CreatedById",
                table: "InventoryEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_InventoryId",
                table: "InventoryEntry",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_ProductId",
                table: "InventoryEntry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_ShiftId",
                table: "InventoryEntry",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_StockId",
                table: "InventoryEntry",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_StockTransactionId",
                table: "InventoryEntry",
                column: "StockTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TransferStockId",
                table: "InventoryTransfer",
                column: "TransferStockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferEntry_TransferStockId",
                table: "InventoryTransferEntry",
                column: "TransferStockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferEntry_TransferStockTransactionId",
                table: "InventoryTransferEntry",
                column: "TransferStockTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsBranch_BranchId",
                table: "NewsBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsBranch_NewsId_BranchId",
                table: "NewsBranch",
                columns: new[] { "NewsId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBranch_BranchId",
                table: "ProductBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBranch_ProductId_BranchId",
                table: "ProductBranch",
                columns: new[] { "ProductId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_CreatedById",
                table: "Promotion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_ModifiedById",
                table: "Promotion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionBranch_BranchId",
                table: "PromotionBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionBranch_PromotionId_BranchId",
                table: "PromotionBranch",
                columns: new[] { "PromotionId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_CreatedById",
                table: "PromotionCode",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_ModifiedById",
                table: "PromotionCode",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_PromotionId",
                table: "PromotionCode",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_Value",
                table: "PromotionCode",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscount_DiscountId",
                table: "PromotionDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountGroup_DiscountGroupId",
                table: "PromotionDiscountGroup",
                column: "DiscountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPeriod_PromotionId",
                table: "PromotionPeriod",
                column: "PromotionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPeriodDay_PromotionPeriodId_Day",
                table: "PromotionPeriodDay",
                columns: new[] { "PromotionPeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPeriodDayTime_PromotionPeriodDayId",
                table: "PromotionPeriodDayTime",
                column: "PromotionPeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_CreatedById",
                table: "ReportPreset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_ModifiedById",
                table: "ReportPreset",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_Name",
                table: "ReportPreset",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_BranchId",
                table: "Stock",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_CreatedById",
                table: "Stock",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ModifiedById",
                table: "Stock",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Name_BranchId",
                table: "Stock",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_CreatedById",
                table: "StockCount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_RegisterId",
                table: "StockCount",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_ShiftId",
                table: "StockCount",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_StockId",
                table: "StockCount",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountEntry_CreatedById",
                table: "StockCountEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountEntry_ProductId_StockCountId",
                table: "StockCountEntry",
                columns: new[] { "ProductId", "StockCountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCountEntry_StockCountId",
                table: "StockCountEntry",
                column: "StockCountId");

            migrationBuilder.CreateIndex(
                name: "IX_Target_CreatedById",
                table: "Target",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Target_TargetGroupId",
                table: "Target",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetBillProfile_BillProfileId",
                table: "TargetBillProfile",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetBillProfile_TargetGroupBillProfileId_BillProfileId",
                table: "TargetBillProfile",
                columns: new[] { "TargetGroupBillProfileId", "BillProfileId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroup_CreatedById",
                table: "TargetGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroup_DiscountId",
                table: "TargetGroup",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroup_ModifiedById",
                table: "TargetGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProduct_ProductId",
                table: "TargetProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProduct_TargetGroupProductId_ProductId",
                table: "TargetProduct",
                columns: new[] { "TargetGroupProductId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductGroup_ProductGroupId",
                table: "TargetProductGroup",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductGroup_TargetGroupProductGroupId_ProductGroupId",
                table: "TargetProductGroup",
                columns: new[] { "TargetGroupProductGroupId", "ProductGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductTime_ProductTimeId",
                table: "TargetProductTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductTime_TargetGroupProductTimeId_ProductTimeId",
                table: "TargetProductTime",
                columns: new[] { "TargetGroupProductTimeId", "ProductTimeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_BranchId_OperatorId",
                table: "UserOperatorBranch",
                columns: new[] { "BranchId", "OperatorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_CreatedById",
                table: "UserOperatorBranch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_ModifiedById",
                table: "UserOperatorBranch",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_OperatorId",
                table: "UserOperatorBranch",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppStat_Branch_BranchId",
                table: "AppStat",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Branch_BranchId",
                table: "Asset",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_Branch_BranchId",
                table: "AssetTransaction",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_Branch_BranchId",
                table: "AssistanceRequest",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Branch_BranchId",
                table: "DepositPayment",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Branch_BranchId",
                table: "Device",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroup_Branch_BranchId",
                table: "HostLayoutGroup",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Branch_BranchId",
                table: "Invoice",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Branch_BranchId",
                table: "InvoicePayment",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Branch_BranchId",
                table: "Payment",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_Branch_BranchId",
                table: "PaymentIntent",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_PaymentMethod_PreferredPaymentMethodId",
                table: "ProductOrder",
                column: "PreferredPaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Branch_BranchId",
                table: "Refund",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Companion_CompanionId",
                table: "Register",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "CompanionId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Stock_StockId",
                table: "Register",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Branch_BranchId",
                table: "Reservation",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_UserMember_PreferredUserId",
                table: "ReservationHost",
                column: "PreferredUserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Branch_BranchId",
                table: "StockTransaction",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_Branch_BranchId",
                table: "UsageSession",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Branch_BranchId",
                table: "User",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_DiscountGroup_DiscountGroupId",
                table: "UserGroup",
                column: "DiscountGroupId",
                principalTable: "DiscountGroup",
                principalColumn: "DiscountGroupId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSession_Branch_BranchId",
                table: "UserSession",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Void_Branch_BranchId",
                table: "Void",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppStat_Branch_BranchId",
                table: "AppStat");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Branch_BranchId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetTransaction_Branch_BranchId",
                table: "AssetTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceRequest_Branch_BranchId",
                table: "AssistanceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositPayment_Branch_BranchId",
                table: "DepositPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_Branch_BranchId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostLayoutGroup_Branch_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Branch_BranchId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayment_Branch_BranchId",
                table: "InvoicePayment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Branch_BranchId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentIntent_Branch_BranchId",
                table: "PaymentIntent");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_PaymentMethod_PreferredPaymentMethodId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Branch_BranchId",
                table: "Refund");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Companion_CompanionId",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Stock_StockId",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Branch_BranchId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_UserMember_PreferredUserId",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Branch_BranchId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_Branch_BranchId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Branch_BranchId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_DiscountGroup_DiscountGroupId",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSession_Branch_BranchId",
                table: "UserSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Void_Branch_BranchId",
                table: "Void");

            migrationBuilder.DropTable(
                name: "AppExeBranch");

            migrationBuilder.DropTable(
                name: "DiscountBasic");

            migrationBuilder.DropTable(
                name: "DiscountBonus");

            migrationBuilder.DropTable(
                name: "DiscountBonusFlat");

            migrationBuilder.DropTable(
                name: "DiscountBranch");

            migrationBuilder.DropTable(
                name: "DiscountGroupDiscount");

            migrationBuilder.DropTable(
                name: "DiscountPeriodDayTime");

            migrationBuilder.DropTable(
                name: "FeedBranch");

            migrationBuilder.DropTable(
                name: "InventoryAdjustment");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentEntry");

            migrationBuilder.DropTable(
                name: "InventoryDocument");

            migrationBuilder.DropTable(
                name: "InventoryInbound");

            migrationBuilder.DropTable(
                name: "InventoryInboundEntry");

            migrationBuilder.DropTable(
                name: "InventoryTransfer");

            migrationBuilder.DropTable(
                name: "InventoryTransferEntry");

            migrationBuilder.DropTable(
                name: "NewsBranch");

            migrationBuilder.DropTable(
                name: "ProductBranch");

            migrationBuilder.DropTable(
                name: "PromotionBranch");

            migrationBuilder.DropTable(
                name: "PromotionCode");

            migrationBuilder.DropTable(
                name: "PromotionDiscount");

            migrationBuilder.DropTable(
                name: "PromotionDiscountGroup");

            migrationBuilder.DropTable(
                name: "PromotionLimit");

            migrationBuilder.DropTable(
                name: "PromotionPeriodDayTime");

            migrationBuilder.DropTable(
                name: "ReportPreset");

            migrationBuilder.DropTable(
                name: "StockCountEntry");

            migrationBuilder.DropTable(
                name: "TargetBillProfile");

            migrationBuilder.DropTable(
                name: "TargetProduct");

            migrationBuilder.DropTable(
                name: "TargetProductGroup");

            migrationBuilder.DropTable(
                name: "TargetProductTime");

            migrationBuilder.DropTable(
                name: "UserOperatorBranch");

            migrationBuilder.DropTable(
                name: "DiscountPeriodDay");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentReason");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "InventoryEntry");

            migrationBuilder.DropTable(
                name: "DiscountGroup");

            migrationBuilder.DropTable(
                name: "PromotionPeriodDay");

            migrationBuilder.DropTable(
                name: "StockCount");

            migrationBuilder.DropTable(
                name: "TargetGroupBillProfile");

            migrationBuilder.DropTable(
                name: "TargetGroupProduct");

            migrationBuilder.DropTable(
                name: "TargetGroupProductGroup");

            migrationBuilder.DropTable(
                name: "TargetGroupProductTime");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.DropTable(
                name: "DiscountPeriod");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "PromotionPeriod");

            migrationBuilder.DropTable(
                name: "TargetGroup");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "DiscountTargeted");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "DiscountPeriodic");

            migrationBuilder.DropTable(
                name: "Companion");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Void_BranchId",
                table: "Void");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_BranchId",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_DiscountGroupId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_User_BranchId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_UsageSession_BranchId",
                table: "UsageSession");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_BranchId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Shift_BranchId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Register_BranchId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_CompanionId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_StockId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Refund_BranchId",
                table: "Refund");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntent_BranchId",
                table: "PaymentIntent");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BranchId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayment_BranchId",
                table: "InvoicePayment");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_BranchId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_HostLayoutGroup_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostLayoutGroup_Name_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_Name_BranchId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_Device_BranchId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_Name_BranchId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_DepositPayment_BranchId",
                table: "DepositPayment");

            migrationBuilder.DropIndex(
                name: "IX_AssistanceRequest_BranchId",
                table: "AssistanceRequest");

            migrationBuilder.DropIndex(
                name: "IX_AssetTransaction_BranchId",
                table: "AssetTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Asset_BranchId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_AppStat_BranchId",
                table: "AppStat");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Void");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "UserSession");

            migrationBuilder.DropColumn(
                name: "DiscountGroupId",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "UsageSession");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "FiscalReceiptPrinterNumber",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "PaymentTerminalNumber",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Refund");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "PaymentIntent");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "InvoicePayment");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Column",
                table: "HostLayoutGroupLayout");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "HostLayoutGroupLayout");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "DepositPayment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BillRate");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AssistanceRequest");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AssetTransaction");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AppStat");

            migrationBuilder.RenameColumn(
                name: "PreferredUserId",
                table: "ReservationHost",
                newName: "PreferedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationHost_PreferredUserId",
                table: "ReservationHost",
                newName: "IX_ReservationHost_PreferedUserId");

            migrationBuilder.RenameColumn(
                name: "PreferredPaymentMethodId",
                table: "ProductOrder",
                newName: "PreferedPaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_PreferredPaymentMethodId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_PreferedPaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "OutstandingPoints",
                table: "Invoice",
                newName: "OutstandngPoints");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_Name",
                table: "HostLayoutGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_Name",
                table: "HostGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_Name",
                table: "Device",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_PaymentMethod_PreferedPaymentMethodId",
                table: "ProductOrder",
                column: "PreferedPaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_UserMember_PreferedUserId",
                table: "ReservationHost",
                column: "PreferedUserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
