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
            migrationBuilder.RenameColumn(
                name: "PaymentLinkUrl",
                table: "PaymentIntent",
                newName: "PaymentUrl");

            migrationBuilder.AddColumn<int>(
                name: "Expiration",
                table: "PaymentIntent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireAt",
                table: "PaymentIntent",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "PaymentIntent");

            migrationBuilder.DropColumn(
                name: "ExpireAt",
                table: "PaymentIntent");

            migrationBuilder.RenameColumn(
                name: "PaymentUrl",
                table: "PaymentIntent",
                newName: "PaymentLinkUrl");
        }
    }
}
