using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class updated_buyer_seller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SellerProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SellerProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BuyerProfile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfile_UserId",
                table: "SellerProfile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProfile_AuthUser_UserId",
                table: "SellerProfile",
                column: "UserId",
                principalTable: "AuthUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProfile_AuthUser_UserId",
                table: "SellerProfile");

            migrationBuilder.DropIndex(
                name: "IX_SellerProfile_UserId",
                table: "SellerProfile");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SellerProfile");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SellerProfile");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BuyerProfile");
        }
    }
}
