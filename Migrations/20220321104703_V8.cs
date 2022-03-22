using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat_WEB.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Dan",
                table: "Meni",
                type: "int",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dan",
                table: "Meni",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 3);
        }
    }
}
