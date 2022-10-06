using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.BookStore.Migrations
{
    public partial class Added2Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "books",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "books");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "books");
        }
    }
}
