using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchIndex.Auto.Migrations
{
    /// <inheritdoc />
    public partial class ProductManifacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manifacturer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manifacturer",
                table: "Products");
        }
    }
}
