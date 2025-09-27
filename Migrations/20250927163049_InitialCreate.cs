using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SQLCRUD.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "DateCreated", "Description", "IsActive", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2025, 8, 28, 22, 0, 48, 96, DateTimeKind.Local).AddTicks(9516), "High-performance laptop with latest processor", true, "Laptop Pro 15", 1299.99m, 25 },
                    { 2, "Electronics", new DateTime(2025, 9, 12, 22, 0, 48, 97, DateTimeKind.Local).AddTicks(114), "Ergonomic wireless mouse with long battery life", true, "Wireless Mouse", 49.99m, 100 },
                    { 3, "Furniture", new DateTime(2025, 9, 20, 22, 0, 48, 97, DateTimeKind.Local).AddTicks(119), "Comfortable ergonomic office chair", true, "Office Chair", 299.99m, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
