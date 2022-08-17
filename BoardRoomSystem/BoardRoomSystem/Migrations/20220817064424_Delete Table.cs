using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class DeleteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_Buildings_BuildingsBuilding_Id",
                table: "MeetingRooms");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_BuildingsBuilding_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "BuildingsBuilding_Id",
                table: "MeetingRooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingsBuilding_Id",
                table: "MeetingRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Building_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building_Name = table.Column<string>(type: "nvarchar(70)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Building_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_BuildingsBuilding_Id",
                table: "MeetingRooms",
                column: "BuildingsBuilding_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_Buildings_BuildingsBuilding_Id",
                table: "MeetingRooms",
                column: "BuildingsBuilding_Id",
                principalTable: "Buildings",
                principalColumn: "Building_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
