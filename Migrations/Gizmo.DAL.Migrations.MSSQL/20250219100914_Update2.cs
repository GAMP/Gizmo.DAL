using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PreparedQuantity",
                table: "ProductOL",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 25)
                .OldAnnotation("Relational:ColumnOrder", 24);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PrepareTime",
                table: "ProductOL",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 26)
                .OldAnnotation("Relational:ColumnOrder", 25);

            migrationBuilder.AlterColumn<int>(
                name: "PrepareStatus",
                table: "ProductOL",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 24)
                .OldAnnotation("Relational:ColumnOrder", 23);

            migrationBuilder.AddColumn<int>(
                name: "ReservationSlot",
                table: "ProductOL",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "InvoiceLineTime",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReservationSlot",
                table: "InvoiceLine",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 24);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentReceipt");

            migrationBuilder.DropColumn(
                name: "ReservationSlot",
                table: "ProductOL");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "InvoiceLineTime");

            migrationBuilder.DropColumn(
                name: "ReservationSlot",
                table: "InvoiceLine");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreparedQuantity",
                table: "ProductOL",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 24)
                .OldAnnotation("Relational:ColumnOrder", 25);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PrepareTime",
                table: "ProductOL",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 25)
                .OldAnnotation("Relational:ColumnOrder", 26);

            migrationBuilder.AlterColumn<int>(
                name: "PrepareStatus",
                table: "ProductOL",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 23)
                .OldAnnotation("Relational:ColumnOrder", 24);
        }
    }
}
