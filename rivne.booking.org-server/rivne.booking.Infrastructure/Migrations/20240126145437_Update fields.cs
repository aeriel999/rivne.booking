using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rivne.booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Is",
                table: "Apartments",
                newName: "IsBooked");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfUpdate",
                table: "Apartments",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfUpdate",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "IsBooked",
                table: "Apartments",
                newName: "Is");
        }
    }
}
