using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameTel",
                table: "Theatres",
                newName: "NameTe");

            migrationBuilder.RenameColumn(
                name: "NameHin",
                table: "Theatres",
                newName: "NameHi");

            migrationBuilder.RenameColumn(
                name: "NameTel",
                table: "Movies",
                newName: "TitleTe");

            migrationBuilder.RenameColumn(
                name: "NameHin",
                table: "Movies",
                newName: "TitleHi");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Movies",
                newName: "TitleEn");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "NameTe",
                table: "Theatres",
                newName: "NameTel");

            migrationBuilder.RenameColumn(
                name: "NameHi",
                table: "Theatres",
                newName: "NameHin");

            migrationBuilder.RenameColumn(
                name: "TitleTe",
                table: "Movies",
                newName: "NameTel");

            migrationBuilder.RenameColumn(
                name: "TitleHi",
                table: "Movies",
                newName: "NameHin");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "Movies",
                newName: "NameEn");
        }
    }
}
