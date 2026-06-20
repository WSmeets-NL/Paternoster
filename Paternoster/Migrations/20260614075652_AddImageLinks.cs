using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paternoster.Migrations
{
    /// <inheritdoc />
    public partial class AddImageLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "ImageReference",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PartAmount",
                table: "ProductParts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageReference",
                table: "Parts",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageReference",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PartAmount",
                table: "ProductParts");

            migrationBuilder.DropColumn(
                name: "ImageReference",
                table: "Parts");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
