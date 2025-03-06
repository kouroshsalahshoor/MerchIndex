using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchIndex.Auto.Migrations
{
    /// <inheritdoc />
    public partial class companyurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Companies");
        }
    }
}
