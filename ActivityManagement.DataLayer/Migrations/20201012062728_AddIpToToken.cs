using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivityManagement.DataLayer.Migrations
{
    public partial class AddIpToToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "RefreshTokens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "RefreshTokens");
        }
    }
}
