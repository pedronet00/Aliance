using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPatrimonyMaintenanceCostCenterAndCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostCenterId",
                table: "PatrimonyMaintenance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaintenanceCost",
                table: "PatrimonyMaintenance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_PatrimonyMaintenance_CostCenterId",
                table: "PatrimonyMaintenance",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatrimonyMaintenance_CostCenter_CostCenterId",
                table: "PatrimonyMaintenance",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatrimonyMaintenance_CostCenter_CostCenterId",
                table: "PatrimonyMaintenance");

            migrationBuilder.DropIndex(
                name: "IX_PatrimonyMaintenance_CostCenterId",
                table: "PatrimonyMaintenance");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "PatrimonyMaintenance");

            migrationBuilder.DropColumn(
                name: "MaintenanceCost",
                table: "PatrimonyMaintenance");
        }
    }
}
