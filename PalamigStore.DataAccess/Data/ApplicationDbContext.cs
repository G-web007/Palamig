using Microsoft.EntityFrameworkCore;
using PalamigStore.Models;

namespace PalamigStore.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ice Candy", Quantity = 45 },
                new Category { Id = 2, Name = "Popsicle", Quantity = 20 },
                new Category { Id = 3, Name = "Granita", Quantity = 32 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Vanilla Ice Cream",
                    Description = "Vanilla ice cream is a classic flavor commonly made with vanilla extract, cream, milk, and sugar.",
                    BrandName = "Magnolia",
                    ListPrice = 200,
                    Price = 230,
                    Price50 = 11500,
                    Price100 = 20000,
                    CategoryId = 7
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Orange Popsicle",
                    Description = "A delightful frozen treat bursting with refreshing orange flavor, perfect for satisfying sweet cravings on hot summer days.",
                    BrandName = "Nestle",
                    ListPrice = 150,
                    Price = 125,
                    Price50 = 6250,
                    Price100 = 125000,
                    CategoryId = 8
                },
                new Product
                {
                    Id = 3,
                    ProductName = "Lemon Granita",
                    Description = "A zesty and refreshing Italian-style dessert made from freshly squeezed lemons, creating a tangy yet cooling sensation that tantalizes the taste buds with every spoonful.",
                    BrandName = "Baskin-Robbins",
                    ListPrice = 60,
                    Price = 45,
                    Price50 = 2250,
                    Price100 = 45000,
                    CategoryId = 9
                },
                new Product
                {
                    Id = 4,
                    ProductName = "Chocolate Ice Cream",
                    Description = "Indulge in creamy decadence with our Chocolate Ice Cream, a rich and velvety delight crafted from the finest cocoa, offering a blissful symphony of intense chocolate flavors in every scoop.",
                    BrandName = "Magnolia",
                    ListPrice = 200,
                    Price = 230,
                    Price50 = 11500,
                    Price100 = 20000,
                    CategoryId = 10
                },
                new Product
                {
                    Id = 5,
                    ProductName = "Grape Popsicle",
                    Description = "Savor the sweetness of summer with our Grape Popsicle, a burst of fruity goodness that captures the essence of ripe grapes, delivering a refreshing and satisfying treat on a stick.",
                    BrandName = "Nestle",
                    ListPrice = 150,
                    Price = 125,
                    Price50 = 6250,
                    Price100 = 125000,
                    CategoryId = 7
                },
                new Product
                {
                    Id = 6,
                    ProductName = "Coffee Granita",
                    Description = "Treat yourself to a taste of Italy with our Coffee Granita, a sophisticated dessert blending robust espresso with a hint of sweetness, offering a revitalizing and indulgent pick-me-up in every icy spoonful.",
                    BrandName = "Baskin-Robbins",
                    ListPrice = 60,
                    Price = 45,
                    Price50 = 2250,
                    Price100 = 45000,
                    CategoryId = 14
                }
            );
        }
    }
}
