using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat_WEB.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "kantinaID",
                table: "Radnik",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kantina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKantine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kantina", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Radnik_kantinaID",
                table: "Radnik",
                column: "kantinaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Radnik_Kantina_kantinaID",
                table: "Radnik",
                column: "kantinaID",
                principalTable: "Kantina",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Radnik_Kantina_kantinaID",
                table: "Radnik");

            migrationBuilder.DropTable(
                name: "Kantina");

            migrationBuilder.DropIndex(
                name: "IX_Radnik_kantinaID",
                table: "Radnik");

            migrationBuilder.DropColumn(
                name: "kantinaID",
                table: "Radnik");
        }
    }
}
