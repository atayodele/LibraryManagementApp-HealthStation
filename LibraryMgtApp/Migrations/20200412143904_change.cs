using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryMgtApp.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OverDueAmount",
                table: "Checkout",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "Checkout",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5869ab93-81da-419b-b5ad-41a7bc82cae8"),
                column: "ConcurrencyStamp",
                value: "8d3b63385bd94f7aae9db3f6c2c57b3b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e4410972-f20a-4d07-afdb-c61550e3dd44"),
                column: "ConcurrencyStamp",
                value: "8aa6ca80aed8442fb2d90dcaa123082c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979"),
                columns: new[] { "ConcurrencyStamp", "CreatedOnUtc", "PasswordHash" },
                values: new object[] { "fbc92671-5317-472d-9b60-9596c8e43fac", new DateTime(2020, 4, 12, 15, 39, 3, 964, DateTimeKind.Local), "AQAAAAEAACcQAAAAEF2/Sp4fnfQbRNrJ8+rHqqNrpl0LLWXOSDePUL2yefxV0xjeG5bpKySGFhJsHo03jg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f"),
                columns: new[] { "ConcurrencyStamp", "CreatedOnUtc", "PasswordHash" },
                values: new object[] { "3ed3752e-3713-4e67-b1b2-de1dee675098", new DateTime(2020, 4, 12, 15, 39, 3, 982, DateTimeKind.Local), "AQAAAAEAACcQAAAAEH8NnyH0CpYwNu2FB1lCEzUW+1nVYsaUaTQgrT5DNOvNXZdwxCANBrkUDI3WnBzqrQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OverDueAmount",
                table: "Checkout",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "Checkout",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

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
    }
}
