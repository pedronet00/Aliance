using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGUIDOnWorshipTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorshipTeamRehearsals_Guid",
                table: "WorshipTeamRehearsals");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "WorshipTeamRehearsals",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(UUID())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "WorshipTeamMember",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(UUID())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "WorshipTeam",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "(UUID())",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "WorshipTeamRehearsals",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(UUID())")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "WorshipTeamMember",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(UUID())")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "WorshipTeam",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(UUID())")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_WorshipTeamRehearsals_Guid",
                table: "WorshipTeamRehearsals",
                column: "Guid",
                unique: true);
        }
    }
}
