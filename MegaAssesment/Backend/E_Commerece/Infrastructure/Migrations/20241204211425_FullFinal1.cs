using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FullFinal1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
