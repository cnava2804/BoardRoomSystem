using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class CreationInitial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Reservation_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reservation_Subject = table.Column<string>(nullable: true),
                    Reservation_Location = table.Column<string>(nullable: true),
                    Reservation_Recipient = table.Column<string>(nullable: true),
                    Reservation_StartDate = table.Column<DateTime>(nullable: false),
                    Reservation_EndtDate = table.Column<DateTime>(nullable: false),
                    Reservation_NumbPeople = table.Column<int>(nullable: false),
                    Reservation_Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Reservation_Delegate = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Reservation_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
