using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountGroupDiscount_UserOperator_CreatedById",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountPeriod_DiscountPeriodic_DiscountId",
                table: "DiscountPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetGroup_DiscountTargeted_DiscountId",
                table: "TargetGroup");

            migrationBuilder.DropTable(
                name: "DiscountBasic");

            migrationBuilder.DropTable(
                name: "DiscountBonus");

            migrationBuilder.DropTable(
                name: "DiscountBonusFlat");

            migrationBuilder.DropTable(
                name: "DiscountTargeted");

            migrationBuilder.DropTable(
                name: "DiscountPeriodic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountGroupDiscount",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropIndex(
                name: "IX_DiscountGroupDiscount_CreatedById",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropIndex(
                name: "IX_DiscountGroupDiscount_DiscountGroupId_DiscountId",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropColumn(
                name: "DiscountGroupDiscountId",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "DiscountGroupDiscount");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "DiscountGroupDiscount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountGroupId",
                table: "DiscountGroupDiscount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Discount",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<int>(
                name: "ApplyType",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<int>(
                name: "CalculationType",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Discount",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "Requirement",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "RewardType",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Discount",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountGroupDiscount",
                table: "DiscountGroupDiscount",
                columns: new[] { "DiscountGroupId", "DiscountId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountPeriod_Discount_DiscountId",
                table: "DiscountPeriod",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetGroup_Discount_DiscountId",
                table: "TargetGroup",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountPeriod_Discount_DiscountId",
                table: "DiscountPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetGroup_Discount_DiscountId",
                table: "TargetGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountGroupDiscount",
                table: "DiscountGroupDiscount");

            migrationBuilder.DropColumn(
                name: "ApplyType",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "CalculationType",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "RewardType",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Discount");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "DiscountGroupDiscount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountGroupId",
                table: "DiscountGroupDiscount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountGroupDiscountId",
                table: "DiscountGroupDiscount",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "DiscountGroupDiscount",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "DiscountGroupDiscount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Discount",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountGroupDiscount",
                table: "DiscountGroupDiscount",
                column: "DiscountGroupDiscountId");

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
                name: "DiscountTargeted",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    ApplyType = table.Column<int>(type: "int", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    Requirement = table.Column<int>(type: "int", nullable: false)
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
                name: "DiscountBasic",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroupDiscount_CreatedById",
                table: "DiscountGroupDiscount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroupDiscount_DiscountGroupId_DiscountId",
                table: "DiscountGroupDiscount",
                columns: new[] { "DiscountGroupId", "DiscountId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountGroupDiscount_UserOperator_CreatedById",
                table: "DiscountGroupDiscount",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountPeriod_DiscountPeriodic_DiscountId",
                table: "DiscountPeriod",
                column: "DiscountId",
                principalTable: "DiscountPeriodic",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetGroup_DiscountTargeted_DiscountId",
                table: "TargetGroup",
                column: "DiscountId",
                principalTable: "DiscountTargeted",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
