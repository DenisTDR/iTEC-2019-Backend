using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class buyer_seller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    LocationX = table.Column<float>(nullable: false),
                    LocationY = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerProfile",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    AddressId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerProfile_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyerProfile_AuthUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AuthUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerProfile",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    TargetType = table.Column<int>(nullable: false),
                    AddressId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerProfile_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_Id",
                table: "Address",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProfile_AddressId",
                table: "BuyerProfile",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProfile_Id",
                table: "BuyerProfile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerProfile_UserId",
                table: "BuyerProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfile_AddressId",
                table: "SellerProfile",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfile_Id",
                table: "SellerProfile",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerProfile");

            migrationBuilder.DropTable(
                name: "SellerProfile");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
