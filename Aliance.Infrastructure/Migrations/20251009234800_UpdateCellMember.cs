using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCellMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellMember_Cell_CellId",
                table: "CellMember");

            migrationBuilder.DropIndex(
                name: "IX_CellMember_CellId",
                table: "CellMember");

            migrationBuilder.DropColumn(
                name: "CellId",
                table: "CellMember");

            migrationBuilder.AddColumn<Guid>(
                name: "CellGuid",
                table: "CellMember",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_CellMember_CellGuid",
                table: "CellMember",
                column: "CellGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CellMember_Cell_CellGuid",
                table: "CellMember",
                column: "CellGuid",
                principalTable: "Cell",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellMember_Cell_CellGuid",
                table: "CellMember");

            migrationBuilder.DropIndex(
                name: "IX_CellMember_CellGuid",
                table: "CellMember");

            migrationBuilder.DropColumn(
                name: "CellGuid",
                table: "CellMember");

            migrationBuilder.AddColumn<int>(
                name: "CellId",
                table: "CellMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CellMember_CellId",
                table: "CellMember",
                column: "CellId");

            migrationBuilder.AddForeignKey(
                name: "FK_CellMember_Cell_CellId",
                table: "CellMember",
                column: "CellId",
                principalTable: "Cell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
