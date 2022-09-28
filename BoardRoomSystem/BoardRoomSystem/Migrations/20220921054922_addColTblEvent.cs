using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class addColTblEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfPeople",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfPeople",
                table: "Events");
        }
    }
}
