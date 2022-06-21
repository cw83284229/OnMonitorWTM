using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnMonitor.DataAccess.Migrations
{
    public partial class createCameralayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.CreateTable(
                name: "CameraLayouts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    monitorRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Build = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    ExcelDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayoutInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayoutInfoPDFId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraLayouts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CameraLayouts_FileAttachments_ExcelDataId",
                        column: x => x.ExcelDataId,
                        principalTable: "FileAttachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CameraLayouts_FileAttachments_LayoutInfoId",
                        column: x => x.LayoutInfoId,
                        principalTable: "FileAttachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CameraLayouts_FileAttachments_LayoutInfoPDFId",
                        column: x => x.LayoutInfoPDFId,
                        principalTable: "FileAttachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CameraLayouts_MonitorRooms_monitorRoomId",
                        column: x => x.monitorRoomId,
                        principalTable: "MonitorRooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CameraLayouts_ExcelDataId",
                table: "CameraLayouts",
                column: "ExcelDataId");

            migrationBuilder.CreateIndex(
                name: "IX_CameraLayouts_LayoutInfoId",
                table: "CameraLayouts",
                column: "LayoutInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CameraLayouts_LayoutInfoPDFId",
                table: "CameraLayouts",
                column: "LayoutInfoPDFId");

            migrationBuilder.CreateIndex(
                name: "IX_CameraLayouts_monitorRoomId",
                table: "CameraLayouts",
                column: "monitorRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraLayouts");
          
        }
    }
}
