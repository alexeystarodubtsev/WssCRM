using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class adddeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "Stages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "Managers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "AbstractPoints",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "AbstractPoints");
        }
    }
}
