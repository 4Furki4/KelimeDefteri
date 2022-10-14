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
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordID = table.Column<long>(type: "bigint", nullable: false),
                    definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    definitionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Definitions_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "date" },
                values: new object[] { 1, new DateTime(2022, 10, 14, 11, 38, 55, 749, DateTimeKind.Local).AddTicks(7044) });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "Name", "RecordId" },
                values: new object[,]
                {
                    { 1L, "Kelime", 1 },
                    { 2L, "Nutuk", 1 },
                    { 3L, "Hamaset", 1 },
                    { 4L, "Lafebesi", 1 }
                });

            migrationBuilder.InsertData(
                table: "Definitions",
                columns: new[] { "Id", "WordID", "definition", "definitionType" },
                values: new object[,]
                {
                    { 1L, 1L, "Anlamlı ses veya ses birliği, söz, sözcük, lügat", "isim" },
                    { 2L, 2L, "Söz, konuşma", "isim" },
                    { 3L, 2L, "Söylev", "isim" },
                    { 4L, 3L, "Yiğitlik, kahramanlık, cesaret", "isim, eskimiş" },
                    { 5L, 3L, "Dinleyenleri etkilemek veya heyecanlandırmak amacıyla yapılan abartılı anlatım", "isim, eskimiş" },
                    { 6L, 4L, "Çok konuşan, herkese laf yetiştiren kimse, dil ebesi, söz ebesi", "isim, mecaz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_WordID",
                table: "Definitions",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_RecordId",
                table: "Words",
                column: "RecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
