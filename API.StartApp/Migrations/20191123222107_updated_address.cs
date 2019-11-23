using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class updated_address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationLong",
                table: "Address",
                newName: "LocationLng");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationLng",
                table: "Address",
                newName: "LocationLong");
        }
    }
}
