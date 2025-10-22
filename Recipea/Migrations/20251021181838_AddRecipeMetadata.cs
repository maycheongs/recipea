using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipea.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveTime",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalTime",
                table: "Recipes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TotalTime",
                table: "Recipes");
        }
    }
}
