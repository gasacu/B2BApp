using Microsoft.EntityFrameworkCore.Migrations;

namespace B2BApp.Data.Migrations
{
    public partial class AddProductCharDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductChar",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductChar",
                table: "Products");
        }
    }
}
