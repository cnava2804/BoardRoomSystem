using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class CreationInitial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Areas_Area_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Location_Location_id",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Location_Id");

            migrationBuilder.CreateTable(
                name: "AreasViewModels",
                columns: table => new
                {
                    Area_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area_Name = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasViewModels", x => x.Area_Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AreasViewModels_Area_Id",
                table: "Reservations",
                column: "Area_Id",
                principalTable: "AreasViewModels",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Locations_Location_id",
                table: "Reservations",
                column: "Location_id",
                principalTable: "Locations",
                principalColumn: "Location_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AreasViewModels_Area_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Locations_Location_id",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "AreasViewModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Location_Id");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Area_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area_Name = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Area_Id);
                });

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
        }
    }
}
