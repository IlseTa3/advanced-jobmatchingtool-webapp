using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace advanced_jobmatchingtool_webapp.Migrations
{
    /// <inheritdoc />
    public partial class SetupModelVragenAntwoorden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoortAntwoorden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NaamSoortAntwoord = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoortAntwoorden", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vragenlijsten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VraagText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    SubCategorieId = table.Column<int>(type: "int", nullable: true),
                    SoortAntwoordId = table.Column<int>(type: "int", nullable: true),
                    GewichtsScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vragenlijsten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vragenlijsten_CategorieLijst_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "CategorieLijst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vragenlijsten_SoortAntwoorden_SoortAntwoordId",
                        column: x => x.SoortAntwoordId,
                        principalTable: "SoortAntwoorden",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vragenlijsten_SubCategorieLijst_SubCategorieId",
                        column: x => x.SubCategorieId,
                        principalTable: "SubCategorieLijst",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AntwoordKandidaten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VragenlijstId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Antwoord = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntwoordKandidaten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntwoordKandidaten_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntwoordKandidaten_Vragenlijsten_VragenlijstId",
                        column: x => x.VragenlijstId,
                        principalTable: "Vragenlijsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AntwoordKlanten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VragenlijstId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Antwoord = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GewichtsScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntwoordKlanten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntwoordKlanten_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntwoordKlanten_Vragenlijsten_VragenlijstId",
                        column: x => x.VragenlijstId,
                        principalTable: "Vragenlijsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AntwoordOpties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SoortAntwoordId = table.Column<int>(type: "int", nullable: false),
                    OptieTekst = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VragenlijstId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntwoordOpties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntwoordOpties_SoortAntwoorden_SoortAntwoordId",
                        column: x => x.SoortAntwoordId,
                        principalTable: "SoortAntwoorden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntwoordOpties_Vragenlijsten_VragenlijstId",
                        column: x => x.VragenlijstId,
                        principalTable: "Vragenlijsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordKandidaten_UserId",
                table: "AntwoordKandidaten",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordKandidaten_VragenlijstId",
                table: "AntwoordKandidaten",
                column: "VragenlijstId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordKlanten_UserId",
                table: "AntwoordKlanten",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordKlanten_VragenlijstId",
                table: "AntwoordKlanten",
                column: "VragenlijstId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordOpties_SoortAntwoordId",
                table: "AntwoordOpties",
                column: "SoortAntwoordId");

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordOpties_VragenlijstId",
                table: "AntwoordOpties",
                column: "VragenlijstId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragenlijsten_CategorieId",
                table: "Vragenlijsten",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragenlijsten_SoortAntwoordId",
                table: "Vragenlijsten",
                column: "SoortAntwoordId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragenlijsten_SubCategorieId",
                table: "Vragenlijsten",
                column: "SubCategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntwoordKandidaten");

            migrationBuilder.DropTable(
                name: "AntwoordKlanten");

            migrationBuilder.DropTable(
                name: "AntwoordOpties");

            migrationBuilder.DropTable(
                name: "Vragenlijsten");

            migrationBuilder.DropTable(
                name: "SoortAntwoorden");
        }
    }
}
