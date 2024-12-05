using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FullFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetail_SalesMaster_InvoiceId",
                table: "SalesDetail");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetail_InvoiceId",
                table: "SalesDetail");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "SalesDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SalesMasterId",
                table: "SalesDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_SalesMasterId",
                table: "SalesDetail",
                column: "SalesMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetail_SalesMaster_SalesMasterId",
                table: "SalesDetail",
                column: "SalesMasterId",
                principalTable: "SalesMaster",
                principalColumn: "SaleMasterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetail_SalesMaster_SalesMasterId",
                table: "SalesDetail");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetail_SalesMasterId",
                table: "SalesDetail");

            migrationBuilder.DropColumn(
                name: "SalesMasterId",
                table: "SalesDetail");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "SalesDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_InvoiceId",
                table: "SalesDetail",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetail_SalesMaster_InvoiceId",
                table: "SalesDetail",
                column: "InvoiceId",
                principalTable: "SalesMaster",
                principalColumn: "SaleMasterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
