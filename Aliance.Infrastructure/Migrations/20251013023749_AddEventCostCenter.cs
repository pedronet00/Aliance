using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEventCostCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostCenterId",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Event_CostCenterId",
                table: "Event",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_CostCenter_CostCenterId",
                table: "Event",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_CostCenter_CostCenterId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_CostCenterId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "Event");
        }
    }
}
