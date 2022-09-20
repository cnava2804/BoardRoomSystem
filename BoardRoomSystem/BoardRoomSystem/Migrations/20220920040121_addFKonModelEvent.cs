using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class addFKonModelEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdArea",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_IdArea",
                table: "Events",
                column: "IdArea");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AreasViewModels_IdArea",
                table: "Events",
                column: "IdArea",
                principalTable: "AreasViewModels",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AreasViewModels_IdArea",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_IdArea",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IdArea",
                table: "Events");
        }
    }
}
