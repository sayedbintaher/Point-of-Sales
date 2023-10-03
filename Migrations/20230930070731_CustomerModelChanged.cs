using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosAPI.Migrations
{
    /// <inheritdoc />
    public partial class CustomerModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Products_ProductId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Transactions_TransactionId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ProductId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TransactionId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ProductId",
                table: "Customers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TransactionId",
                table: "Customers",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Products_ProductId",
                table: "Customers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Transactions_TransactionId",
                table: "Customers",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
