using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class updated_product_photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsThumbnail",
                table: "ProductPhoto",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsThumbnail",
                table: "ProductPhoto");
        }
    }
}
