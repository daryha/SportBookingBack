using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSports.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhotoColumnFromSportFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "SportFacilities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "SportFacilities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
