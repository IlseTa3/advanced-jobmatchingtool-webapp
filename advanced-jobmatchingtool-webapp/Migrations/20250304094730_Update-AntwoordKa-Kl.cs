using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace advanced_jobmatchingtool_webapp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAntwoordKaKl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "AntwoordenKlanten",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "AntwoordenKandidaten",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "AntwoordenKlanten");

            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "AntwoordenKandidaten");
        }
    }
}
