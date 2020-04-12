using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryMgtApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AuthorName = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checkout",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CheckoutDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    CheckInDate = table.Column<DateTime>(nullable: false),
                    OverDueAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkout", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.UniqueConstraint("AK_UserLogins_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Activated = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    NIN = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    ISBN = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    PublichYear = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCheckout",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CheckoutId = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCheckout", x => new { x.BookId, x.CheckoutId });
                    table.ForeignKey(
                        name: "FK_BookCheckout_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCheckout_Checkout_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5869ab93-81da-419b-b5ad-41a7bc82cae8"), "29784dfffd734aa18ec8d96d2bec0d3a", "ADMIN", "ADMIN" },
                    { new Guid("e4410972-f20a-4d07-afdb-c61550e3dd44"), "0b2b3f2cec1a46689d13e29be19e7b17", "USER", "USER" }
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "AU_01", "201", new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979") },
                    { 2, "AU_02", "202", new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979") },
                    { 3, "AU_03", "203", new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979") },
                    { 4, "AU_04", "204", new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979") },
                    { 5, "AU_05", "205", new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979") },
                    { 6, "PU_01", "301", new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f") },
                    { 7, "PU_02", "302", new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f") },
                    { 8, "PU_03", "303", new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f") },
                    { 9, "PU_04", "304", new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f") },
                    { 10, "PU_05", "305", new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979"), new Guid("5869ab93-81da-419b-b5ad-41a7bc82cae8") },
                    { new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f"), new Guid("e4410972-f20a-4d07-afdb-c61550e3dd44") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Activated", "ConcurrencyStamp", "CreatedOnUtc", "Email", "EmailConfirmed", "FirstName", "FullName", "Gender", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NIN", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("129712e3-9214-4dd3-9c03-cfc4eb9ba979"), 0, true, "bac83f34-e314-45e0-87a4-7c0883d0f78b", new DateTime(2020, 4, 12, 4, 25, 57, 159, DateTimeKind.Local), "admin@gmail.com", true, "Timilehin", "Timilehin Ayodele", null, false, "Ayodele", false, null, "73279-40572", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEJrVU/JXosfor3j5ogEqhsrVYBHjEzLn9B56PLaeoriIdYq5p4MwQwXuZkDOluUNdg==", "07032369247", true, "953d3fd1-99e3-4fe7-a20d-3598baa96099", false, "admin@gmail.com" },
                    { new Guid("193a9488-ad75-41d6-a3e0-db3f10b6468f"), 0, true, "114883f9-1cec-44ee-82e3-0a413cc0de33", new DateTime(2020, 4, 12, 4, 25, 57, 176, DateTimeKind.Local), "user@gmail.com", true, "Ayeni", "Ayeni Ayodele", null, false, "Ayodele", false, null, "67354-46571", "USER@GMAIL.COM", "USER@GMAIL.COM", "AQAAAAEAACcQAAAAENlD4zqzRZu8+pjUOPRa/kBhcMAKp2BHZ+AYV9hoLhmRuOaKFN+XuuWg65LG6zo7dw==", "07032367234", true, "9c41c8cc-b489-40c6-bbcf-12edee681919", false, "user@gmail.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCheckout_CheckoutId",
                table: "BookCheckout",
                column: "CheckoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCheckout");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Checkout");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
