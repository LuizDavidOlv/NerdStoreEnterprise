using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Identidade.API.Migrations
{
    public partial class RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "RefreshToken",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "RefreshToken",
                newName: "UserName");
        }
    }
}
