using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpopertyfeeShipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FeeShipping",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeShipping",
                table: "Orders");
        }
    }
}
