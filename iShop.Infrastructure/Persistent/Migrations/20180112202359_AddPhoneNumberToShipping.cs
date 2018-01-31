using Microsoft.EntityFrameworkCore.Migrations;

namespace iShop.Infrastructure.Persistent.Migrations
{
    public partial class AddPhoneNumberToShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Shippings",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Shippings");
        }
    }
}
