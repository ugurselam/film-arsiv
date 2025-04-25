using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Film_Arsiv.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFilmModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "Films",
                newName: "imdbID");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Runtime",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Writer",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Director",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Writer",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "imdbID",
                table: "Films",
                newName: "PosterUrl");
        }
    }
}
