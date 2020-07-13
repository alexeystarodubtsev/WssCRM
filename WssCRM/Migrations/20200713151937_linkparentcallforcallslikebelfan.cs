using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class linkparentcallforcallslikebelfan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCallID",
                table: "Calls",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calls_ParentCallID",
                table: "Calls",
                column: "ParentCallID");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Calls_ParentCallID",
                table: "Calls",
                column: "ParentCallID",
                principalTable: "Calls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Calls_ParentCallID",
                table: "Calls");

            migrationBuilder.DropIndex(
                name: "IX_Calls_ParentCallID",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "ParentCallID",
                table: "Calls");
        }
    }
}
