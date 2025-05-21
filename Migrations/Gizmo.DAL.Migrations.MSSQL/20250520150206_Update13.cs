using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Requirement",
                table: "DiscountTargeted",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Discount",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 3);

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

            migrationBuilder.CreateIndex(
                name: "IX_TargetPaymentMethod_MethodId",
                table: "TargetPaymentMethod",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetPaymentMethod_TargetGroupPaymentMethodId_MethodId",
                table: "TargetPaymentMethod",
                columns: new[] { "TargetGroupPaymentMethodId", "MethodId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetPaymentMethod");

            migrationBuilder.DropTable(
                name: "TargetGroupPaymentMethod");

            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "DiscountTargeted");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Discount");
        }
    }
}
