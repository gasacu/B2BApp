using Microsoft.EntityFrameworkCore.Migrations;

namespace B2BApp.Data.Migrations
{
    public partial class AddCheckoutCui : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyCui",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyCui",
                table: "Orders");
        }
    }
}
