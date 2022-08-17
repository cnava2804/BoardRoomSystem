using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class CreationInitialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_States_StatesState_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_StatesState_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "MTGR_Location",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "StatesState_Id",
                table: "MeetingRooms");

            migrationBuilder.AlterColumn<string>(
                name: "state_Name",
                table: "States",
                type: "nvarchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AddColumn<int>(
                name: "Location_Id",
                table: "MeetingRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State_Id",
                table: "MeetingRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_Location_Id",
                table: "MeetingRooms",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_State_Id",
                table: "MeetingRooms",
                column: "State_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_Locations_Location_Id",
                table: "MeetingRooms",
                column: "Location_Id",
                principalTable: "Locations",
                principalColumn: "Location_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_States_State_Id",
                table: "MeetingRooms",
                column: "State_Id",
                principalTable: "States",
                principalColumn: "State_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_Locations_Location_Id",
                table: "MeetingRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_States_State_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_Location_Id",
                table: "MeetingRooms");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_State_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Location_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "State_Id",
                table: "MeetingRooms");

            migrationBuilder.AlterColumn<string>(
                name: "state_Name",
                table: "States",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MTGR_Location",
                table: "MeetingRooms",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatesState_Id",
                table: "MeetingRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_StatesState_Id",
                table: "MeetingRooms",
                column: "StatesState_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_States_StatesState_Id",
                table: "MeetingRooms",
                column: "StatesState_Id",
                principalTable: "States",
                principalColumn: "State_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
