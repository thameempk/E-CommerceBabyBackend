using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabyBackend.Migrations
{
    /// <inheritdoc />
    public partial class WhishList1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_whishList_Users_UserId",
                table: "whishList");

            migrationBuilder.DropForeignKey(
                name: "FK_whishList_products_ProductId",
                table: "whishList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_whishList",
                table: "whishList");

            migrationBuilder.RenameTable(
                name: "whishList",
                newName: "whishLists");

            migrationBuilder.RenameIndex(
                name: "IX_whishList_UserId",
                table: "whishLists",
                newName: "IX_whishLists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_whishList_ProductId",
                table: "whishLists",
                newName: "IX_whishLists_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_whishLists",
                table: "whishLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_whishLists_Users_UserId",
                table: "whishLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_whishLists_products_ProductId",
                table: "whishLists",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_whishLists_Users_UserId",
                table: "whishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_whishLists_products_ProductId",
                table: "whishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_whishLists",
                table: "whishLists");

            migrationBuilder.RenameTable(
                name: "whishLists",
                newName: "whishList");

            migrationBuilder.RenameIndex(
                name: "IX_whishLists_UserId",
                table: "whishList",
                newName: "IX_whishList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_whishLists_ProductId",
                table: "whishList",
                newName: "IX_whishList_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_whishList",
                table: "whishList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_whishList_Users_UserId",
                table: "whishList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_whishList_products_ProductId",
                table: "whishList",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
