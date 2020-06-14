using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class addstages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "agreementStage",
                table: "Stages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "incomeStage",
                table: "Stages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "preAgreementStage",
                table: "Stages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "agreementStage",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "incomeStage",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "preAgreementStage",
                table: "Stages");
        }
    }
}
