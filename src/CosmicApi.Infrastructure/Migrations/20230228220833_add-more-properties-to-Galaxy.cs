using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmorepropertiestoGalaxy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AbsoluteMagnitude",
                table: "Galaxies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "Galaxies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EscapeVelocity",
                table: "Galaxies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfStars",
                table: "Galaxies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Radius",
                table: "Galaxies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsoluteMagnitude",
                table: "Galaxies");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Galaxies");

            migrationBuilder.DropColumn(
                name: "EscapeVelocity",
                table: "Galaxies");

            migrationBuilder.DropColumn(
                name: "NumberOfStars",
                table: "Galaxies");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Galaxies");
        }
    }
}
