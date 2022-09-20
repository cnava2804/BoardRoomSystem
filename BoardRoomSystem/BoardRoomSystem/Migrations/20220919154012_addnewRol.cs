using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRoomSystem.Migrations
{
    public partial class addnewRol : Migration
    {
        private string SuperAdminRoleId = Guid.NewGuid().ToString();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{SuperAdminRoleId}', 'SuperAdmin', 'SUPERADMIN', null);");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
