using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace iShop.Web.Migrations
{
    public partial class ChangeAddressInUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Shippings",
                newName: "Ward");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "User",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disctrict",
                table: "Shippings",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Disctrict",
                table: "Shippings");

            migrationBuilder.RenameColumn(
                name: "Ward",
                table: "Shippings",
                newName: "Street");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "User",
                nullable: false,
                defaultValue: "");
        }
    }
}
