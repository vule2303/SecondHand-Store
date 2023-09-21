using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deletemodifieldpromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Modifield",
                table: "Promotions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Modifield",
                table: "Promotions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "Conditon", "Created", "Defects", "Description", "Name", "Price", "Size", "Status" },
                values: new object[] { 1, null, null, "Đen", "Mới", new DateTime(2023, 9, 15, 18, 57, 38, 30, DateTimeKind.Local).AddTicks(6983), "Không có", "Description", "Áo Nike", 100000m, "M", false });
        }
    }
}
