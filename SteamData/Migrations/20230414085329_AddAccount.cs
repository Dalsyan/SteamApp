using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class AddAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_Account_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "EmailId", "CreationDate", "Email", "Password", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dalsyan@email.com", "0001", 1 },
                    { 2, new DateTime(2000, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "tentxten@email.com", "0002", 2 },
                    { 3, new DateTime(2000, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "jamonsioo@email.com", "0003", 3 },
                    { 4, new DateTime(2001, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "enricdetu@email.com", "0004", 4 },
                    { 5, new DateTime(2000, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "reisapo@email.com", "0005", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
