using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paternoster.Migrations
{
    /// <inheritdoc />
    public partial class PartContainerFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_PaternosterContainers_ContainerId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ContainerId",
                table: "Parts");

            migrationBuilder.CreateIndex(
                name: "IX_PaternosterContainers_PartId",
                table: "PaternosterContainers",
                column: "PartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaternosterContainers_Parts_PartId",
                table: "PaternosterContainers",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaternosterContainers_Parts_PartId",
                table: "PaternosterContainers");

            migrationBuilder.DropIndex(
                name: "IX_PaternosterContainers_PartId",
                table: "PaternosterContainers");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ContainerId",
                table: "Parts",
                column: "ContainerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_PaternosterContainers_ContainerId",
                table: "Parts",
                column: "ContainerId",
                principalTable: "PaternosterContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
