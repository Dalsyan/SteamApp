using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class CountryAndUserGameMTM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserId",
                table: "Games");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "GameUser",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUser", x => new { x.GamesGameId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_GameUser_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Gender", "Title" },
                values: new object[,]
                {
                    { 1, "MOBA", "League of Legends" },
                    { 2, "Shooter", "Fortnite" },
                    { 3, "Rol", "Call of Cthulhu" },
                    { 4, "Simulator", "Stardew Valley" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CountryId", "Nickname" },
                values: new object[,]
                {
                    { 1, 1, "dalsyan" },
                    { 2, 1, "Tentxten" },
                    { 3, 1, "Jamonsioo" },
                    { 4, 1, "EnricDeTu" },
                    { 5, 1, "ReiSapo" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { 1, "Spain" },
                    { 2, "UK" },
                    { 3, "Italy" },
                    { 4, "USA" },
                    { 5, "China" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_UsersUserId",
                table: "GameUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_GamesGameId",
                table: "GameUser",
                column: "GamesGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "GameUser");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Nickname" },
                values: new object[,]
                {
                    { 1, "Dalsyan" },
                    { 2, "EnricDeTu" },
                    { 3, "Jamonsioo" },
                    { 4, "ReiSapo" },
                    { 5, "Tenxten" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Gender", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "MOBA", "League of Legends", 1 },
                    { 2, "Shooter", "Fortnite", 2 },
                    { 3, "Horror", "Call of Cthulhu", 3 },
                    { 4, "Farming", "Slime Rancher", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
