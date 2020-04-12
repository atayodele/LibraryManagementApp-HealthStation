using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryMgtApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "StatusMode",
                table: "Book",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5869ab93-81da-419b-b5ad-41a7bc82cae8"),
                column: "ConcurrencyStamp",
                value: "df786600e8e14aed83db3a71915f0416");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e4410972-f20a-4d07-afdb-c61550e3dd44"),
                column: "ConcurrencyStamp",
                value: "76ca0a7f131645448f2ea69f2b6ba48e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979"),
                columns: new[] { "ConcurrencyStamp", "CreatedOnUtc", "PasswordHash" },
                values: new object[] { "afe51527-031f-49dc-bb57-b686d481b043", new DateTime(2020, 4, 12, 13, 41, 56, 857, DateTimeKind.Local), "AQAAAAEAACcQAAAAEDNwyESIixL1WYV3ctCng9+UOtPyj0R7E3b/Me+otkDUGeXX6MjOqsQkOgWHDviwGQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f"),
                columns: new[] { "ConcurrencyStamp", "CreatedOnUtc", "PasswordHash" },
                values: new object[] { "408736e6-1490-4a3a-ba8b-870f6ad27a9b", new DateTime(2020, 4, 12, 13, 41, 56, 886, DateTimeKind.Local), "AQAAAAEAACcQAAAAED2FLKQOO7bJslD5vVyoQYwhraevg4EyMelyO7FyaLOyoO6JGWKRdm7tuHMNLFDYew==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusMode",
                table: "Book");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5869ab93-81da-419b-b5ad-41a7bc82cae8"),
                column: "ConcurrencyStamp",
                value: "29784dfffd734aa18ec8d96d2bec0d3a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e4410972-f20a-4d07-afdb-c61550e3dd44"),
                column: "ConcurrencyStamp",
                value: "0b2b3f2cec1a46689d13e29be19e7b17");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979"),
                columns: new[] { "ConcurrencyStamp", "CreatedOnUtc", "PasswordHash" },
                values: new object[] { "bac83f34-e314-45e0-87a4-7c0883d0f78b", new DateTime(2020, 4, 12, 4, 25, 57, 159, DateTimeKind.Local), "AQAAAAEAACcQAAAAEJrVU/JXosfor3j5ogEqhsrVYBHjEzLn9B56PLaeoriIdYq5p4MwQwXuZkDOluUNdg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f"),
                columns: new[] { "ConcurrencyStamp", "CreatedOnUtc", "PasswordHash" },
                values: new object[] { "114883f9-1cec-44ee-82e3-0a413cc0de33", new DateTime(2020, 4, 12, 4, 25, 57, 176, DateTimeKind.Local), "AQAAAAEAACcQAAAAENlD4zqzRZu8+pjUOPRa/kBhcMAKp2BHZ+AYV9hoLhmRuOaKFN+XuuWg65LG6zo7dw==" });
        }
    }
}
