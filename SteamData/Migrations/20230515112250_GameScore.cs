using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class GameScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Games",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreV);
                });

            migrationBuilder.CreateTable(
                name: "GameScore",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    ScoresScoreV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameScore", x => new { x.GamesGameId, x.ScoresScoreV });
                    table.ForeignKey(
                        name: "FK_GameScore_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameScore_Scores_ScoresScoreV",
                        column: x => x.ScoresScoreV,
                        principalTable: "Scores",
                        principalColumn: "ScoreV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreUser",
                columns: table => new
                {
                    ScoresScoreV = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreUser", x => new { x.ScoresScoreV, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_ScoreUser_Scores_ScoresScoreV",
                        column: x => x.ScoresScoreV,
                        principalTable: "Scores",
                        principalColumn: "ScoreV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                column: "Score",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2,
                column: "Score",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 3,
                column: "Score",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 4,
                column: "Score",
                value: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_GameScore_ScoresScoreV",
                table: "GameScore",
                column: "ScoresScoreV");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreUser_UsersUserId",
                table: "ScoreUser",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameScore");

            migrationBuilder.DropTable(
                name: "ScoreUser");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Games");
        }
    }
}
