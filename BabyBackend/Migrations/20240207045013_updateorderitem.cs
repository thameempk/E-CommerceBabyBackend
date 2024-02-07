using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabyBackend.Migrations
{
    /// <inheritdoc />
    public partial class updateorderitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCity",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "OrderString",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "orderItems");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCity",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderString",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCity",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderString",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCity",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderString",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
