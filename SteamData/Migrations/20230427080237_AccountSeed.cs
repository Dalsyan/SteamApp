using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class AccountSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "EmailId", "CountryId", "CreationDate", "Email", "Password", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2000, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dalsyan@email.com", "0001", 1 },
                    { 2, 1, new DateTime(2000, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "tentxten@email.com", "0002", 2 },
                    { 3, 1, new DateTime(2000, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "jamonsioo@email.com", "0003", 3 },
                    { 4, 1, new DateTime(2001, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "enricdetu@email.com", "0004", 4 },
                    { 5, 1, new DateTime(2000, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "reisapo@email.com", "0005", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 5);
        }
    }
}
