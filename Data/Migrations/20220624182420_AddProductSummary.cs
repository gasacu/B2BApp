using Microsoft.EntityFrameworkCore.Migrations;

namespace B2BApp.Data.Migrations
{
    public partial class AddProductSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductSummary",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSummary",
                table: "Products");
        }
    }
}
