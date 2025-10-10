using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateCellTableAndAddRelationshipsToChurch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "Location",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "Baptism",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cell",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LeaderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MeetingDay = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CellBanner = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChurchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cell_AspNetUsers_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cell_Church_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Church",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cell_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ChurchId",
                table: "Location",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ChurchId",
                table: "Event",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ChurchId",
                table: "Department",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Baptism_ChurchId",
                table: "Baptism",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_ChurchId",
                table: "Cell",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_LeaderId",
                table: "Cell",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_LocationId",
                table: "Cell",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baptism_Church_ChurchId",
                table: "Baptism",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Church_ChurchId",
                table: "Department",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Church_ChurchId",
                table: "Event",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Church_ChurchId",
                table: "Location",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baptism_Church_ChurchId",
                table: "Baptism");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Church_ChurchId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Church_ChurchId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Church_ChurchId",
                table: "Location");

            migrationBuilder.DropTable(
                name: "Cell");

            migrationBuilder.DropIndex(
                name: "IX_Location_ChurchId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Event_ChurchId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Department_ChurchId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Baptism_ChurchId",
                table: "Baptism");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "Baptism");
        }
    }
}
