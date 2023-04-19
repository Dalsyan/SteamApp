using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamData.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Users_UserId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Developer_Companies_CompanyId",
                table: "Developer");

            migrationBuilder.DropForeignKey(
                name: "FK_Developer_Countries_CountryId",
                table: "Developer");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperGame_Developer_DevelopersDevId",
                table: "DeveloperGame");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_Companies_CompanyId",
                table: "Server");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_Countries_CountryId",
                table: "Server");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_Games_GameId",
                table: "Server");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Server",
                table: "Server");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Developer",
                table: "Developer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Server",
                newName: "Servers");

            migrationBuilder.RenameTable(
                name: "Developer",
                newName: "Devs");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Server_GameId",
                table: "Servers",
                newName: "IX_Servers_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Server_CountryId",
                table: "Servers",
                newName: "IX_Servers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Server_CompanyId",
                table: "Servers",
                newName: "IX_Servers_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Developer_CountryId",
                table: "Devs",
                newName: "IX_Devs_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Developer_CompanyId",
                table: "Devs",
                newName: "IX_Devs_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_UserId",
                table: "Accounts",
                newName: "IX_Accounts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servers",
                table: "Servers",
                column: "ServerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devs",
                table: "Devs",
                column: "DevId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperGame_Devs_DevelopersDevId",
                table: "DeveloperGame",
                column: "DevelopersDevId",
                principalTable: "Devs",
                principalColumn: "DevId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devs_Companies_CompanyId",
                table: "Devs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devs_Countries_CountryId",
                table: "Devs",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Companies_CompanyId",
                table: "Servers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Countries_CountryId",
                table: "Servers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Games_GameId",
                table: "Servers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperGame_Devs_DevelopersDevId",
                table: "DeveloperGame");

            migrationBuilder.DropForeignKey(
                name: "FK_Devs_Companies_CompanyId",
                table: "Devs");

            migrationBuilder.DropForeignKey(
                name: "FK_Devs_Countries_CountryId",
                table: "Devs");

            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Companies_CompanyId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Countries_CountryId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Games_GameId",
                table: "Servers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servers",
                table: "Servers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devs",
                table: "Devs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Servers",
                newName: "Server");

            migrationBuilder.RenameTable(
                name: "Devs",
                newName: "Developer");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_GameId",
                table: "Server",
                newName: "IX_Server_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_CountryId",
                table: "Server",
                newName: "IX_Server_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_CompanyId",
                table: "Server",
                newName: "IX_Server_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Devs_CountryId",
                table: "Developer",
                newName: "IX_Developer_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Devs_CompanyId",
                table: "Developer",
                newName: "IX_Developer_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserId",
                table: "Account",
                newName: "IX_Account_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Server",
                table: "Server",
                column: "ServerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Developer",
                table: "Developer",
                column: "DevId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Users_UserId",
                table: "Account",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_Companies_CompanyId",
                table: "Developer",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_Countries_CountryId",
                table: "Developer",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperGame_Developer_DevelopersDevId",
                table: "DeveloperGame",
                column: "DevelopersDevId",
                principalTable: "Developer",
                principalColumn: "DevId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Companies_CompanyId",
                table: "Server",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Countries_CountryId",
                table: "Server",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Games_GameId",
                table: "Server",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
