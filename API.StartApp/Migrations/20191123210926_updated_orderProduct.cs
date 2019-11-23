using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class updated_orderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderProduct",
                newName: "UnitPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderProduct",
                newName: "Price");
        }
    }
}
