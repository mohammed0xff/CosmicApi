using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class multibletokensforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId1",
                table: "Tokens");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId1",
                table: "Tokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_Name",
                table: "Pictures",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId1",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_Name",
                table: "Pictures");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId1",
                table: "Tokens",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");
        }
    }
}
