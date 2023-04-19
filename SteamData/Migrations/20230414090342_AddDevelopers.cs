using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class AddDevelopers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    DevId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.DevId);
                    table.ForeignKey(
                        name: "FK_Developer_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Developer_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperGame",
                columns: table => new
                {
                    DevelopersDevId = table.Column<int>(type: "int", nullable: false),
                    GamesGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperGame", x => new { x.DevelopersDevId, x.GamesGameId });
                    table.ForeignKey(
                        name: "FK_DeveloperGame_Developer_DevelopersDevId",
                        column: x => x.DevelopersDevId,
                        principalTable: "Developer",
                        principalColumn: "DevId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperGame_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Developer_CompanyId",
                table: "Developer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Developer_CountryId",
                table: "Developer",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperGame_GamesGameId",
                table: "DeveloperGame",
                column: "GamesGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperGame");

            migrationBuilder.DropTable(
                name: "Developer");
        }
    }
}
