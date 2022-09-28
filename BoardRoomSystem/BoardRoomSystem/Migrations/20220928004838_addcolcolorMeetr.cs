using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class addcolcolorMeetr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThemeColorMeetR",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeColorMeetR",
                table: "MeetingRooms");
        }
    }
}
