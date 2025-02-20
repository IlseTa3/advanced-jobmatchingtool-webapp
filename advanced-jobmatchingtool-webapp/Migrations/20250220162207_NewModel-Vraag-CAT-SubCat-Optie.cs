using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace advanced_jobmatchingtool_webapp.Migrations
{
    /// <inheritdoc />
    public partial class NewModelVraagCATSubCatOptie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AntwoordOpties_SoortAntwoorden_SoortAntwoordId",
                table: "AntwoordOpties");

            migrationBuilder.DropForeignKey(
                name: "FK_AntwoordOpties_Vragenlijsten_VragenlijstId",
                table: "AntwoordOpties");

            migrationBuilder.DropTable(
                name: "AntwoordKandidaten");

            migrationBuilder.DropTable(
                name: "AntwoordKlanten");

            migrationBuilder.DropTable(
                name: "Vragenlijsten");

            migrationBuilder.DropTable(
                name: "SoortAntwoorden");

            migrationBuilder.DropIndex(
                name: "IX_AntwoordOpties_SoortAntwoordId",
                table: "AntwoordOpties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategorieLijst",
                table: "SubCategorieLijst");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorieLijst",
                table: "CategorieLijst");

            migrationBuilder.DropColumn(
                name: "SoortAntwoordId",
                table: "AntwoordOpties");

            migrationBuilder.RenameTable(
                name: "SubCategorieLijst",
                newName: "SubCategorieën");

            migrationBuilder.RenameTable(
                name: "CategorieLijst",
                newName: "Categorieën");

            migrationBuilder.RenameColumn(
                name: "VragenlijstId",
                table: "AntwoordOpties",
                newName: "VraagId");

            migrationBuilder.RenameIndex(
                name: "IX_AntwoordOpties_VragenlijstId",
                table: "AntwoordOpties",
                newName: "IX_AntwoordOpties_VraagId");

            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "SubCategorieën",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategorieën",
                table: "SubCategorieën",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorieën",
                table: "Categorieën",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Vragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VraagText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    SubCategorieId = table.Column<int>(type: "int", nullable: true),
                    SoortAntwoord = table.Column<int>(type: "int", nullable: false),
                    GewichtsScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vragen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vragen_Categorieën_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorieën",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vragen_SubCategorieën_SubCategorieId",
                        column: x => x.SubCategorieId,
                        principalTable: "SubCategorieën",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorieën_CategorieId",
                table: "SubCategorieën",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragen_CategorieId",
                table: "Vragen",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragen_SubCategorieId",
                table: "Vragen",
                column: "SubCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AntwoordOpties_Vragen_VraagId",
                table: "AntwoordOpties",
                column: "VraagId",
                principalTable: "Vragen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategorieën_Categorieën_CategorieId",
                table: "SubCategorieën",
                column: "CategorieId",
                principalTable: "Categorieën",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AntwoordOpties_Vragen_VraagId",
                table: "AntwoordOpties");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategorieën_Categorieën_CategorieId",
                table: "SubCategorieën");

            migrationBuilder.DropTable(
                name: "Vragen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategorieën",
                table: "SubCategorieën");

            migrationBuilder.DropIndex(
                name: "IX_SubCategorieën_CategorieId",
                table: "SubCategorieën");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorieën",
                table: "Categorieën");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "SubCategorieën");

            migrationBuilder.RenameTable(
                name: "SubCategorieën",
                newName: "SubCategorieLijst");

            migrationBuilder.RenameTable(
                name: "Categorieën",
                newName: "CategorieLijst");

            migrationBuilder.RenameColumn(
                name: "VraagId",
                table: "AntwoordOpties",
                newName: "VragenlijstId");

            migrationBuilder.RenameIndex(
                name: "IX_AntwoordOpties_VraagId",
                table: "AntwoordOpties",
                newName: "IX_AntwoordOpties_VragenlijstId");

            migrationBuilder.AddColumn<int>(
                name: "SoortAntwoordId",
                table: "AntwoordOpties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategorieLijst",
                table: "SubCategorieLijst",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorieLijst",
                table: "CategorieLijst",
                column: "Id");

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
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    SoortAntwoordId = table.Column<int>(type: "int", nullable: true),
                    SubCategorieId = table.Column<int>(type: "int", nullable: true),
                    GewichtsScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    VraagText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VragenlijstId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VragenlijstId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_AntwoordOpties_SoortAntwoordId",
                table: "AntwoordOpties",
                column: "SoortAntwoordId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AntwoordOpties_SoortAntwoorden_SoortAntwoordId",
                table: "AntwoordOpties",
                column: "SoortAntwoordId",
                principalTable: "SoortAntwoorden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AntwoordOpties_Vragenlijsten_VragenlijstId",
                table: "AntwoordOpties",
                column: "VragenlijstId",
                principalTable: "Vragenlijsten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
