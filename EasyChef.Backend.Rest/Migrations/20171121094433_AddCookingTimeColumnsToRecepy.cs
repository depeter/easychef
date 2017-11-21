using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Migrations
{
    public partial class AddCookingTimeColumnsToRecepy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CookingDuration",
                table: "Recepies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeople",
                table: "Recepies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TotalDuration",
                table: "Recepies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkDuration",
                table: "Recepies",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookingDuration",
                table: "Recepies");

            migrationBuilder.DropColumn(
                name: "NumberOfPeople",
                table: "Recepies");

            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "Recepies");

            migrationBuilder.DropColumn(
                name: "WorkDuration",
                table: "Recepies");
        }
    }
}
