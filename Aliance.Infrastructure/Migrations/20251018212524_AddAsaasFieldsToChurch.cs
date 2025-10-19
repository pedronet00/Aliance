using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAsaasFieldsToChurch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AsaasCustomerId",
                table: "Church",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillingType",
                table: "Church",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateActivated",
                table: "Church",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCanceled",
                table: "Church",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentDate",
                table: "Church",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextDueDate",
                table: "Church",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Church",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PlanValue",
                table: "Church",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionId",
                table: "Church",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsaasCustomerId",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "BillingType",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "DateActivated",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "DateCanceled",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "LastPaymentDate",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "NextDueDate",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "PlanValue",
                table: "Church");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Church");
        }
    }
}
