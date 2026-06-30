using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_HostId",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_ProductId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PointTransaction_UserId",
                table: "PointTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DepositTransaction_UserId",
                table: "DepositTransaction");

            migrationBuilder.DropIndex(
                name: "IX_AppStat_UserId",
                table: "AppStat");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_HostId_State",
                table: "UserSession",
                columns: new[] { "HostId", "State" })
                .Annotation("Npgsql:IndexInclude", new[] { "UserId", "CreatedTime", "Span" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_NotEnded",
                table: "UserSession",
                column: "State",
                filter: "\"State\" <> 2")
                .Annotation("Npgsql:IndexInclude", new[] { "UserId", "HostId", "Slot" });

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_IsActive",
                table: "UsageSession",
                column: "IsActive",
                filter: "\"IsActive\" = true")
                .Annotation("Npgsql:IndexInclude", new[] { "UserId", "NegativeSeconds", "RatesTotal", "CurrentUsageId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_CreatedTime_ProductId",
                table: "StockTransaction",
                columns: new[] { "CreatedTime", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_ProductId_StockId_StockTransactionId",
                table: "StockTransaction",
                columns: new[] { "ProductId", "StockId", "StockTransactionId" },
                descending: new[] { false, false, true })
                .Annotation("Npgsql:IndexInclude", new[] { "OnHand" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Status_ActivationTime_ExpireAfter",
                table: "Reservation",
                columns: new[] { "Status", "ActivationTime", "ExpireAfter" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_CreatedTime_Status",
                table: "ProductOrder",
                columns: new[] { "CreatedTime", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_UserId_PointTransactionId",
                table: "PointTransaction",
                columns: new[] { "UserId", "PointTransactionId" },
                descending: new[] { false, true })
                .Annotation("Npgsql:IndexInclude", new[] { "Balance" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_CreatedTime_CreatedById_RegisterId",
                table: "InvoicePayment",
                columns: new[] { "CreatedTime", "CreatedById", "RegisterId" })
                .Annotation("Npgsql:IndexInclude", new[] { "Amount", "RefundStatus", "RefundedAmount" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CreatedTime_CreatedById_RegisterId",
                table: "Invoice",
                columns: new[] { "CreatedTime", "CreatedById", "RegisterId" })
                .Annotation("Npgsql:IndexInclude", new[] { "Total", "Outstanding", "IsVoided", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ReturnFiscalReceiptStatus_Pending",
                table: "Invoice",
                column: "ReturnFiscalReceiptStatus",
                filter: "\"ReturnFiscalReceiptStatus\" IN (2, 5) AND \"IsVoided\" = true AND \"Status\" = 2 AND \"Total\" > 0")
                .Annotation("Npgsql:IndexInclude", new[] { "RegisterId" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SaleFiscalReceiptStatus_Pending",
                table: "Invoice",
                column: "SaleFiscalReceiptStatus",
                filter: "\"SaleFiscalReceiptStatus\" IN (2, 5) AND \"IsVoided\" = false AND \"Status\" = 2 AND \"Total\" > 0")
                .Annotation("Npgsql:IndexInclude", new[] { "RegisterId" });

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_Created_Voided_Register",
                table: "DepositTransaction",
                columns: new[] { "CreatedTime", "IsVoided", "CreatedById", "RegisterId" })
                .Annotation("Npgsql:IndexInclude", new[] { "Type", "Amount", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_UserId_DepositTransactionId",
                table: "DepositTransaction",
                columns: new[] { "UserId", "DepositTransactionId" },
                descending: new[] { false, true })
                .Annotation("Npgsql:IndexInclude", new[] { "Balance" });

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_CreatedTime",
                table: "DepositPayment",
                column: "CreatedTime")
                .Annotation("Npgsql:IndexInclude", new[] { "Amount", "UserId", "CreatedById", "ShiftId", "RegisterId" });

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_FiscalReceiptStatus_Pending",
                table: "DepositPayment",
                column: "FiscalReceiptStatus",
                filter: "\"FiscalReceiptStatus\" IN (2, 5) AND \"FiscalReceiptId\" IS NULL AND \"IsVoided\" = false")
                .Annotation("Npgsql:IndexInclude", new[] { "RegisterId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_StartTime",
                table: "AppStat",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_UserId_AppId",
                table: "AppStat",
                columns: new[] { "UserId", "AppId" })
                .Annotation("Npgsql:IndexInclude", new[] { "Span" });

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_HostId_State",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_NotEnded",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_UsageSession_IsActive",
                table: "UsageSession");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_CreatedTime_ProductId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_ProductId_StockId_StockTransactionId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_Status_ActivationTime_ExpireAfter",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_CreatedTime_Status",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_PointTransaction_UserId_PointTransactionId",
                table: "PointTransaction");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayment_CreatedTime_CreatedById_RegisterId",
                table: "InvoicePayment");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CreatedTime_CreatedById_RegisterId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ReturnFiscalReceiptStatus_Pending",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_SaleFiscalReceiptStatus_Pending",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_DepositTransaction_Created_Voided_Register",
                table: "DepositTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DepositTransaction_UserId_DepositTransactionId",
                table: "DepositTransaction");

            migrationBuilder.DropIndex(
                name: "IX_DepositPayment_CreatedTime",
                table: "DepositPayment");

            migrationBuilder.DropIndex(
                name: "IX_DepositPayment_FiscalReceiptStatus_Pending",
                table: "DepositPayment");

            migrationBuilder.DropIndex(
                name: "IX_AppStat_StartTime",
                table: "AppStat");

            migrationBuilder.DropIndex(
                name: "IX_AppStat_UserId_AppId",
                table: "AppStat");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_HostId",
                table: "UserSession",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_ProductId",
                table: "StockTransaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_UserId",
                table: "PointTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_UserId",
                table: "DepositTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_UserId",
                table: "AppStat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
