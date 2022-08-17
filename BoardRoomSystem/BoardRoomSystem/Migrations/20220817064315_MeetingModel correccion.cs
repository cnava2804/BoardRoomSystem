using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class MeetingModelcorreccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MTGR_Image",
                table: "MeetingRooms",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "MTGR_Image",
                table: "MeetingRooms",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
