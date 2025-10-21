using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisableReceiptPrinting",
                table: "PaymentIntentOrder");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentUrl",
                table: "PaymentIntent",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisableReceiptPrinting",
                table: "PaymentIntent",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.CreateTable(
                name: "IntentInvoice",
                columns: table => new
                {
                    IntentInvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentIntentOrderId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentInvoice", x => x.IntentInvoiceId);
                    table.ForeignKey(
                        name: "FK_IntentInvoice_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId");
                    table.ForeignKey(
                        name: "FK_IntentInvoice_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntentInvoice_PaymentIntentOrder_PaymentIntentOrderId",
                        column: x => x.PaymentIntentOrderId,
                        principalTable: "PaymentIntentOrder",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntentInvoice_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_IntentInvoice_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntentInvoice_CreatedById",
                table: "IntentInvoice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentInvoice_InvoiceId",
                table: "IntentInvoice",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_IntentInvoice_InvoicePaymentId",
                table: "IntentInvoice",
                column: "InvoicePaymentId",
                unique: true,
                filter: "[InvoicePaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntentInvoice_ModifiedById",
                table: "IntentInvoice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentInvoice_PaymentIntentOrderId_InvoiceId",
                table: "IntentInvoice",
                columns: new[] { "PaymentIntentOrderId", "InvoiceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntentInvoice");

            migrationBuilder.DropColumn(
                name: "DisableReceiptPrinting",
                table: "PaymentIntent");

            migrationBuilder.AddColumn<bool>(
                name: "DisableReceiptPrinting",
                table: "PaymentIntentOrder",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentUrl",
                table: "PaymentIntent",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}
