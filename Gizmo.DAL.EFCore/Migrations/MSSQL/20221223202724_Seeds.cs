using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    public partial class Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BillProfile",
                columns: new[] { "BillProfileId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Member Prices" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Guests Prices" }
                });

            migrationBuilder.InsertData(
                table: "HostLayoutGroup",
                columns: new[] { "HostLayoutGroupId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Name" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, "Default" });

            migrationBuilder.InsertData(
                table: "MonetaryUnit",
                columns: new[] { "MonetaryUnitId", "CreatedById", "CreatedTime", "DisplayOrder", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null, "1 Cent", 0.01m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "5 Cent", 0.05m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, null, "10 Cent", 0.10m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, null, "25 Cent", 0.25m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, false, null, null, "1 Dollar", 1.00m },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, null, null, "2 Dollar", 2.00m },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, false, null, null, "5 Dollar", 5.00m },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, false, null, null, "10 Dollar", 10.00m },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, false, null, null, "20 Dollar", 20.00m },
                    { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, false, null, null, "50 Dollar", 50.00m },
                    { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, false, null, null, "100 Dollar", 100.00m }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "PaymentMethodId", "CreatedById", "CreatedTime", "Description", "DisplayOrder", "IsClient", "IsDeleted", "IsEnabled", "IsManager", "IsPortal", "ModifiedById", "ModifiedTime", "Name", "Options", "PaymentProvider", "Surcharge" },
                values: new object[,]
                {
                    { -4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, false, true, true, false, null, null, "Points", 0, null, 0m },
                    { -3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, false, true, true, false, null, null, "Deposit", 0, null, 0m },
                    { -2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, false, true, true, false, null, null, "Credit Card", 0, null, 0m },
                    { -1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, true, false, true, true, false, null, null, "Cash", 0, null, 0m }
                });

            migrationBuilder.InsertData(
                table: "PresetTimeSale",
                columns: new[] { "PresetTimeSaleId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 1 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 5 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 15 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 30 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 60 }
                });

            migrationBuilder.InsertData(
                table: "PresetTimeSaleMoney",
                columns: new[] { "PresetTimeSaleMoneyId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 1m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 2m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 5m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 10m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 20m }
                });

            migrationBuilder.InsertData(
                table: "ProductGroup",
                columns: new[] { "ProductGroupId", "CreatedById", "CreatedTime", "DisplayOrder", "Guid", "ModifiedById", "ModifiedTime", "Name", "ParentId", "SortOption" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70271"), null, null, "Time Offers", null, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70272"), null, null, "Food", null, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70273"), null, null, "Drinks", null, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70274"), null, null, "Sweets", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tax",
                columns: new[] { "TaxId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Name", "UseOrder", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "24%", 0, 23m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "16%", 1, 16m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "None", 2, 0m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "BirthDate", "City", "Country", "CreatedById", "CreatedTime", "FirstName", "Guid", "Identification", "IsDeleted", "IsDisabled", "LastName", "MobilePhone", "ModifiedById", "ModifiedTime", "Phone", "PostCode", "Sex", "SmartCardUID" },
                values: new object[,]
                {
                    { 2, null, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("38753737-24f1-40d7-8ac4-ba61660d666a"), null, false, false, null, null, null, null, null, null, 0, null },
                    { 1, null, null, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("691ea8b4-d794-4096-84ae-bbdb7bcc0b02"), null, false, false, null, null, null, null, null, null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "BillRate",
                columns: new[] { "BillRateId", "BillProfileId", "ChargeAfter", "ChargeEvery", "IsDefault", "MinimumFee", "Options", "Rate", "StartFee" },
                values: new object[,]
                {
                    { 1, 1, 1, 5, true, 2m, 0, 2m, 1m },
                    { 2, 2, 1, 5, true, 2m, 0, 2m, 1m }
                });

            migrationBuilder.InsertData(
                table: "ProductBase",
                columns: new[] { "ProductId", "Barcode", "Cost", "CreatedById", "CreatedTime", "Description", "DisplayOrder", "Guid", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "OrderOptions", "Points", "PointsPrice", "Price", "ProductGroupId", "PurchaseOptions", "StockAlert", "StockOptions", "StockProductAmount", "StockProductId" },
                values: new object[,]
                {
                    { 1, null, 0.90m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba1"), false, null, null, "Mars Bar", 0, 10, null, 1.10m, 4, 0, 0m, 1, 0m, null },
                    { 2, null, 1.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba2"), false, null, null, "Snickers Bar", 0, 15, null, 2.0m, 4, 0, 0m, 1, 0m, null },
                    { 3, null, 2.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba3"), false, null, null, "Pizza (Small)", 0, null, null, 6.0m, 2, 0, 0m, 0, 0m, null },
                    { 4, null, 1.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba4"), false, null, null, "Coca Cola (Can)", 0, null, null, 2.0m, 3, 0, 0m, 0, 0m, null },
                    { 5, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba5"), false, null, null, "Pizza and Cola", 0, 200, null, 3.40m, 2, 0, 0m, 1, 0m, null },
                    { 6, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba6"), false, null, null, "Six Hours (6)", 0, null, null, 12m, 1, 0, 0m, 0, 0m, null },
                    { 7, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba7"), false, null, null, "Six Hours (6 Weekends)", 0, null, null, 16m, 1, 0, 0m, 0, 0m, null }
                });

            migrationBuilder.InsertData(
                table: "UserCredential",
                columns: new[] { "UserId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Password", "Salt" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new byte[] { 227, 190, 117, 189, 14, 131, 27, 251, 244, 196, 14, 55, 126, 183, 143, 152, 146, 122, 121, 195, 5, 57, 241, 24, 184, 41, 122, 231, 166, 174, 210, 155, 233, 8, 83, 128, 145, 208, 28, 139, 149, 46, 168, 246, 21, 38, 126, 197, 29, 147, 234, 81, 253, 218, 217, 136, 216, 237, 206, 196, 113, 231, 152, 52 }, new byte[] { 213, 217, 89, 164, 125, 194, 157, 170, 86, 35, 202, 5, 236, 165, 229, 151, 191, 209, 130, 41, 234, 120, 64, 104, 216, 200, 194, 9, 221, 163, 100, 236, 125, 143, 49, 114, 227, 161, 166, 20, 120, 7, 250, 81, 128, 236, 241, 116, 231, 235, 216, 208, 131, 155, 104, 218, 249, 75, 34, 190, 62, 160, 147, 82, 158, 78, 172, 74, 131, 17, 26, 236, 95, 7, 190, 245, 165, 235, 103, 17, 172, 55, 141, 182, 51, 96, 212, 209, 67, 164, 111, 234, 83, 101, 64, 224, 84, 84, 54, 4 } });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "UserGroupId", "AppGroupId", "BillProfileId", "BillingOptions", "CreatedById", "CreatedTime", "CreditLimit", "CreditLimitOptions", "Description", "IsAgeRatingEnabled", "IsDefault", "IsNegativeBalanceAllowed", "IsWaitingLinePriorityEnabled", "ModifiedById", "ModifiedTime", "Name", "Options", "Overrides", "Points", "PointsAwardOptions", "PointsMoneyRatio", "PointsTimeRatio", "RequiredUserInfo", "SecurityProfileId", "WaitingLinePriority" },
                values: new object[,]
                {
                    { 1, null, 1, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0, null, false, true, false, false, null, null, "Members", 0, 0, null, 0, 0m, 0, 0, null, 0 },
                    { 2, null, 2, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0, null, false, false, false, false, null, null, "Guests", 8, 0, null, 0, 0m, 0, 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserOperator",
                columns: new[] { "UserId", "Email", "ShiftOptions", "Username" },
                values: new object[] { 1, null, 0, "Admin" });

            migrationBuilder.InsertData(
                table: "UserPermission",
                columns: new[] { "UserPermissionId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "*" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "CustomPrice" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "NonDefaultVat" },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "PayLater" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidInvoices" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidUsedTimeInvoices" },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidClosedShiftInvoices" },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidOtherOperatorInvoices" },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidPastDaysInvoices" },
                    { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "Deposit" },
                    { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "Withdraw" },
                    { 12, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidDeposits" },
                    { 13, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ManualOpenCashDrawer" },
                    { 14, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ModifyBillingOptions" },
                    { 15, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "AllowTimeCredit" },
                    { 16, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewInvoices" },
                    { 17, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPaidInvoices" },
                    { 18, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysInvoices" },
                    { 19, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewDeposits" },
                    { 20, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysDeposits" },
                    { 21, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewRegisterTransactions" },
                    { 22, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysRegisterTransactions" },
                    { 23, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "DeleteTimePurchases" },
                    { 24, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Shift", 1, "ViewExpected" },
                    { 25, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "*" },
                    { 26, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "Manage" },
                    { 27, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "ViewStockTransactions" },
                    { 28, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "ViewPastDaysStockTransactions" },
                    { 29, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "*" }
                });

            migrationBuilder.InsertData(
                table: "UserPermission",
                columns: new[] { "UserPermissionId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { 30, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Tasks" },
                    { 31, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Processes" },
                    { 32, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Files" },
                    { 33, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Maintenance" },
                    { 34, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Security" },
                    { 35, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "LockState" },
                    { 36, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "ModuleRestart" },
                    { 37, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Deployment", 1, "*" },
                    { 38, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Monitoring", 1, "*" },
                    { 39, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Reports", 1, "*" },
                    { 40, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Settings", 1, "*" },
                    { 41, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Apps", 1, "*" },
                    { 42, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "News", 1, "*" },
                    { 43, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserPasswordReset" },
                    { 44, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserEnable" },
                    { 45, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserDisable" },
                    { 46, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserManualLogin" },
                    { 47, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Add" },
                    { 48, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Delete" },
                    { 49, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "ChangeUserName" },
                    { 50, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "ChangeUserGroup" },
                    { 51, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Edit" },
                    { 52, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "AccessStats" },
                    { 53, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Log", 1, "*" },
                    { 54, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Log", 1, "Clear" },
                    { 55, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WaitingLines", 1, "*" },
                    { 56, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WaitingLines", 1, "Manage" },
                    { 57, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "RegisterTransactions", 1, "RegisterTransactionsPayIn" },
                    { 58, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "RegisterTransactions", 1, "RegisterTransactionsPayOut" },
                    { 59, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WebApi", 1, "*" }
                });

            migrationBuilder.InsertData(
                table: "HostGroup",
                columns: new[] { "HostGroupId", "AppGroupId", "CreatedById", "CreatedTime", "DefaultGuestGroupId", "ModifiedById", "ModifiedTime", "Name", "Options", "SecurityProfileId", "SkinName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Computers", 0, null, null },
                    { 2, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Endpoints", 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductBaseExtended",
                column: "ProductId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5
                });

            migrationBuilder.InsertData(
                table: "ProductPeriod",
                columns: new[] { "ProductId", "EndDate", "Options", "StartDate" },
                values: new object[] { 7, null, 0, null });

            migrationBuilder.InsertData(
                table: "ProductTax",
                columns: new[] { "ProductTaxId", "CreatedById", "CreatedTime", "IsEnabled", "ModifiedById", "ModifiedTime", "ProductId", "TaxId", "UseOrder" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 1, 1, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 2, 1, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 3, 1, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 4, 1, 0 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 5, 1, 0 },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 6, 1, 0 },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 7, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductTime",
                columns: new[] { "ProductId", "AppGroupId", "ExpirationOptions", "ExpireAfterType", "ExpireAtDayTimeMinute", "ExpireFromOptions", "ExpiresAfter", "Minutes", "UsageOptions", "UseOrder", "WeekDayMaxMinutes", "WeekEndMaxMinutes" },
                values: new object[,]
                {
                    { 6, null, 0, 0, 0, 0, 0, 360, 0, 0, null, null },
                    { 7, null, 0, 0, 0, 0, 0, 360, 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserMember",
                columns: new[] { "UserId", "BillingOptions", "DisabledDate", "Email", "EnableDate", "IsNegativeBalanceAllowed", "IsPersonalInfoRequested", "UserGroupId", "Username" },
                values: new object[] { 2, null, null, null, null, null, false, 1, "User" });

            migrationBuilder.InsertData(
                table: "Host",
                columns: new[] { "HostId", "CreatedById", "CreatedTime", "Guid", "HostGroupId", "IconId", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "Number", "State" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803871"), 2, null, false, null, null, "XBOX-ONE-1", 1, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803872"), 2, null, false, null, null, "XBOX-ONE-2", 2, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803873"), 2, null, false, null, null, "PS4-1", 3, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803874"), 2, null, false, null, null, "WII-1", 4, 0 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                column: "ProductId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "ProductBundle",
                columns: new[] { "ProductId", "BundleStockOptions" },
                values: new object[] { 5, 0 });

            migrationBuilder.InsertData(
                table: "ProductPeriodDay",
                columns: new[] { "ProductPeriodDayId", "Day", "ProductPeriodId" },
                values: new object[,]
                {
                    { 7, 6, 7 },
                    { 8, 0, 7 }
                });

            migrationBuilder.InsertData(
                table: "ProductTimePeriod",
                columns: new[] { "ProductId", "EndDate", "Options", "StartDate" },
                values: new object[] { 6, null, 0, null });

            migrationBuilder.InsertData(
                table: "BundleProduct",
                columns: new[] { "BundleProductId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Options", "Price", "ProductBundleId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 0, 1m, 5, 3, 1m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 0, 2m, 5, 4, 1m }
                });

            migrationBuilder.InsertData(
                table: "HostEndpoint",
                columns: new[] { "HostId", "MaximumUsers" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BillRate",
                keyColumn: "BillRateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillRate",
                keyColumn: "BillRateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BundleProduct",
                keyColumn: "BundleProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BundleProduct",
                keyColumn: "BundleProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HostEndpoint",
                keyColumn: "HostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HostEndpoint",
                keyColumn: "HostId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HostEndpoint",
                keyColumn: "HostId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HostEndpoint",
                keyColumn: "HostId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HostGroup",
                keyColumn: "HostGroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HostLayoutGroup",
                keyColumn: "HostLayoutGroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MonetaryUnit",
                keyColumn: "MonetaryUnitId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PaymentMethod",
                keyColumn: "PaymentMethodId",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "PaymentMethod",
                keyColumn: "PaymentMethodId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "PaymentMethod",
                keyColumn: "PaymentMethodId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "PaymentMethod",
                keyColumn: "PaymentMethodId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "PresetTimeSale",
                keyColumn: "PresetTimeSaleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PresetTimeSale",
                keyColumn: "PresetTimeSaleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PresetTimeSale",
                keyColumn: "PresetTimeSaleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PresetTimeSale",
                keyColumn: "PresetTimeSaleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PresetTimeSale",
                keyColumn: "PresetTimeSaleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PresetTimeSaleMoney",
                keyColumn: "PresetTimeSaleMoneyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PresetTimeSaleMoney",
                keyColumn: "PresetTimeSaleMoneyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PresetTimeSaleMoney",
                keyColumn: "PresetTimeSaleMoneyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PresetTimeSaleMoney",
                keyColumn: "PresetTimeSaleMoneyId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PresetTimeSaleMoney",
                keyColumn: "PresetTimeSaleMoneyId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductPeriodDay",
                keyColumn: "ProductPeriodDayId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductPeriodDay",
                keyColumn: "ProductPeriodDayId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTax",
                keyColumn: "ProductTaxId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTime",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTimePeriod",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tax",
                keyColumn: "TaxId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tax",
                keyColumn: "TaxId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserCredential",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserMember",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserOperator",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "UserPermissionId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Host",
                keyColumn: "HostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Host",
                keyColumn: "HostId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Host",
                keyColumn: "HostId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Host",
                keyColumn: "HostId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductBaseExtended",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductBaseExtended",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductBaseExtended",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductBaseExtended",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductBundle",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductPeriod",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTime",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tax",
                keyColumn: "TaxId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "UserGroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillProfile",
                keyColumn: "BillProfileId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HostGroup",
                keyColumn: "HostGroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductBaseExtended",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductBase",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductGroup",
                keyColumn: "ProductGroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductGroup",
                keyColumn: "ProductGroupId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductGroup",
                keyColumn: "ProductGroupId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "UserGroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BillProfile",
                keyColumn: "BillProfileId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductGroup",
                keyColumn: "ProductGroupId",
                keyValue: 2);
        }
    }
}
