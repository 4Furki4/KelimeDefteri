using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelimeDefteri.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GunlukKayitlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GunlukKayitlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kelimeler",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GunlukKayitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kelimeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kelimeler_GunlukKayitlar_GunlukKayitID",
                        column: x => x.GunlukKayitID,
                        principalTable: "GunlukKayitlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tanimlar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KelimeID = table.Column<long>(type: "bigint", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AciklamaTuru = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tanimlar_Kelimeler_KelimeID",
                        column: x => x.KelimeID,
                        principalTable: "Kelimeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GunlukKayitlar",
                columns: new[] { "Id", "date" },
                values: new object[] { 1, new DateTime(2022, 10, 8, 0, 6, 21, 374, DateTimeKind.Local).AddTicks(2569) });

            migrationBuilder.InsertData(
                table: "Kelimeler",
                columns: new[] { "Id", "GunlukKayitID", "Name" },
                values: new object[,]
                {
                    { 1L, 1, "Kelime" },
                    { 2L, 1, "Nutuk" },
                    { 3L, 1, "Hamaset" },
                    { 4L, 1, "Lafebesi" }
                });

            migrationBuilder.InsertData(
                table: "Tanimlar",
                columns: new[] { "Id", "Aciklama", "AciklamaTuru", "KelimeID" },
                values: new object[,]
                {
                    { 1L, "Anlamlı ses veya ses birliği, söz, sözcük, lügat", "isim", 1L },
                    { 2L, "Söz, konuşma", "isim", 2L },
                    { 3L, "Söylev", "isim", 2L },
                    { 4L, "Yiğitlik, kahramanlık, cesaret", "isim, eskimiş", 3L },
                    { 5L, "Dinleyenleri etkilemek veya heyecanlandırmak amacıyla yapılan abartılı anlatım", "isim, eskimiş", 3L },
                    { 6L, "Çok konuşan, herkese laf yetiştiren kimse, dil ebesi, söz ebesi", "isim, mecaz", 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kelimeler_GunlukKayitID",
                table: "Kelimeler",
                column: "GunlukKayitID");

            migrationBuilder.CreateIndex(
                name: "IX_Tanimlar_KelimeID",
                table: "Tanimlar",
                column: "KelimeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tanimlar");

            migrationBuilder.DropTable(
                name: "Kelimeler");

            migrationBuilder.DropTable(
                name: "GunlukKayitlar");
        }
    }
}
