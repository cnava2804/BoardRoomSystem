using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class HomecalendarIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MTGR_ImageDescription",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "MTGR_ImageFile",
                table: "MeetingRooms");

            migrationBuilder.CreateTable(
                name: "HomeCalendars",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    ThemeColor = table.Column<string>(nullable: true),
                    IsFullDay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeCalendars", x => x.EventID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeCalendars");

            migrationBuilder.AddColumn<string>(
                name: "MTGR_ImageDescription",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MTGR_ImageFile",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
