using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class DevSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Devs",
                columns: new[] { "DevId", "CompanyId", "CountryId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1, 1, "Pau", "Vidal" },
                    { 2, 1, 1, "Ana", "Pérez" },
                    { 3, 2, 1, "Enric", "Puigcerver" },
                    { 4, 4, 1, "Ivan", "Fullana" },
                    { 5, 2, 3, "Mario", "Valencia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devs",
                keyColumn: "DevId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Devs",
                keyColumn: "DevId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Devs",
                keyColumn: "DevId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Devs",
                keyColumn: "DevId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Devs",
                keyColumn: "DevId",
                keyValue: 5);
        }
    }
}
