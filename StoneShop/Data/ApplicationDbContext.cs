using Microsoft.EntityFrameworkCore;
using StoneShop.Models;

namespace StoneShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
