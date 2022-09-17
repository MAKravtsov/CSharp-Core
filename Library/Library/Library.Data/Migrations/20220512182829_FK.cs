using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                schema: "dbo",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                schema: "dbo",
                table: "Books",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId",
                schema: "dbo",
                table: "Books",
                column: "GenreId",
                principalSchema: "dbo",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId",
                schema: "dbo",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_GenreId",
                schema: "dbo",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "GenreId",
                schema: "dbo",
                table: "Books");
        }
    }
}
