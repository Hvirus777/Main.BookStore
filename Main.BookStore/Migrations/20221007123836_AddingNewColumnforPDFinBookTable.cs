using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.BookStore.Migrations
{
    public partial class AddingNewColumnforPDFinBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookPdfUrl",
                table: "books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookPdfUrl",
                table: "books");
        }
    }
}
