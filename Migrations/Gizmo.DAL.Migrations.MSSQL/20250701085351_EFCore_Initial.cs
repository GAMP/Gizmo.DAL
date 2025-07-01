using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class EFCore_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HostGroupWaitingLine_HostGroup_HosGroupId",
                table: "HostGroupWaitingLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_PaymentMethod_PreferedPaymentMethodId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_UserMember_PreferedUserId",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

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

            migrationBuilder.RenameIndex(
                name: "UQ_Pin",
                table: "Reservation",
                newName: "IX_Reservation_Pin");

            migrationBuilder.RenameIndex(
                name: "UQ_ProductTimePeriodDay",
                table: "ProductTimePeriodDay",
                newName: "IX_ProductTimePeriodDay_ProductTimePeriodId_Day");

            migrationBuilder.RenameIndex(
                name: "UQ_TaxProduct",
                table: "ProductTax",
                newName: "IX_ProductTax_ProductId_TaxId");

            migrationBuilder.RenameColumn(
                name: "PreferedPaymentMethodId",
                table: "ProductOrder",
                newName: "PreferredPaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_PreferedPaymentMethodId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_PreferredPaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Time",
                table: "Log",
                newName: "IX_Log_Time");

            migrationBuilder.RenameIndex(
                name: "IX_MessageType",
                table: "Log",
                newName: "IX_Log_MessageType");

            migrationBuilder.RenameIndex(
                name: "IX_HostNumber",
                table: "Log",
                newName: "IX_Log_HostNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Category",
                table: "Log",
                newName: "IX_Log_Category");

            migrationBuilder.RenameColumn(
                name: "OutstandngPoints",
                table: "Invoice",
                newName: "OutstandingPoints");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "HostGroupWaitingLineEntry",
                newName: "HostGroupWaitingLineEntryId");

            migrationBuilder.RenameColumn(
                name: "HosGroupId",
                table: "HostGroupWaitingLine",
                newName: "HostGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_HostGroupWaitingLine_HosGroupId",
                table: "HostGroupWaitingLine",
                newName: "IX_HostGroupWaitingLine_HostGroupId");

            migrationBuilder.RenameIndex(
                name: "UQ_HostDevice",
                table: "DeviceHost",
                newName: "IX_DeviceHost_DeviceId_HostId");

            migrationBuilder.RenameIndex(
                name: "UQ_AppExeAppExeMode",
                table: "AppExeMaxUser",
                newName: "IX_AppExeMaxUser_AppExeId_Mode");

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

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserOperator",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserOperator",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserMember",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserMember",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserGuest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountGroupId",
                table: "UserGroup",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionSetId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredChannel",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "StartFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "RatesTotal",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<double>(
                name: "NegativeSeconds",
                table: "UsageSession",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "UsageSession",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "UsageSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillProfileStamp",
                table: "UsageRate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "UsageRate",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "DiscountCalculationType",
                table: "UsageRate",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "UsageRate",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "UsageRate",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
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

            migrationBuilder.AddColumn<bool>(
                name: "DisableDesktopSwitching",
                table: "SecurityProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisableStartMenu",
                table: "SecurityProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StickyShell",
                table: "SecurityProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationTime",
                table: "ReservationHost",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalizedById",
                table: "ReservationHost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovedToReservationHostId",
                table: "ReservationHost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ReservationHost",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationTime",
                table: "Reservation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CancellationGracePeriod",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CancellationRefundPercentage",
                table: "Reservation",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ExpireAfter",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalizedById",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginBlockAfterTime",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginBlockBeforeTime",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
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
                name: "PrepareStatus",
                table: "ProductOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrepareTime",
                table: "ProductOrder",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PreparedQuantity",
                table: "ProductOrder",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PrepareStatus",
                table: "ProductOL",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 24);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrepareTime",
                table: "ProductOL",
                type: "datetime2",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 26);

            migrationBuilder.AddColumn<decimal>(
                name: "PreparedQuantity",
                table: "ProductOL",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 25);

            migrationBuilder.AddColumn<int>(
                name: "ReservationHostId",
                table: "ProductOL",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 22);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "ProductOL",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 21);

            migrationBuilder.AddColumn<int>(
                name: "ReservationSlot",
                table: "ProductOL",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 23);

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

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "InvoiceLineTime",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReservationHostId",
                table: "InvoiceLine",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "InvoiceLine",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 22);

            migrationBuilder.AddColumn<int>(
                name: "ReservationSlot",
                table: "InvoiceLine",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 24);

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
                name: "BillProfileId",
                table: "HostGroup",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "HostGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientOptionsId",
                table: "HostGroup",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "DepositPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "RegisterId",
                table: "DepositPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundedAmount",
                table: "DepositPayment",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "RefundStatus",
                table: "DepositPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "IsVoided",
                table: "DepositPayment",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "FiscalReceiptStatus",
                table: "DepositPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "FiscalReceiptId",
                table: "DepositPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "DepositPayment",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 3);

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
                name: "AgeRestriction",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeFrom = table.Column<int>(type: "int", nullable: false),
                    AgeTo = table.Column<int>(type: "int", nullable: false),
                    DayMinuteFrom = table.Column<int>(type: "int", nullable: true),
                    DayMinuteTo = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestriction", x => x.AgeRestrictionId);
                    table.ForeignKey(
                        name: "FK_AgeRestriction_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ClientOptions",
                columns: table => new
                {
                    ClientOptionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOptions", x => x.ClientOptionsId);
                    table.ForeignKey(
                        name: "FK_ClientOptions_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ClientOptions_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

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
                    ApplyType = table.Column<int>(type: "int", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    RewardType = table.Column<int>(type: "int", nullable: false),
                    Requirement = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
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
                name: "File",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hash = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_File_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_File_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustmentReason",
                columns: table => new
                {
                    InventoryAdjustmentReasonId = table.Column<int>(type: "int", nullable: false),
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
                name: "InventoryTransferReason",
                columns: table => new
                {
                    InventoryTransferReasonId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InventoryTransferReason", x => x.InventoryTransferReasonId);
                    table.ForeignKey(
                        name: "FK_InventoryTransferReason_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_InventoryTransferReason_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FocusType = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Notification_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceipt",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    RRN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceipt", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentReceipt_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentReceipt_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_PaymentReceipt_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_PaymentReceipt_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PresetReservationTime",
                columns: table => new
                {
                    PresetReservationTimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetReservationTime", x => x.PresetReservationTimeId);
                    table.ForeignKey(
                        name: "FK_PresetReservationTime_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_PresetReservationTime_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PresetTopUp",
                columns: table => new
                {
                    PresetTopUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetTopUp", x => x.PresetTopUpId);
                    table.ForeignKey(
                        name: "FK_PresetTopUp_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_PresetTopUp_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProductOLReservationFee",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLReservationFee", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLReservationFee_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                name: "Recipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_Recipient_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RefundReceipt",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false),
                    RRN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundReceipt", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_RefundReceipt_UserOperator_CreatedById",
                        column: x => x.CreatedById,
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
                name: "ReservationProductOrder",
                columns: table => new
                {
                    ReservationProductOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationProductOrder", x => x.ReservationProductOrderId);
                    table.ForeignKey(
                        name: "FK_ReservationProductOrder_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "ProductOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationProductOrder_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationProductOrder_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Schedule_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Target",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Target", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_Target_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserApiKey",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApiKey", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserApiKey_UserOperator_UserId",
                        column: x => x.UserId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChannel",
                columns: table => new
                {
                    UserChannelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Channel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannel", x => x.UserChannelId);
                    table.ForeignKey(
                        name: "FK_UserChannel_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserChannel_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserChannel_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionSet",
                columns: table => new
                {
                    UserPermissionSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionSet", x => x.UserPermissionSetId);
                    table.ForeignKey(
                        name: "FK_UserPermissionSet_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermissionSet_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AgeRestrictionLogin",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestrictionLogin", x => x.AgeRestrictionId);
                    table.ForeignKey(
                        name: "FK_AgeRestrictionLogin_AgeRestriction_AgeRestrictionId",
                        column: x => x.AgeRestrictionId,
                        principalTable: "AgeRestriction",
                        principalColumn: "AgeRestrictionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgeRestrictionProduct",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestrictionProduct", x => x.AgeRestrictionId);
                    table.ForeignKey(
                        name: "FK_AgeRestrictionProduct_AgeRestriction_AgeRestrictionId",
                        column: x => x.AgeRestrictionId,
                        principalTable: "AgeRestriction",
                        principalColumn: "AgeRestrictionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgeRestrictionProduct_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
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
                    HasBusinessSchedule = table.Column<bool>(type: "bit", nullable: false),
                    BusinessDayStart = table.Column<TimeOnly>(type: "time", nullable: true),
                    BusinessDayEnd = table.Column<TimeOnly>(type: "time", nullable: true),
                    BusinessStartWeekDay = table.Column<int>(type: "int", nullable: true),
                    BusinessEndWeekDay = table.Column<int>(type: "int", nullable: true),
                    IsFiscalizationEnabled = table.Column<bool>(type: "bit", nullable: true),
                    BusinessVATId = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    TaxSystem = table.Column<int>(type: "int", nullable: true),
                    GoodsTaxSystem = table.Column<int>(type: "int", nullable: true),
                    ServicesTaxSystem = table.Column<int>(type: "int", nullable: true),
                    TreatDepositsAsService = table.Column<bool>(type: "bit", nullable: true),
                    DepositServiceDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TimeBasedServiceVATRate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    DepositVATRate = table.Column<int>(type: "int", nullable: true),
                    DepositAdvancePaymentType = table.Column<int>(type: "int", nullable: true),
                    CompanionId = table.Column<int>(type: "int", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    DisableTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_DiscountPeriod_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
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
                        name: "FK_TargetGroup_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
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
                name: "DiscountGroupDiscount",
                columns: table => new
                {
                    DiscountGroupId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountGroupDiscount", x => new { x.DiscountGroupId, x.DiscountId });
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
                });

            migrationBuilder.CreateTable(
                name: "FileDocument",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDocument", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileDocument_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileDocument_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileImage",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImage", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileImage_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTimed",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimed", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationTimed_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineReservationFee",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineReservationFee", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineReservationFee_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLineReservationFee_ProductOLReservationFee_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "ProductOLReservationFee",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "RecipientChannel",
                columns: table => new
                {
                    RecipientChannelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    ChannelType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientChannel", x => x.RecipientChannelId);
                    table.ForeignKey(
                        name: "FK_RecipientChannel_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientChannel_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReport",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReport", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_ScheduleReport_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionSetPermission",
                columns: table => new
                {
                    UserPermissionSetPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionSetId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionSetPermission", x => x.UserPermissionSetPermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermissionSetPermission_UserPermissionSet_PermissionSetId",
                        column: x => x.PermissionSetId,
                        principalTable: "UserPermissionSet",
                        principalColumn: "UserPermissionSetId",
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
                name: "TargetGroupPaymentMethod",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupPaymentMethod", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupPaymentMethod_TargetGroup_TargetGroupId",
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
                name: "NotificationTimedRemaining",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimedRemaining", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationTimedRemaining_NotificationTimed_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "NotificationTimed",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTimedReservation",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimedReservation", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationTimedReservation_NotificationTimed_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "NotificationTimed",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDiscount",
                columns: table => new
                {
                    ProductOrderDiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderLineId = table.Column<int>(type: "int", nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    PromotionCodeId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    DiscountName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    ApplyType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDiscount", x => x.ProductOrderDiscountId);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_ProductOL_ProductOrderLineId",
                        column: x => x.ProductOrderLineId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "ProductOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_PromotionCode_PromotionCodeId",
                        column: x => x.PromotionCodeId,
                        principalTable: "PromotionCode",
                        principalColumn: "PromotionCodeId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
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
                name: "ScheduleReportEntry",
                columns: table => new
                {
                    ScheduleReportEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleReportId = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportRange = table.Column<int>(type: "int", nullable: false),
                    Filters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportPresetId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReportEntry", x => x.ScheduleReportEntryId);
                    table.ForeignKey(
                        name: "FK_ScheduleReportEntry_ReportPreset_ReportPresetId",
                        column: x => x.ReportPresetId,
                        principalTable: "ReportPreset",
                        principalColumn: "ReportPresetId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ScheduleReportEntry_ScheduleReport_ScheduleReportId",
                        column: x => x.ScheduleReportId,
                        principalTable: "ScheduleReport",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReportRecipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    ScheduleReportId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReportRecipient", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_ScheduleReport_ScheduleReportId",
                        column: x => x.ScheduleReportId,
                        principalTable: "ScheduleReport",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_UserOperator_UserId",
                        column: x => x.UserId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    StockCountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UnexpectedEntries = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCount", x => x.StockCountId);
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
                name: "TargetPaymentMethod",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TargetGroupPaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    MethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetPaymentMethod", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetPaymentMethod_PaymentMethod_MethodId",
                        column: x => x.MethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetPaymentMethod_TargetGroupPaymentMethod_TargetGroupPaymentMethodId",
                        column: x => x.TargetGroupPaymentMethodId,
                        principalTable: "TargetGroupPaymentMethod",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetPaymentMethod_Target_TargetId",
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
                    Cost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
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
                    FileDocumentId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocument", x => x.InventoryDocumentId);
                    table.ForeignKey(
                        name: "FK_InventoryDocument_FileDocument_FileDocumentId",
                        column: x => x.FileDocumentId,
                        principalTable: "FileDocument",
                        principalColumn: "FileId");
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
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                name: "StockCountEntry",
                columns: table => new
                {
                    StockCountEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockCountId = table.Column<int>(type: "int", nullable: false),
                    Expected = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Actual = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountEntry", x => x.StockCountEntryId);
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
                        principalColumn: "StockCountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCountEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StockCountAdjustment",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "int", nullable: false),
                    AdjustmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountAdjustment", x => x.StockCountId);
                    table.ForeignKey(
                        name: "FK_StockCountAdjustment_InventoryAdjustment_AdjustmentId",
                        column: x => x.AdjustmentId,
                        principalTable: "InventoryAdjustment",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCountAdjustment_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "StockCountId",
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
                name: "InventoryTransferEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "int", nullable: false),
                    TransferReasonId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_InventoryTransferEntry_InventoryTransferReason_TransferReasonId",
                        column: x => x.TransferReasonId,
                        principalTable: "InventoryTransferReason",
                        principalColumn: "InventoryTransferReasonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryInboundEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    InventoryTransferEntryId = table.Column<int>(type: "int", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_InventoryInboundEntry_InventoryTransferEntry_InventoryTransferEntryId",
                        column: x => x.InventoryTransferEntryId,
                        principalTable: "InventoryTransferEntry",
                        principalColumn: "InventoryEntryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryInbound",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    InventoryTransferId = table.Column<int>(type: "int", nullable: true)
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
                    TransferStockId = table.Column<int>(type: "int", nullable: false),
                    InventoryInboundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransfer", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_InventoryInbound_InventoryInboundId",
                        column: x => x.InventoryInboundId,
                        principalTable: "InventoryInbound",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "StockCountInbound",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "int", nullable: false),
                    InboundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountInbound", x => x.StockCountId);
                    table.ForeignKey(
                        name: "FK_StockCountInbound_InventoryInbound_InboundId",
                        column: x => x.InboundId,
                        principalTable: "InventoryInbound",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCountInbound_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "StockCountId",
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
                name: "IX_User_PermissionSetId",
                table: "User",
                column: "PermissionSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_BranchId",
                table: "UsageSession",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRate_DiscountId",
                table: "UsageRate",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_StockId",
                table: "StockTransaction",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BranchId",
                table: "Shift",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_FinalizedById",
                table: "ReservationHost",
                column: "FinalizedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost",
                column: "MovedToReservationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_Status",
                table: "ReservationHost",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FinalizedById",
                table: "Reservation",
                column: "FinalizedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Status",
                table: "Reservation",
                column: "Status");

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
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Register_StockId",
                table: "Register",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_BranchId",
                table: "Refund",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ReservationHostId",
                table: "ProductOL",
                column: "ReservationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ReservationId",
                table: "ProductOL",
                column: "ReservationId");

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
                name: "IX_InvoiceLine_ReservationHostId",
                table: "InvoiceLine",
                column: "ReservationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ReservationId",
                table: "InvoiceLine",
                column: "ReservationId");

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
                name: "IX_HostGroup_BillProfileId",
                table: "HostGroup",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_ClientOptionsId",
                table: "HostGroup",
                column: "ClientOptionsId");

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
                name: "IX_AgeRestriction_CreatedById",
                table: "AgeRestriction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgeRestrictionProduct_ProductId",
                table: "AgeRestrictionProduct",
                column: "ProductId");

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
                name: "IX_ClientOptions_CreatedById",
                table: "ClientOptions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOptions_ModifiedById",
                table: "ClientOptions",
                column: "ModifiedById");

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
                name: "IX_File_CreatedById",
                table: "File",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_File_Guid",
                table: "File",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_ModifiedById",
                table: "File",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FileDocument_DocumentTypeId",
                table: "FileDocument",
                column: "DocumentTypeId");

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
                name: "IX_InventoryAdjustmentReason_Name",
                table: "InventoryAdjustmentReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_CreatedById",
                table: "InventoryDocument",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_FileDocumentId",
                table: "InventoryDocument",
                column: "FileDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_InventoryId_FileDocumentId",
                table: "InventoryDocument",
                columns: new[] { "InventoryId", "FileDocumentId" },
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
                name: "IX_InventoryInbound_InventoryTransferId",
                table: "InventoryInbound",
                column: "InventoryTransferId",
                unique: true,
                filter: "[InventoryTransferId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInboundEntry_InventoryTransferEntryId",
                table: "InventoryInboundEntry",
                column: "InventoryTransferEntryId",
                unique: true,
                filter: "[InventoryTransferEntryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer",
                column: "InventoryInboundId",
                unique: true,
                filter: "[InventoryInboundId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TransferStockId",
                table: "InventoryTransfer",
                column: "TransferStockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferEntry_TransferReasonId",
                table: "InventoryTransferEntry",
                column: "TransferReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferReason_CreatedById",
                table: "InventoryTransferReason",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferReason_ModifiedById",
                table: "InventoryTransferReason",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferReason_Name",
                table: "InventoryTransferReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineReservationFee_OrderLineId",
                table: "InvoiceLineReservationFee",
                column: "OrderLineId",
                unique: true);

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
                name: "IX_Notification_CreatedById",
                table: "Notification",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ModifiedById",
                table: "Notification",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_CreatedById",
                table: "PaymentReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_RegisterId",
                table: "PaymentReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_ShiftId",
                table: "PaymentReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_PresetReservationTime_CreatedById",
                table: "PresetReservationTime",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetReservationTime_ModifiedById",
                table: "PresetReservationTime",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTopUp_CreatedById",
                table: "PresetTopUp",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTopUp_ModifiedById",
                table: "PresetTopUp",
                column: "ModifiedById");

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
                name: "IX_ProductOLReservationFee_ProductOLId",
                table: "ProductOLReservationFee",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_CreatedById",
                table: "ProductOrderDiscount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_DiscountId",
                table: "ProductOrderDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ProductOrderId",
                table: "ProductOrderDiscount",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ProductOrderLineId",
                table: "ProductOrderDiscount",
                column: "ProductOrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_PromotionCodeId",
                table: "ProductOrderDiscount",
                column: "PromotionCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_PromotionId",
                table: "ProductOrderDiscount",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_RegisterId",
                table: "ProductOrderDiscount",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ShiftId",
                table: "ProductOrderDiscount",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_UserId",
                table: "ProductOrderDiscount",
                column: "UserId");

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
                name: "IX_Recipient_CreatedById",
                table: "Recipient",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientChannel_CreatedById",
                table: "RecipientChannel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientChannel_RecipientId_ChannelType",
                table: "RecipientChannel",
                columns: new[] { "RecipientId", "ChannelType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_CreatedById",
                table: "RefundReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_RegisterId",
                table: "RefundReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_ShiftId",
                table: "RefundReceipt",
                column: "ShiftId");

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
                name: "IX_ReservationProductOrder_CreatedById",
                table: "ReservationProductOrder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationProductOrder_ProductOrderId",
                table: "ReservationProductOrder",
                column: "ProductOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationProductOrder_ReservationId_ProductOrderId",
                table: "ReservationProductOrder",
                columns: new[] { "ReservationId", "ProductOrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CreatedById",
                table: "Schedule",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ModifiedById",
                table: "Schedule",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Name",
                table: "Schedule",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportEntry_CreatedById",
                table: "ScheduleReportEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportEntry_ReportPresetId",
                table: "ScheduleReportEntry",
                column: "ReportPresetId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportEntry_ScheduleReportId",
                table: "ScheduleReportEntry",
                column: "ScheduleReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_ScheduleReportId_UserId",
                table: "ScheduleReportRecipient",
                columns: new[] { "ScheduleReportId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_UserId",
                table: "ScheduleReportRecipient",
                column: "UserId");

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
                name: "IX_StockCountAdjustment_AdjustmentId",
                table: "StockCountAdjustment",
                column: "AdjustmentId",
                unique: true);

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
                name: "IX_StockCountInbound_InboundId",
                table: "StockCountInbound",
                column: "InboundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Target_CreatedById",
                table: "Target",
                column: "CreatedById");

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
                name: "IX_TargetPaymentMethod_MethodId",
                table: "TargetPaymentMethod",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetPaymentMethod_TargetGroupPaymentMethodId_MethodId",
                table: "TargetPaymentMethod",
                columns: new[] { "TargetGroupPaymentMethodId", "MethodId" },
                unique: true);

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
                name: "IX_UserApiKey_ApiKey",
                table: "UserApiKey",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApiKey_UserId",
                table: "UserApiKey",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_CreatedById",
                table: "UserChannel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_ModifiedById",
                table: "UserChannel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_UserId_Channel",
                table: "UserChannel",
                columns: new[] { "UserId", "Channel" });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_CreatedById",
                table: "UserPermissionSet",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_ModifiedById",
                table: "UserPermissionSet",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_Name",
                table: "UserPermissionSet",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSetPermission_PermissionSetId_Type_Value",
                table: "UserPermissionSetPermission",
                columns: new[] { "PermissionSetId", "Type", "Value" },
                unique: true);

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
                name: "FK_HostGroup_BillProfile_BillProfileId",
                table: "HostGroup",
                column: "BillProfileId",
                principalTable: "BillProfile",
                principalColumn: "BillProfileId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_ClientOptions_ClientOptionsId",
                table: "HostGroup",
                column: "ClientOptionsId",
                principalTable: "ClientOptions",
                principalColumn: "ClientOptionsId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLine_HostGroup_HostGroupId",
                table: "HostGroupWaitingLine",
                column: "HostGroupId",
                principalTable: "HostGroup",
                principalColumn: "HostGroupId",
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
                name: "FK_InvoiceLine_ReservationHost_ReservationHostId",
                table: "InvoiceLine",
                column: "ReservationHostId",
                principalTable: "ReservationHost",
                principalColumn: "ReservationHostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Reservation_ReservationId",
                table: "InvoiceLine",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ProductOL_ReservationHost_ReservationHostId",
                table: "ProductOL",
                column: "ReservationHostId",
                principalTable: "ReservationHost",
                principalColumn: "ReservationHostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_Reservation_ReservationId",
                table: "ProductOL",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Reservation_User_FinalizedById",
                table: "Reservation",
                column: "FinalizedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost",
                column: "MovedToReservationHostId",
                principalTable: "ReservationHost",
                principalColumn: "ReservationHostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_UserMember_PreferredUserId",
                table: "ReservationHost",
                column: "PreferredUserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_FinalizedById",
                table: "ReservationHost",
                column: "FinalizedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Stock_StockId",
                table: "StockTransaction",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRate_Discount_DiscountId",
                table: "UsageRate",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_Branch_BranchId",
                table: "UsageSession",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Branch_BranchId",
                table: "User",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserPermissionSet_PermissionSetId",
                table: "User",
                column: "PermissionSetId",
                principalTable: "UserPermissionSet",
                principalColumn: "UserPermissionSetId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryInbound_InventoryTransfer_InventoryTransferId",
                table: "InventoryInbound",
                column: "InventoryTransferId",
                principalTable: "InventoryTransfer",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);
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
                name: "FK_HostGroup_BillProfile_BillProfileId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_ClientOptions_ClientOptionsId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroupWaitingLine_HostGroup_HostGroupId",
                table: "HostGroupWaitingLine");

            migrationBuilder.DropForeignKey(
                name: "FK_HostLayoutGroup_Branch_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Branch_BranchId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_ReservationHost_ReservationHostId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Reservation_ReservationId",
                table: "InvoiceLine");

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
                name: "FK_ProductOL_ReservationHost_ReservationHostId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_Reservation_ReservationId",
                table: "ProductOL");

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
                name: "FK_Reservation_User_FinalizedById",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_UserMember_PreferredUserId",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_User_FinalizedById",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Stock_StockId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageRate_Discount_DiscountId",
                table: "UsageRate");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_Branch_BranchId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Branch_BranchId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserPermissionSet_PermissionSetId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Branch_BranchId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Stock_StockId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Stock_TransferStockId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInbound_Inventory_InventoryId",
                table: "InventoryInbound");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Inventory_InventoryId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInbound_InventoryTransfer_InventoryTransferId",
                table: "InventoryInbound");

            migrationBuilder.DropTable(
                name: "AgeRestrictionLogin");

            migrationBuilder.DropTable(
                name: "AgeRestrictionProduct");

            migrationBuilder.DropTable(
                name: "AppExeBranch");

            migrationBuilder.DropTable(
                name: "ClientOptions");

            migrationBuilder.DropTable(
                name: "DiscountBranch");

            migrationBuilder.DropTable(
                name: "DiscountGroupDiscount");

            migrationBuilder.DropTable(
                name: "DiscountPeriodDayTime");

            migrationBuilder.DropTable(
                name: "FeedBranch");

            migrationBuilder.DropTable(
                name: "FileImage");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentEntry");

            migrationBuilder.DropTable(
                name: "InventoryDocument");

            migrationBuilder.DropTable(
                name: "InventoryInboundEntry");

            migrationBuilder.DropTable(
                name: "InvoiceLineReservationFee");

            migrationBuilder.DropTable(
                name: "NewsBranch");

            migrationBuilder.DropTable(
                name: "NotificationTimedRemaining");

            migrationBuilder.DropTable(
                name: "NotificationTimedReservation");

            migrationBuilder.DropTable(
                name: "PaymentReceipt");

            migrationBuilder.DropTable(
                name: "PresetReservationTime");

            migrationBuilder.DropTable(
                name: "PresetTopUp");

            migrationBuilder.DropTable(
                name: "ProductBranch");

            migrationBuilder.DropTable(
                name: "ProductOrderDiscount");

            migrationBuilder.DropTable(
                name: "PromotionBranch");

            migrationBuilder.DropTable(
                name: "PromotionDiscount");

            migrationBuilder.DropTable(
                name: "PromotionDiscountGroup");

            migrationBuilder.DropTable(
                name: "PromotionLimit");

            migrationBuilder.DropTable(
                name: "PromotionPeriodDayTime");

            migrationBuilder.DropTable(
                name: "RecipientChannel");

            migrationBuilder.DropTable(
                name: "RefundReceipt");

            migrationBuilder.DropTable(
                name: "ReservationProductOrder");

            migrationBuilder.DropTable(
                name: "ScheduleReportEntry");

            migrationBuilder.DropTable(
                name: "ScheduleReportRecipient");

            migrationBuilder.DropTable(
                name: "StockCountAdjustment");

            migrationBuilder.DropTable(
                name: "StockCountEntry");

            migrationBuilder.DropTable(
                name: "StockCountInbound");

            migrationBuilder.DropTable(
                name: "TargetBillProfile");

            migrationBuilder.DropTable(
                name: "TargetPaymentMethod");

            migrationBuilder.DropTable(
                name: "TargetProduct");

            migrationBuilder.DropTable(
                name: "TargetProductGroup");

            migrationBuilder.DropTable(
                name: "TargetProductTime");

            migrationBuilder.DropTable(
                name: "UserApiKey");

            migrationBuilder.DropTable(
                name: "UserChannel");

            migrationBuilder.DropTable(
                name: "UserOperatorBranch");

            migrationBuilder.DropTable(
                name: "UserPermissionSetPermission");

            migrationBuilder.DropTable(
                name: "AgeRestriction");

            migrationBuilder.DropTable(
                name: "DiscountPeriodDay");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentReason");

            migrationBuilder.DropTable(
                name: "FileDocument");

            migrationBuilder.DropTable(
                name: "InventoryTransferEntry");

            migrationBuilder.DropTable(
                name: "ProductOLReservationFee");

            migrationBuilder.DropTable(
                name: "NotificationTimed");

            migrationBuilder.DropTable(
                name: "PromotionCode");

            migrationBuilder.DropTable(
                name: "DiscountGroup");

            migrationBuilder.DropTable(
                name: "PromotionPeriodDay");

            migrationBuilder.DropTable(
                name: "ReportPreset");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "ScheduleReport");

            migrationBuilder.DropTable(
                name: "InventoryAdjustment");

            migrationBuilder.DropTable(
                name: "StockCount");

            migrationBuilder.DropTable(
                name: "TargetGroupBillProfile");

            migrationBuilder.DropTable(
                name: "TargetGroupPaymentMethod");

            migrationBuilder.DropTable(
                name: "TargetGroupProduct");

            migrationBuilder.DropTable(
                name: "TargetGroupProductGroup");

            migrationBuilder.DropTable(
                name: "TargetGroupProductTime");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.DropTable(
                name: "UserPermissionSet");

            migrationBuilder.DropTable(
                name: "DiscountPeriod");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "InventoryEntry");

            migrationBuilder.DropTable(
                name: "InventoryTransferReason");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PromotionPeriod");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "TargetGroup");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Companion");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "InventoryTransfer");

            migrationBuilder.DropTable(
                name: "InventoryInbound");

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
                name: "IX_User_PermissionSetId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_UsageSession_BranchId",
                table: "UsageSession");

            migrationBuilder.DropIndex(
                name: "IX_UsageRate_DiscountId",
                table: "UsageRate");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_StockId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Shift_BranchId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHost_FinalizedById",
                table: "ReservationHost");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHost_Status",
                table: "ReservationHost");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_FinalizedById",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_Status",
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
                name: "IX_ProductOL_ReservationHostId",
                table: "ProductOL");

            migrationBuilder.DropIndex(
                name: "IX_ProductOL_ReservationId",
                table: "ProductOL");

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
                name: "IX_InvoiceLine_ReservationHostId",
                table: "InvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLine_ReservationId",
                table: "InvoiceLine");

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
                name: "IX_HostGroup_BillProfileId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_ClientOptionsId",
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
                name: "PermissionSetId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PreferredChannel",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "UsageSession");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "UsageSession");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountCalculationType",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "DisableDesktopSwitching",
                table: "SecurityProfile");

            migrationBuilder.DropColumn(
                name: "DisableStartMenu",
                table: "SecurityProfile");

            migrationBuilder.DropColumn(
                name: "StickyShell",
                table: "SecurityProfile");

            migrationBuilder.DropColumn(
                name: "ActivationTime",
                table: "ReservationHost");

            migrationBuilder.DropColumn(
                name: "FinalizedById",
                table: "ReservationHost");

            migrationBuilder.DropColumn(
                name: "MovedToReservationHostId",
                table: "ReservationHost");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReservationHost");

            migrationBuilder.DropColumn(
                name: "ActivationTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CancellationGracePeriod",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CancellationRefundPercentage",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ExpireAfter",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "FinalizedById",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "LoginBlockAfterTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "LoginBlockBeforeTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
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
                name: "PrepareStatus",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "PrepareTime",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "PreparedQuantity",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "PrepareStatus",
                table: "ProductOL");

            migrationBuilder.DropColumn(
                name: "PrepareTime",
                table: "ProductOL");

            migrationBuilder.DropColumn(
                name: "PreparedQuantity",
                table: "ProductOL");

            migrationBuilder.DropColumn(
                name: "ReservationHostId",
                table: "ProductOL");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "ProductOL");

            migrationBuilder.DropColumn(
                name: "ReservationSlot",
                table: "ProductOL");

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
                name: "IsExpired",
                table: "InvoiceLineTime");

            migrationBuilder.DropColumn(
                name: "ReservationHostId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "ReservationSlot",
                table: "InvoiceLine");

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
                name: "BillProfileId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "ClientOptionsId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DepositPayment");

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

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_Pin",
                table: "Reservation",
                newName: "UQ_Pin");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTimePeriodDay_ProductTimePeriodId_Day",
                table: "ProductTimePeriodDay",
                newName: "UQ_ProductTimePeriodDay");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTax_ProductId_TaxId",
                table: "ProductTax",
                newName: "UQ_TaxProduct");

            migrationBuilder.RenameColumn(
                name: "PreferredPaymentMethodId",
                table: "ProductOrder",
                newName: "PreferedPaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_PreferredPaymentMethodId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_PreferedPaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Log_Time",
                table: "Log",
                newName: "IX_Time");

            migrationBuilder.RenameIndex(
                name: "IX_Log_MessageType",
                table: "Log",
                newName: "IX_MessageType");

            migrationBuilder.RenameIndex(
                name: "IX_Log_HostNumber",
                table: "Log",
                newName: "IX_HostNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Log_Category",
                table: "Log",
                newName: "IX_Category");

            migrationBuilder.RenameColumn(
                name: "OutstandingPoints",
                table: "Invoice",
                newName: "OutstandngPoints");

            migrationBuilder.RenameColumn(
                name: "HostGroupWaitingLineEntryId",
                table: "HostGroupWaitingLineEntry",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HostGroupId",
                table: "HostGroupWaitingLine",
                newName: "HosGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_HostGroupWaitingLine_HostGroupId",
                table: "HostGroupWaitingLine",
                newName: "IX_HostGroupWaitingLine_HosGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceHost_DeviceId_HostId",
                table: "DeviceHost",
                newName: "UQ_HostDevice");

            migrationBuilder.RenameIndex(
                name: "IX_AppExeMaxUser_AppExeId_Mode",
                table: "AppExeMaxUser",
                newName: "UQ_AppExeAppExeMode");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserOperator",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserOperator",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserMember",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserMember",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserGuest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", 0)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "StartFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "RatesTotal",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<double>(
                name: "NegativeSeconds",
                table: "UsageSession",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "UsageSession",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillProfileStamp",
                table: "UsageRate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "DepositPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "RegisterId",
                table: "DepositPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundedAmount",
                table: "DepositPayment",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "RefundStatus",
                table: "DepositPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<bool>(
                name: "IsVoided",
                table: "DepositPayment",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "FiscalReceiptStatus",
                table: "DepositPayment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "FiscalReceiptId",
                table: "DepositPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

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
                name: "FK_HostGroupWaitingLine_HostGroup_HosGroupId",
                table: "HostGroupWaitingLine",
                column: "HosGroupId",
                principalTable: "HostGroup",
                principalColumn: "HostGroupId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");
        }
    }
}
