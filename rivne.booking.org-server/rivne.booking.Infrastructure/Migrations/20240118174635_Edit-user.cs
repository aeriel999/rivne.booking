using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rivne.booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Edituser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23e39638-0a35-47f6-9f67-364da0d2b877");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9ac1446-0d12-455f-ace5-da460e81ff54");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "222a546d-102f-4509-aa86-c0d7023adabb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af297070-2156-4534-a035-8b6736420070");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04b31130-9325-4226-af2f-244e8257ad0d", null, "Administrator", "ADMINISTRATOR" },
                    { "23def46f-aec5-4423-948e-d33797da64d7", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0a430f60-da00-4120-8358-30fed06082c7", 0, "30328e00-80fc-45c4-9d03-28c39ac8961d", "User", "admin@email.com", true, "Bob", "", false, null, "ADMIN@EMAIL.COM", null, null, "+xx(xxx)xxx-xx-xx", true, "9b9748aa-e571-4bab-ab35-21311836f8ae", false, "admin@email.com" },
                    { "b66dfea4-a119-4bd1-a8c0-4efdde9c84a2", 0, "4f550e59-5682-4e7d-80f8-9e4934c6f94c", "User", "user1@email.com", true, "Alice", "", false, null, "USER1@EMAIL.COM", null, null, "+xx(xxx)xxx-xx-xx", true, "e3063f59-b764-4385-91af-397d001abf61", false, "user1@email.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04b31130-9325-4226-af2f-244e8257ad0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23def46f-aec5-4423-948e-d33797da64d7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a430f60-da00-4120-8358-30fed06082c7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b66dfea4-a119-4bd1-a8c0-4efdde9c84a2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23e39638-0a35-47f6-9f67-364da0d2b877", null, "User", "USER" },
                    { "f9ac1446-0d12-455f-ace5-da460e81ff54", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "222a546d-102f-4509-aa86-c0d7023adabb", 0, "4978751b-0a36-48c8-8d1d-a0ebbdad49a2", "User", "user1@email.com", true, "Alice", "", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "0a16597a-0caa-4c39-8f76-ac1ac18657dd", false, "user1@email.com" },
                    { "af297070-2156-4534-a035-8b6736420070", 0, "09c55c55-cdb8-4697-b18a-99813e86139d", "User", "admin@email.com", true, "Bob", "", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "050b8699-b2c6-4ede-9c1b-2c7e792d9461", false, "admin@email.com" }
                });
        }
    }
}
