using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessVATId",
                table: "Branch",
                newName: "BusinessVATID");

            migrationBuilder.RenameColumn(
                name: "DepositAdvancePaymentType",
                table: "Branch",
                newName: "ServicesVATRate");

            migrationBuilder.AddColumn<int>(
                name: "PaymentReversalStatus",
                table: "Refund",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "TreatDepositsAsService",
                table: "Branch",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 30)
                .OldAnnotation("Relational:ColumnOrder", 25);

            migrationBuilder.AlterColumn<decimal>(
                name: "TimeBasedServiceVATRate",
                table: "Branch",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 29)
                .OldAnnotation("Relational:ColumnOrder", 27);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "Branch",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 34)
                .OldAnnotation("Relational:ColumnOrder", 32);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Branch",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 36)
                .OldAnnotation("Relational:ColumnOrder", 34);

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "Branch",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 33)
                .OldAnnotation("Relational:ColumnOrder", 31);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisableTime",
                table: "Branch",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 35)
                .OldAnnotation("Relational:ColumnOrder", 33);

            migrationBuilder.AlterColumn<string>(
                name: "DepositServiceDescription",
                table: "Branch",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 31)
                .OldAnnotation("Relational:ColumnOrder", 26);

            migrationBuilder.AlterColumn<int>(
                name: "CompanionId",
                table: "Branch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 32)
                .OldAnnotation("Relational:ColumnOrder", 30);

            migrationBuilder.AlterColumn<int>(
                name: "ServicesVATRate",
                table: "Branch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 27)
                .OldAnnotation("Relational:ColumnOrder", 29);

            migrationBuilder.AddColumn<int>(
                name: "DepositTaxSystem",
                table: "Branch",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 25);

            migrationBuilder.AddColumn<int>(
                name: "GoodsVATRate",
                table: "Branch",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 26);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentReversalStatus",
                table: "Refund");

            migrationBuilder.DropColumn(
                name: "DepositTaxSystem",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "GoodsVATRate",
                table: "Branch");

            migrationBuilder.RenameColumn(
                name: "BusinessVATID",
                table: "Branch",
                newName: "BusinessVATId");

            migrationBuilder.RenameColumn(
                name: "ServicesVATRate",
                table: "Branch",
                newName: "DepositAdvancePaymentType");

            migrationBuilder.AlterColumn<bool>(
                name: "TreatDepositsAsService",
                table: "Branch",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 25)
                .OldAnnotation("Relational:ColumnOrder", 30);

            migrationBuilder.AlterColumn<decimal>(
                name: "TimeBasedServiceVATRate",
                table: "Branch",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 27)
                .OldAnnotation("Relational:ColumnOrder", 29);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "Branch",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 32)
                .OldAnnotation("Relational:ColumnOrder", 34);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Branch",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 34)
                .OldAnnotation("Relational:ColumnOrder", 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "Branch",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 31)
                .OldAnnotation("Relational:ColumnOrder", 33);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisableTime",
                table: "Branch",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 33)
                .OldAnnotation("Relational:ColumnOrder", 35);

            migrationBuilder.AlterColumn<string>(
                name: "DepositServiceDescription",
                table: "Branch",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 26)
                .OldAnnotation("Relational:ColumnOrder", 31);

            migrationBuilder.AlterColumn<int>(
                name: "CompanionId",
                table: "Branch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 30)
                .OldAnnotation("Relational:ColumnOrder", 32);

            migrationBuilder.AlterColumn<int>(
                name: "DepositAdvancePaymentType",
                table: "Branch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 29)
                .OldAnnotation("Relational:ColumnOrder", 27);
        }
    }
}
