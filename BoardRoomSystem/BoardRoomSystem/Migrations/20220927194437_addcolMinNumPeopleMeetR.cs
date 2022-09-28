using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class addcolMinNumPeopleMeetR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinNumbPeopleMeetR",
                table: "MeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinNumbPeopleMeetR",
                table: "MeetingRooms");
        }
    }
}
