using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipea.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSourceUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Recipes",
                type: "TEXT",
                nullable: true);
        }
    }
}
