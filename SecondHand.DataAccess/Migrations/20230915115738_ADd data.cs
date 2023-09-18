using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ADddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "Conditon", "Created", "Defects", "Description", "Img1", "Img2", "Img3", "Img4", "Img5", "IsNew", "Name", "Price", "Size", "Status" },
                values: new object[] { 2, null, null, "Đen", "Mới", new DateTime(2023, 9, 15, 18, 57, 38, 30, DateTimeKind.Local).AddTicks(6983), "Không có", "Description", "img1.jpg", "img2.jpg", "img3.jpg", "img4.jpg", "img5.jpg", true, "Áo Nike", 100000m, "M", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
