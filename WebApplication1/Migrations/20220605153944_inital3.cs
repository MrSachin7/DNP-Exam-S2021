using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class inital3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Players",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamName",
                table: "Players",
                column: "TeamName");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamName",
                table: "Players",
                column: "TeamName",
                principalTable: "Teams",
                principalColumn: "TeamName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamName",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Players");
        }
    }
}
