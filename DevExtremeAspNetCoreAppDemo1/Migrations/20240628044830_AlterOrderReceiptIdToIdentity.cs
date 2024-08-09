using Microsoft.EntityFrameworkCore.Migrations;

namespace CustOrderPro.Migrations
{
    public partial class AlterOrderReceiptIdToIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderReceipts",
                table: "OrderReceipts");

            migrationBuilder.DropColumn(
                name: "OrderReceiptId",
                table: "OrderReceipts");

            migrationBuilder.AddColumn<int>(
                name: "OrderReceiptId",
                table: "OrderReceipts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReceipts",
                table: "OrderReceipts",
                column: "OrderReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
             name: "PK_OrderReceipts",
             table: "OrderReceipts");

            migrationBuilder.DropColumn(
                name: "OrderReceiptId",
                table: "OrderReceipts");

            migrationBuilder.AddColumn<int>(
                name: "OrderReceiptId",
                table: "OrderReceipts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReceipts",
                table: "OrderReceipts",
                column: "OrderReceiptId");
        }
    }
}
