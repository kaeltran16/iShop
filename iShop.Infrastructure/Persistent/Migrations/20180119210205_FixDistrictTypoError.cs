using Microsoft.EntityFrameworkCore.Migrations;

namespace iShop.Infrastructure.Persistent.Migrations
{
    public partial class FixDistrictTypoError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Disctrict",
                table: "Shippings",
                newName: "District");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "District",
                table: "Shippings",
                newName: "Disctrict");
        }
    }
}
