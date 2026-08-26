using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QrDisplayNumber",
                table: "PaymentIntent",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 18);

            migrationBuilder.AddColumn<int>(
                name: "RegisterId",
                table: "PaymentIntent",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 16);

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "PaymentIntent",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 17);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_RegisterId",
                table: "PaymentIntent",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_ShiftId",
                table: "PaymentIntent",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_Register_RegisterId",
                table: "PaymentIntent",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_Shift_ShiftId",
                table: "PaymentIntent",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentIntent_Register_RegisterId",
                table: "PaymentIntent");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentIntent_Shift_ShiftId",
                table: "PaymentIntent");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntent_RegisterId",
                table: "PaymentIntent");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntent_ShiftId",
                table: "PaymentIntent");

            migrationBuilder.DropColumn(
                name: "QrDisplayNumber",
                table: "PaymentIntent");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "PaymentIntent");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "PaymentIntent");
        }
    }
}
