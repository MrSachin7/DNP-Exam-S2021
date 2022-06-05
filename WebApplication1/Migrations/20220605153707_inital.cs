using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ShirtNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Salary = table.Column<decimal>(type: "TEXT", nullable: false),
                    GoalsThisSeason = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => new { x.Name, x.ShirtNumber });
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamName = table.Column<string>(type: "TEXT", nullable: false),
                    NameOfCoach = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Ranking = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
