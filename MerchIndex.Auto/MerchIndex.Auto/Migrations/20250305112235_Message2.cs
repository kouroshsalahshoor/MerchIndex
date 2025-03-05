using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchIndex.Auto.Migrations
{
    /// <inheritdoc />
    public partial class Message2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnsweredOn",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnsweredOn",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Messages");
        }
    }
}
