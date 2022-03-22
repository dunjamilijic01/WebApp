using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat_WEB.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KantinaID",
                table: "Meni",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meni_KantinaID",
                table: "Meni",
                column: "KantinaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meni_Kantina_KantinaID",
                table: "Meni",
                column: "KantinaID",
                principalTable: "Kantina",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meni_Kantina_KantinaID",
                table: "Meni");

            migrationBuilder.DropIndex(
                name: "IX_Meni_KantinaID",
                table: "Meni");

            migrationBuilder.DropColumn(
                name: "KantinaID",
                table: "Meni");
        }
    }
}
