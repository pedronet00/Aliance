using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGUIDOnCellsMeetings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellMeeting_AspNetUsers_LeaderId",
                table: "CellMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_CellMeeting_Cell_CellId",
                table: "CellMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_CellMeeting_Location_LocationId",
                table: "CellMeeting");

            migrationBuilder.DropIndex(
                name: "IX_CellMeeting_CellId",
                table: "CellMeeting");

            migrationBuilder.DropIndex(
                name: "IX_CellMeeting_LocationId",
                table: "CellMeeting");

            migrationBuilder.DropColumn(
                name: "CellId",
                table: "CellMeeting");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CellMeeting");

            migrationBuilder.RenameColumn(
                name: "LeaderId",
                table: "CellMeeting",
                newName: "LeaderGuid");

            migrationBuilder.RenameIndex(
                name: "IX_CellMeeting_LeaderId",
                table: "CellMeeting",
                newName: "IX_CellMeeting_LeaderGuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CellGuid",
                table: "CellMeeting",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationGuid",
                table: "CellMeeting",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Location_Guid",
                table: "Location",
                column: "Guid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cell_Guid",
                table: "Cell",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_CellMeeting_CellGuid",
                table: "CellMeeting",
                column: "CellGuid");

            migrationBuilder.CreateIndex(
                name: "IX_CellMeeting_LocationGuid",
                table: "CellMeeting",
                column: "LocationGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CellMeeting_AspNetUsers_LeaderGuid",
                table: "CellMeeting",
                column: "LeaderGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CellMeeting_Cell_CellGuid",
                table: "CellMeeting",
                column: "CellGuid",
                principalTable: "Cell",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CellMeeting_Location_LocationGuid",
                table: "CellMeeting",
                column: "LocationGuid",
                principalTable: "Location",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellMeeting_AspNetUsers_LeaderGuid",
                table: "CellMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_CellMeeting_Cell_CellGuid",
                table: "CellMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_CellMeeting_Location_LocationGuid",
                table: "CellMeeting");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Location_Guid",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_CellMeeting_CellGuid",
                table: "CellMeeting");

            migrationBuilder.DropIndex(
                name: "IX_CellMeeting_LocationGuid",
                table: "CellMeeting");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cell_Guid",
                table: "Cell");

            migrationBuilder.DropColumn(
                name: "CellGuid",
                table: "CellMeeting");

            migrationBuilder.DropColumn(
                name: "LocationGuid",
                table: "CellMeeting");

            migrationBuilder.RenameColumn(
                name: "LeaderGuid",
                table: "CellMeeting",
                newName: "LeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_CellMeeting_LeaderGuid",
                table: "CellMeeting",
                newName: "IX_CellMeeting_LeaderId");

            migrationBuilder.AddColumn<int>(
                name: "CellId",
                table: "CellMeeting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "CellMeeting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CellMeeting_CellId",
                table: "CellMeeting",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_CellMeeting_LocationId",
                table: "CellMeeting",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CellMeeting_AspNetUsers_LeaderId",
                table: "CellMeeting",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CellMeeting_Cell_CellId",
                table: "CellMeeting",
                column: "CellId",
                principalTable: "Cell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CellMeeting_Location_LocationId",
                table: "CellMeeting",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
