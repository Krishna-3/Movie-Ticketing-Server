using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Movies",
                newName: "DescriptionTe");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionHi",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DescriptionHi",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "DescriptionTe",
                table: "Movies",
                newName: "Description");
        }
    }
}
