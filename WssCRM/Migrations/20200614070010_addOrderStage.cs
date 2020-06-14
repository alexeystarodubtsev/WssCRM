using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class addOrderStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "Stages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Num",
                table: "Stages");
        }
    }
}
