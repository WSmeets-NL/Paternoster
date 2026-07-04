using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paternoster.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageReference",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageReference",
                table: "Parts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageReference",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageReference",
                table: "Parts",
                type: "TEXT",
                nullable: true);
        }
    }
}
