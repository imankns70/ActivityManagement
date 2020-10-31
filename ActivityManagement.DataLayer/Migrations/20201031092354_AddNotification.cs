using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivityManagement.DataLayer.Migrations
{
    public partial class AddNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "CONVERT(datetime,GetDate())"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "CONVERT(datetime,GetDate())"),
                    EnterEmail = table.Column<bool>(nullable: false),
                    EnterSms = table.Column<bool>(nullable: false),
                    EnterTelegram = table.Column<bool>(nullable: false),
                    ExitEmail = table.Column<bool>(nullable: false),
                    ExitSms = table.Column<bool>(nullable: false),
                    ExitTelegram = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
