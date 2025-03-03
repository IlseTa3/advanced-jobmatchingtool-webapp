using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace advanced_jobmatchingtool_webapp.Migrations
{
    /// <inheritdoc />
    public partial class Antwoordenkandidatenklanten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntwoordenKandidaten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VraagKandidaatId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AntwoordTekst = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntwoordenKandidaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntwoordenKandidaten_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AntwoordenKandidaten_VragenKandidaten_VraagKandidaatId",
                        column: x => x.VraagKandidaatId,
                        principalTable: "VragenKandidaten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AntwoordenKlanten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VraagKlantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AntwoordTekst = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntwoordenKlanten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntwoordenKlanten_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AntwoordenKlanten_VragenKlanten_VraagKlantId",
                        column: x => x.VraagKlantId,
                        principalTable: "VragenKlanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordenKandidaten_UserId",
                table: "AntwoordenKandidaten",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordenKandidaten_VraagKandidaatId",
                table: "AntwoordenKandidaten",
                column: "VraagKandidaatId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordenKlanten_UserId",
                table: "AntwoordenKlanten",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordenKlanten_VraagKlantId",
                table: "AntwoordenKlanten",
                column: "VraagKlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntwoordenKandidaten");

            migrationBuilder.DropTable(
                name: "AntwoordenKlanten");
        }
    }
}
