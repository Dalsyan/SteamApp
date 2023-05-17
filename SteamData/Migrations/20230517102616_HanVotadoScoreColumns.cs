using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class HanVotadoScoreColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HanVotado",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                });

            migrationBuilder.CreateTable(
                name: "GameVote",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    VotesVoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVote", x => new { x.GamesGameId, x.VotesVoteId });
                    table.ForeignKey(
                        name: "FK_GameVote_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameVote_Votes_VotesVoteId",
                        column: x => x.VotesVoteId,
                        principalTable: "Votes",
                        principalColumn: "VoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVote",
                columns: table => new
                {
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    VotesVoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVote", x => new { x.UsersUserId, x.VotesVoteId });
                    table.ForeignKey(
                        name: "FK_UserVote_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVote_Votes_VotesVoteId",
                        column: x => x.VotesVoteId,
                        principalTable: "Votes",
                        principalColumn: "VoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                columns: new[] { "HanVotado", "Score" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2,
                columns: new[] { "HanVotado", "Score" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 3,
                columns: new[] { "HanVotado", "Score" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 4,
                columns: new[] { "HanVotado", "Score" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.CreateIndex(
                name: "IX_GameVote_VotesVoteId",
                table: "GameVote",
                column: "VotesVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVote_VotesVoteId",
                table: "UserVote",
                column: "VotesVoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameVote");

            migrationBuilder.DropTable(
                name: "UserVote");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropColumn(
                name: "HanVotado",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Games");
        }
    }
}
