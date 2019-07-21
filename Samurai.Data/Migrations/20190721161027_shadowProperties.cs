﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samurai.Data.Migrations
{
    public partial class shadowProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Samurais",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Samurais");
        }
    }
}
