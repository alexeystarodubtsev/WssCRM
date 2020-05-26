using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class fixRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stages_Name_CompanyID",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Companies_name",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AbstractPoints_name_StageID",
                table: "AbstractPoints");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Correction",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientState",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Calls",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "AbstractPoints",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_Name_CompanyID",
                table: "Stages",
                columns: new[] { "Name", "CompanyID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_name",
                table: "Companies",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbstractPoints_name_StageID",
                table: "AbstractPoints",
                columns: new[] { "name", "StageID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stages_Name_CompanyID",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Companies_name",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AbstractPoints_name_StageID",
                table: "AbstractPoints");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stages",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Correction",
                table: "Calls",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientState",
                table: "Calls",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Calls",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "AbstractPoints",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Stages_Name_CompanyID",
                table: "Stages",
                columns: new[] { "Name", "CompanyID" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_name",
                table: "Companies",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractPoints_name_StageID",
                table: "AbstractPoints",
                columns: new[] { "name", "StageID" },
                unique: true,
                filter: "[name] IS NOT NULL");
        }
    }
}
