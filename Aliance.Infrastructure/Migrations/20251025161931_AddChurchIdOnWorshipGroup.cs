using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChurchIdOnWorshipGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "WorshipTeam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorshipTeam_ChurchId",
                table: "WorshipTeam",
                column: "ChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorshipTeam_Church_ChurchId",
                table: "WorshipTeam",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorshipTeam_Church_ChurchId",
                table: "WorshipTeam");

            migrationBuilder.DropIndex(
                name: "IX_WorshipTeam_ChurchId",
                table: "WorshipTeam");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "WorshipTeam");
        }
    }
}
