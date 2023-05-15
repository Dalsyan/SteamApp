using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class OnlinePremiumColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasOnline",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Premium",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 1,
                column: "Premium",
                value: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 2,
                column: "Premium",
                value: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 3,
                column: "Premium",
                value: false);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 4,
                column: "Premium",
                value: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "EmailId",
                keyValue: 5,
                column: "Premium",
                value: false);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                column: "HasOnline",
                value: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2,
                column: "HasOnline",
                value: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 3,
                column: "HasOnline",
                value: false);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 4,
                column: "HasOnline",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasOnline",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Premium",
                table: "Accounts");
        }
    }
}
