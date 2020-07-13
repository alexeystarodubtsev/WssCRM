using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class trymakeunicode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Correction",
                table: "Calls",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Correction",
                table: "Calls",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ntext");
        }
    }
}
