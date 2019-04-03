using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pluto.Repository.Migrations
{
    public partial class UserAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Order",
                newName: "Created");

            migrationBuilder.AddColumn<int>(
                name: "Profile",
                table: "User",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Approved",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Canceled",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Commited",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Canceled",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Commited",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Order",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }
    }
}
