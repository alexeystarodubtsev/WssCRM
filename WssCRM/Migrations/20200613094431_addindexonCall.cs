using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class addindexonCall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Correction",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Calls_Date_ClientName_Correction_duration_ManagerID_StageID",
                table: "Calls",
                columns: new[] { "Date", "ClientName", "Correction", "duration", "ManagerID", "StageID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calls_Date_ClientName_Correction_duration_ManagerID_StageID",
                table: "Calls");

            migrationBuilder.AlterColumn<string>(
                name: "Correction",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
