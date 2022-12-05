using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorusanServices.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Malzeme",
                columns: table => new
                {
                    MalzemeKodu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MalzemeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malzeme", x => x.MalzemeKodu);
                });

            migrationBuilder.CreateTable(
                name: "Siparis",
                columns: table => new
                {
                    SistemSiparisNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriSiparisNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CikisAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarisAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miktar = table.Column<int>(type: "int", nullable: false),
                    MiktarBirim = table.Column<int>(type: "int", nullable: false),
                    Agirlik = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgirlikBirim = table.Column<int>(type: "int", nullable: false),
                    MalzemeKodu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Not = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiparisDurum = table.Column<int>(type: "int", nullable: false),
                    DegisimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparis", x => x.SistemSiparisNo);
                    table.ForeignKey(
                        name: "FK_Siparis_Malzeme_MalzemeKodu",
                        column: x => x.MalzemeKodu,
                        principalTable: "Malzeme",
                        principalColumn: "MalzemeKodu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_MalzemeKodu",
                table: "Siparis",
                column: "MalzemeKodu");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_MusteriSiparisNo",
                table: "Siparis",
                column: "MusteriSiparisNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Siparis");

            migrationBuilder.DropTable(
                name: "Malzeme");
        }
    }
}
