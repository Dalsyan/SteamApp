using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Users",
                newName: "Nickname");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Users",
                newName: "NickName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");
        }
    }
}
