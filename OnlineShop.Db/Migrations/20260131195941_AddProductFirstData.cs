using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddProductFirstData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ComparisonId", "Cost", "Description", "FavoriteId", "Name", "PhotoPath" },
                values: new object[,]
                {
                    { 1, null, 100m, "Описание 1", null, "Продукт 1", "/img/anyProduct.png" },
                    { 2, null, 200m, "Описание 2", null, "Продукт 2", "/img/anyProduct.png" },
                    { 3, null, 300m, "Описание 3", null, "Продукт 3", "/img/anyProduct.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
