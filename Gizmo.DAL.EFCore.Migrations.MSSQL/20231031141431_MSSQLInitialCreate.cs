using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _
{
    /// <inheritdoc />
    public partial class MSSQLInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostNumber = table.Column<int>(type: "int", nullable: true),
                    Hostname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ModuleType = table.Column<int>(type: "int", nullable: false),
                    ModuleVersion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "LogException",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false),
                    ExceptionData = table.Column<byte[]>(type: "varbinary(max)", maxLength: 65535, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogException", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_LogException_Log_LogId",
                        column: x => x.LogId,
                        principalTable: "Log",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    DeveloperId = table.Column<int>(type: "int", nullable: true),
                    AppCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false),
                    AgeRating = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultExecutableId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "AppCategory",
                columns: table => new
                {
                    AppCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategory", x => x.AppCategoryId);
                    table.ForeignKey(
                        name: "FK_AppCategory_AppCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppCategory",
                        principalColumn: "AppCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "AppEnterprise",
                columns: table => new
                {
                    AppEnterpriseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEnterprise", x => x.AppEnterpriseId);
                });

            migrationBuilder.CreateTable(
                name: "AppExe",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExecutablePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Arguments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkingDirectory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Modes = table.Column<int>(type: "int", nullable: false),
                    RunMode = table.Column<int>(type: "int", nullable: false),
                    DefaultDeploymentId = table.Column<int>(type: "int", nullable: true),
                    ReservationType = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Accessible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExe", x => x.AppExeId);
                    table.ForeignKey(
                        name: "FK_AppExe_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppExeCdImage",
                columns: table => new
                {
                    AppExeCdImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MountOptions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    CheckExitCode = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeCdImage", x => x.AppExeCdImageId);
                    table.ForeignKey(
                        name: "FK_AppExeCdImage_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeDeployment",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    DeploymentId = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeDeployment", x => new { x.AppExeId, x.DeploymentId });
                    table.ForeignKey(
                        name: "FK_AppExeDeployment_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeImage",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 16777215, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeImage", x => x.AppExeId);
                    table.ForeignKey(
                        name: "FK_AppExeImage_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeLicense",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeLicense", x => new { x.AppExeId, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_AppExeLicense_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeMaxUser",
                columns: table => new
                {
                    AppExeMaxUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeMaxUser", x => x.AppExeMaxUserId);
                    table.ForeignKey(
                        name: "FK_AppExeMaxUser_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExePersonalFile",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    PersonalFileId = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExePersonalFile", x => new { x.AppExeId, x.PersonalFileId });
                    table.ForeignKey(
                        name: "FK_AppExePersonalFile_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeTask",
                columns: table => new
                {
                    AppExeTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activation = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    TaskBaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeTask", x => x.AppExeTaskId);
                    table.ForeignKey(
                        name: "FK_AppExeTask_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppGroup",
                columns: table => new
                {
                    AppGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGroup", x => x.AppGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AppGroupApp",
                columns: table => new
                {
                    AppGroupId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGroupApp", x => new { x.AppGroupId, x.AppId });
                    table.ForeignKey(
                        name: "FK_AppGroupApp_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppGroupApp_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppImage",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 16777215, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImage", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_AppImage_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppLink",
                columns: table => new
                {
                    AppLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLink", x => x.AppLinkId);
                    table.ForeignKey(
                        name: "FK_AppLink_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRating",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRating", x => new { x.AppId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AppRating_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppStat",
                columns: table => new
                {
                    AppStatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    AppExeId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Span = table.Column<double>(type: "float", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStat", x => x.AppStatId);
                    table.ForeignKey(
                        name: "FK_AppStat_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppStat_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetTypeId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SmartCardUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "AssetTransaction",
                columns: table => new
                {
                    AssetTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetTypeId = table.Column<int>(type: "int", nullable: false),
                    AssetTypeName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CheckedInById = table.Column<int>(type: "int", nullable: true),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTransaction", x => x.AssetTransactionId);
                    table.ForeignKey(
                        name: "FK_AssetTransaction_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    AssetTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.AssetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AssistanceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssistanceRequestTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssistanceRequestType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceRequestType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.AttributeId);
                });

            migrationBuilder.CreateTable(
                name: "BillProfile",
                columns: table => new
                {
                    BillProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillProfile", x => x.BillProfileId);
                });

            migrationBuilder.CreateTable(
                name: "BillRate",
                columns: table => new
                {
                    BillRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillProfileId = table.Column<int>(type: "int", nullable: false),
                    StartFee = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    MinimumFee = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    ChargeEvery = table.Column<int>(type: "int", nullable: false),
                    ChargeAfter = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRate", x => x.BillRateId);
                    table.ForeignKey(
                        name: "FK_BillRate_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillRatePeriodDay",
                columns: table => new
                {
                    BillRatePeriodDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillRateId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRatePeriodDay", x => x.BillRatePeriodDayId);
                    table.ForeignKey(
                        name: "FK_BillRatePeriodDay_BillRate_BillRateId",
                        column: x => x.BillRateId,
                        principalTable: "BillRate",
                        principalColumn: "BillRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillRateStep",
                columns: table => new
                {
                    BillRateStepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillRateId = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Charge = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    TargetMinute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRateStep", x => x.BillRateStepId);
                    table.ForeignKey(
                        name: "FK_BillRateStep_BillRate_BillRateId",
                        column: x => x.BillRateId,
                        principalTable: "BillRate",
                        principalColumn: "BillRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillRatePeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "int", nullable: false),
                    EndSecond = table.Column<int>(type: "int", nullable: false),
                    PeriodDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRatePeriodDayTime", x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_BillRatePeriodDayTime_BillRatePeriodDay_PeriodDayId",
                        column: x => x.PeriodDayId,
                        principalTable: "BillRatePeriodDay",
                        principalColumn: "BillRatePeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BundleProduct",
                columns: table => new
                {
                    BundleProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBundleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleProduct", x => x.BundleProductId);
                });

            migrationBuilder.CreateTable(
                name: "BundleProductUserPrice",
                columns: table => new
                {
                    BundleProductUserPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    BundleProductId = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleProductUserPrice", x => x.BundleProductUserPriceId);
                    table.ForeignKey(
                        name: "FK_BundleProductUserPrice_BundleProduct_BundleProductId",
                        column: x => x.BundleProductId,
                        principalTable: "BundleProduct",
                        principalColumn: "BundleProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientTask",
                columns: table => new
                {
                    ClientTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activation = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    TaskBaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTask", x => x.ClientTaskId);
                });

            migrationBuilder.CreateTable(
                name: "Deployment",
                columns: table => new
                {
                    DeploymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExcludeDirectories = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    ExcludeFiles = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    IncludeDirectories = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    IncludeFiles = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    RegistryString = table.Column<string>(type: "nvarchar(max)", maxLength: 16777215, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComparisonLevel = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deployment", x => x.DeploymentId);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentDeployment",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentDeployment", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_DeploymentDeployment_Deployment_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Deployment",
                        principalColumn: "DeploymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeploymentDeployment_Deployment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Deployment",
                        principalColumn: "DeploymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositPayment",
                columns: table => new
                {
                    DepositPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositTransactionId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "int", nullable: false),
                    FiscalReceiptStatus = table.Column<int>(type: "int", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "int", nullable: true),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositPayment", x => x.DepositPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "DepositTransaction",
                columns: table => new
                {
                    DepositTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositTransaction", x => x.DepositTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHdmi",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHdmi", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_DeviceHdmi_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHost",
                columns: table => new
                {
                    DeviceHostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHost", x => x.DeviceHostId);
                    table.ForeignKey(
                        name: "FK_DeviceHost_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Maximum = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.FeedId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalReceipt",
                columns: table => new
                {
                    FiscalReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TaxSystem = table.Column<int>(type: "int", nullable: true),
                    DocumentNumber = table.Column<int>(type: "int", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalReceipt", x => x.FiscalReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "Host",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    HostGroupId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Host", x => x.HostId);
                });

            migrationBuilder.CreateTable(
                name: "HostComputer",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "int", nullable: false),
                    Hostname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MACAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostComputer", x => x.HostId);
                    table.ForeignKey(
                        name: "FK_HostComputer_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostEndpoint",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "int", nullable: false),
                    MaximumUsers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostEndpoint", x => x.HostId);
                    table.ForeignKey(
                        name: "FK_HostEndpoint_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroup",
                columns: table => new
                {
                    HostGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    AppGroupId = table.Column<int>(type: "int", nullable: true),
                    SecurityProfileId = table.Column<int>(type: "int", nullable: true),
                    SkinName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false),
                    DefaultGuestGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroup", x => x.HostGroupId);
                    table.ForeignKey(
                        name: "FK_HostGroup_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId");
                });

            migrationBuilder.CreateTable(
                name: "HostGroupUserBillProfile",
                columns: table => new
                {
                    HostGroupUserBillProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillProfileId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    HostGroupId = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupUserBillProfile", x => x.HostGroupUserBillProfileId);
                    table.ForeignKey(
                        name: "FK_HostGroupUserBillProfile_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HostGroupUserBillProfile_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroupWaitingLine",
                columns: table => new
                {
                    HosGroupId = table.Column<int>(type: "int", nullable: false),
                    TimeOutOptions = table.Column<int>(type: "int", nullable: false),
                    EnablePriorities = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupWaitingLine", x => x.HosGroupId);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLine_HostGroup_HosGroupId",
                        column: x => x.HosGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroupWaitingLineEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsManualPosition = table.Column<bool>(type: "bit", nullable: false),
                    TimeInLine = table.Column<double>(type: "float", nullable: false),
                    ReadyTime = table.Column<double>(type: "float", nullable: false),
                    IsReadyTimedOut = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupWaitingLineEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLineEntry_HostGroupWaitingLine_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroupWaitingLine",
                        principalColumn: "HosGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLineEntry_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroup",
                columns: table => new
                {
                    HostLayoutGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroup", x => x.HostLayoutGroupId);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroupImage",
                columns: table => new
                {
                    HostLayoutGroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 16777215, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroupImage", x => x.HostLayoutGroupId);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupImage_HostLayoutGroup_HostLayoutGroupId",
                        column: x => x.HostLayoutGroupId,
                        principalTable: "HostLayoutGroup",
                        principalColumn: "HostLayoutGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroupLayout",
                columns: table => new
                {
                    HostLayoutGroupLayoutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostLayoutGroupId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroupLayout", x => x.HostLayoutGroupLayoutId);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupLayout_HostLayoutGroup_HostLayoutGroupId",
                        column: x => x.HostLayoutGroupId,
                        principalTable: "HostLayoutGroup",
                        principalColumn: "HostLayoutGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupLayout_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    IconId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 16777215, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.IconId);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "int", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Outstanding = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    OutstandngPoints = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    SaleFiscalReceiptStatus = table.Column<int>(type: "int", nullable: false),
                    ReturnFiscalReceiptStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceFiscalReceipt",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "int", nullable: false),
                    InvoiceFiscalReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFiscalReceipt", x => x.InvoiceFiscalReceiptId);
                    table.ForeignKey(
                        name: "FK_InvoiceFiscalReceipt_FiscalReceipt_FiscalReceiptId",
                        column: x => x.FiscalReceiptId,
                        principalTable: "FiscalReceipt",
                        principalColumn: "FiscalReceiptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceFiscalReceipt_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitListPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPointsPrice = table.Column<int>(type: "int", nullable: false),
                    UnitPointsListPrice = table.Column<int>(type: "int", nullable: true),
                    UnitCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PreTaxTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    PointsAward = table.Column<int>(type: "int", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PayType = table.Column<int>(type: "int", nullable: false),
                    PointsTransactionId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineExtended",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false),
                    BundleLineId = table.Column<int>(type: "int", nullable: true),
                    StockTransactionId = table.Column<int>(type: "int", nullable: true),
                    StockReturnTransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineExtended", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineExtended_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineProduct",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineProduct", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineProduct_InvoiceLineExtended_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineExtended",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineSession",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    UsageSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineSession", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineSession_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineTime",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    ProductTimeId = table.Column<int>(type: "int", nullable: false),
                    IsDepleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineTime", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineTime_InvoiceLineExtended_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineExtended",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineTimeFixed",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    IsDepleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineTimeFixed", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineTimeFixed_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayment",
                columns: table => new
                {
                    InvoicePaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => x.InvoicePaymentId);
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Assembly = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Plugin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Settings = table.Column<byte[]>(type: "varbinary(max)", maxLength: 65535, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.LicenseId);
                });

            migrationBuilder.CreateTable(
                name: "LicenseKey",
                columns: table => new
                {
                    LicenseKeyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", maxLength: 65535, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AssignedHostId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseKey", x => x.LicenseKeyId);
                    table.ForeignKey(
                        name: "FK_LicenseKey_HostComputer_AssignedHostId",
                        column: x => x.AssignedHostId,
                        principalTable: "HostComputer",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_LicenseKey_License_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "License",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mapping",
                columns: table => new
                {
                    MappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MountPoint = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapping", x => x.MappingId);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryUnit",
                columns: table => new
                {
                    MonetaryUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryUnit", x => x.MonetaryUnitId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BackgroundUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Options = table.Column<int>(type: "int", nullable: false),
                    Sevirity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 16777215, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    AmountReceived = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    DepositTransactionId = table.Column<int>(type: "int", nullable: true),
                    PointTransactionId = table.Column<int>(type: "int", nullable: true),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_DepositTransaction_DepositTransactionId",
                        column: x => x.DepositTransactionId,
                        principalTable: "DepositTransaction",
                        principalColumn: "DepositTransactionId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentIntent",
                columns: table => new
                {
                    PaymentIntentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TransactionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntent", x => x.PaymentIntentId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentIntentDeposit",
                columns: table => new
                {
                    PaymentIntentId = table.Column<int>(type: "int", nullable: false),
                    DepositPaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntentDeposit", x => x.PaymentIntentId);
                    table.ForeignKey(
                        name: "FK_PaymentIntentDeposit_DepositPayment_DepositPaymentId",
                        column: x => x.DepositPaymentId,
                        principalTable: "DepositPayment",
                        principalColumn: "DepositPaymentId");
                    table.ForeignKey(
                        name: "FK_PaymentIntentDeposit_PaymentIntent_PaymentIntentId",
                        column: x => x.PaymentIntentId,
                        principalTable: "PaymentIntent",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentIntentOrder",
                columns: table => new
                {
                    PaymentIntentId = table.Column<int>(type: "int", nullable: false),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntentOrder", x => x.PaymentIntentId);
                    table.ForeignKey(
                        name: "FK_PaymentIntentOrder_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId");
                    table.ForeignKey(
                        name: "FK_PaymentIntentOrder_PaymentIntent_PaymentIntentId",
                        column: x => x.PaymentIntentId,
                        principalTable: "PaymentIntent",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Surcharge = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    IsClient = table.Column<bool>(type: "bit", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    IsPortal = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PaymentProvider = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalFile",
                columns: table => new
                {
                    PersonalFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Activation = table.Column<int>(type: "int", nullable: false),
                    Deactivation = table.Column<int>(type: "int", nullable: false),
                    MaxQuota = table.Column<int>(type: "int", nullable: false),
                    CompressionLevel = table.Column<int>(type: "int", nullable: false),
                    ExcludeDirectories = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    ExcludeFiles = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    IncludeDirectories = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    IncludeFiles = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    Accessible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalFile", x => x.PersonalFileId);
                });

            migrationBuilder.CreateTable(
                name: "PluginLibrary",
                columns: table => new
                {
                    PluginLibraryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Scope = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginLibrary", x => x.PluginLibraryId);
                });

            migrationBuilder.CreateTable(
                name: "PointTransaction",
                columns: table => new
                {
                    PointTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointTransaction", x => x.PointTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "PresetTimeSale",
                columns: table => new
                {
                    PresetTimeSaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetTimeSale", x => x.PresetTimeSaleId);
                });

            migrationBuilder.CreateTable(
                name: "PresetTimeSaleMoney",
                columns: table => new
                {
                    PresetTimeSaleMoneyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetTimeSaleMoney", x => x.PresetTimeSaleMoneyId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductBase",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    PointsPrice = table.Column<int>(type: "int", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrderOptions = table.Column<int>(type: "int", nullable: false),
                    PurchaseOptions = table.Column<int>(type: "int", nullable: false),
                    StockOptions = table.Column<int>(type: "int", nullable: false),
                    StockAlert = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    StockProductId = table.Column<int>(type: "int", nullable: true),
                    StockProductAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBase", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBase_ProductBase_StockProductId",
                        column: x => x.StockProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductBaseExtended",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBaseExtended", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBaseExtended_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPeriod",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPeriod", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductPeriod_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTime",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    WeekDayMaxMinutes = table.Column<int>(type: "int", nullable: true),
                    WeekEndMaxMinutes = table.Column<int>(type: "int", nullable: true),
                    AppGroupId = table.Column<int>(type: "int", nullable: true),
                    ExpiresAfter = table.Column<int>(type: "int", nullable: false),
                    ExpirationOptions = table.Column<int>(type: "int", nullable: false),
                    ExpireFromOptions = table.Column<int>(type: "int", nullable: false),
                    UsageOptions = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    ExpireAfterType = table.Column<int>(type: "int", nullable: false),
                    ExpireAtDayTimeMinute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTime", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductTime_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId");
                    table.ForeignKey(
                        name: "FK_ProductTime_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundle",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BundleStockOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundle", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBundle_ProductBaseExtended_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBaseExtended",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPeriodDay",
                columns: table => new
                {
                    ProductPeriodDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPeriodId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPeriodDay", x => x.ProductPeriodDayId);
                    table.ForeignKey(
                        name: "FK_ProductPeriodDay_ProductPeriod_ProductPeriodId",
                        column: x => x.ProductPeriodId,
                        principalTable: "ProductPeriod",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimePeriod",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimePeriod", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductTimePeriod_ProductTime_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "int", nullable: false),
                    EndSecond = table.Column<int>(type: "int", nullable: false),
                    PeriodDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPeriodDayTime", x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_ProductPeriodDayTime_ProductPeriodDay_PeriodDayId",
                        column: x => x.PeriodDayId,
                        principalTable: "ProductPeriodDay",
                        principalColumn: "ProductPeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimePeriodDay",
                columns: table => new
                {
                    ProductTimePeriodDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTimePeriodId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimePeriodDay", x => x.ProductTimePeriodDayId);
                    table.ForeignKey(
                        name: "FK_ProductTimePeriodDay_ProductTimePeriod_ProductTimePeriodId",
                        column: x => x.ProductTimePeriodId,
                        principalTable: "ProductTimePeriod",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimePeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "int", nullable: false),
                    EndSecond = table.Column<int>(type: "int", nullable: false),
                    PeriodDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimePeriodDayTime", x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_ProductTimePeriodDayTime_ProductTimePeriodDay_PeriodDayId",
                        column: x => x.PeriodDayId,
                        principalTable: "ProductTimePeriodDay",
                        principalColumn: "ProductTimePeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundleUserPrice",
                columns: table => new
                {
                    ProductBundleUserPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBundleId = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    PointsPrice = table.Column<int>(type: "int", nullable: true),
                    PurchaseOptions = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundleUserPrice", x => x.ProductBundleUserPriceId);
                    table.ForeignKey(
                        name: "FK_ProductBundleUserPrice_ProductBundle_ProductBundleId",
                        column: x => x.ProductBundleId,
                        principalTable: "ProductBundle",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    ProductGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    SortOption = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.ProductGroupId);
                    table.ForeignKey(
                        name: "FK_ProductGroup_ProductGroup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductGroup",
                        principalColumn: "ProductGroupId");
                });

            migrationBuilder.CreateTable(
                name: "ProductHostHidden",
                columns: table => new
                {
                    ProductHostHiddenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    HostGroupId = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHostHidden", x => x.ProductHostHiddenId);
                    table.ForeignKey(
                        name: "FK_ProductHostHidden_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHostHidden_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ProductImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 16777215, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImage_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOL",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitListPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPointsPrice = table.Column<int>(type: "int", nullable: false),
                    UnitPointsListPrice = table.Column<int>(type: "int", nullable: true),
                    UnitCost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PreTaxTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    PointsAward = table.Column<int>(type: "int", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PayType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    DeliveredQuantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DeliveredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOL", x => x.ProductOLId);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLTimeFixed",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLTimeFixed", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLTimeFixed_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLExtended",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false),
                    BundleLineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLExtended", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLExtended_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLProduct",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLProduct", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLProduct_ProductBaseExtended_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBaseExtended",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ProductOLProduct_ProductOLExtended_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOLExtended",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLTime",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false),
                    ProductTimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLTime", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLTime_ProductOLExtended_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOLExtended",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOLTime_ProductTime_ProductTimeId",
                        column: x => x.ProductTimeId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductOLSession",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "int", nullable: false),
                    UsageSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLSession", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLSession_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    ProductOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    PreferedPaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    DeliveredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Source = table.Column<int>(type: "int", nullable: false),
                    UserNote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.ProductOrderId);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_ProductOrder_PaymentMethod_PreferedPaymentMethodId",
                        column: x => x.PreferedPaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId");
                });

            migrationBuilder.CreateTable(
                name: "ProductTax",
                columns: table => new
                {
                    ProductTaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTax", x => x.ProductTaxId);
                    table.ForeignKey(
                        name: "FK_ProductTax_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimeHostDisallowed",
                columns: table => new
                {
                    ProductTimeHostDisallowedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTimeId = table.Column<int>(type: "int", nullable: false),
                    HostGroupId = table.Column<int>(type: "int", nullable: false),
                    IsDisallowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimeHostDisallowed", x => x.ProductTimeHostDisallowedId);
                    table.ForeignKey(
                        name: "FK_ProductTimeHostDisallowed_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTimeHostDisallowed_ProductTime_ProductTimeId",
                        column: x => x.ProductTimeId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductUserDisallowed",
                columns: table => new
                {
                    ProductUserDisallowedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    IsDisallowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserDisallowed", x => x.ProductUserDisallowedId);
                    table.ForeignKey(
                        name: "FK_ProductUserDisallowed_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductUserPrice",
                columns: table => new
                {
                    ProductUserPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    PointsPrice = table.Column<int>(type: "int", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserPrice", x => x.ProductUserPriceId);
                    table.ForeignKey(
                        name: "FK_ProductUserPrice_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    DepositTransactionId = table.Column<int>(type: "int", nullable: true),
                    PointTransactionId = table.Column<int>(type: "int", nullable: true),
                    RefundMethodId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_Refund_DepositTransaction_DepositTransactionId",
                        column: x => x.DepositTransactionId,
                        principalTable: "DepositTransaction",
                        principalColumn: "DepositTransactionId");
                    table.ForeignKey(
                        name: "FK_Refund_PaymentMethod_RefundMethodId",
                        column: x => x.RefundMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refund_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_Refund_PointTransaction_PointTransactionId",
                        column: x => x.PointTransactionId,
                        principalTable: "PointTransaction",
                        principalColumn: "PointTransactionId");
                });

            migrationBuilder.CreateTable(
                name: "RefundDepositPayment",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false),
                    DepositPaymentId = table.Column<int>(type: "int", nullable: true),
                    FiscalReceiptStatus = table.Column<int>(type: "int", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundDepositPayment", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundDepositPayment_DepositPayment_DepositPaymentId",
                        column: x => x.DepositPaymentId,
                        principalTable: "DepositPayment",
                        principalColumn: "DepositPaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundDepositPayment_FiscalReceipt_FiscalReceiptId",
                        column: x => x.FiscalReceiptId,
                        principalTable: "FiscalReceipt",
                        principalColumn: "FiscalReceiptId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundDepositPayment_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundInvoicePayment",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundInvoicePayment", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    RegisterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    MacAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartCash = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IdleTimeout = table.Column<int>(type: "int", nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.RegisterId);
                });

            migrationBuilder.CreateTable(
                name: "RegisterTransaction",
                columns: table => new
                {
                    RegisterTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterTransaction", x => x.RegisterTransactionId);
                    table.ForeignKey(
                        name: "FK_RegisterTransaction_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHost",
                columns: table => new
                {
                    ReservationHostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    PreferedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHost", x => x.ReservationHostId);
                    table.ForeignKey(
                        name: "FK_ReservationHost_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHost_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationUser",
                columns: table => new
                {
                    ReservationUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationUser", x => x.ReservationUserId);
                    table.ForeignKey(
                        name: "FK_ReservationUser_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityProfile",
                columns: table => new
                {
                    SecurityProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    DisabledDrives = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProfile", x => x.SecurityProfileId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityProfilePolicy",
                columns: table => new
                {
                    SecurityProfilePolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityProfileId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProfilePolicy", x => x.SecurityProfilePolicyId);
                    table.ForeignKey(
                        name: "FK_SecurityProfilePolicy_SecurityProfile_SecurityProfileId",
                        column: x => x.SecurityProfileId,
                        principalTable: "SecurityProfile",
                        principalColumn: "SecurityProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityProfileRestriction",
                columns: table => new
                {
                    SecurityProfileRestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityProfileId = table.Column<int>(type: "int", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProfileRestriction", x => x.SecurityProfileRestrictionId);
                    table.ForeignKey(
                        name: "FK_SecurityProfileRestriction_SecurityProfile_SecurityProfileId",
                        column: x => x.SecurityProfileId,
                        principalTable: "SecurityProfile",
                        principalColumn: "SecurityProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    RegisterId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartCash = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsEnding = table.Column<bool>(type: "bit", nullable: false),
                    EndedById = table.Column<int>(type: "int", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shift_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftCount",
                columns: table => new
                {
                    ShiftCountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    StartCash = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Sales = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Deposits = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PayIns = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Withdrawals = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PayOuts = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Refunds = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Voids = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Expected = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Actual = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftCount", x => x.ShiftCountId);
                    table.ForeignKey(
                        name: "FK_ShiftCount_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftCount_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransaction",
                columns: table => new
                {
                    StockTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SourceProductId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    OnHand = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    SourceProductAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    SourceProductOnHand = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: true),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.StockTransactionId);
                    table.ForeignKey(
                        name: "FK_StockTransaction_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransaction_ProductBase_SourceProductId",
                        column: x => x.SourceProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "TaskBase",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBase", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "TaskJunction",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    SourceDirectory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DestinationDirectory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskJunction", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskJunction_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskNotification",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: false),
                    NotificationOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNotification", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskNotification_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskProcess",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Arguments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkingDirectory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ProcessOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProcess", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskProcess_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskScript",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ScriptType = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: false),
                    ProcessOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskScript", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskScript_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    TaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ConfirmationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "Usage",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsageSessionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<double>(type: "float", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usage", x => x.UsageId);
                });

            migrationBuilder.CreateTable(
                name: "UsageRate",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false),
                    BillRateId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    BillProfileStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRate", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageRate_BillRate_BillRateId",
                        column: x => x.BillRateId,
                        principalTable: "BillRate",
                        principalColumn: "BillRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsageSession",
                columns: table => new
                {
                    UsageSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CurrentUsageId = table.Column<int>(type: "int", nullable: true),
                    CurrentSecond = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NegativeSeconds = table.Column<double>(type: "float", nullable: false),
                    StartFee = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    MinimumFee = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    RatesTotal = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageSession", x => x.UsageSessionId);
                    table.ForeignKey(
                        name: "FK_UsageSession_Usage_CurrentUsageId",
                        column: x => x.CurrentUsageId,
                        principalTable: "Usage",
                        principalColumn: "UsageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsageTime",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false),
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageTime", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageTime_InvoiceLineTime_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineTime",
                        principalColumn: "InvoiceLineId");
                });

            migrationBuilder.CreateTable(
                name: "UsageTimeFixed",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false),
                    InvoiceLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageTimeFixed", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageTimeFixed_InvoiceLineTimeFixed_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineTimeFixed",
                        principalColumn: "InvoiceLineId");
                });

            migrationBuilder.CreateTable(
                name: "UsageUserSession",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false),
                    UserSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageUserSession", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageUserSession_Usage_UsageId",
                        column: x => x.UsageId,
                        principalTable: "Usage",
                        principalColumn: "UsageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SmartCardUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserOperator",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    ShiftOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperator", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserOperator_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPicture",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", maxLength: 16777215, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPicture", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserPicture_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPicture_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPicture_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_Verification_Token_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Token",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserAgreement",
                columns: table => new
                {
                    UserAgreementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Agreement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    DisplayOptions = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgreement", x => x.UserAgreementId);
                    table.ForeignKey(
                        name: "FK_UserAgreement_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAgreement_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserAttribute",
                columns: table => new
                {
                    UserAttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAttribute", x => x.UserAttributeId);
                    table.ForeignKey(
                        name: "FK_UserAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAttribute_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAttribute_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAttribute_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredential",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<byte[]>(type: "binary(64)", fixedLength: true, maxLength: 64, nullable: true),
                    Salt = table.Column<byte[]>(type: "binary(100)", fixedLength: true, maxLength: 100, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredential", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCredential_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserCredential_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserCredential_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AppGroupId = table.Column<int>(type: "int", nullable: true),
                    SecurityProfileId = table.Column<int>(type: "int", nullable: true),
                    BillProfileId = table.Column<int>(type: "int", nullable: true),
                    RequiredUserInfo = table.Column<int>(type: "int", nullable: false),
                    Overrides = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreditLimitOptions = table.Column<int>(type: "int", nullable: false),
                    IsNegativeBalanceAllowed = table.Column<bool>(type: "bit", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsAwardOptions = table.Column<int>(type: "int", nullable: false),
                    PointsMoneyRatio = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTimeRatio = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsAgeRatingEnabled = table.Column<bool>(type: "bit", nullable: false),
                    BillingOptions = table.Column<int>(type: "int", nullable: false),
                    WaitingLinePriority = table.Column<int>(type: "int", nullable: false),
                    IsWaitingLinePriorityEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UserGroupId);
                    table.ForeignKey(
                        name: "FK_UserGroup_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId");
                    table.ForeignKey(
                        name: "FK_UserGroup_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId");
                    table.ForeignKey(
                        name: "FK_UserGroup_SecurityProfile_SecurityProfileId",
                        column: x => x.SecurityProfileId,
                        principalTable: "SecurityProfile",
                        principalColumn: "SecurityProfileId");
                    table.ForeignKey(
                        name: "FK_UserGroup_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserGroup_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    UserPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => x.UserPermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermission_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermission_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variable",
                columns: table => new
                {
                    VariableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: false),
                    Scope = table.Column<int>(type: "int", nullable: false),
                    UseOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variable", x => x.VariableId);
                    table.ForeignKey(
                        name: "FK_Variable_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Variable_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Void",
                columns: table => new
                {
                    VoidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Void", x => x.VoidId);
                    table.ForeignKey(
                        name: "FK_Void_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_Void_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_Void_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "VerificationEmail",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationEmail", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_VerificationEmail_Verification_VerificationId",
                        column: x => x.VerificationId,
                        principalTable: "Verification",
                        principalColumn: "VerificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerificationMobilePhone",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationMobilePhone", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_VerificationMobilePhone_Verification_VerificationId",
                        column: x => x.VerificationId,
                        principalTable: "Verification",
                        principalColumn: "VerificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAgreementState",
                columns: table => new
                {
                    UserAgreementStateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAgreementId = table.Column<int>(type: "int", nullable: false),
                    AcceptState = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgreementState", x => x.UserAgreementStateId);
                    table.ForeignKey(
                        name: "FK_UserAgreementState_UserAgreement_UserAgreementId",
                        column: x => x.UserAgreementId,
                        principalTable: "UserAgreement",
                        principalColumn: "UserAgreementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAgreementState_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAgreementState_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAgreementState_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupHostDisallowed",
                columns: table => new
                {
                    UserGroupHostDisallowedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    HostGroupId = table.Column<int>(type: "int", nullable: false),
                    IsDisallowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupHostDisallowed", x => x.UserGroupHostDisallowedId);
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId");
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId");
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserMember",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    IsNegativeBalanceAllowed = table.Column<bool>(type: "bit", nullable: true),
                    IsPersonalInfoRequested = table.Column<bool>(type: "bit", nullable: false),
                    BillingOptions = table.Column<int>(type: "int", nullable: true),
                    EnableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisabledDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMember", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserMember_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoidDepositPayment",
                columns: table => new
                {
                    VoidId = table.Column<int>(type: "int", nullable: false),
                    DepositPaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoidDepositPayment", x => x.VoidId);
                    table.ForeignKey(
                        name: "FK_VoidDepositPayment_DepositPayment_DepositPaymentId",
                        column: x => x.DepositPaymentId,
                        principalTable: "DepositPayment",
                        principalColumn: "DepositPaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoidDepositPayment_Void_VoidId",
                        column: x => x.VoidId,
                        principalTable: "Void",
                        principalColumn: "VoidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoidInvoice",
                columns: table => new
                {
                    VoidId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoidInvoice", x => x.VoidId);
                    table.ForeignKey(
                        name: "FK_VoidInvoice_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoidInvoice_Void_VoidId",
                        column: x => x.VoidId,
                        principalTable: "Void",
                        principalColumn: "VoidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCreditLimit",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCreditLimit", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCreditLimit_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCreditLimit_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserCreditLimit_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserGuest",
                columns: table => new
                {
                    IsJoined = table.Column<bool>(type: "bit", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    ReservedHostId = table.Column<int>(type: "int", nullable: true),
                    ReservedSlot = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGuest", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserGuest_Host_ReservedHostId",
                        column: x => x.ReservedHostId,
                        principalTable: "Host",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_UserGuest_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserNoteOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNote", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_UserNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNote_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    UserSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Span = table.Column<double>(type: "float", nullable: false),
                    BilledSpan = table.Column<double>(type: "float", nullable: false),
                    PendTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PendSpan = table.Column<double>(type: "float", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PendSpanTotal = table.Column<double>(type: "float", nullable: false),
                    PauseSpan = table.Column<double>(type: "float", nullable: false),
                    PauseSpanTotal = table.Column<double>(type: "float", nullable: false),
                    GraceTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GraceSpan = table.Column<double>(type: "float", nullable: false),
                    GraceSpanTotal = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.UserSessionId);
                    table.ForeignKey(
                        name: "FK_UserSession_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSession_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserSession_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserSessionChange",
                columns: table => new
                {
                    UserSessionChangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSessionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Span = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessionChange", x => x.UserSessionChangeId);
                    table.ForeignKey(
                        name: "FK_UserSessionChange_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSessionChange_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserSessionChange_UserSession_UserSessionId",
                        column: x => x.UserSessionId,
                        principalTable: "UserSession",
                        principalColumn: "UserSessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSessionChange_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "BillProfile",
                columns: new[] { "BillProfileId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Member Prices" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Guests Prices" }
                });

            migrationBuilder.InsertData(
                table: "HostLayoutGroup",
                columns: new[] { "HostLayoutGroupId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Name" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, "Default" });

            migrationBuilder.InsertData(
                table: "MonetaryUnit",
                columns: new[] { "MonetaryUnitId", "CreatedById", "CreatedTime", "DisplayOrder", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null, "1 Cent", 0.01m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "5 Cent", 0.05m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, null, "10 Cent", 0.10m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, null, "25 Cent", 0.25m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, false, null, null, "1 Dollar", 1.00m },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, null, null, "2 Dollar", 2.00m },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, false, null, null, "5 Dollar", 5.00m },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, false, null, null, "10 Dollar", 10.00m },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, false, null, null, "20 Dollar", 20.00m },
                    { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, false, null, null, "50 Dollar", 50.00m },
                    { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, false, null, null, "100 Dollar", 100.00m }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "PaymentMethodId", "CreatedById", "CreatedTime", "Description", "DisplayOrder", "IsClient", "IsDeleted", "IsEnabled", "IsManager", "IsPortal", "ModifiedById", "ModifiedTime", "Name", "Options", "PaymentProvider", "Surcharge" },
                values: new object[,]
                {
                    { -4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, false, true, true, false, null, null, "Points", 0, null, 0m },
                    { -3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, false, true, true, false, null, null, "Deposit", 0, null, 0m },
                    { -2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, false, true, true, false, null, null, "Credit Card", 0, null, 0m },
                    { -1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, true, false, true, true, false, null, null, "Cash", 0, null, 0m }
                });

            migrationBuilder.InsertData(
                table: "PresetTimeSale",
                columns: new[] { "PresetTimeSaleId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 1 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 5 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 15 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 30 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 60 }
                });

            migrationBuilder.InsertData(
                table: "PresetTimeSaleMoney",
                columns: new[] { "PresetTimeSaleMoneyId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 1m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 2m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 5m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 10m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 20m }
                });

            migrationBuilder.InsertData(
                table: "ProductGroup",
                columns: new[] { "ProductGroupId", "CreatedById", "CreatedTime", "DisplayOrder", "Guid", "ModifiedById", "ModifiedTime", "Name", "ParentId", "SortOption" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70271"), null, null, "Time Offers", null, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70272"), null, null, "Food", null, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70273"), null, null, "Drinks", null, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70274"), null, null, "Sweets", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tax",
                columns: new[] { "TaxId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Name", "UseOrder", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "24%", 0, 23m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "16%", 1, 16m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "None", 2, 0m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "BirthDate", "City", "Country", "CreatedById", "CreatedTime", "FirstName", "Guid", "Identification", "IsDeleted", "IsDisabled", "LastName", "MobilePhone", "ModifiedById", "ModifiedTime", "Phone", "PostCode", "Sex", "SmartCardUID" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("691ea8b4-d794-4096-84ae-bbdb7bcc0b02"), null, false, false, null, null, null, null, null, null, 0, null },
                    { 2, null, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("38753737-24f1-40d7-8ac4-ba61660d666a"), null, false, false, null, null, null, null, null, null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "BillRate",
                columns: new[] { "BillRateId", "BillProfileId", "ChargeAfter", "ChargeEvery", "IsDefault", "MinimumFee", "Options", "Rate", "StartFee" },
                values: new object[,]
                {
                    { 1, 1, 1, 5, true, 2m, 0, 2m, 1m },
                    { 2, 2, 1, 5, true, 2m, 0, 2m, 1m }
                });

            migrationBuilder.InsertData(
                table: "ProductBase",
                columns: new[] { "ProductId", "Barcode", "Cost", "CreatedById", "CreatedTime", "Description", "DisplayOrder", "Guid", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "OrderOptions", "Points", "PointsPrice", "Price", "ProductGroupId", "PurchaseOptions", "StockAlert", "StockOptions", "StockProductAmount", "StockProductId" },
                values: new object[,]
                {
                    { 1, null, 0.90m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba1"), false, null, null, "Mars Bar", 0, 10, null, 1.10m, 4, 0, 0m, 1, 0m, null },
                    { 2, null, 1.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba2"), false, null, null, "Snickers Bar", 0, 15, null, 2.0m, 4, 0, 0m, 1, 0m, null },
                    { 3, null, 2.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba3"), false, null, null, "Pizza (Small)", 0, null, null, 6.0m, 2, 0, 0m, 0, 0m, null },
                    { 4, null, 1.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba4"), false, null, null, "Coca Cola (Can)", 0, null, null, 2.0m, 3, 0, 0m, 0, 0m, null },
                    { 5, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba5"), false, null, null, "Pizza and Cola", 0, 200, null, 3.40m, 2, 0, 0m, 1, 0m, null },
                    { 6, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba6"), false, null, null, "Six Hours (6)", 0, null, null, 12m, 1, 0, 0m, 0, 0m, null },
                    { 7, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba7"), false, null, null, "Six Hours (6 Weekends)", 0, null, null, 16m, 1, 0, 0m, 0, 0m, null }
                });

            migrationBuilder.InsertData(
                table: "UserCredential",
                columns: new[] { "UserId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Password", "Salt" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new byte[] { 227, 190, 117, 189, 14, 131, 27, 251, 244, 196, 14, 55, 126, 183, 143, 152, 146, 122, 121, 195, 5, 57, 241, 24, 184, 41, 122, 231, 166, 174, 210, 155, 233, 8, 83, 128, 145, 208, 28, 139, 149, 46, 168, 246, 21, 38, 126, 197, 29, 147, 234, 81, 253, 218, 217, 136, 216, 237, 206, 196, 113, 231, 152, 52 }, new byte[] { 213, 217, 89, 164, 125, 194, 157, 170, 86, 35, 202, 5, 236, 165, 229, 151, 191, 209, 130, 41, 234, 120, 64, 104, 216, 200, 194, 9, 221, 163, 100, 236, 125, 143, 49, 114, 227, 161, 166, 20, 120, 7, 250, 81, 128, 236, 241, 116, 231, 235, 216, 208, 131, 155, 104, 218, 249, 75, 34, 190, 62, 160, 147, 82, 158, 78, 172, 74, 131, 17, 26, 236, 95, 7, 190, 245, 165, 235, 103, 17, 172, 55, 141, 182, 51, 96, 212, 209, 67, 164, 111, 234, 83, 101, 64, 224, 84, 84, 54, 4 } });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "UserGroupId", "AppGroupId", "BillProfileId", "BillingOptions", "CreatedById", "CreatedTime", "CreditLimit", "CreditLimitOptions", "Description", "IsAgeRatingEnabled", "IsDefault", "IsNegativeBalanceAllowed", "IsWaitingLinePriorityEnabled", "ModifiedById", "ModifiedTime", "Name", "Options", "Overrides", "Points", "PointsAwardOptions", "PointsMoneyRatio", "PointsTimeRatio", "RequiredUserInfo", "SecurityProfileId", "WaitingLinePriority" },
                values: new object[,]
                {
                    { 1, null, 1, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0, null, false, true, false, false, null, null, "Members", 0, 0, null, 0, 0m, 0, 0, null, 0 },
                    { 2, null, 2, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0, null, false, false, false, false, null, null, "Guests", 8, 0, null, 0, 0m, 0, 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserOperator",
                columns: new[] { "UserId", "Email", "ShiftOptions", "Username" },
                values: new object[] { 1, null, 0, "Admin" });

            migrationBuilder.InsertData(
                table: "UserPermission",
                columns: new[] { "UserPermissionId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "*" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "CustomPrice" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "NonDefaultVat" },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "PayLater" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidInvoices" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidUsedTimeInvoices" },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidClosedShiftInvoices" },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidOtherOperatorInvoices" },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidPastDaysInvoices" },
                    { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "Deposit" },
                    { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "Withdraw" },
                    { 12, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidDeposits" },
                    { 13, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ManualOpenCashDrawer" },
                    { 14, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ModifyBillingOptions" },
                    { 15, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "AllowTimeCredit" },
                    { 16, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "AllowDisableReceiptPrint" },
                    { 17, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewInvoices" },
                    { 18, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPaidInvoices" },
                    { 19, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysInvoices" },
                    { 20, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewDeposits" },
                    { 21, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysDeposits" },
                    { 22, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewRegisterTransactions" },
                    { 23, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysRegisterTransactions" },
                    { 24, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "DeleteTimePurchases" },
                    { 25, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Shift", 1, "ViewExpected" },
                    { 26, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "*" },
                    { 27, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "Manage" },
                    { 28, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "ViewStockTransactions" },
                    { 29, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "ViewPastDaysStockTransactions" },
                    { 30, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "*" },
                    { 31, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Tasks" },
                    { 32, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Processes" },
                    { 33, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Files" },
                    { 34, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Maintenance" },
                    { 35, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Security" },
                    { 36, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "LockState" },
                    { 37, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "ModuleRestart" },
                    { 38, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "PowerOnEndpoints" },
                    { 39, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Deployment", 1, "*" },
                    { 40, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Monitoring", 1, "*" },
                    { 41, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Reports", 1, "*" },
                    { 42, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Settings", 1, "*" },
                    { 43, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Apps", 1, "*" },
                    { 44, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "News", 1, "*" },
                    { 45, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserPasswordReset" },
                    { 46, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserEnable" },
                    { 47, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserDisable" },
                    { 48, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserManualLogin" },
                    { 49, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Add" },
                    { 50, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Delete" },
                    { 51, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "ChangeUserName" },
                    { 52, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "ChangeUserGroup" },
                    { 53, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Edit" },
                    { 54, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "AccessStats" },
                    { 55, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Log", 1, "*" },
                    { 56, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Log", 1, "Clear" },
                    { 57, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WaitingLines", 1, "*" },
                    { 58, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WaitingLines", 1, "Manage" },
                    { 59, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "RegisterTransactions", 1, "RegisterTransactionsPayIn" },
                    { 60, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "RegisterTransactions", 1, "RegisterTransactionsPayOut" },
                    { 61, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WebApi", 1, "*" }
                });

            migrationBuilder.InsertData(
                table: "HostGroup",
                columns: new[] { "HostGroupId", "AppGroupId", "CreatedById", "CreatedTime", "DefaultGuestGroupId", "ModifiedById", "ModifiedTime", "Name", "Options", "SecurityProfileId", "SkinName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Computers", 0, null, null },
                    { 2, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Endpoints", 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductBaseExtended",
                column: "ProductId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5
                });

            migrationBuilder.InsertData(
                table: "ProductPeriod",
                columns: new[] { "ProductId", "EndDate", "Options", "StartDate" },
                values: new object[] { 7, null, 0, null });

            migrationBuilder.InsertData(
                table: "ProductTax",
                columns: new[] { "ProductTaxId", "CreatedById", "CreatedTime", "IsEnabled", "ModifiedById", "ModifiedTime", "ProductId", "TaxId", "UseOrder" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 1, 1, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 2, 1, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 3, 1, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 4, 1, 0 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 5, 1, 0 },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 6, 1, 0 },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 7, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductTime",
                columns: new[] { "ProductId", "AppGroupId", "ExpirationOptions", "ExpireAfterType", "ExpireAtDayTimeMinute", "ExpireFromOptions", "ExpiresAfter", "Minutes", "UsageOptions", "UseOrder", "WeekDayMaxMinutes", "WeekEndMaxMinutes" },
                values: new object[,]
                {
                    { 6, null, 0, 0, 0, 0, 0, 360, 0, 0, null, null },
                    { 7, null, 0, 0, 0, 0, 0, 360, 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserMember",
                columns: new[] { "UserId", "BillingOptions", "DisabledDate", "Email", "EnableDate", "IsNegativeBalanceAllowed", "IsPersonalInfoRequested", "UserGroupId", "Username" },
                values: new object[] { 2, null, null, null, null, null, false, 1, "User" });

            migrationBuilder.InsertData(
                table: "Host",
                columns: new[] { "HostId", "CreatedById", "CreatedTime", "Guid", "HostGroupId", "IconId", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "Number", "State" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803871"), 2, null, false, null, null, "XBOX-ONE-1", 1, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803872"), 2, null, false, null, null, "XBOX-ONE-2", 2, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803873"), 2, null, false, null, null, "PS4-1", 3, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803874"), 2, null, false, null, null, "WII-1", 4, 0 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                column: "ProductId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "ProductBundle",
                columns: new[] { "ProductId", "BundleStockOptions" },
                values: new object[] { 5, 0 });

            migrationBuilder.InsertData(
                table: "ProductPeriodDay",
                columns: new[] { "ProductPeriodDayId", "Day", "ProductPeriodId" },
                values: new object[,]
                {
                    { 7, 6, 7 },
                    { 8, 0, 7 }
                });

            migrationBuilder.InsertData(
                table: "ProductTimePeriod",
                columns: new[] { "ProductId", "EndDate", "Options", "StartDate" },
                values: new object[] { 6, null, 0, null });

            migrationBuilder.InsertData(
                table: "BundleProduct",
                columns: new[] { "BundleProductId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Options", "Price", "ProductBundleId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 0, 1m, 5, 3, 1m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 0, 2m, 5, 4, 1m }
                });

            migrationBuilder.InsertData(
                table: "HostEndpoint",
                columns: new[] { "HostId", "MaximumUsers" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryId",
                table: "App",
                column: "AppCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "App",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperId",
                table: "App",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "App",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherId",
                table: "App",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "App",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppCategory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppCategory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ParentId",
                table: "AppCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "AppCategory",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppEnterprise",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppEnterprise",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "AppEnterprise",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "AppEnterprise",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppId",
                table: "AppExe",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExe",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultDeploymentId",
                table: "AppExe",
                column: "DefaultDeploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExe",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppExeCdImage",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExeCdImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExeCdImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "AppExeCdImage",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppExeDeployment",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExeDeployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentId",
                table: "AppExeDeployment",
                column: "DeploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExeDeployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppExeImage",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExeImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExeImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppExeLicense",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExeLicense",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseId",
                table: "AppExeLicense",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExeLicense",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExeMaxUser",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExeMaxUser",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_AppExeAppExeMode",
                table: "AppExeMaxUser",
                columns: new[] { "AppExeId", "Mode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppExePersonalFile",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExePersonalFile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExePersonalFile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFileId",
                table: "AppExePersonalFile",
                column: "PersonalFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppExeTask",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppExeTask",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppExeTask",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseId",
                table: "AppExeTask",
                column: "TaskBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "AppGroup",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "AppGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupId",
                table: "AppGroupApp",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppId",
                table: "AppGroupApp",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppId",
                table: "AppImage",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppId",
                table: "AppLink",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AppLink",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AppLink",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "AppLink",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppId",
                table: "AppRating",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AppRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeId",
                table: "AppStat",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppId",
                table: "AppStat",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "AppStat",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AppStat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Asset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Asset",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Barcode",
                table: "Asset",
                column: "Barcode",
                unique: true,
                filter: "[Barcode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_SmartCardUID",
                table: "Asset",
                column: "SmartCardUID",
                unique: true,
                filter: "[SmartCardUID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetId",
                table: "AssetTransaction",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypeId",
                table: "AssetTransaction",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckedInById",
                table: "AssetTransaction",
                column: "CheckedInById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AssetTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AssetTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AssetTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AssetType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AssetType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "AssetType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequestTypeId",
                table: "AssistanceRequest",
                column: "AssistanceRequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AssistanceRequest",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "AssistanceRequest",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AssistanceRequest",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AssistanceRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "AssistanceRequestType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "AssistanceRequestType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Attribute",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Attribute",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "Attribute",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "BillProfile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "BillProfile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "BillProfile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillProfileId",
                table: "BillRate",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "UQ_BillRatePeriodDay",
                table: "BillRatePeriodDay",
                columns: new[] { "BillRateId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDayId",
                table: "BillRatePeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "UQ_BillRateMinute",
                table: "BillRateStep",
                columns: new[] { "BillRateId", "Minute" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "BundleProduct",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "BundleProduct",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundleId",
                table: "BundleProduct",
                column: "ProductBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "BundleProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "BundleProductUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "BundleProductUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupId",
                table: "BundleProductUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_BundleProductUserGroup",
                table: "BundleProductUserPrice",
                columns: new[] { "BundleProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ClientTask",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ClientTask",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseId",
                table: "ClientTask",
                column: "TaskBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Deployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Deployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "Deployment",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "Deployment",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChildId",
                table: "DeploymentDeployment",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "DeploymentDeployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "DeploymentDeployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ParentId",
                table: "DeploymentDeployment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "DepositPayment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransactionId",
                table: "DepositPayment",
                column: "DepositTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceiptId",
                table: "DepositPayment",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "DepositPayment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentId",
                table: "DepositPayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "DepositPayment",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "DepositPayment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "DepositPayment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "DepositTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "DepositTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "DepositTransaction",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "DepositTransaction",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "DepositTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Device",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Device",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "Device",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceId",
                table: "DeviceHdmi",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UniqueId",
                table: "DeviceHdmi",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "DeviceHost",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "DeviceHost",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "DeviceHost",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_HostDevice",
                table: "DeviceHost",
                columns: new[] { "DeviceId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Feed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Feed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "FiscalReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "FiscalReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "FiscalReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Host",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupId",
                table: "Host",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IconId",
                table: "Host",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Host",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "Host",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "HostComputer",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "UQ_MACAddress",
                table: "HostComputer",
                column: "MACAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "HostEndpoint",
                column: "HostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupId",
                table: "HostGroup",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "HostGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultGuestGroupId",
                table: "HostGroup",
                column: "DefaultGuestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "HostGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfileId",
                table: "HostGroup",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "HostGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillProfileId",
                table: "HostGroupUserBillProfile",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupId",
                table: "HostGroupUserBillProfile",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_HostGroupUserBillProfile",
                table: "HostGroupUserBillProfile",
                columns: new[] { "HostGroupId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "HostGroupWaitingLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HosGroupId",
                table: "HostGroupWaitingLine",
                column: "HosGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "HostGroupWaitingLine",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "HostGroupWaitingLineEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupId",
                table: "HostGroupWaitingLineEntry",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "HostGroupWaitingLineEntry",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "HostGroupWaitingLineEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "HostLayoutGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "HostLayoutGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "HostLayoutGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "HostLayoutGroupImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupId",
                table: "HostLayoutGroupImage",
                column: "HostLayoutGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "HostLayoutGroupImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "HostLayoutGroupLayout",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "HostLayoutGroupLayout",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "HostLayoutGroupLayout",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_HostLayoutGroupHost",
                table: "HostLayoutGroupLayout",
                columns: new[] { "HostLayoutGroupId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Icon",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Icon",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Invoice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Invoice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderId",
                table: "Invoice",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "Invoice",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "Invoice",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Invoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "InvoiceFiscalReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceId",
                table: "InvoiceFiscalReceipt",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "InvoiceFiscalReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "InvoiceFiscalReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "UQ_FiscalReceipt",
                table: "InvoiceFiscalReceipt",
                column: "FiscalReceiptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "InvoiceLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "InvoiceLine",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "InvoiceLine",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "InvoiceLine",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "InvoiceLine",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_PointsTransaction",
                table: "InvoiceLine",
                column: "PointsTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BundleLineId",
                table: "InvoiceLineExtended",
                column: "BundleLineId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "InvoiceLineExtended",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_StockReturnTransaction",
                table: "InvoiceLineExtended",
                column: "StockReturnTransactionId",
                unique: true,
                filter: "[StockReturnTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_StockTransaction",
                table: "InvoiceLineExtended",
                column: "StockTransactionId",
                unique: true,
                filter: "[StockTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "InvoiceLineProduct",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "InvoiceLineProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UQ_OrderLine",
                table: "InvoiceLineProduct",
                column: "OrderLineId",
                unique: true,
                filter: "[OrderLineId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "InvoiceLineSession",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineId",
                table: "InvoiceLineSession",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "UQ_UsageSession",
                table: "InvoiceLineSession",
                column: "UsageSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "InvoiceLineTime",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimeId",
                table: "InvoiceLineTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "UQ_OrderLine",
                table: "InvoiceLineTime",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "InvoiceLineTimeFixed",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_OrderLine",
                table: "InvoiceLineTimeFixed",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "InvoicePayment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceId",
                table: "InvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "InvoicePayment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentId",
                table: "InvoicePayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "InvoicePayment",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "InvoicePayment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "InvoicePayment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "License",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "License",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "License",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "License",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHostId",
                table: "LicenseKey",
                column: "AssignedHostId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "LicenseKey",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseId",
                table: "LicenseKey",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "LicenseKey",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "LicenseKey",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category",
                table: "Log",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_HostNumber",
                table: "Log",
                column: "HostNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MessageType",
                table: "Log",
                column: "MessageType");

            migrationBuilder.CreateIndex(
                name: "IX_Time",
                table: "Log",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_LogId",
                table: "LogException",
                column: "LogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Mapping",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Mapping",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_MountPoint",
                table: "Mapping",
                column: "MountPoint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "MonetaryUnit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "MonetaryUnit",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "MonetaryUnit",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "News",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "News",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Note",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Note",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Payment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Payment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "Payment",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "Payment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_DepositTransaction",
                table: "Payment",
                column: "DepositTransactionId",
                unique: true,
                filter: "[DepositTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_PointsTransaction",
                table: "Payment",
                column: "PointTransactionId",
                unique: true,
                filter: "[PointTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PaymentIntent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PaymentIntent",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodId",
                table: "PaymentIntent",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "PaymentIntent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "PaymentIntent",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentId",
                table: "PaymentIntentDeposit",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DepositPayment",
                table: "PaymentIntentDeposit",
                column: "DepositPaymentId",
                unique: true,
                filter: "[DepositPaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentId",
                table: "PaymentIntentOrder",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "UQ_InvoicePayment",
                table: "PaymentIntentOrder",
                column: "InvoicePaymentId",
                unique: true,
                filter: "[InvoicePaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PaymentMethod",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PaymentMethod",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "PaymentMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PersonalFile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PersonalFile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "PersonalFile",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "PersonalFile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PluginLibrary",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PluginLibrary",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_FileName",
                table: "PluginLibrary",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PointTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PointTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "PointTransaction",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "PointTransaction",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "PointTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PresetTimeSale",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PresetTimeSale",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "PresetTimeSaleMoney",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "PresetTimeSaleMoney",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "Product",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductBase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductBase",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroupId",
                table: "ProductBase",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StockProductId",
                table: "ProductBase",
                column: "StockProductId");

            migrationBuilder.CreateIndex(
                name: "UQ_Barcode",
                table: "ProductBase",
                column: "Barcode",
                unique: true,
                filter: "[Barcode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "ProductBase",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductBaseExtended",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductBundle",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductBundleUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductBundleUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupId",
                table: "ProductBundleUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductBundlePriceUserGroup",
                table: "ProductBundleUserPrice",
                columns: new[] { "ProductBundleId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ParentId",
                table: "ProductGroup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "ProductGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductHostHidden",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupId",
                table: "ProductHostHidden",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductHostHidden",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductHostGroup",
                table: "ProductHostHidden",
                columns: new[] { "ProductId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductOL",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductOL",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLId",
                table: "ProductOL",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderId",
                table: "ProductOL",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "ProductOL",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "ProductOL",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "ProductOL",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleLineId",
                table: "ProductOLExtended",
                column: "BundleLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLId",
                table: "ProductOLExtended",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductOLProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLId",
                table: "ProductOLProduct",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLId",
                table: "ProductOLSession",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageSessionId",
                table: "ProductOLSession",
                column: "UsageSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLId",
                table: "ProductOLTime",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimeId",
                table: "ProductOLTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLId",
                table: "ProductOLTimeFixed",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductOrder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "ProductOrder",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductOrder",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PreferedPaymentMethodId",
                table: "ProductOrder",
                column: "PreferedPaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "ProductOrder",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "ProductOrder",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "ProductOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductPeriod",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductPeriodDay",
                table: "ProductPeriodDay",
                columns: new[] { "ProductPeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDayId",
                table: "ProductPeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductTax",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductTax",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaxId",
                table: "ProductTax",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "UQ_TaxProduct",
                table: "ProductTax",
                columns: new[] { "ProductId", "TaxId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupId",
                table: "ProductTime",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductTime",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductTimeHostDisallowed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupId",
                table: "ProductTimeHostDisallowed",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductTimeHostDisallowed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductTimeHostGroup",
                table: "ProductTimeHostDisallowed",
                columns: new[] { "ProductTimeId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "ProductTimePeriod",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimePeriodDayId",
                table: "ProductTimePeriodDay",
                column: "ProductTimePeriodDayId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductTimePeriodDay",
                table: "ProductTimePeriodDay",
                columns: new[] { "ProductTimePeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDayId",
                table: "ProductTimePeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductUserDisallowed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductUserDisallowed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupId",
                table: "ProductUserDisallowed",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductUserGroup",
                table: "ProductUserDisallowed",
                columns: new[] { "ProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ProductUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ProductUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupId",
                table: "ProductUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductUserGroup",
                table: "ProductUserPrice",
                columns: new[] { "ProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Refund",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransactionId",
                table: "Refund",
                column: "DepositTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentId",
                table: "Refund",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransactionId",
                table: "Refund",
                column: "PointTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundMethodId",
                table: "Refund",
                column: "RefundMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "Refund",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "Refund",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceiptId",
                table: "RefundDepositPayment",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundId",
                table: "RefundDepositPayment",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DepositPayment",
                table: "RefundDepositPayment",
                column: "DepositPaymentId",
                unique: true,
                filter: "[DepositPaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceId",
                table: "RefundInvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundId",
                table: "RefundInvoicePayment",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_InvoicePayment",
                table: "RefundInvoicePayment",
                column: "InvoicePaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Register",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Register",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_MACAddress",
                table: "Register",
                column: "MacAddress",
                unique: true,
                filter: "[MacAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "RegisterTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "RegisterTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "RegisterTransaction",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "RegisterTransaction",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Reservation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Reservation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Pin",
                table: "Reservation",
                column: "Pin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ReservationHost",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "ReservationHost",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ReservationHost",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PreferedUserId",
                table: "ReservationHost",
                column: "PreferedUserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Reservation_Host",
                table: "ReservationHost",
                columns: new[] { "ReservationId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ReservationUser",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ReservationUser",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "ReservationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Reservation_User",
                table: "ReservationUser",
                columns: new[] { "ReservationId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "SecurityProfile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "SecurityProfile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "SecurityProfile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "SecurityProfilePolicy",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "SecurityProfilePolicy",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_SecurityProfilePolicyType",
                table: "SecurityProfilePolicy",
                columns: new[] { "SecurityProfileId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "SecurityProfileRestriction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "SecurityProfileRestriction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfileId",
                table: "SecurityProfileRestriction",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Setting",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Setting",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_NameGroup",
                table: "Setting",
                columns: new[] { "Name", "GroupName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Shift",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EndedById",
                table: "Shift",
                column: "EndedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Shift",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorId",
                table: "Shift",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "Shift",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "Shift",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "ShiftCount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "ShiftCount",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodId",
                table: "ShiftCount",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "UQ_ShiftCountPaymentMethod",
                table: "ShiftCount",
                columns: new[] { "ShiftId", "PaymentMethodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "StockTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "StockTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "StockTransaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceProductId",
                table: "StockTransaction",
                column: "SourceProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "TaskBase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "TaskBase",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskId",
                table: "TaskBase",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "TaskBase",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "TaskBase",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskId",
                table: "TaskJunction",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskId",
                table: "TaskNotification",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskId",
                table: "TaskProcess",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskId",
                table: "TaskScript",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Tax",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Tax",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "Tax",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Token",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Token",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Token",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Value",
                table: "Token",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageSessionId",
                table: "Usage",
                column: "UsageSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Usage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillRateId",
                table: "UsageRate",
                column: "BillRateId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageId",
                table: "UsageRate",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentUsageId",
                table: "UsageSession",
                column: "CurrentUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageSessionId",
                table: "UsageSession",
                column: "UsageSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UsageSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "UsageTime",
                column: "InvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageId",
                table: "UsageTime",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineId",
                table: "UsageTimeFixed",
                column: "InvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageId",
                table: "UsageTimeFixed",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageId",
                table: "UsageUserSession",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionId",
                table: "UsageUserSession",
                column: "UserSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "User",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid",
                table: "User",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Identification",
                table: "User",
                column: "Identification",
                unique: true,
                filter: "[Identification] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_SmartCardUID",
                table: "User",
                column: "SmartCardUID",
                unique: true,
                filter: "[SmartCardUID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserAgreement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserAgreement",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserAgreementState",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserAgreementState",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserAgreementState",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_UserAgreementState",
                table: "UserAgreementState",
                columns: new[] { "UserAgreementId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeId",
                table: "UserAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserAttribute",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserAttribute",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_UserAttribute",
                table: "UserAttribute",
                columns: new[] { "UserId", "AttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserCredential",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserCredential",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserCredential",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserCreditLimit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserCreditLimit",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserCreditLimit",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupId",
                table: "UserGroup",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BillProfileId",
                table: "UserGroup",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfileId",
                table: "UserGroup",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "UserGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserGroupHostDisallowed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupId",
                table: "UserGroupHostDisallowed",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserGroupHostDisallowed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_UserGroupHostGroup",
                table: "UserGroupHostDisallowed",
                columns: new[] { "UserGroupId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserGuest",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UserGuestHostSlot",
                table: "UserGuest",
                columns: new[] { "ReservedHostId", "ReservedSlot" },
                unique: true,
                filter: "[ReservedHostId] IS NOT NULL AND [ReservedSlot] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupId",
                table: "UserMember",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserMember",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Email",
                table: "UserMember",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Username",
                table: "UserMember",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteId",
                table: "UserNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserNote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserOperator",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Email",
                table: "UserOperator",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Username",
                table: "UserOperator",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_UserPermission",
                table: "UserPermission",
                columns: new[] { "UserId", "Type", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserPicture",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "UserPicture",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserPicture",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserSession",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "UserSession",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "UserSessionChange",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostId",
                table: "UserSessionChange",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UserSessionChange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionId",
                table: "UserSessionChange",
                column: "UserSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Variable",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Variable",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name",
                table: "Variable",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Verification",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedById",
                table: "Verification",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TokenId",
                table: "Verification",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Verification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationId",
                table: "VerificationEmail",
                column: "VerificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationId",
                table: "VerificationMobilePhone",
                column: "VerificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedById",
                table: "Void",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterId",
                table: "Void",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftId",
                table: "Void",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_VoidId",
                table: "VoidDepositPayment",
                column: "VoidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DepositPayment",
                table: "VoidDepositPayment",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoidId",
                table: "VoidInvoice",
                column: "VoidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Invoice",
                table: "VoidInvoice",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_App_AppCategory_AppCategoryId",
                table: "App",
                column: "AppCategoryId",
                principalTable: "AppCategory",
                principalColumn: "AppCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_App_AppEnterprise_DeveloperId",
                table: "App",
                column: "DeveloperId",
                principalTable: "AppEnterprise",
                principalColumn: "AppEnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_AppEnterprise_PublisherId",
                table: "App",
                column: "PublisherId",
                principalTable: "AppEnterprise",
                principalColumn: "AppEnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_UserOperator_CreatedById",
                table: "App",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_UserOperator_ModifiedById",
                table: "App",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCategory_UserOperator_CreatedById",
                table: "AppCategory",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCategory_UserOperator_ModifiedById",
                table: "AppCategory",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEnterprise_UserOperator_CreatedById",
                table: "AppEnterprise",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEnterprise_UserOperator_ModifiedById",
                table: "AppEnterprise",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExe_Deployment_DefaultDeploymentId",
                table: "AppExe",
                column: "DefaultDeploymentId",
                principalTable: "Deployment",
                principalColumn: "DeploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExe_UserOperator_CreatedById",
                table: "AppExe",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExe_UserOperator_ModifiedById",
                table: "AppExe",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeCdImage_UserOperator_CreatedById",
                table: "AppExeCdImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeCdImage_UserOperator_ModifiedById",
                table: "AppExeCdImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeDeployment_Deployment_DeploymentId",
                table: "AppExeDeployment",
                column: "DeploymentId",
                principalTable: "Deployment",
                principalColumn: "DeploymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeDeployment_UserOperator_CreatedById",
                table: "AppExeDeployment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeDeployment_UserOperator_ModifiedById",
                table: "AppExeDeployment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeImage_UserOperator_CreatedById",
                table: "AppExeImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeImage_UserOperator_ModifiedById",
                table: "AppExeImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeLicense_License_LicenseId",
                table: "AppExeLicense",
                column: "LicenseId",
                principalTable: "License",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeLicense_UserOperator_CreatedById",
                table: "AppExeLicense",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeLicense_UserOperator_ModifiedById",
                table: "AppExeLicense",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeMaxUser_UserOperator_CreatedById",
                table: "AppExeMaxUser",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeMaxUser_UserOperator_ModifiedById",
                table: "AppExeMaxUser",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExePersonalFile_PersonalFile_PersonalFileId",
                table: "AppExePersonalFile",
                column: "PersonalFileId",
                principalTable: "PersonalFile",
                principalColumn: "PersonalFileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExePersonalFile_UserOperator_CreatedById",
                table: "AppExePersonalFile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExePersonalFile_UserOperator_ModifiedById",
                table: "AppExePersonalFile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeTask_TaskBase_TaskBaseId",
                table: "AppExeTask",
                column: "TaskBaseId",
                principalTable: "TaskBase",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeTask_UserOperator_CreatedById",
                table: "AppExeTask",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeTask_UserOperator_ModifiedById",
                table: "AppExeTask",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGroup_UserOperator_CreatedById",
                table: "AppGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGroup_UserOperator_ModifiedById",
                table: "AppGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImage_UserOperator_CreatedById",
                table: "AppImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImage_UserOperator_ModifiedById",
                table: "AppImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLink_UserOperator_CreatedById",
                table: "AppLink",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLink_UserOperator_ModifiedById",
                table: "AppLink",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRating_UserMember_UserId",
                table: "AppRating",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStat_HostComputer_HostId",
                table: "AppStat",
                column: "HostId",
                principalTable: "HostComputer",
                principalColumn: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppStat_UserMember_UserId",
                table: "AppStat",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_AssetType_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId",
                principalTable: "AssetType",
                principalColumn: "AssetTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_UserOperator_CreatedById",
                table: "Asset",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_UserOperator_ModifiedById",
                table: "Asset",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_AssetType_AssetTypeId",
                table: "AssetTransaction",
                column: "AssetTypeId",
                principalTable: "AssetType",
                principalColumn: "AssetTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserMember_UserId",
                table: "AssetTransaction",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserOperator_CheckedInById",
                table: "AssetTransaction",
                column: "CheckedInById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserOperator_CreatedById",
                table: "AssetTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserOperator_ModifiedById",
                table: "AssetTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetType_UserOperator_CreatedById",
                table: "AssetType",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetType_UserOperator_ModifiedById",
                table: "AssetType",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_AssistanceRequestType_AssistanceRequestTypeId",
                table: "AssistanceRequest",
                column: "AssistanceRequestTypeId",
                principalTable: "AssistanceRequestType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_Host_HostId",
                table: "AssistanceRequest",
                column: "HostId",
                principalTable: "Host",
                principalColumn: "HostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_UserMember_UserId",
                table: "AssistanceRequest",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_User_CreatedById",
                table: "AssistanceRequest",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_User_ModifiedById",
                table: "AssistanceRequest",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequestType_UserOperator_CreatedById",
                table: "AssistanceRequestType",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequestType_UserOperator_ModifiedById",
                table: "AssistanceRequestType",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_UserOperator_CreatedById",
                table: "Attribute",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_UserOperator_ModifiedById",
                table: "Attribute",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillProfile_UserOperator_CreatedById",
                table: "BillProfile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillProfile_UserOperator_ModifiedById",
                table: "BillProfile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_ProductBase_ProductId",
                table: "BundleProduct",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_ProductBundle_ProductBundleId",
                table: "BundleProduct",
                column: "ProductBundleId",
                principalTable: "ProductBundle",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_UserOperator_CreatedById",
                table: "BundleProduct",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_UserOperator_ModifiedById",
                table: "BundleProduct",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProductUserPrice_UserGroup_UserGroupId",
                table: "BundleProductUserPrice",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProductUserPrice_UserOperator_CreatedById",
                table: "BundleProductUserPrice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProductUserPrice_UserOperator_ModifiedById",
                table: "BundleProductUserPrice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTask_TaskBase_TaskBaseId",
                table: "ClientTask",
                column: "TaskBaseId",
                principalTable: "TaskBase",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTask_UserOperator_CreatedById",
                table: "ClientTask",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTask_UserOperator_ModifiedById",
                table: "ClientTask",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_UserOperator_CreatedById",
                table: "Deployment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_UserOperator_ModifiedById",
                table: "Deployment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeploymentDeployment_UserOperator_CreatedById",
                table: "DeploymentDeployment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeploymentDeployment_UserOperator_ModifiedById",
                table: "DeploymentDeployment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_DepositTransaction_DepositTransactionId",
                table: "DepositPayment",
                column: "DepositTransactionId",
                principalTable: "DepositTransaction",
                principalColumn: "DepositTransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_FiscalReceipt_FiscalReceiptId",
                table: "DepositPayment",
                column: "FiscalReceiptId",
                principalTable: "FiscalReceipt",
                principalColumn: "FiscalReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Payment_PaymentId",
                table: "DepositPayment",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Register_RegisterId",
                table: "DepositPayment",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Shift_ShiftId",
                table: "DepositPayment",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_UserMember_UserId",
                table: "DepositPayment",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_UserOperator_CreatedById",
                table: "DepositPayment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_UserOperator_ModifiedById",
                table: "DepositPayment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_Register_RegisterId",
                table: "DepositTransaction",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_Shift_ShiftId",
                table: "DepositTransaction",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_UserMember_UserId",
                table: "DepositTransaction",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_UserOperator_CreatedById",
                table: "DepositTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_UserOperator_ModifiedById",
                table: "DepositTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_UserOperator_CreatedById",
                table: "Device",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_UserOperator_ModifiedById",
                table: "Device",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHost_Host_HostId",
                table: "DeviceHost",
                column: "HostId",
                principalTable: "Host",
                principalColumn: "HostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHost_UserOperator_CreatedById",
                table: "DeviceHost",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHost_UserOperator_ModifiedById",
                table: "DeviceHost",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_UserOperator_CreatedById",
                table: "Feed",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_UserOperator_ModifiedById",
                table: "Feed",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_Register_RegisterId",
                table: "FiscalReceipt",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_Shift_ShiftId",
                table: "FiscalReceipt",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_UserOperator_CreatedById",
                table: "FiscalReceipt",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_HostGroup_HostGroupId",
                table: "Host",
                column: "HostGroupId",
                principalTable: "HostGroup",
                principalColumn: "HostGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_Icon_IconId",
                table: "Host",
                column: "IconId",
                principalTable: "Icon",
                principalColumn: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_UserOperator_CreatedById",
                table: "Host",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_UserOperator_ModifiedById",
                table: "Host",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_SecurityProfile_SecurityProfileId",
                table: "HostGroup",
                column: "SecurityProfileId",
                principalTable: "SecurityProfile",
                principalColumn: "SecurityProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_UserGroup_DefaultGuestGroupId",
                table: "HostGroup",
                column: "DefaultGuestGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_UserOperator_CreatedById",
                table: "HostGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_UserOperator_ModifiedById",
                table: "HostGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupUserBillProfile_UserGroup_UserGroupId",
                table: "HostGroupUserBillProfile",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLine_UserOperator_CreatedById",
                table: "HostGroupWaitingLine",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLine_UserOperator_ModifiedById",
                table: "HostGroupWaitingLine",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLineEntry_UserMember_UserId",
                table: "HostGroupWaitingLineEntry",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLineEntry_UserOperator_CreatedById",
                table: "HostGroupWaitingLineEntry",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLineEntry_User_ModifiedById",
                table: "HostGroupWaitingLineEntry",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroup_UserOperator_CreatedById",
                table: "HostLayoutGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroup_UserOperator_ModifiedById",
                table: "HostLayoutGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupImage_UserOperator_CreatedById",
                table: "HostLayoutGroupImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupImage_UserOperator_ModifiedById",
                table: "HostLayoutGroupImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupLayout_UserOperator_CreatedById",
                table: "HostLayoutGroupLayout",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupLayout_UserOperator_ModifiedById",
                table: "HostLayoutGroupLayout",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Icon_UserOperator_CreatedById",
                table: "Icon",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Icon_UserOperator_ModifiedById",
                table: "Icon",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_ProductOrder_ProductOrderId",
                table: "Invoice",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Register_RegisterId",
                table: "Invoice",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Shift_ShiftId",
                table: "Invoice",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserMember_UserId",
                table: "Invoice",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserOperator_CreatedById",
                table: "Invoice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserOperator_ModifiedById",
                table: "Invoice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFiscalReceipt_Register_RegisterId",
                table: "InvoiceFiscalReceipt",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFiscalReceipt_Shift_ShiftId",
                table: "InvoiceFiscalReceipt",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFiscalReceipt_UserOperator_CreatedById",
                table: "InvoiceFiscalReceipt",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_PointTransaction_PointsTransactionId",
                table: "InvoiceLine",
                column: "PointsTransactionId",
                principalTable: "PointTransaction",
                principalColumn: "PointTransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Register_RegisterId",
                table: "InvoiceLine",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Shift_ShiftId",
                table: "InvoiceLine",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UserMember_UserId",
                table: "InvoiceLine",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UserOperator_CreatedById",
                table: "InvoiceLine",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UserOperator_ModifiedById",
                table: "InvoiceLine",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineExtended_InvoiceLineProduct_BundleLineId",
                table: "InvoiceLineExtended",
                column: "BundleLineId",
                principalTable: "InvoiceLineProduct",
                principalColumn: "InvoiceLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineExtended_StockTransaction_StockReturnTransactionId",
                table: "InvoiceLineExtended",
                column: "StockReturnTransactionId",
                principalTable: "StockTransaction",
                principalColumn: "StockTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineExtended_StockTransaction_StockTransactionId",
                table: "InvoiceLineExtended",
                column: "StockTransactionId",
                principalTable: "StockTransaction",
                principalColumn: "StockTransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineProduct_ProductBaseExtended_ProductId",
                table: "InvoiceLineProduct",
                column: "ProductId",
                principalTable: "ProductBaseExtended",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineProduct_ProductOLProduct_OrderLineId",
                table: "InvoiceLineProduct",
                column: "OrderLineId",
                principalTable: "ProductOLProduct",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineSession_ProductOLSession_OrderLineId",
                table: "InvoiceLineSession",
                column: "OrderLineId",
                principalTable: "ProductOLSession",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineSession_UsageSession_UsageSessionId",
                table: "InvoiceLineSession",
                column: "UsageSessionId",
                principalTable: "UsageSession",
                principalColumn: "UsageSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineTime_ProductOLTime_OrderLineId",
                table: "InvoiceLineTime",
                column: "OrderLineId",
                principalTable: "ProductOLTime",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineTime_ProductTime_ProductTimeId",
                table: "InvoiceLineTime",
                column: "ProductTimeId",
                principalTable: "ProductTime",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineTimeFixed_ProductOLTimeFixed_OrderLineId",
                table: "InvoiceLineTimeFixed",
                column: "OrderLineId",
                principalTable: "ProductOLTimeFixed",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Payment_PaymentId",
                table: "InvoicePayment",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Register_RegisterId",
                table: "InvoicePayment",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Shift_ShiftId",
                table: "InvoicePayment",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_UserMember_UserId",
                table: "InvoicePayment",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_UserOperator_CreatedById",
                table: "InvoicePayment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_UserOperator_ModifiedById",
                table: "InvoicePayment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_License_UserOperator_CreatedById",
                table: "License",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_License_UserOperator_ModifiedById",
                table: "License",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseKey_UserOperator_CreatedById",
                table: "LicenseKey",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseKey_UserOperator_ModifiedById",
                table: "LicenseKey",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_UserOperator_CreatedById",
                table: "Mapping",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_UserOperator_ModifiedById",
                table: "Mapping",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryUnit_UserOperator_CreatedById",
                table: "MonetaryUnit",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryUnit_UserOperator_ModifiedById",
                table: "MonetaryUnit",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_UserOperator_CreatedById",
                table: "News",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_UserOperator_ModifiedById",
                table: "News",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_UserOperator_CreatedById",
                table: "Note",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_UserOperator_ModifiedById",
                table: "Note",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentMethod_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PointTransaction_PointTransactionId",
                table: "Payment",
                column: "PointTransactionId",
                principalTable: "PointTransaction",
                principalColumn: "PointTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Register_RegisterId",
                table: "Payment",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Shift_ShiftId",
                table: "Payment",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserMember_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserOperator_CreatedById",
                table: "Payment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserOperator_ModifiedById",
                table: "Payment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_PaymentMethod_PaymentMethodId",
                table: "PaymentIntent",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_UserMember_UserId",
                table: "PaymentIntent",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_User_CreatedById",
                table: "PaymentIntent",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_User_ModifiedById",
                table: "PaymentIntent",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntentOrder_ProductOrder_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_UserOperator_CreatedById",
                table: "PaymentMethod",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_UserOperator_ModifiedById",
                table: "PaymentMethod",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalFile_UserOperator_CreatedById",
                table: "PersonalFile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalFile_UserOperator_ModifiedById",
                table: "PersonalFile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PluginLibrary_UserOperator_CreatedById",
                table: "PluginLibrary",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PluginLibrary_UserOperator_ModifiedById",
                table: "PluginLibrary",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_Register_RegisterId",
                table: "PointTransaction",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_Shift_ShiftId",
                table: "PointTransaction",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_UserMember_UserId",
                table: "PointTransaction",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_UserOperator_CreatedById",
                table: "PointTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_UserOperator_ModifiedById",
                table: "PointTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSale_UserOperator_CreatedById",
                table: "PresetTimeSale",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSale_UserOperator_ModifiedById",
                table: "PresetTimeSale",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSaleMoney_UserOperator_CreatedById",
                table: "PresetTimeSaleMoney",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSaleMoney_UserOperator_ModifiedById",
                table: "PresetTimeSaleMoney",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBaseExtended_ProductId",
                table: "Product",
                column: "ProductId",
                principalTable: "ProductBaseExtended",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_ProductGroup_ProductGroupId",
                table: "ProductBase",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "ProductGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_UserOperator_CreatedById",
                table: "ProductBase",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_UserOperator_ModifiedById",
                table: "ProductBase",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundleUserPrice_UserGroup_UserGroupId",
                table: "ProductBundleUserPrice",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundleUserPrice_UserOperator_CreatedById",
                table: "ProductBundleUserPrice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundleUserPrice_UserOperator_ModifiedById",
                table: "ProductBundleUserPrice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_UserOperator_CreatedById",
                table: "ProductGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_UserOperator_ModifiedById",
                table: "ProductGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHostHidden_UserOperator_CreatedById",
                table: "ProductHostHidden",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHostHidden_UserOperator_ModifiedById",
                table: "ProductHostHidden",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_UserOperator_CreatedById",
                table: "ProductImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_UserOperator_ModifiedById",
                table: "ProductImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_ProductOrder_ProductOrderId",
                table: "ProductOL",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_Register_RegisterId",
                table: "ProductOL",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_Shift_ShiftId",
                table: "ProductOL",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_UserMember_UserId",
                table: "ProductOL",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_UserOperator_CreatedById",
                table: "ProductOL",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_UserOperator_ModifiedById",
                table: "ProductOL",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOLExtended_ProductOLProduct_BundleLineId",
                table: "ProductOLExtended",
                column: "BundleLineId",
                principalTable: "ProductOLProduct",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOLSession_UsageSession_UsageSessionId",
                table: "ProductOLSession",
                column: "UsageSessionId",
                principalTable: "UsageSession",
                principalColumn: "UsageSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Register_RegisterId",
                table: "ProductOrder",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Shift_ShiftId",
                table: "ProductOrder",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_UserMember_UserId",
                table: "ProductOrder",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_UserOperator_CreatedById",
                table: "ProductOrder",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_UserOperator_ModifiedById",
                table: "ProductOrder",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_Tax_TaxId",
                table: "ProductTax",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_UserOperator_CreatedById",
                table: "ProductTax",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_UserOperator_ModifiedById",
                table: "ProductTax",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTimeHostDisallowed_UserOperator_CreatedById",
                table: "ProductTimeHostDisallowed",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTimeHostDisallowed_UserOperator_ModifiedById",
                table: "ProductTimeHostDisallowed",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserDisallowed_UserGroup_UserGroupId",
                table: "ProductUserDisallowed",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserDisallowed_UserOperator_CreatedById",
                table: "ProductUserDisallowed",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserDisallowed_UserOperator_ModifiedById",
                table: "ProductUserDisallowed",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserPrice_UserGroup_UserGroupId",
                table: "ProductUserPrice",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserPrice_UserOperator_CreatedById",
                table: "ProductUserPrice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserPrice_UserOperator_ModifiedById",
                table: "ProductUserPrice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Register_RegisterId",
                table: "Refund",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Shift_ShiftId",
                table: "Refund",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_UserOperator_CreatedById",
                table: "Refund",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_UserOperator_CreatedById",
                table: "Register",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_UserOperator_ModifiedById",
                table: "Register",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_Shift_ShiftId",
                table: "RegisterTransaction",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_UserOperator_CreatedById",
                table: "RegisterTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_UserOperator_ModifiedById",
                table: "RegisterTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_UserMember_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_CreatedById",
                table: "Reservation",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ModifiedById",
                table: "Reservation",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_UserMember_PreferedUserId",
                table: "ReservationHost",
                column: "PreferedUserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_CreatedById",
                table: "ReservationHost",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_ModifiedById",
                table: "ReservationHost",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationUser_UserMember_UserId",
                table: "ReservationUser",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationUser_User_CreatedById",
                table: "ReservationUser",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationUser_User_ModifiedById",
                table: "ReservationUser",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfile_UserOperator_CreatedById",
                table: "SecurityProfile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfile_UserOperator_ModifiedById",
                table: "SecurityProfile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfilePolicy_UserOperator_CreatedById",
                table: "SecurityProfilePolicy",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfilePolicy_UserOperator_ModifiedById",
                table: "SecurityProfilePolicy",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfileRestriction_UserOperator_CreatedById",
                table: "SecurityProfileRestriction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfileRestriction_UserOperator_ModifiedById",
                table: "SecurityProfileRestriction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setting_UserOperator_CreatedById",
                table: "Setting",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setting_UserOperator_ModifiedById",
                table: "Setting",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_CreatedById",
                table: "Shift",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_EndedById",
                table: "Shift",
                column: "EndedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_ModifiedById",
                table: "Shift",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_OperatorId",
                table: "Shift",
                column: "OperatorId",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftCount_UserOperator_CreatedById",
                table: "ShiftCount",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftCount_UserOperator_ModifiedById",
                table: "ShiftCount",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_UserOperator_CreatedById",
                table: "StockTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_UserOperator_ModifiedById",
                table: "StockTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskBase_UserOperator_CreatedById",
                table: "TaskBase",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskBase_UserOperator_ModifiedById",
                table: "TaskBase",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_UserOperator_CreatedById",
                table: "Tax",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_UserOperator_ModifiedById",
                table: "Tax",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_CreatedById",
                table: "Token",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_ModifiedById",
                table: "Token",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_UserId",
                table: "Token",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usage_UsageSession_UsageSessionId",
                table: "Usage",
                column: "UsageSessionId",
                principalTable: "UsageSession",
                principalColumn: "UsageSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usage_UserMember_UserId",
                table: "Usage",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRate_UsageUserSession_UsageId",
                table: "UsageRate",
                column: "UsageId",
                principalTable: "UsageUserSession",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsageTime_UsageUserSession_UsageId",
                table: "UsageTime",
                column: "UsageId",
                principalTable: "UsageUserSession",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageTimeFixed_UsageUserSession_UsageId",
                table: "UsageTimeFixed",
                column: "UsageId",
                principalTable: "UsageUserSession",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageUserSession_UserSession_UserSessionId",
                table: "UsageUserSession",
                column: "UserSessionId",
                principalTable: "UserSession",
                principalColumn: "UserSessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserOperator_CreatedById",
                table: "User",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGroup_UserOperator_CreatedById",
                table: "AppGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_AppGroup_UserOperator_ModifiedById",
                table: "AppGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_BillProfile_UserOperator_CreatedById",
                table: "BillProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_BillProfile_UserOperator_ModifiedById",
                table: "BillProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Host_UserOperator_CreatedById",
                table: "Host");

            migrationBuilder.DropForeignKey(
                name: "FK_Host_UserOperator_ModifiedById",
                table: "Host");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_UserOperator_CreatedById",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_UserOperator_ModifiedById",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Icon_UserOperator_CreatedById",
                table: "Icon");

            migrationBuilder.DropForeignKey(
                name: "FK_Icon_UserOperator_ModifiedById",
                table: "Icon");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserOperator_CreatedById",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserOperator_ModifiedById",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UserOperator_CreatedById",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UserOperator_ModifiedById",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_UserOperator_CreatedById",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_UserOperator_ModifiedById",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_UserOperator_CreatedById",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_UserOperator_ModifiedById",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBase_UserOperator_CreatedById",
                table: "ProductBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBase_UserOperator_ModifiedById",
                table: "ProductBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_UserOperator_CreatedById",
                table: "ProductGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_UserOperator_ModifiedById",
                table: "ProductGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_UserOperator_CreatedById",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_UserOperator_ModifiedById",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_UserOperator_CreatedById",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_UserOperator_ModifiedById",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_UserOperator_CreatedById",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_UserOperator_ModifiedById",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityProfile_UserOperator_CreatedById",
                table: "SecurityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityProfile_UserOperator_ModifiedById",
                table: "SecurityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_CreatedById",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_EndedById",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_ModifiedById",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_OperatorId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_UserOperator_CreatedById",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_UserOperator_ModifiedById",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserOperator_CreatedById",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_UserOperator_CreatedById",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_UserOperator_ModifiedById",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_AppGroup_AppGroupId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_AppGroup_AppGroupId",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserMember_UserId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UserMember_UserId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_UserMember_UserId",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_UserMember_UserId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_UserMember_UserId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Usage_UserMember_UserId",
                table: "Usage");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Host_HostId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBaseExtended_ProductBase_ProductId",
                table: "ProductBaseExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_ProductBase_ProductId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_ProductBase_SourceProductId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Register_RegisterId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Register_RegisterId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_Register_RegisterId",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_Register_RegisterId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Register_RegisterId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Register_RegisterId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Shift_ShiftId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Shift_ShiftId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_Shift_ShiftId",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_Shift_ShiftId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Shift_ShiftId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_ProductOrder_ProductOrderId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_ProductOrder_ProductOrderId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_PointTransaction_PointsTransactionId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineExtended_InvoiceLineProduct_BundleLineId",
                table: "InvoiceLineExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOLProduct_ProductBaseExtended_ProductId",
                table: "ProductOLProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOLExtended_ProductOLProduct_BundleLineId",
                table: "ProductOLExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_Usage_UsageSession_UsageSessionId",
                table: "Usage");

            migrationBuilder.DropTable(
                name: "AppExeCdImage");

            migrationBuilder.DropTable(
                name: "AppExeDeployment");

            migrationBuilder.DropTable(
                name: "AppExeImage");

            migrationBuilder.DropTable(
                name: "AppExeLicense");

            migrationBuilder.DropTable(
                name: "AppExeMaxUser");

            migrationBuilder.DropTable(
                name: "AppExePersonalFile");

            migrationBuilder.DropTable(
                name: "AppExeTask");

            migrationBuilder.DropTable(
                name: "AppGroupApp");

            migrationBuilder.DropTable(
                name: "AppImage");

            migrationBuilder.DropTable(
                name: "AppLink");

            migrationBuilder.DropTable(
                name: "AppRating");

            migrationBuilder.DropTable(
                name: "AppStat");

            migrationBuilder.DropTable(
                name: "AssetTransaction");

            migrationBuilder.DropTable(
                name: "AssistanceRequest");

            migrationBuilder.DropTable(
                name: "BillRatePeriodDayTime");

            migrationBuilder.DropTable(
                name: "BillRateStep");

            migrationBuilder.DropTable(
                name: "BundleProductUserPrice");

            migrationBuilder.DropTable(
                name: "ClientTask");

            migrationBuilder.DropTable(
                name: "DeploymentDeployment");

            migrationBuilder.DropTable(
                name: "DeviceHdmi");

            migrationBuilder.DropTable(
                name: "DeviceHost");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "HostEndpoint");

            migrationBuilder.DropTable(
                name: "HostGroupUserBillProfile");

            migrationBuilder.DropTable(
                name: "HostGroupWaitingLineEntry");

            migrationBuilder.DropTable(
                name: "HostLayoutGroupImage");

            migrationBuilder.DropTable(
                name: "HostLayoutGroupLayout");

            migrationBuilder.DropTable(
                name: "InvoiceFiscalReceipt");

            migrationBuilder.DropTable(
                name: "InvoiceLineSession");

            migrationBuilder.DropTable(
                name: "LicenseKey");

            migrationBuilder.DropTable(
                name: "LogException");

            migrationBuilder.DropTable(
                name: "Mapping");

            migrationBuilder.DropTable(
                name: "MonetaryUnit");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "PaymentIntentDeposit");

            migrationBuilder.DropTable(
                name: "PaymentIntentOrder");

            migrationBuilder.DropTable(
                name: "PluginLibrary");

            migrationBuilder.DropTable(
                name: "PresetTimeSale");

            migrationBuilder.DropTable(
                name: "PresetTimeSaleMoney");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductBundleUserPrice");

            migrationBuilder.DropTable(
                name: "ProductHostHidden");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductPeriodDayTime");

            migrationBuilder.DropTable(
                name: "ProductTax");

            migrationBuilder.DropTable(
                name: "ProductTimeHostDisallowed");

            migrationBuilder.DropTable(
                name: "ProductTimePeriodDayTime");

            migrationBuilder.DropTable(
                name: "ProductUserDisallowed");

            migrationBuilder.DropTable(
                name: "ProductUserPrice");

            migrationBuilder.DropTable(
                name: "RefundDepositPayment");

            migrationBuilder.DropTable(
                name: "RefundInvoicePayment");

            migrationBuilder.DropTable(
                name: "RegisterTransaction");

            migrationBuilder.DropTable(
                name: "ReservationHost");

            migrationBuilder.DropTable(
                name: "ReservationUser");

            migrationBuilder.DropTable(
                name: "SecurityProfilePolicy");

            migrationBuilder.DropTable(
                name: "SecurityProfileRestriction");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ShiftCount");

            migrationBuilder.DropTable(
                name: "TaskJunction");

            migrationBuilder.DropTable(
                name: "TaskNotification");

            migrationBuilder.DropTable(
                name: "TaskProcess");

            migrationBuilder.DropTable(
                name: "TaskScript");

            migrationBuilder.DropTable(
                name: "UsageRate");

            migrationBuilder.DropTable(
                name: "UsageTime");

            migrationBuilder.DropTable(
                name: "UsageTimeFixed");

            migrationBuilder.DropTable(
                name: "UserAgreementState");

            migrationBuilder.DropTable(
                name: "UserAttribute");

            migrationBuilder.DropTable(
                name: "UserCredential");

            migrationBuilder.DropTable(
                name: "UserCreditLimit");

            migrationBuilder.DropTable(
                name: "UserGroupHostDisallowed");

            migrationBuilder.DropTable(
                name: "UserGuest");

            migrationBuilder.DropTable(
                name: "UserNote");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "UserPicture");

            migrationBuilder.DropTable(
                name: "UserSessionChange");

            migrationBuilder.DropTable(
                name: "Variable");

            migrationBuilder.DropTable(
                name: "VerificationEmail");

            migrationBuilder.DropTable(
                name: "VerificationMobilePhone");

            migrationBuilder.DropTable(
                name: "VoidDepositPayment");

            migrationBuilder.DropTable(
                name: "VoidInvoice");

            migrationBuilder.DropTable(
                name: "PersonalFile");

            migrationBuilder.DropTable(
                name: "AppExe");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "AssistanceRequestType");

            migrationBuilder.DropTable(
                name: "BillRatePeriodDay");

            migrationBuilder.DropTable(
                name: "BundleProduct");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "HostGroupWaitingLine");

            migrationBuilder.DropTable(
                name: "HostLayoutGroup");

            migrationBuilder.DropTable(
                name: "ProductOLSession");

            migrationBuilder.DropTable(
                name: "HostComputer");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "PaymentIntent");

            migrationBuilder.DropTable(
                name: "ProductPeriodDay");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "ProductTimePeriodDay");

            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "TaskBase");

            migrationBuilder.DropTable(
                name: "InvoiceLineTime");

            migrationBuilder.DropTable(
                name: "InvoiceLineTimeFixed");

            migrationBuilder.DropTable(
                name: "UsageUserSession");

            migrationBuilder.DropTable(
                name: "UserAgreement");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "DepositPayment");

            migrationBuilder.DropTable(
                name: "Void");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "Deployment");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "BillRate");

            migrationBuilder.DropTable(
                name: "ProductBundle");

            migrationBuilder.DropTable(
                name: "ProductPeriod");

            migrationBuilder.DropTable(
                name: "ProductTimePeriod");

            migrationBuilder.DropTable(
                name: "ProductOLTime");

            migrationBuilder.DropTable(
                name: "ProductOLTimeFixed");

            migrationBuilder.DropTable(
                name: "UserSession");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "FiscalReceipt");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "AppCategory");

            migrationBuilder.DropTable(
                name: "AppEnterprise");

            migrationBuilder.DropTable(
                name: "ProductTime");

            migrationBuilder.DropTable(
                name: "DepositTransaction");

            migrationBuilder.DropTable(
                name: "UserOperator");

            migrationBuilder.DropTable(
                name: "AppGroup");

            migrationBuilder.DropTable(
                name: "UserMember");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Host");

            migrationBuilder.DropTable(
                name: "HostGroup");

            migrationBuilder.DropTable(
                name: "Icon");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "BillProfile");

            migrationBuilder.DropTable(
                name: "SecurityProfile");

            migrationBuilder.DropTable(
                name: "ProductBase");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "PointTransaction");

            migrationBuilder.DropTable(
                name: "InvoiceLineProduct");

            migrationBuilder.DropTable(
                name: "InvoiceLineExtended");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "StockTransaction");

            migrationBuilder.DropTable(
                name: "ProductBaseExtended");

            migrationBuilder.DropTable(
                name: "ProductOLProduct");

            migrationBuilder.DropTable(
                name: "ProductOLExtended");

            migrationBuilder.DropTable(
                name: "ProductOL");

            migrationBuilder.DropTable(
                name: "UsageSession");

            migrationBuilder.DropTable(
                name: "Usage");
        }
    }
}
