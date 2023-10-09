using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_states",
                table: "states");

            migrationBuilder.RenameTable(
                name: "states",
                newName: "States");

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "states");

            migrationBuilder.AddPrimaryKey(
                name: "PK_states",
                table: "states",
                column: "Id");
        }
    }
}
