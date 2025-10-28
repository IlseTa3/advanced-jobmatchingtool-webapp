using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace advanced_jobmatchingtool_webapp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProfileComplete",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TermsCond",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileComplete",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TermsCond",
                table: "AspNetUsers");
        }
    }
}
