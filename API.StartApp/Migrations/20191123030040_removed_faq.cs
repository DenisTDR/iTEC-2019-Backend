using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.StartApp.Migrations
{
    public partial class removed_faq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faq");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faq",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    Category = table.Column<string>(maxLength: 256, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faq", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faq_Id",
                table: "Faq",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faq_OrderIndex",
                table: "Faq",
                column: "OrderIndex");
        }
    }
}
