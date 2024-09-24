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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ice Candy", Quantity = 45 },
                new Category { Id = 2, Name = "Popsicle", Quantity = 20 },
                new Category { Id = 3, Name = "Granita", Quantity = 32 }
            );
        }
    }
}
