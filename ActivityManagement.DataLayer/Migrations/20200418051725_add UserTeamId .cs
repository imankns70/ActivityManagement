using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivityManagement.DataLayer.Migrations
{
    public partial class addUserTeamId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam");

            migrationBuilder.AddColumn<int>(
                name: "UserTeamId",
                table: "UserTeam",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam",
                column: "UserTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_UserId",
                table: "UserTeam",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam");

            migrationBuilder.DropIndex(
                name: "IX_UserTeam_UserId",
                table: "UserTeam");

            migrationBuilder.DropColumn(
                name: "UserTeamId",
                table: "UserTeam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam",
                columns: new[] { "UserId", "TeamId" });
        }
    }
}
