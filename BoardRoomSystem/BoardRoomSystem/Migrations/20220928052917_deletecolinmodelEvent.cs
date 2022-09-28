using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class deletecolinmodelEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeColor",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThemeColor",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
