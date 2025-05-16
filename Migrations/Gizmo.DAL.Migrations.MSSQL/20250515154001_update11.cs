using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class update11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipientChannel_RecipientChanneled_RecipientChanneledId",
                table: "RecipientChannel");

            migrationBuilder.DropForeignKey(
                name: "FK_Target_TargetGroup_TargetGroupId",
                table: "Target");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

            migrationBuilder.DropTable(
                name: "RecipientUser");

            migrationBuilder.DropTable(
                name: "ScheduleReportRecipient");

            migrationBuilder.DropTable(
                name: "RecipientChanneled");

            migrationBuilder.DropIndex(
                name: "IX_Target_TargetGroupId",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "TargetGroupId",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "Template",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "ApplyType",
                table: "DiscountBasic");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DiscountBasic");

            migrationBuilder.RenameColumn(
                name: "ReportParameters",
                table: "ScheduleReportEntry",
                newName: "Filters");

            migrationBuilder.RenameColumn(
                name: "RecipientChanneledId",
                table: "RecipientChannel",
                newName: "RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipientChannel_RecipientChanneledId_ChannelType",
                table: "RecipientChannel",
                newName: "IX_RecipientChannel_RecipientId_ChannelType");

            migrationBuilder.AlterColumn<decimal>(
                name: "StartFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "RatesTotal",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<double>(
                name: "NegativeSeconds",
                table: "UsageSession",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "UsageSession",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillProfileStamp",
                table: "UsageRate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "UsageRate",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "DiscountCalculationType",
                table: "UsageRate",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "UsageRate",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "UsageRate",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Recipient",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "PromotionDiscountGroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "PromotionDiscount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Promotion",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Promotion",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "CodeType",
                table: "Promotion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "Promotion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ApplyType",
                table: "DiscountTargeted",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "CalculationType",
                table: "DiscountTargeted",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "DiscountBasic",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.CreateTable(
                name: "ProductOrderDiscount",
                columns: table => new
                {
                    ProductOrderDiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderLineId = table.Column<int>(type: "int", nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    PromotionCodeId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    DiscountName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    ApplyType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDiscount", x => x.ProductOrderDiscountId);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_ProductOL_ProductOrderLineId",
                        column: x => x.ProductOrderLineId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "ProductOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_PromotionCode_PromotionCodeId",
                        column: x => x.PromotionCodeId,
                        principalTable: "PromotionCode",
                        principalColumn: "PromotionCodeId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsageRate_DiscountId",
                table: "UsageRate",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_CreatedById",
                table: "ProductOrderDiscount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_DiscountId",
                table: "ProductOrderDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ProductOrderId",
                table: "ProductOrderDiscount",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ProductOrderLineId",
                table: "ProductOrderDiscount",
                column: "ProductOrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_PromotionCodeId",
                table: "ProductOrderDiscount",
                column: "PromotionCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_PromotionId",
                table: "ProductOrderDiscount",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_RegisterId",
                table: "ProductOrderDiscount",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ShiftId",
                table: "ProductOrderDiscount",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_UserId",
                table: "ProductOrderDiscount",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientChannel_Recipient_RecipientId",
                table: "RecipientChannel",
                column: "RecipientId",
                principalTable: "Recipient",
                principalColumn: "RecipientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRate_Discount_DiscountId",
                table: "UsageRate",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipientChannel_Recipient_RecipientId",
                table: "RecipientChannel");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageRate_Discount_DiscountId",
                table: "UsageRate");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

            migrationBuilder.DropTable(
                name: "ProductOrderDiscount");

            migrationBuilder.DropIndex(
                name: "IX_UsageRate_DiscountId",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "UsageSession");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountCalculationType",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "UsageRate");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Recipient");

            migrationBuilder.DropColumn(
                name: "ApplyType",
                table: "DiscountTargeted");

            migrationBuilder.DropColumn(
                name: "CalculationType",
                table: "DiscountTargeted");

            migrationBuilder.RenameColumn(
                name: "Filters",
                table: "ScheduleReportEntry",
                newName: "ReportParameters");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "RecipientChannel",
                newName: "RecipientChanneledId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipientChannel_RecipientId_ChannelType",
                table: "RecipientChannel",
                newName: "IX_RecipientChannel_RecipientChanneledId_ChannelType");

            migrationBuilder.AlterColumn<decimal>(
                name: "StartFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "RatesTotal",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<double>(
                name: "NegativeSeconds",
                table: "UsageSession",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumFee",
                table: "UsageSession",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "UsageSession",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillProfileStamp",
                table: "UsageRate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "TargetGroupId",
                table: "Target",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "PromotionDiscountGroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "PromotionDiscount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Promotion",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Promotion",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "CodeType",
                table: "Promotion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "PromotionId",
                table: "Promotion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", 0)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "DiscountBasic",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "ApplyType",
                table: "DiscountBasic",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "DiscountBasic",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateTable(
                name: "RecipientChanneled",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientChanneled", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_RecipientChanneled_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReportRecipient",
                columns: table => new
                {
                    RecipientScheduleReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleReportId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReportRecipient", x => x.RecipientScheduleReportId);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_ScheduleReport_ScheduleReportId",
                        column: x => x.ScheduleReportId,
                        principalTable: "ScheduleReport",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RecipientUser",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientUser", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_RecipientUser_RecipientChanneled_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "RecipientChanneled",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Target_TargetGroupId",
                table: "Target",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientUser_UserId",
                table: "RecipientUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_CreatedById",
                table: "ScheduleReportRecipient",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_RecipientId",
                table: "ScheduleReportRecipient",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_ScheduleReportId",
                table: "ScheduleReportRecipient",
                column: "ScheduleReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientChannel_RecipientChanneled_RecipientChanneledId",
                table: "RecipientChannel",
                column: "RecipientChanneledId",
                principalTable: "RecipientChanneled",
                principalColumn: "RecipientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Target_TargetGroup_TargetGroupId",
                table: "Target",
                column: "TargetGroupId",
                principalTable: "TargetGroup",
                principalColumn: "TargetGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");
        }
    }
}
