using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace iShop.Web.Migrations
{
    public partial class ChangeSupplierInventoryToOneOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_SupplierId",
                table: "Inventories");

            migrationBuilder.AddColumn<Guid>(
                name: "InventoryId",
                table: "Suppliers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_SupplierId",
                table: "Inventories",
                column: "SupplierId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_SupplierId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Suppliers");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_SupplierId",
                table: "Inventories",
                column: "SupplierId");
        }
    }
}
