using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentIntentOrder_InvoicePayment_InvoicePaymentId",
                table: "PaymentIntentOrder");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntentOrder_InvoicePaymentId",
                table: "PaymentIntentOrder");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntentOrder_PaymentIntentId",
                table: "PaymentIntentOrder");

            migrationBuilder.DropColumn(
                name: "InvoicePaymentId",
                table: "PaymentIntentOrder");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "ProductOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductOrderId",
                table: "PaymentIntentOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<bool>(
                name: "AutoComplete",
                table: "PaymentIntentOrder",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<bool>(
                name: "DisableReceiptPrinting",
                table: "PaymentIntentOrder",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpireAt",
                table: "PaymentIntent",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "Expiration",
                table: "PaymentIntent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "PaymentIntent",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.CreateTable(
                name: "IntentOrder",
                columns: table => new
                {
                    IntentOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentIntentOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentOrder", x => x.IntentOrderId);
                    table.ForeignKey(
                        name: "FK_IntentOrder_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId");
                    table.ForeignKey(
                        name: "FK_IntentOrder_PaymentIntentOrder_PaymentIntentOrderId",
                        column: x => x.PaymentIntentOrderId,
                        principalTable: "PaymentIntentOrder",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntentOrder_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "ProductOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntentOrder_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_IntentOrder_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "IntentOrderDeposit",
                columns: table => new
                {
                    IntentOrderDepositId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentIntentOrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DepositPaymentId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentOrderDeposit", x => x.IntentOrderDepositId);
                    table.ForeignKey(
                        name: "FK_IntentOrderDeposit_PaymentIntentOrder_PaymentIntentOrderId",
                        column: x => x.PaymentIntentOrderId,
                        principalTable: "PaymentIntentOrder",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntentOrderDeposit_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntentOrderDeposit_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_IntentOrderDeposit_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RefundPayment",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false),
                    FiscalReceiptStatus = table.Column<int>(type: "int", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundPayment", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundPayment_FiscalReceipt_FiscalReceiptId",
                        column: x => x.FiscalReceiptId,
                        principalTable: "FiscalReceipt",
                        principalColumn: "FiscalReceiptId");
                    table.ForeignKey(
                        name: "FK_RefundPayment_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_BranchId",
                table: "ProductOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_PaymentId",
                table: "PaymentIntent",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_CreatedById",
                table: "IntentOrder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_InvoicePaymentId",
                table: "IntentOrder",
                column: "InvoicePaymentId",
                unique: true,
                filter: "[InvoicePaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_ModifiedById",
                table: "IntentOrder",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_PaymentIntentOrderId_ProductOrderId",
                table: "IntentOrder",
                columns: new[] { "PaymentIntentOrderId", "ProductOrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_ProductOrderId",
                table: "IntentOrder",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_CreatedById",
                table: "IntentOrderDeposit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_ModifiedById",
                table: "IntentOrderDeposit",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_PaymentIntentOrderId_UserId",
                table: "IntentOrderDeposit",
                columns: new[] { "PaymentIntentOrderId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_UserId",
                table: "IntentOrderDeposit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundPayment_FiscalReceiptId",
                table: "RefundPayment",
                column: "FiscalReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_Payment_PaymentId",
                table: "PaymentIntent",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Branch_BranchId",
                table: "ProductOrder",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentIntent_Payment_PaymentId",
                table: "PaymentIntent");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Branch_BranchId",
                table: "ProductOrder");

            migrationBuilder.DropTable(
                name: "IntentOrder");

            migrationBuilder.DropTable(
                name: "IntentOrderDeposit");

            migrationBuilder.DropTable(
                name: "RefundPayment");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_BranchId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntent_PaymentId",
                table: "PaymentIntent");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "AutoComplete",
                table: "PaymentIntentOrder");

            migrationBuilder.DropColumn(
                name: "DisableReceiptPrinting",
                table: "PaymentIntentOrder");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "PaymentIntent");

            migrationBuilder.AlterColumn<int>(
                name: "ProductOrderId",
                table: "PaymentIntentOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "InvoicePaymentId",
                table: "PaymentIntentOrder",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpireAt",
                table: "PaymentIntent",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "Expiration",
                table: "PaymentIntent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentOrder_InvoicePaymentId",
                table: "PaymentIntentOrder",
                column: "InvoicePaymentId",
                unique: true,
                filter: "[InvoicePaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentOrder_PaymentIntentId",
                table: "PaymentIntentOrder",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntentOrder_InvoicePayment_InvoicePaymentId",
                table: "PaymentIntentOrder",
                column: "InvoicePaymentId",
                principalTable: "InvoicePayment",
                principalColumn: "InvoicePaymentId");
        }
    }
}
