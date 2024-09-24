using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PalamigStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableAndSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandName", "Description", "ListPrice", "Price", "Price100", "Price50", "ProductName" },
                values: new object[,]
                {
                    { 7, "Magnolia", "Vanilla ice cream is a classic flavor commonly made with vanilla extract, cream, milk, and sugar.", 200.0, 230.0, 20000.0, 11500.0, "Vanilla Ice Cream" },
                    { 8, "Nestle", "A delightful frozen treat bursting with refreshing orange flavor, perfect for satisfying sweet cravings on hot summer days.", 150.0, 125.0, 125000.0, 6250.0, "Orange Popsicle" },
                    { 9, "Baskin-Robbins", "A zesty and refreshing Italian-style dessert made from freshly squeezed lemons, creating a tangy yet cooling sensation that tantalizes the taste buds with every spoonful.", 60.0, 45.0, 45000.0, 2250.0, "Lemon Granita" },
                    { 10, "Magnolia", "Indulge in creamy decadence with our Chocolate Ice Cream, a rich and velvety delight crafted from the finest cocoa, offering a blissful symphony of intense chocolate flavors in every scoop.", 200.0, 230.0, 20000.0, 11500.0, "Chocolate Ice Cream" },
                    { 11, "Baskin-Robbins", "Treat yourself to a taste of Italy with our Coffee Granita, a sophisticated dessert blending robust espresso with a hint of sweetness, offering a revitalizing and indulgent pick-me-up in every icy spoonful.", 60.0, 45.0, 45000.0, 2250.0, "Coffee Granita" },
                    { 14, "Nestle", "Savor the sweetness of summer with our Grape Popsicle, a burst of fruity goodness that captures the essence of ripe grapes, delivering a refreshing and satisfying treat on a stick.", 150.0, 125.0, 125000.0, 6250.0, "Grape Popsicle" }
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
