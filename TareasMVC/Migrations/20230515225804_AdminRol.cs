using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    public partial class AdminRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT Id FROM AspNetRoles where id ='2f8add94-9e9d-4f7d-aa8b-73832ba806dd')
BEGIN
INSERT AspNetRoles (Id,[Name],[NormalizedName])
VALUES ('2f8add94-9e9d-4f7d-aa8b-73832ba806dd','Admin','ADMIN')
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles WHERE Id = '2f8add94-9e9d-4f7d-aa8b-73832ba806dd';");
        }
    }
}
