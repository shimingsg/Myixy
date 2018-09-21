using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Myixy.App.Data.Migrations
{
    public partial class CreateLineSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedDatetime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Heartfelt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lines");
        }
    }
}
