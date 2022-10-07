using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.BookStore.Migrations
{
    public partial class AddedNewColumnToBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "books");
        }
    }
}
