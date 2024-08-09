using Microsoft.EntityFrameworkCore.Migrations;

namespace CustOrderPro.Migrations
{
    public partial class NewTableOReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "OrderReceipts",
            columns: table => new
            {
                OrderReceiptId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderId = table.Column<int>(nullable: false),
                ReceiptDetails = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderReceipts", x => x.OrderReceiptId);
                table.ForeignKey(
                    name: "FK_OrderReceipts_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "OrderId",
                    onDelete: ReferentialAction.Cascade);
            });


            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b9f3fbc9-8b41-4fa9-b166-9aeb3924c3d7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4d8c8dcd-da5d-41bb-9f5a-72dc417e3070");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f0158626-03d0-4498-8609-36f1debce55e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b164e84d-0ff9-47df-9450-4a78fb85e2d8");
        }
    }
}
