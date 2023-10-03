﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddpropertydateonOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          


   

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

        }
    }
}
