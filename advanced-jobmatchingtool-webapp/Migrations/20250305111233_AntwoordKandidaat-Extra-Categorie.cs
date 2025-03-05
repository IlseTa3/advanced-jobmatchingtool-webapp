using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace advanced_jobmatchingtool_webapp.Migrations
{
    /// <inheritdoc />
    public partial class AntwoordKandidaatExtraCategorie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categorie",
                table: "AntwoordenKandidaten",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumIngevuld",
                table: "AntwoordenKandidaten",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categorie",
                table: "AntwoordenKandidaten");

            migrationBuilder.DropColumn(
                name: "DatumIngevuld",
                table: "AntwoordenKandidaten");
        }
    }
}
