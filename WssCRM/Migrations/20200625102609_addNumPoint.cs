using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class addNumPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbstractPoints_name_StageID",
                table: "AbstractPoints");

            migrationBuilder.AddColumn<int>(
                name: "num",
                table: "AbstractPoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AbstractPoints_name_StageID_num",
                table: "AbstractPoints",
                columns: new[] { "name", "StageID", "num" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbstractPoints_name_StageID_num",
                table: "AbstractPoints");

            migrationBuilder.DropColumn(
                name: "num",
                table: "AbstractPoints");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractPoints_name_StageID",
                table: "AbstractPoints",
                columns: new[] { "name", "StageID" },
                unique: true);
        }
    }
}
