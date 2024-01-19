﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rivne.booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}