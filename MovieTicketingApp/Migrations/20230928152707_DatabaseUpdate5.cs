using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class DatabseUpdate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Movies",
                newName: "LanguageTe");

            migrationBuilder.AddColumn<string>(
                name: "LanguageEn",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageHi",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageEn",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LanguageHi",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "LanguageTe",
                table: "Movies",
                newName: "Language");
        }
    }
}
