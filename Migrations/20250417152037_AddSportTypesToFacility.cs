using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSports.Migrations
{
    /// <inheritdoc />
    public partial class AddSportTypesToFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Coaches_CoachId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_SportFacilities_SportFacilityId1",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_SportFacilities_SportFacilityId2",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SportFacilityId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SportFacilityId2",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "AvailableDate",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SportFacilityId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SportFacilityId2",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "SportFacilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasEquipment",
                table: "SportFacilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLighting",
                table: "SportFacilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLockerRooms",
                table: "SportFacilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParking",
                table: "SportFacilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasShower",
                table: "SportFacilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasStands",
                table: "SportFacilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "SportFacilities",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "SportFacilities",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string[]>(
                name: "SportTypes",
                table: "SportFacilities",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "SurfaceType",
                table: "SportFacilities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "SportFacilities",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Coaches_CoachId",
                table: "Schedules",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Coaches_CoachId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "HasEquipment",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "HasLighting",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "HasLockerRooms",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "HasParking",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "HasShower",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "HasStands",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "SportTypes",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "SurfaceType",
                table: "SportFacilities");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "SportFacilities");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableDate",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SportFacilityId1",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SportFacilityId2",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SportFacilityId1",
                table: "Schedules",
                column: "SportFacilityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SportFacilityId2",
                table: "Schedules",
                column: "SportFacilityId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Coaches_CoachId",
                table: "Schedules",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_SportFacilities_SportFacilityId1",
                table: "Schedules",
                column: "SportFacilityId1",
                principalTable: "SportFacilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_SportFacilities_SportFacilityId2",
                table: "Schedules",
                column: "SportFacilityId2",
                principalTable: "SportFacilities",
                principalColumn: "Id");
        }
    }
}
