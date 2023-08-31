using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb1.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Genre", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Stephen King", "A scary book.", "Horror", "The Shining", 1977 },
                    { 2, "David Baldacci", "It's about a winner.", "Thriller", "The Winner", 1997 },
                    { 3, "A.A. Milne", "It's about a bear.", "Children", "Winnie The Pooh", 1926 },
                    { 4, "Douglas Adams", "A funny book.", "Science Fiction", "The Hitchhicker's Guide to the Galaxy", 1979 },
                    { 5, "J.R.R. Tolkien", "It's about a ring.", "Fantasy", "The Lord of the Rings", 1954 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
