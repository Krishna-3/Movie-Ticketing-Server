using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "States",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_States_UserId",
                table: "States",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Users_UserId",
                table: "States",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_UserId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_UserId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "States");
        }
    }
}
