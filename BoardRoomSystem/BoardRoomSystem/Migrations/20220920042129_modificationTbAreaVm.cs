using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class modificationTbAreaVm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area_Name",
                table: "AreasViewModels",
                newName: "NameArea");

            migrationBuilder.RenameColumn(
                name: "Area_Id",
                table: "AreasViewModels",
                newName: "IdArea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameArea",
                table: "AreasViewModels",
                newName: "Area_Name");

            migrationBuilder.RenameColumn(
                name: "IdArea",
                table: "AreasViewModels",
                newName: "Area_Id");
        }
    }
}
