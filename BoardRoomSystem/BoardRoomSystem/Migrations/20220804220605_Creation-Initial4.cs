using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class CreationInitial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reservation_Location",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "Reservation_Delegate",
                table: "Reservations",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Area_Id",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Location_id",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MTGR_Id",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Building_Id",
                table: "MeetingRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MTGR_NumbRoom",
                table: "MeetingRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Area_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area_Name = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Area_Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Building_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building_Name = table.Column<string>(type: "nvarchar(70)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Building_Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Location_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location_Name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Location_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Area_Id",
                table: "Reservations",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Location_id",
                table: "Reservations",
                column: "Location_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_MTGR_Id",
                table: "Reservations",
                column: "MTGR_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_Building_Id",
                table: "MeetingRooms",
                column: "Building_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_Buildings_Building_Id",
                table: "MeetingRooms",
                column: "Building_Id",
                principalTable: "Buildings",
                principalColumn: "Building_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Areas_Area_Id",
                table: "Reservations",
                column: "Area_Id",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Location_Location_id",
                table: "Reservations",
                column: "Location_id",
                principalTable: "Location",
                principalColumn: "Location_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_MeetingRooms_MTGR_Id",
                table: "Reservations",
                column: "MTGR_Id",
                principalTable: "MeetingRooms",
                principalColumn: "MTGR_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_Buildings_Building_Id",
                table: "MeetingRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Areas_Area_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Location_Location_id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_MeetingRooms_MTGR_Id",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Area_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Location_id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_MTGR_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_MeetingRooms_Building_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Area_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Location_id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "MTGR_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Building_Id",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "MTGR_NumbRoom",
                table: "MeetingRooms");

            migrationBuilder.AlterColumn<string>(
                name: "Reservation_Delegate",
                table: "Reservations",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reservation_Location",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
