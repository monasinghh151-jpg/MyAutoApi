using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAutoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationAndLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Catalog",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Catalog",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Catalog");
        }
    }
}
