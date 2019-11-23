using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class updatedbuyerseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgMetadata");

            migrationBuilder.RenameColumn(
                name: "LocationY",
                table: "Address",
                newName: "LocationLong");

            migrationBuilder.RenameColumn(
                name: "LocationX",
                table: "Address",
                newName: "LocationLat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationLong",
                table: "Address",
                newName: "LocationY");

            migrationBuilder.RenameColumn(
                name: "LocationLat",
                table: "Address",
                newName: "LocationX");

            migrationBuilder.CreateTable(
                name: "OgMetadata",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgMetadata_File_ImageId",
                        column: x => x.ImageId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OgMetadata_Id",
                table: "OgMetadata",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OgMetadata_ImageId",
                table: "OgMetadata",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_OgMetadata_Slug",
                table: "OgMetadata",
                column: "Slug");
        }
    }
}
