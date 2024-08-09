using Microsoft.EntityFrameworkCore.Migrations;

namespace CustOrderPro.Migrations
{
    public partial class AddedNewTabletOReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "c39bc4d6-1ff8-48a4-9047-5cc22755baa4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2f4ec4ab-725e-43a6-969d-952d221bb250");
        }
    }
}
