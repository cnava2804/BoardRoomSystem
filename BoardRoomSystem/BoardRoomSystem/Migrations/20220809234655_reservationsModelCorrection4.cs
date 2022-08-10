using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class reservationsModelCorrection4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_Buildings_Building_Id",
                table: "MeetingRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_States_State_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_Building_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_State_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Building_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "State_Id",
                table: "MeetingRooms");

            migrationBuilder.AddColumn<int>(
                name: "BuildingsBuilding_Id",
                table: "MeetingRooms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatesState_Id",
                table: "MeetingRooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_BuildingsBuilding_Id",
                table: "MeetingRooms",
                column: "BuildingsBuilding_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_StatesState_Id",
                table: "MeetingRooms",
                column: "StatesState_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_Buildings_BuildingsBuilding_Id",
                table: "MeetingRooms",
                column: "BuildingsBuilding_Id",
                principalTable: "Buildings",
                principalColumn: "Building_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_States_StatesState_Id",
                table: "MeetingRooms",
                column: "StatesState_Id",
                principalTable: "States",
                principalColumn: "State_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_Buildings_BuildingsBuilding_Id",
                table: "MeetingRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_States_StatesState_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_BuildingsBuilding_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_StatesState_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "BuildingsBuilding_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "StatesState_Id",
                table: "MeetingRooms");

            migrationBuilder.AddColumn<int>(
                name: "Building_Id",
                table: "MeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State_Id",
                table: "MeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_Building_Id",
                table: "MeetingRooms",
                column: "Building_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_State_Id",
                table: "MeetingRooms",
                column: "State_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_Buildings_Building_Id",
                table: "MeetingRooms",
                column: "Building_Id",
                principalTable: "Buildings",
                principalColumn: "Building_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_States_State_Id",
                table: "MeetingRooms",
                column: "State_Id",
                principalTable: "States",
                principalColumn: "State_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
