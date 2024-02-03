using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabyBackend.Migrations
{
    /// <inheritdoc />
    public partial class WhishList2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "whishLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_whishLists_ProductId1",
                table: "whishLists",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_whishLists_products_ProductId1",
                table: "whishLists",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_whishLists_products_ProductId1",
                table: "whishLists");

            migrationBuilder.DropIndex(
                name: "IX_whishLists_ProductId1",
                table: "whishLists");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "whishLists");
        }
    }
}
