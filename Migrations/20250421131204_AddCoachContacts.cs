using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSports.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Coaches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "Coaches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhatsApp",
                table: "Coaches",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "WhatsApp",
                table: "Coaches");
        }
    }
}
