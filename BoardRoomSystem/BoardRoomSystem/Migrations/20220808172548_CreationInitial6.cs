using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardRoomSystem.Migrations
{
    public partial class CreationInitial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AreasViewModels_Area_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Locations_Location_id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_MeetingRooms_MTGR_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Area_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_MTGR_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Area_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "MTGR_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Location_id",
                table: "Reservations",
                newName: "Location_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_Location_id",
                table: "Reservations",
                newName: "IX_Reservations_Location_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Location_Id",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AreasViewModelArea_Id",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeetingRoomsMTGR_Id",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reservations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AplicationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AreasViewModelArea_Id",
                table: "Reservations",
                column: "AreasViewModelArea_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_MeetingRoomsMTGR_Id",
                table: "Reservations",
                column: "MeetingRoomsMTGR_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AreasViewModels_AreasViewModelArea_Id",
                table: "Reservations",
                column: "AreasViewModelArea_Id",
                principalTable: "AreasViewModels",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Locations_Location_Id",
                table: "Reservations",
                column: "Location_Id",
                principalTable: "Locations",
                principalColumn: "Location_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_MeetingRooms_MeetingRoomsMTGR_Id",
                table: "Reservations",
                column: "MeetingRoomsMTGR_Id",
                principalTable: "MeetingRooms",
                principalColumn: "MTGR_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AreasViewModels_AreasViewModelArea_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Locations_Location_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_MeetingRooms_MeetingRoomsMTGR_Id",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "AplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_AreasViewModelArea_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_MeetingRoomsMTGR_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AreasViewModelArea_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "MeetingRoomsMTGR_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Location_Id",
                table: "Reservations",
                newName: "Location_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_Location_Id",
                table: "Reservations",
                newName: "IX_Reservations_Location_id");

            migrationBuilder.AlterColumn<int>(
                name: "Location_id",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Area_Id",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MTGR_Id",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Area_Id",
                table: "Reservations",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_MTGR_Id",
                table: "Reservations",
                column: "MTGR_Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_MeetingRooms_MTGR_Id",
                table: "Reservations",
                column: "MTGR_Id",
                principalTable: "MeetingRooms",
                principalColumn: "MTGR_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
