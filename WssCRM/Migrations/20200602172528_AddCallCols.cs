using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class AddCallCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateNext",
                table: "Calls",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfClose",
                table: "Calls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "correctioncolor",
                table: "Calls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateNext",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "DateOfClose",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "correctioncolor",
                table: "Calls");
        }
    }
}
