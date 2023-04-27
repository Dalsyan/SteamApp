using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class ServerSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "ServerId", "CompanyId", "CountryId", "GameId", "ServerName" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, "Riot 1" },
                    { 2, 1, 4, 1, "Riot 2" },
                    { 3, 4, 4, 2, "Epic 1" },
                    { 4, 2, 4, 4, "Stardew 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "ServerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "ServerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "ServerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "ServerId",
                keyValue: 4);
        }
    }
}
