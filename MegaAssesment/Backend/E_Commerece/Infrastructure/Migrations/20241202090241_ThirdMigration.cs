using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SalesMaster",
                newName: "SaleMasterId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SalesDetail",
                newName: "SaleDetailId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Product",
                newName: "ProdcutId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartMaster",
                newName: "CardMasterId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartDetail",
                newName: "CardDetailId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Card",
                newName: "CardId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Card",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaleMasterId",
                table: "SalesMaster",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SaleDetailId",
                table: "SalesDetail",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProdcutId",
                table: "Product",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CardMasterId",
                table: "CartMaster",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CardDetailId",
                table: "CartDetail",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Card",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiryDate",
                table: "Card",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
