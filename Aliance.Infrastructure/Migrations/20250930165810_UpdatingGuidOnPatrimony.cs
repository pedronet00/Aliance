﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingGuidOnPatrimony : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadedAt",
                table: "PatrimonyDocuments",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "PatrimonyDocuments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "NEWID()")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "Patrimony",
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
            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadedAt",
                table: "PatrimonyDocuments",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "PatrimonyDocuments",
                type: "char(36)",
                nullable: false,
                defaultValueSql: "NEWID()",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "Patrimony",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValueSql: "(UUID())")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
