using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class order_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Order",
                nullable: false,
                defaultValue: false);
        }
    }
}
