using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSports.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhotoColumnFromCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Coaches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Coaches",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
