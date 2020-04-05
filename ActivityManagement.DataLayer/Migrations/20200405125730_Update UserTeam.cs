using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivityManagement.DataLayer.Migrations
{
    public partial class UpdateUserTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentTeam",
                table: "UserTeam",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeader",
                table: "UserTeam",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentTeam",
                table: "UserTeam");

            migrationBuilder.DropColumn(
                name: "IsLeader",
                table: "UserTeam");
        }
    }
}
