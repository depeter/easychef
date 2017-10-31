using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Migrations
{
    public partial class AddLastScanDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastScan",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastProductScan",
                table: "Categories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastScan",
                table: "Categories",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastScan",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastProductScan",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastScan",
                table: "Categories");
        }
    }
}
