using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat_WEB.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jelo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivJela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Restoran = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jelo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Radnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Meni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dan = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    JeloNaMenijuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Meni_Jelo_JeloNaMenijuID",
                        column: x => x.JeloNaMenijuID,
                        principalTable: "Jelo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JeloPorudzbineID = table.Column<int>(type: "int", nullable: false),
                    PreuzetaPorudzbina = table.Column<bool>(type: "bit", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PorudzbinaZaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Porudzbina_Jelo_JeloPorudzbineID",
                        column: x => x.JeloPorudzbineID,
                        principalTable: "Jelo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Porudzbina_Radnik_PorudzbinaZaID",
                        column: x => x.PorudzbinaZaID,
                        principalTable: "Radnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meni_JeloNaMenijuID",
                table: "Meni",
                column: "JeloNaMenijuID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_JeloPorudzbineID",
                table: "Porudzbina",
                column: "JeloPorudzbineID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_PorudzbinaZaID",
                table: "Porudzbina",
                column: "PorudzbinaZaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meni");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Jelo");

            migrationBuilder.DropTable(
                name: "Radnik");
        }
    }
}
