using Microsoft.EntityFrameworkCore.Migrations;

namespace WssCRM.Migrations
{
    public partial class addpassedtoCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "passedOnToCustomer",
                table: "Calls",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passedOnToCustomer",
                table: "Calls");
        }
    }
}
