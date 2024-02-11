using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabyBackend.Migrations
{
    /// <inheritdoc />
    public partial class setdefaultvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "processing",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "processing");
        }
    }
}
