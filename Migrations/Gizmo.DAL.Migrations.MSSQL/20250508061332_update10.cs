using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class update10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovedToReservationHostId",
                table: "ReservationHost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationTime",
                table: "Reservation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "InventoryInboundEntry",
                type: "datetime2",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost",
                column: "MovedToReservationHostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost",
                column: "MovedToReservationHostId",
                principalTable: "ReservationHost",
                principalColumn: "ReservationHostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost");

            migrationBuilder.DropColumn(
                name: "MovedToReservationHostId",
                table: "ReservationHost");

            migrationBuilder.DropColumn(
                name: "ActivationTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "InventoryInboundEntry");
        }
    }
}
